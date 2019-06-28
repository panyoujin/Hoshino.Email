﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Hoshino.Email.Core;
using Hoshino.Email.Entity;
using Hoshino.Email.Repository;
using Hoshino.Email.Services.Tasks;

namespace Hoshino.Email.Services
{
    public class MainServices
    {
        public readonly static Dictionary<string, SmartSendEmail> EmailAccountDic = new Dictionary<string, SmartSendEmail>();
        EmailAccountRepository Repository = new EmailAccountRepository();

        Thread _Thread;
        public bool IsStart = false;
        int Interval = 60;

        public void Start()
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
                            foreach (var ea in list)
                            {

                                SmartSendEmail task;
                                if (!EmailAccountDic.ContainsKey(ea.EmailAccountID))
                                {
                                    task = EmailAccountDic[ea.EmailAccountID];
                                    //如果服务线程已经终止，则重新启动
                                    if (!task.IsStart)
                                    {
                                        task.Run(ea);
                                        EmailAccountDic[ea.EmailAccountID] = task;
                                    }
                                }
                                else
                                {
                                    //2. 修改邮箱使用的服务器信息-增加占用信息和占用时间
                                    ea.OccupyIP = NetHelper.LANIP;
                                    if (!Repository.UpdateEmailAccountOccupy(ea))
                                    {
                                        //占用失败，已经被其他服务器占用，跳过当前邮箱
                                        continue;
                                    }
                                    task = new SmartSendEmail();
                                    task.Run(ea);
                                    EmailAccountDic[ea.EmailAccountID] = task;
                                }
                            }
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

                        }
                    }
                    _Thread = null;
                }
            }
            catch (Exception ex)
            {

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

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void SleepInterval(int interval)
        {
            while (interval > 0 && IsStart)
            {
                Thread.Sleep((interval > 10 ? 10 : interval) * 1000);
                interval -= 10;
            }
        }
    }
}
