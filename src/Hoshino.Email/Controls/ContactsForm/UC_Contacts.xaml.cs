using Hoshino.Email.Core;
using Hoshino.Email.Entity;
using Hoshino.Email.Repository;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;

namespace Hoshino.Email.Controls.ContactsForm
{
    /// <summary>
    /// UC_Contacts.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Contacts : System.Windows.Controls.UserControl
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
            var entity = (sender as System.Windows.Controls.Button).DataContext as EmailBccAccountEntity;
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
            var entity = (sender as System.Windows.Controls.Button).DataContext as EmailBccAccountEntity;
            string.Format("是否刪除發件箱【{0}】", entity.EmailBccAccountAddress).ShowYesOrNoDialog(yesAction: () =>
            {
                EBA_Repository.Delete(entity.EmailBccAccountID);
                this.ucPage.RefreshList(false);
            });
        }

        private void BtnDataExport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            if (EBA_Repository.ExistsEmailbccaccountTemp())
            {
                "正在導入數據中，請稍後再試".ShowDialog();
                return;
            }
            try
            {
                System.Windows.Forms.OpenFileDialog op = new System.Windows.Forms.OpenFileDialog
                {
                    Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*",
                    Multiselect = true
                };
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Thread thread = new Thread(s =>
                    {
                        var list = s as string[];
                        foreach (var file in list)
                        {
                            DateTime t = DateTime.Now;
                            this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("準備導入【{0}】", file));
                            ImportData(file);
                            EBA_Repository.CopyEmailBccAccount();
                            this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("導入【{0}】完成,耗時:{1}ms", file, (DateTime.Now - t).TotalMilliseconds));
                        }
                    });
                    thread.Start(op.FileNames);

                    "正在後臺進行批量導入！".ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("批量導入異常", ex);
                "批量導入失敗！".ShowDialog();
            }
        }

        private void ImportData(string filePath)
        {
            try
            {
                //LogHelper.Info("處理excel開始時間" + DateTime.Now);
                List<EmailBccAccountEntity> list = new List<EmailBccAccountEntity>();

                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
                HSSFWorkbook workbook = new HSSFWorkbook(stream);
                //获取excel的第一个sheet
                HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(0);

                //最后一列的标号  即总的行数
                int rowCount = sheet.LastRowNum;
                int rowFirst = sheet.FirstRowNum;
                this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("當前導入數據{0}行", rowCount - rowFirst));
                for (int i = (rowFirst + 1); i < rowCount + 1; i++)
                {
                    HSSFRow row = (HSSFRow)sheet.GetRow(i);
                    if (row.GetCell(1) != null && !string.IsNullOrEmpty(row.GetCell(1).ToString()))
                    {
                        string bccAccountAddress = row.GetCell(1).ToString().Trim();
                        string bccCategoryName = row.GetCell(2).ToString().Trim();
                        string bccAccountName = "";
                        if (row.GetCell(0) != null)
                        {
                            bccAccountName = row.GetCell(0).ToString().Trim();
                        }
                        list.Add(new EmailBccAccountEntity() { EmailBccAccountName = bccAccountName, EmailBccAccountAddress = bccAccountAddress, EmailBccAccountCategoryName = bccCategoryName });
                    }
                }
                stream.Close();
                EBA_Repository.DeleteTemp();
                EBA_Repository.InsertTemp(list);
                this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("導入數據{0}行成功", rowCount - rowFirst));
            }
            catch (Exception ex)
            {
                LogHelper.Error(string.Format("導入失敗，文件：{0},錯誤信息:{1}", filePath, ex.Message), ex);
                EBA_Repository.DeleteTemp();
            }
        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + "Files\\excel\\ReceiptMail.xls";

            if (File.Exists(path))
            {
                FileInfo fi = new FileInfo(path);
                //MessageBox.Show(fi.Name);

                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
                sfd.FileName = fi.Name.Split('.')[0];
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ExportExcel(fi, sfd.FileName);
                }
            }
            else
            {
                "沒有模板文件！".ShowDialog();
            }
        }
        void ExportExcel(FileInfo fi, string savePath)
        {
            try
            {
                fi.CopyTo(savePath, true);
                "模板導出成功！".ShowDialog();
            }
            catch (Exception ex)
            {
                "模板導出失敗！".ShowDialog();
            }
        }

        private void BtnDeleteExport_Click(object sender, RoutedEventArgs e)
        {
            if (EBA_Repository.ExistsEmailbccaccountTemp())
            {
                "正在導入數據中，請稍後再試".ShowDialog();
                return;
            }
            try
            {
                System.Windows.Forms.OpenFileDialog op = new System.Windows.Forms.OpenFileDialog
                {
                    Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*",
                    Multiselect = true
                };
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Thread thread = new Thread(s =>
                    {
                        var list = s as string[];
                        foreach (var file in list)
                        {
                            DateTime t = DateTime.Now;
                            this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("準備導入【{0}】", file));
                            ImportData(file);
                            EBA_Repository.ImportDelete();
                            this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("導入【{0}】完成,耗時:{1}ms", file, (DateTime.Now - t).TotalMilliseconds));
                        }
                    });
                    thread.Start(op.FileNames);

                    "正在後臺進行批量導入！".ShowDialog();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("批量導入異常", ex);
                "批量導入失敗！".ShowDialog();
            }
        }
    }
}
