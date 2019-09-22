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

namespace Hoshino.Email.Controls.ContactsForm
{
    /// <summary>
    /// Win_NewMainGroup.xaml 的交互逻辑
    /// </summary>
    public partial class Win_NewContactsGroup : Window
    {
        EmailBccAccountCategoryRepository EBAC_Repository = new EmailBccAccountCategoryRepository();
        int CategoryID = -1;
        public Win_NewContactsGroup()
        {
            InitializeComponent();
            this.CategoryID = -1;
        }
        public Win_NewContactsGroup(int categoryID)
        {
            InitializeComponent();
            this.CategoryID = categoryID;
            var entity = EBAC_Repository.Get(this.CategoryID);
            this.tbGroup.Text = entity.Name;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.tbGroup.Text.Trim()))
            {
                "請輸入分類名稱".ShowDialog();
                return;
            }
            var entity = EBAC_Repository.Get(this.tbGroup.Text.Trim());
            if (this.CategoryID == -1 && entity != null)
            {
                "分類已存在".ShowDialog();
                return;
            }
            if (this.CategoryID != -1 && entity != null && this.CategoryID != entity.ID)
            {
                "分類已存在".ShowDialog();
                return;
            }
            if (this.CategoryID == -1)
            {
                EBAC_Repository.Insert(this.tbGroup.Text.Trim());
            }
            else
            {
                entity = EBAC_Repository.Get(this.CategoryID);
                if (entity.Name != this.tbGroup.Text.Trim())
                {
                    entity.Name = this.tbGroup.Text.Trim();
                    EBAC_Repository.Update(entity);
                }
            }
            "成功".ShowDialog();
            this.DialogResult = true;
            this.Close();
        }
    }
}
