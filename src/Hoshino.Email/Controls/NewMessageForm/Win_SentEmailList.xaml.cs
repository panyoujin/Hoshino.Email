using Hoshino.Email.Entity;
using Hoshino.Email.Repository;
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
using System.Windows.Shapes;

namespace Hoshino.Email.Controls.EmailInfoManage
{
    /// <summary>
    /// Win_ReceiptList.xaml 的交互逻辑
    /// </summary>
    public partial class Win_SentEmailList : Window
    {
        EmailAccountRepository EA_Repository = new EmailAccountRepository();

        /// <summary>
        /// 已经选择的发送人列表
        /// </summary>
        List<EmailAccountEntity> SendMailList = new List<EmailAccountEntity>();

        /// <summary>
        /// 获取查询的结果数据
        /// </summary>
        List<EmailAccountEntity> EmailList=new List<EmailAccountEntity>();
        public Win_SentEmailList()
        {
            InitializeComponent();

            this.Loaded += Win_SentEmailList_Loaded;
        }

        private void Win_SentEmailList_Loaded(object sender, RoutedEventArgs e)
        {
            this.ucPage.RefreshList();
        }


        private void GetList()
        {
            var (list, total) = EA_Repository.GetList(this.tbAddress.Text, this.tbGroup.Text, this.ucPage.PageIndex, this.ucPage.PageSize);
            EmailList = list.ToList();
            dgEmail.ItemsSource = list;
            this.ucPage.InitData(total);
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            GetList();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            NewMessageForm.UC_NewMessageForm._SendMailList = SendMailList;
            this.Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int emailAccountID = int.Parse(cb.Tag.ToString());   //获取该行的FID  
            var account = EmailList.FirstOrDefault(s => s.EmailAccountID == emailAccountID);
            if (cb.IsChecked == true)
            {
                if (account != null)
                {
                    SendMailList.Add(account);
                }
            }
            else
            {
                SendMailList.Remove(account);
            }
        }

        private void BtnAll_Click(object sender, RoutedEventArgs e)
        {
            NewMessageForm.UC_NewMessageForm._SendMailList = EA_Repository.GetEmailAccountList(this.tbAddress.Text, this.tbGroup.Text).ToList();
            this.Close();
        }
    }
}
