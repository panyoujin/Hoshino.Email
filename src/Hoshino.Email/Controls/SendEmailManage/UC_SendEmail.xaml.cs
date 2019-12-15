using Hoshino.Email.Controls.Common;
using Hoshino.Email.Core;
using Hoshino.Email.Core.Helper;
using Hoshino.Email.Entity;
using Hoshino.Email.Repository;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
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
            bool? result = new Win_NewMoreEmail().ShowDialog();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
                openFile.Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<string> failCount = DeleteEmailData(openFile.FileName);

                    if (failCount != null && failCount.Count > 0)
                    {
                        int count = failCount.Count;
                        string failStr = "";
                        int i = 0;
                        foreach (var str in failCount)
                        {
                            i++;
                            failStr += str;
                            if (i != count)
                            {
                                failStr += " | ";
                            }
                        }
                        string.Format("刪除失敗郵箱：{0},其他刪除成功", failStr).ShowDialog();
                    }
                    else
                    {
                        "郵箱刪除成功".ShowDialog();
                    }
                    this.ucPage.RefreshList(false);

                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowDialog();
            }
        }

        private List<string> DeleteEmailData(string filePath)
        {
            List<string> failAccount = new List<string>();
            List<FailEmailAccountEntity> failEntity = new List<FailEmailAccountEntity>();

            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            HSSFWorkbook workbook = new HSSFWorkbook(stream);
            //获取excel的第一个sheet
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(0);
            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum;
            int total = 0;
            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum + 1; i++)
            {
                total++;
                FailEmailAccountEntity ea = new FailEmailAccountEntity();
                try
                {
                    HSSFRow row = (HSSFRow)sheet.GetRow(i);
                    if (row.GetCell(0) != null)
                        ea.EmailAccountName = row.GetCell(0).ToString();
                    if (row.GetCell(1) != null)
                        ea.EmailAccountAddress = row.GetCell(1).ToString();


                    if (!EA_Repository.Delete(ea.EmailAccountAddress))
                    {
                        failAccount.Add(ea.EmailAccountAddress);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error(string.Format("删除郵箱【{0}】異常", ea.EmailAccountAddress), ex);
                    ea.FailMessage = ex.Message;
                    failEntity.Add(ea);
                }
            }
            return failAccount;
        }
    }
}
