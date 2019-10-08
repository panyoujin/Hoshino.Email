using Hoshino.Email.Controls.EmailInfoManage;
using Hoshino.Email.Core.Helper;
using Hoshino.Email.Entity;
using LumiSoft.Net.MIME;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class UC_NewMessageForm : UserControl
    {
        /// <summary>
        /// 發件人
        /// </summary>
        public static List<EmailAccountEntity> _SendMailList = new List<EmailAccountEntity>();
        /// <summary>
        /// 密送人
        /// </summary>
        public static List<EmailBccAccountEntity> _BccMailList = new List<EmailBccAccountEntity>();

        public UC_NewMessageForm()
        {
            InitializeComponent();
        }

        ~UC_NewMessageForm()
        {
            _SendMailList.Clear();
            _BccMailList.Clear();
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
            op.Filter = "邮件文件(*.eml)|*.eml|所有文件(*.*)|*.*";
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
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
            new Win_ReceiptEmailList().Show();
        }
        /// <summary>
        /// 選擇發送人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSent_Click(object sender, RoutedEventArgs e)
        {
            new Win_SentEmailList().Show();

        }
        #endregion

        /// <summary>
        /// 解析eml內容到編輯器上
        /// </summary>
        private void AnalyzeEmlFile(string emlFile)
        {
            tbPath.Text = emlFile;

            var msg = LumiSoft.Net.Mail.Mail_Message.ParseFromFile(emlFile);
            MIME_Entity[] mimeEntity = null;

            mimeEntity = msg.AllEntities;


            Dictionary<string, string> pic = MailHelper.GetPathByImage(mimeEntity);

            string BodyHtmlText = msg.BodyHtmlText;
            foreach (var one in pic)
            {
                BodyHtmlText = BodyHtmlText.Replace(one.Key, one.Value);
            }
            tbTheme.Text = msg.Subject;
            htmlEditor.ContentHtml = BodyHtmlText;
        }


    }
}
