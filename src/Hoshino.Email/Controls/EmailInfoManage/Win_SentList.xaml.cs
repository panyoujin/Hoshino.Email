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
    public partial class Win_SentList : Window
    {
        EmailSendAccountRepository ESAR_Repository = new EmailSendAccountRepository();

        int EmailID = 0;
        public Win_SentList(int EmailID)
        {
            InitializeComponent();
            this.EmailID = EmailID;
            this.Loaded += Win_SentList_Loaded;
            this.ucPage.ChangePageAction = () =>
            {
                GetList();
            };
        }

        private void Win_SentList_Loaded(object sender, RoutedEventArgs e)
        {
            this.ucPage.RefreshList();
        }

        private void GetList()
        {
            var (list, total) = ESAR_Repository.GetEmailInfoSendAccountList(EmailID,this.tbAccount.Text,  this.ucPage.PageIndex, this.ucPage.PageSize);
            dgEmail.ItemsSource = list;
            this.ucPage.InitData(total);
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            GetList();
        }
    }
}
