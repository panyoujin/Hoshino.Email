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

namespace Hoshino.Email.Controls.SendEmailManage
{
    /// <summary>
    /// UC_MaimGroup.xaml 的交互逻辑
    /// </summary>
    public partial class UC_MainGroup : UserControl
    {
        EmailAccountCategoryRepository EAC_Repository = new EmailAccountCategoryRepository();
        public UC_MainGroup()
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
            var (list, total) = EAC_Repository.GetList(this.tbCategory.Text.Trim(), this.ucPage.PageIndex, this.ucPage.PageSize);
            dgEmail.ItemsSource = list;
            this.ucPage.InitData(total);
        }

        private void BtnInsertGroup_Click(object sender, RoutedEventArgs e)
        {
            if (new Win_NewMainGroup().ShowDialog().Value)
            {
                this.ucPage.RefreshList();
            }
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            this.ucPage.RefreshList();
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var entity = (sender as Button).DataContext as EmailAccountCategoryEntity;
            if (new Win_NewMainGroup(entity.ID).ShowDialog().Value)
            {
                this.ucPage.RefreshList(false);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var entity = (sender as Button).DataContext as EmailAccountCategoryEntity;
            EAC_Repository.Delete(entity.ID);
            this.ucPage.RefreshList(false);
        }
    }
}
