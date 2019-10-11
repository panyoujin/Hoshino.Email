using Hoshino.Email.Controls.Common;
using Hoshino.Email.Entity;
using Hoshino.Email.Repository;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Hoshino.Email.Controls.SendEmailManage
{
    /// <summary>
    /// UC_MainEmail.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SendEmail : UserControl
    {
        EmailAccountRepository EA_Repository = new EmailAccountRepository();
        public UC_SendEmail()
        {
            InitializeComponent();
            this.Loaded += this.UC_MainEmail_Loaded;
            this.ucPage.ChangePageAction = () =>
            {
                GetList();
            };
        }

        private void UC_MainEmail_Loaded(object sender, RoutedEventArgs e)
        {
            this.ucPage.RefreshList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ucPage.RefreshList();
        }

        private void GetList()
        {
            var (list, total) = EA_Repository.GetList(this.tbEmailAccount.Text, this.tbGroup.Text, this.ucPage.PageIndex, this.ucPage.PageSize);
            dgEmail.ItemsSource = list;
            this.ucPage.InitData(total);
        }

        private void BtnNewOneEmail_Click(object sender, RoutedEventArgs e)
        {
            if (new Win_NewOneEmail().ShowDialog().Value)
            {
                this.ucPage.RefreshList();
            }
        }

        private void BtnNewMoreEmail_Click(object sender, RoutedEventArgs e)
        {
            bool? result= new Win_NewMoreEmail().ShowDialog();
            if (result.HasValue && result.Value)
            {
                GetList();
            }
        }
        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var entity = (sender as Button).DataContext as EmailAccountEntity;
            if (new Win_NewOneEmail(entity.EmailAccountID).ShowDialog().Value)
            {
                this.ucPage.RefreshList(false);
            }
        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var entity = (sender as Button).DataContext as EmailAccountEntity;
            string.Format("是否刪除發件箱【{0}】", entity.EmailAccountAddress).ShowYesOrNoDialog(yesAction: () =>
            {
                EA_Repository.Delete(entity.EmailAccountID);
                this.ucPage.RefreshList(false);
            });
        }
    }
}
