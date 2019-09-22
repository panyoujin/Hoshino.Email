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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hoshino.Email.Controls.ContactsForm
{
    /// <summary>
    /// UC_Contacts.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Contacts : UserControl
    {
        EmailBccAccountRepository EBA_Repository = new EmailBccAccountRepository();
        public UC_Contacts()
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
            var (list, total) = EBA_Repository.GetList(this.tbEmailAccount.Text, this.tbGroup.Text, this.ucPage.PageIndex, this.ucPage.PageSize);
            dgEmail.ItemsSource = list;
            this.ucPage.InitData(total);
        }

        private void BtnNewContact_Click(object sender, RoutedEventArgs e)
        {
            if (new Win_NewContacts().ShowDialog().Value)
            {
                this.ucPage.RefreshList();
            }
        }
        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var entity = (sender as Button).DataContext as EmailBccAccountEntity;
            if (new Win_NewContacts(entity.EmailBccAccountID).ShowDialog().Value)
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
            var entity = (sender as Button).DataContext as EmailBccAccountEntity;
            string.Format("是否刪除發件箱【{0}】", entity.EmailBccAccountAddress).ShowYesOrNoDialog(yesAction: () =>
            {
                EBA_Repository.Delete(entity.EmailBccAccountID);
                this.ucPage.RefreshList(false);
            });
        }
    }
}
