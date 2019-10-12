using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Hoshino.Email.Core;
using Hoshino.Email.Entity;
using Hoshino.Email.Repository;

namespace Hoshino.Email.Services.Tasks
{
    public class SmartSendEmail
    {
        Thread _Thread;
        public bool IsStart = false;
        int Interval = 60;
        EmailAccountRepository EA_Repository = new EmailAccountRepository();
        EmailInfoRepository E_Repository = new EmailInfoRepository();
        EmailSendBccAccountRepository ESB_Repository = new EmailSendBccAccountRepository();
        EmailSendFailureRepository ESF_Repository = new EmailSendFailureRepository();
        EmailAccountEntity EmailAccount;
        public void Run(EmailAccountEntity emailAccount)
        {
            EmailAccount = emailAccount;
            //Interval = emailAccount.EmailAccountSpace * 60;
            if (!IsStart)
            {
                IsStart = true;
                _Thread = new Thread((s) =>
                {
                    while (IsStart)
                    {
                        try
                        {
                            //每一次賦值為10秒
                            Interval = 10;
                            //1. 重新获取邮箱信息，判断邮箱是否被占用，如果占用时间太久并不进行发送操作可以认为已经异常结束占用
                            EmailAccount = EA_Repository.Get(EmailAccount.EmailAccountID);
                            if (EmailAccount == null)
                            {
                                LogHelper.Info(string.Format("SmartSendEmail : 发件箱【{0}:{1}】不存在", emailAccount.EmailAccountID, emailAccount.EmailAccountAddress));
                                //占用失败，退出线程
                                IsStart = false;
                                break;
                            }
                            //2. 修改邮箱使用的服务器信息-增加占用信息和占用时间
                            EmailAccount.OccupyIP = NetHelper.LANIP;
                            LogHelper.Debug(string.Format("SmartSendEmail : 准备继续占用发件箱【{0}:{1}】的IP【{2} -> {3}】", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, EmailAccount.OccupyIP, NetHelper.LANIP));
                            if (!EA_Repository.UpdateEmailAccountOccupy(EmailAccount))
                            {
                                LogHelper.Info(string.Format("SmartSendEmail : 继续占用发件箱【{0}:{1}】的IP【{2} -> {3}】失败，已被其他占用", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, EmailAccount.OccupyIP, NetHelper.LANIP));
                                //占用失败，退出线程
                                IsStart = false;
                                break;
                            }
                            LogHelper.Debug(string.Format("SmartSendEmail : 发件箱【{0}:{1}】下一次发送时间和数量为【{2} -> {3}】", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, EmailAccount.EmailAccountNextSendTime, EmailAccount.EmailAccountRemainEmailCount));
                            //3. 判断下一次发送时间和当期时间对比
                            if (EmailAccount.EmailAccountNextSendTime >= DateTime.Now)
                            {
                                //还没到时间执行
                                Interval = (EmailAccount.EmailAccountNextSendTime - DateTime.Now).Seconds + 1;
                                if (Interval > (EmailAccount.EmailAccountSpace * 60 / 3))
                                {
                                    Interval = (EmailAccount.EmailAccountSpace * 60 / 3);
                                }
                                if (Interval <= 0)
                                {
                                    Interval = 10;
                                }
                                continue;
                            }
                            //本次可發送的數量
                            var sendCount = EmailAccount.EmailAccountRemainEmailCount;
                            //如果减少了最大发送数量，或者上一次发送离现在已经超过一个间隔,或者下一次发送已经过去一个间隔
                            if (sendCount > EmailAccount.EmailAccountMaxEmailCount
                            || EmailAccount.EmailAccountPreSendTime.AddMinutes(EmailAccount.EmailAccountSpace) < DateTime.Now || emailAccount.EmailAccountNextSendTime.AddMinutes(EmailAccount.EmailAccountSpace) < DateTime.Now)
                            {
                                sendCount = EmailAccount.EmailAccountMaxEmailCount;
                            }
                            LogHelper.Debug(string.Format("SmartSendEmail : 发件箱【{0}:{1}】可发送实际数量为【{2}】", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, sendCount));
                            //if (sendCount <= 0)
                            //{
                            //    continue;
                            //}
                            //4. 获取发送列表，按照指定数量获取
                            //先获取已分配但是未发送的
                            var emailInfo = E_Repository.GetAssignSendEmailInfoByEmailAccount(EmailAccount.EmailAccountID);
                            if (emailInfo == null || emailInfo.EmailID <= 0)
                            {
                                //获取发送中还没开始分配的
                                emailInfo = E_Repository.GetNextSendEmailInfoByEmailAccount(EmailAccount.EmailAccountID);
                            }
                            else
                            {
                                LogHelper.Info(string.Format("SmartSendEmail : 发件箱【{0}:{1}】准备发送上次分配未发送的邮件【{2} ->{3}】", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, emailInfo.EmailID, emailInfo.EmailTitle));
                            }
                            if (emailInfo == null || emailInfo.EmailID <= 0)
                            {
                                LogHelper.Debug(string.Format("SmartSendEmail : 发件箱【{0}:{1}】没有待发送的邮件", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, sendCount));
                                //如果本次没需要发送的邮件，则60查看一次
                                Interval = 10;
                                continue;
                            }
                            LogHelper.Info(string.Format("SmartSendEmail : 发件箱【{0}:{1}】准备发送邮件【{2} ->{3}】", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, emailInfo.EmailID, emailInfo.EmailTitle));
                            //分配以及獲取分配好的記錄，防止因上次異常分配導致的分配數量超出可發送數量的問題
                            var sendList = ESB_Repository.GetListByEmailAccountAndEmailID(emailInfo.EmailID, EmailAccount.EmailAccountID, sendCount);
                            if (sendList == null || sendList.Count() <= 0)
                            {
                                LogHelper.Info(string.Format("SmartSendEmail : 发件箱【{0}:{1}】准备发送邮件【{2} ->{3}】时找不到可发送的收件箱", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, emailInfo.EmailID, emailInfo.EmailTitle));
                                //如果本次没需要发送的邮件，则等待10秒马上查看下一个需要发送的邮件
                                Interval = 10;
                                continue;
                            }
                            try
                            {
                                LogHelper.Info(string.Format("SmartSendEmail : 发件箱【{0}:{1}】发送邮件【{2} ->{3}】开始", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, emailInfo.EmailID, emailInfo.EmailTitle));
                                //5. 发送邮件
                                CDOSendEmail cdoSend = new CDOSendEmail();
                                cdoSend.SendEmail(emailInfo, EmailAccount, sendList.ToList());
                                LogHelper.Info(string.Format("SmartSendEmail : 发件箱【{0}:{1}】发送邮件【{2} ->{3}】完成", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, emailInfo.EmailID, emailInfo.EmailTitle));
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Error(string.Format("SmartSendEmail : 发件箱【{0}:{1}】发送邮件【{2} ->{3}】Exception:{4}", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, emailInfo.EmailID, emailInfo.EmailTitle, ex.Message), ex);
                                foreach (var f in sendList)
                                {
                                    f.EmailSendBccAccountState = -1;
                                    f.Result = ex.Message;
                                }
                            }
                            //6. 记录结果
                            LogHelper.Debug(string.Format("SmartSendEmail : 发件箱【{0}:{1}】发送邮件【{2} ->{3}】完成-修改发送状态", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, emailInfo.EmailID, emailInfo.EmailTitle));
                            ESB_Repository.UpdateState(sendList.ToList());
                            E_Repository.Update_EmaiInfoQTY_ByEmailID(emailInfo.EmailID);
                            List<EmailSendFailureEntity> FailList = new List<EmailSendFailureEntity>();
                            foreach (var f in sendList.Where(l => l.EmailSendBccAccountState == -1))
                            {
                                FailList.Add(new EmailSendFailureEntity() { EmailID = f.EmailID, EmailAccountID = f.EmailAccountID, EmailBccAccountID = f.EmailBccAccountID, EmailSendFailureSendTime = DateTime.Now, Result = f.Result });
                            }
                            if (FailList != null && FailList.Count > 0)
                            {
                                ESF_Repository.Insert(FailList);
                                LogHelper.Info(string.Format("SmartSendEmail : 发件箱【{0}:{1}】发送邮件【{2} ->{3}】本次发送失败【{4}】", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, emailInfo.EmailID, emailInfo.EmailTitle, FailList.Count));
                            }

                            //7. 修改信息
                            var RemainCount = sendCount - sendList.Count();
                            var nextSendTime = DateTime.Now;//舍弃一次不足，防止计算重复的问题
                            if (RemainCount <= 0)
                            {
                                if (EmailAccount.EmailAccountSpace > 0)
                                {
                                    nextSendTime = DateTime.Now.AddMinutes(EmailAccount.EmailAccountSpace);
                                }
                                RemainCount = EmailAccount.EmailAccountMaxEmailCount;
                            }
                            LogHelper.Info(string.Format("SmartSendEmail : 发件箱【{0}:{1}】发送邮件【{2} ->{3}】完成，修改发件箱下次发送时间【{4}】，剩余发送数量【{5}】", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, emailInfo.EmailID, emailInfo.EmailTitle, nextSendTime, RemainCount));
                            EA_Repository.UpdateRemainCount(EmailAccount.EmailAccountID, RemainCount, nextSendTime);
                            Interval = 0;
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(string.Format("SmartSendEmail : 发件箱【{0}:{1}】 Exception:{2}", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, ex.Message), ex);
                        }
                        finally
                        {
                            SleepInterval(Interval);
                        }
                    }
                });
                _Thread.Start();
            }
        }
        public void SleepInterval(int interval)
        {
            while (interval > 0 && IsStart)
            {
                Thread.Sleep((interval > 5 ? 5 : interval) * 1000);
                interval -= 5;
            }
        }

        public void Stop()
        {
            try
            {
                IsStart = false;
                if (_Thread != null)
                {
                    while (_Thread.IsAlive)
                    {
                        try
                        {
                            _Thread.Abort();
                        }
                        catch (Exception ex)
                        {
                            if (EmailAccount != null)
                            {
                                LogHelper.Error(string.Format("SmartSendEmail : 停止发件箱【{0}:{1}】 线程 Exception:{2}", EmailAccount.EmailAccountID, EmailAccount.EmailAccountAddress, ex.Message), ex);
                            }
                        }
                    }
                    _Thread = null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(string.Format("SmartSendEmail : 停止线程 Exception:{0}", ex.Message), ex);
            }
        }
    }
}
