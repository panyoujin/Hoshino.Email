using Hoshino.Email.Core;
using Hoshino.Email.Repository;
using Hoshino.Email.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Topshelf;

namespace Hoshino.Email.Services
{
    public class MainServices : ServiceControl
    {
        public readonly static Dictionary<int, SmartSendEmail> EmailAccountDic = new Dictionary<int, SmartSendEmail>();
        EmailAccountRepository Repository = new EmailAccountRepository();

        Thread _Thread;
        public bool IsStart = false;
        int Interval = 30;

        public bool Start(HostControl hostControl)
        {
            if (!IsStart)
            {
                IsStart = true;
                _Thread = new Thread((s) =>
                {
                    while (IsStart)
                    {
                        try
                        {
                            var list = Repository.GetList();
                            LogHelper.Debug(string.Format("MainServices : 获取到发件箱数量【{0}】", list.Count()));
                            foreach (var ea in list)
                            {

                                SmartSendEmail task;
                                if (EmailAccountDic.ContainsKey(ea.EmailAccountID))
                                {
                                    task = EmailAccountDic[ea.EmailAccountID];
                                    //如果服务线程已经终止，则重新启动
                                    if (!task.IsStart)
                                    {
                                        LogHelper.Info(string.Format("MainServices : 再次启动已经停止的发件箱【{0}:{1}】", ea.EmailAccountID, ea.EmailAccountAddress));
                                        task.Run(ea);
                                        EmailAccountDic[ea.EmailAccountID] = task;
                                    }
                                }
                                else
                                {
                                    LogHelper.Debug(string.Format("MainServices : 准备将发件箱【{0}:{1}】的占用IP改为【{2} -> {3}】", ea.EmailAccountID, ea.EmailAccountAddress, ea.OccupyIP, NetHelper.LANIP));
                                    //2. 修改邮箱使用的服务器信息-增加占用信息和占用时间
                                    ea.OccupyIP = NetHelper.LANIP;
                                    if (!Repository.UpdateEmailAccountOccupy(ea))
                                    {
                                        LogHelper.Debug(string.Format("MainServices : 将发件箱【{0}:{1}】的占用IP改为【{2} -> {3}】失败", ea.EmailAccountID, ea.EmailAccountAddress, ea.OccupyIP, NetHelper.LANIP));
                                        //占用失败，已经被其他服务器占用，跳过当前邮箱
                                        continue;
                                    }
                                    LogHelper.Info(string.Format("MainServices : 将发件箱【{0}:{1}】的占用IP改为【{2} -> {3}】成功", ea.EmailAccountID, ea.EmailAccountAddress, ea.OccupyIP, NetHelper.LANIP));
                                    task = new SmartSendEmail();
                                    task.Run(ea);
                                    EmailAccountDic[ea.EmailAccountID] = task;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(string.Format("MainServices : Exception:{0}", ex.Message), ex);
                        }
                        finally
                        {
                            SleepInterval(Interval);
                        }
                    }
                });
                _Thread.Start();
            }
            return true;

        }


        public bool Stop(HostControl hostControl)
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
                            LogHelper.Error(string.Format("MainServices : 停止线程 Exception:{0}", ex.Message), ex);
                        }
                    }
                    _Thread = null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(string.Format("MainServices : 停止发件线程 Exception:{0}", ex.Message), ex);
            }
            try
            {
                foreach (var dic in EmailAccountDic)
                {
                    try
                    {
                        if (dic.Value != null)
                        {
                            dic.Value.Stop();
                        }
                    }
                    catch (Exception e)
                    {
                        LogHelper.Error(string.Format("MainServices : 停止发件线程 Exception:{0}", e.Message), e);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(string.Format("MainServices : 停止发件线程 Exception:{0}", ex.Message), ex);
            }
            return true;
        }

        public void SleepInterval(int interval)
        {
            while (interval > 0 && IsStart)
            {
                Thread.Sleep((interval > 5 ? 5 : interval) * 1000);
                interval -= 5;
            }
        }

    }
}
