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
    public partial class Win_ReceiptEmailList : Window
    {
        EmailBccAccountRepository EBA_Repository = new EmailBccAccountRepository();

        /// <summary>
        /// 已经选择的收件人列表
        /// </summary>
        public List<EmailBccAccountEntity> BccMailList = new List<EmailBccAccountEntity>();
        /// <summary>
        /// 获取查询的结果数据
        /// </summary>
        List<EmailBccAccountEntity> ResultList = new List<EmailBccAccountEntity>();

        public Win_ReceiptEmailList()
        {
            InitializeComponent();

            this.Loaded += Win_ReceiptEmailList_Loaded;
            this.ucPage.ChangePageAction = () =>
            {
                GetList();
            };
        }

        private void Win_ReceiptEmailList_Loaded(object sender, RoutedEventArgs e)
        {
            this.ucPage.RefreshList();
        }

        private void GetList()
        {
            var (list, total) = EBA_Repository.GetList(this.tbBccAccount.Text, this.tbGroup.Text, this.ucPage.PageIndex, this.ucPage.PageSize);
            ResultList = list.ToList();
            dgEmail.ItemsSource = list;
            this.ucPage.InitData(total);
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int emailBccAccountID = int.Parse(cb.Tag.ToString());   
            var account = ResultList.FirstOrDefault(s => s.EmailBccAccountID == emailBccAccountID);
            if (cb.IsChecked == true)
            {
                if (account != null)
                {
                    BccMailList.Add(account);
                }
            }
            else
            {
                BccMailList.Remove(account);
            }
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            GetList();
        }

        private void BtnAll_Click(object sender, RoutedEventArgs e)
        {
            BccMailList = EBA_Repository.GetBccEmailList(this.tbBccAccount.Text, this.tbGroup.Text).ToList();
            this.Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
