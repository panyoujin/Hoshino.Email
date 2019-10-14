using Hoshino.Email.Core;
using Hoshino.Email.Entity;
using Hoshino.Email.Repository;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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


            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "Excel文件(*.xls)|*.xlsx|Excel文件(*.xlsx)|*.xls|所有文件(*.*)|*.*";
            sfd.FileName = "ReceiptMail.xls";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Thread t = new Thread((obj) =>
                {
                    Stopwatch st = new Stopwatch();
                    st.Start();
                    var dic = obj as Dictionary<string, string>;
                    var list = EBA_Repository.GetBccEmailListByExport(dic["EmailAccount"], dic["Group"]);
                    if (list != null && list.Count > 0)
                    {
                        this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("準備導出【{0}】條", list.Count));
                        string path = ExportExcel("Bcc", list);
                        if (File.Exists(path))
                        {
                            FileInfo fi = new FileInfo(path);
                            fi.CopyTo(dic["FileName"], true);
                            st.Stop();
                            this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("導出【{0}】條完成,耗時{1}", list.Count, st.ElapsedMilliseconds / 1000));
                        }
                    }
                });
                t.Start(new Dictionary<string, string> { ["FileName"] = sfd.FileName, ["EmailAccount"] = this.tbEmailAccount.Text.Trim(), ["Group"] = this.tbGroup.Text.Trim() });
            }
        }

        Dictionary<string, Dictionary<string, string>> DicColumn = new Dictionary<string, Dictionary<string, string>>
        {
            ["Bcc"] = new Dictionary<string, string>
            {
                ["Name"] = "名稱",
                ["Address"] = "郵箱地址",
                ["CategoryName"] = "分类"
            }

        };
        private string ExportExcel(string Model, List<dynamic> list)
        {
            string sFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Temp");
            if (!Directory.Exists(sFileName))
            {
                Directory.CreateDirectory(sFileName);
            }
            sFileName = Path.Combine(sFileName, "ReceiptMail.xls");
            if (System.IO.File.Exists(sFileName))
            {
                System.IO.File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sFileName));
            }

            var cloumns = DicColumn[Model];
            using (var fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet1 = workbook.CreateSheet(Model);
                var rowIndex = 0;
                var cloumnIndex = 0;
                IRow row = sheet1.CreateRow(rowIndex);
                foreach (var c in cloumns)
                {
                    var cc = row.CreateCell(cloumnIndex);
                    cc.SetCellValue(c.Value);
                    sheet1.AutoSizeColumn(cloumnIndex++);
                }

                Stopwatch st = new Stopwatch();
                st.Start();
                DateTime t2 = DateTime.Now;
                foreach (var item in list)
                {
                    rowIndex++;
                    if (rowIndex % 10 == 0)
                    {
                        this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("導出中【{0}/{1}】條,當前10條耗時{2},總耗時{3}", rowIndex, list.Count, (DateTime.Now - t2).Milliseconds, st.ElapsedMilliseconds));
                        t2 = DateTime.Now;
                    }
                    IRow rowData = sheet1.CreateRow(rowIndex);
                    cloumnIndex = 0;
                    foreach (var c in cloumns)
                    {
                        var dics = item as System.Collections.Generic.IDictionary<string, object>;
                        var dic = dics[c.Key];
                        var cc = rowData.CreateCell(cloumnIndex);
                        cc.SetCellValue(dic == null ? "" : dic.ToString());
                        cloumnIndex++;
                    }
                }
                st.Stop();
                workbook.Write(fs);
            }
            return sFileName;
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
                            Stopwatch st = new Stopwatch();
                            st.Start();
                            this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("準備導入【{0}】", file));
                            ImportData(file, st);
                            EBA_Repository.CopyEmailBccAccount();
                            st.Stop();
                            this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("導入【{0}】完成,耗時:{1}秒", file, st.ElapsedMilliseconds / 1000));
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

        private void ImportData(string filePath, Stopwatch st)
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
                int bccCount = sheet.LastRowNum - sheet.FirstRowNum;
                this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("當前預計載入數據{0}行,已耗時:{1}秒", bccCount, st.ElapsedMilliseconds / 1000));

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

                        this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("載入進度{0}/{1},已耗時:{2}秒", i, bccCount, st.ElapsedMilliseconds / 1000));
                    }
                }

                this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("載入數據完成,已耗時:{0}秒", st.ElapsedMilliseconds / 1000));

                stream.Close();
                EBA_Repository.DeleteTemp();
                List<EmailBccAccountEntity> BccOneBatch = new List<EmailBccAccountEntity>();
                int alertCount = 0;
                int totalCount = list.Count;
                foreach (var model in list)
                {
                    alertCount++;
                    BccOneBatch.Add(model);
                    if (BccOneBatch.Count > 100)
                    {
                        EBA_Repository.InsertTemp(BccOneBatch);
                        BccOneBatch.Clear();
                        this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("導入臨時表進度{0}/{1},已耗時:{2}秒", alertCount, totalCount, st.ElapsedMilliseconds / 1000));
                    }
                }
                if (BccOneBatch.Count > 0)
                {
                    EBA_Repository.InsertTemp(BccOneBatch);
                }


                this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("準備複製數據請稍候,耗時{0}", st.ElapsedMilliseconds));
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
                            Stopwatch st = new Stopwatch();
                            st.Start();
                            this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("準備導入【{0}】", file));
                            ImportData(file, st);
                            EBA_Repository.ImportDelete();
                            this.Dispatcher.BeginInvoke(MainWindow.ShowMessage, string.Format("導入【{0}】完成,耗時:{1}ms", file, st.ElapsedMilliseconds));
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
