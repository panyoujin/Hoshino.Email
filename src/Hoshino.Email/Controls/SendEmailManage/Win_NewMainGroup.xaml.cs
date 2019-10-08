using Hoshino.Email.Core;
using Hoshino.Email.Repository;
using System;
using System.Windows;

namespace Hoshino.Email.Controls.SendEmailManage
{
    /// <summary>
    /// Win_NewMainGroup.xaml 的交互逻辑
    /// </summary>
    public partial class Win_NewMainGroup : Window
    {
        EmailAccountCategoryRepository EAC_Repository = new EmailAccountCategoryRepository();
        int CategoryID = -1;
        public Win_NewMainGroup()
        {
            InitializeComponent();
            this.CategoryID = -1;
        }
        public Win_NewMainGroup(int categoryID)
        {
            InitializeComponent();
            this.CategoryID = categoryID;
            var entity = EAC_Repository.Get(this.CategoryID);
            this.tbGroup.Text = entity.Name;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.tbGroup.Text.Trim()))
                {
                    "請輸入分類名稱".ShowDialog();
                    return;
                }
                var entity = EAC_Repository.Get(this.tbGroup.Text.Trim());
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
                    LogHelper.Info(string.Format("新增發件箱分類【{0}】", this.tbGroup.Text.Trim()));
                    EAC_Repository.Insert(this.tbGroup.Text.Trim());
                    LogHelper.Info(string.Format("新增發件箱分類【{0}】成功", this.tbGroup.Text.Trim()));
                }
                else
                {
                    entity = EAC_Repository.Get(this.CategoryID);
                    if (entity.Name != this.tbGroup.Text.Trim())
                    {
                        LogHelper.Info(string.Format("修改發件箱分類【{0}-{1}】為【{2}】", this.CategoryID, entity.Name, this.tbGroup.Text.Trim()));
                        entity.Name = this.tbGroup.Text.Trim();
                        EAC_Repository.Update(entity);
                        LogHelper.Info(string.Format("修改發件箱分類【{0}】為【{1}】成功", this.CategoryID, entity.Name));
                    }
                }
                "成功".ShowDialog();
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                if (this.CategoryID == -1)
                {
                    LogHelper.Error(string.Format("新增發件箱分類【{0}】異常", tbGroup.Text.Trim()), ex);
                    "發件箱分類添加失败！".ShowDialog();
                }
                else
                {
                    LogHelper.Error(string.Format("修改發件箱分類【{0}】為【{1}】異常", this.CategoryID, tbGroup.Text.Trim()), ex);
                    "發件箱分類修改失败！".ShowDialog();
                }
            }
        }
    }
}
