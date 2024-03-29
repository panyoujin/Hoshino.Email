﻿using Hoshino.Email.Controls.EmailInfoManage;
using Hoshino.Email.Core.Helper;
using Hoshino.Email.Entity;
using Hoshino.Email.Repository;
using LumiSoft.Net.MIME;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hoshino.Email.Controls.NewMessageForm
{
    /// <summary>
    /// UC_NewMessageForm.xaml 的交互逻辑
    /// </summary>
    public partial class UC_NewHtmlMessageForm : UserControl
    {
        /// <summary>
        /// 發件人
        /// </summary>
        public static List<EmailAccountEntity> _SendMailList = new List<EmailAccountEntity>();
        /// <summary>
        /// 密送人
        /// </summary>
        public static List<EmailBccAccountEntity> _BccMailList = new List<EmailBccAccountEntity>();


        EmailInfoRepository ER_Repository = new EmailInfoRepository();
        EmailSendAccountRepository ESR_Repository = new EmailSendAccountRepository();
        EmailSendBccAccountRepository ESBR_Repository = new EmailSendBccAccountRepository();


        string EmailBody = "";


        public UC_NewHtmlMessageForm()
        {
            InitializeComponent();
        }

        void ClearControl()
        {

            tbPath.Text = "";
            tbSendEmail.Text = "";
            tbBccs.Text = "";
            tbTheme.Text = "";
            dpSendDate.Text = "";
            tpSendTime.Text = "";
            EmailBody = "";
            WebBrowserControl.Source = null;
        }

        #region 控件event
        /// <summary>
        /// 導入eml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExportEml_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog op = new System.Windows.Forms.OpenFileDialog();
            op.Filter = "邮件文件(*.html)|*.html|所有文件(*.*)|*.*";
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(op.FileName))
                    AnalyzeEmlFile(op.FileName);
            }
        }
        /// <summary>
        /// 選擇密送人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReceipt_Click(object sender, RoutedEventArgs e)
        {
            var receiptWin = new Win_ReceiptEmailList();
            receiptWin.ShowDialog();
            _BccMailList = receiptWin.BccMailList;
            this.tbBccs.Text = "";
            foreach (var item in _BccMailList)
            {
                if (this.tbBccs.Text.Length >= 500)
                {
                    this.tbBccs.Text += "...";
                    break;
                }
                this.tbBccs.Text += item.EmailBccAccountAddress + ";";
            }
        }
        /// <summary>
        /// 選擇發送人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSent_Click(object sender, RoutedEventArgs e)
        {
            var sendWin = new Win_SentEmailList();
            sendWin.ShowDialog();
            _SendMailList = sendWin.SendMailList;
            this.tbSendEmail.Text = "";

            foreach (var item in _SendMailList)
            {
                if (this.tbSendEmail.Text.Length >= 500)
                {
                    this.tbSendEmail.Text += "...";
                    break;
                }
                this.tbSendEmail.Text += item.EmailAccountAddress + ";";
            }
        }

        /// <summary>
        /// 發送郵件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailBody))
            {
                "郵件内容不能爲空".ShowDialog();
                return;
            }
            DateTime? startDatetime = null;

            if ((!dpSendDate.SelectedDate.HasValue && tpSendTime.SelectedTime.HasValue) || (dpSendDate.SelectedDate.HasValue && !tpSendTime.SelectedTime.HasValue))
            {
                "請選擇發送日期/時間".ShowDialog();
                return;
            }
            if (dpSendDate.SelectedDate.HasValue && tpSendTime.SelectedTime.HasValue)
            {
                startDatetime = dpSendDate.SelectedDate.Value;
                startDatetime = startDatetime.Value.AddHours(tpSendTime.SelectedTime.Value.Hour);
                startDatetime = startDatetime.Value.AddMinutes(tpSendTime.SelectedTime.Value.Minute);
            }


            #region 判斷有沒填寫發件人抄送人
            if (_SendMailList.Count == 0)
            {
                "你還沒填寫發件人！".ShowDialog();
                return;
            }
            if (_BccMailList.Count == 0)
            {
                "你還沒填寫抄送人！".ShowDialog();
                return;
            }
            #endregion

            EmailInfoEntity email = new EmailInfoEntity();
            email.EmailTitle = tbTheme.Text;
            //读取邮件发送内容
            email.EmailFilePath = EmailBody;
            email.EmailState = 2;//新增的時候，是草稿狀態；等新增完成會修改狀態為0
            email.TotalQty = _BccMailList.Count;
            if (startDatetime.HasValue)
                email.EmailStartSendTime = startDatetime.Value;

            Thread thread = new Thread(s =>
            {
                Stopwatch st = new Stopwatch();
                st.Start();
                int bccTotal = _BccMailList.Count;
                this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("正在新增邮件，进度：{0}/{1}，耗時：{2}秒", 0, bccTotal, 0));

                //新增郵件
                int EmailID = ER_Repository.Insert(email);
                if (EmailID <= 0)
                {
                    return;
                }
                foreach (var send in _SendMailList)
                {
                    EmailSendAccountEntity entity = new EmailSendAccountEntity();
                    entity.EmailID = EmailID;
                    entity.EmailAccountID = send.EmailAccountID;
                    ESR_Repository.Insert(entity);
                }


                //LogHelper.Info("插入發送信息時間：" + DateTime.Now + ",插入密送人數量：" + _BccMailList.Count);
                int bccCount = 0;
                List<EmailSendBccAccountEntity> sendBcc = new List<EmailSendBccAccountEntity>();
                foreach (var bcc in _BccMailList)
                {
                    bccCount++;
                    EmailSendBccAccountEntity entity = new EmailSendBccAccountEntity();
                    entity.EmailID = EmailID;
                    entity.EmailBccAccountID = bcc.EmailBccAccountID;
                    entity.EmailSendBccAccountState = 0;
                    sendBcc.Add(entity);
                    if (bccCount > 100)
                    {
                        ESBR_Repository.InsertBatch(sendBcc);
                        this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("新增郵件中，進度：{0}/{1}，耗時：{2}秒", bccCount, bccTotal, st.ElapsedMilliseconds / 1000));
                        sendBcc.Clear();
                    }
                }
                if (sendBcc.Count > 0)
                {
                    ESBR_Repository.InsertBatch(sendBcc);
                }

                email.EmailID = EmailID;
                email.EmailState = 0;
                ER_Repository.Update(email);
                st.Stop();
                this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("新增邮件完成，进度：{0}/{1}，耗時：{2}秒", bccCount, bccTotal, st.ElapsedMilliseconds / 1000));
                _SendMailList.Clear();
                _BccMailList.Clear();
            });
            thread.Start();
            "正在後臺進行郵件新建,將會關閉當前窗口，草稿狀態==正在創建郵件中".ShowDialog();

            this.Dispatcher.BeginInvoke(MainWindow.GotoPage, "郵件管理");

            ClearControl();
        }

        #endregion

        /// <summary>
        /// 解析eml內容到編輯器上
        /// </summary>
        private void AnalyzeEmlFile(string emlFile)
        {
            tbPath.Text = emlFile;

            WebBrowserControl.Source = new Uri(emlFile);
            EmailBody = File.ReadAllText(emlFile, Encoding.UTF8);


            Regex regTitle = new Regex(@"<title>(.+?)<\/title>", RegexOptions.IgnoreCase);
            MatchCollection matches = regTitle.Matches(EmailBody);
            // 取得匹配项列表   
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                    tbTheme.Text = match.Groups[1].Value;
            }
        }


    }
}
