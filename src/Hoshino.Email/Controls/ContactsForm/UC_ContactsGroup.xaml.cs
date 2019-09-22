using Hoshino.Email.Controls.Common;
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
    /// UC_ContactsGroup.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ContactsGroup : UserControl
    {
        EmailBccAccountCategoryRepository EBAC_Repository = new EmailBccAccountCategoryRepository();
        public UC_ContactsGroup()
        {
            InitializeComponent();
            this.Loaded += this.UC_MainGroup_Loaded;
            this.ucPage.ChangePageAction = () =>
            {
                GetList();
            };
        }

        private void UC_MainGroup_Loaded(object sender, RoutedEventArgs e)
        {
            this.ucPage.RefreshList();
        }

        private void GetList()
        {
            var (list, total) = EBAC_Repository.GetList(this.tbCategory.Text.Trim(), this.ucPage.PageIndex, this.ucPage.PageSize);
            dgEmail.ItemsSource = list;
            this.ucPage.InitData(total);
        }

        private void BtnInsertGroup_Click(object sender, RoutedEventArgs e)
        {
            if (new Win_NewContactsGroup().ShowDialog().Value)
            {
                this.ucPage.RefreshList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ucPage.RefreshList();
        }
        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var entity = (sender as Button).DataContext as EmailBccAccountCategoryEntity;
            if (new Win_NewContactsGroup(entity.ID).ShowDialog().Value)
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
            var entity = (sender as Button).DataContext as EmailBccAccountCategoryEntity;
            EBAC_Repository.Delete(entity.ID);
            this.ucPage.RefreshList(false);
        }
    }
}
