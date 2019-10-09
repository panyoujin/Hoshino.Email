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
    public partial class Win_ReceiptList : Window
    {
        EmailSendBccAccountRepository ESBAR_Repository = new EmailSendBccAccountRepository();


        int EmailID = 0;

        public Win_ReceiptList(int EmailID)
        {
            InitializeComponent();

            this.EmailID = EmailID;
            this.Loaded += Win_ReceiptList_Loaded; ;
            this.ucPage.ChangePageAction = () =>
            {
                GetList();
            };
        }

        private void Win_ReceiptList_Loaded(object sender, RoutedEventArgs e)
        {
            this.ucPage.RefreshList();
        }

        private void GetList()
        {
            var (list, total) = ESBAR_Repository.GetList(EmailID, this.tbBccAddress.Text, this.ucPage.PageIndex, this.ucPage.PageSize);
            dgEmail.ItemsSource = list;
            this.ucPage.InitData(total);
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            GetList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            string.Format("是否刪除該收件箱").ShowYesOrNoDialog(yesAction: () =>
            {
                Button bt = sender as Button;
                if (bt != null && bt.Tag != null)
                {
                    ESBAR_Repository.Delete((int)bt.Tag);
                    GetList();
                }
            });


        }

        private void BtnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            "是否刪除該郵件的所有收件人".ShowYesOrNoDialog(yesAction: () =>
            {
                ESBAR_Repository.DeleteAllByEmailID(EmailID);

                GetList();
            });
        }

        private void BtnDeleteFailEmail_Click(object sender, RoutedEventArgs e)
        {
            "是否刪除該郵件的所有收件人".ShowYesOrNoDialog(yesAction: () =>
            {
                ESBAR_Repository.DeleteAllByEmailID(EmailID, -1);

                GetList();
            });
        }
    }
}
