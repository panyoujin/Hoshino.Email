using Hoshino.Email.Controls.Common;
using Hoshino.Email.Core;
using Hoshino.Email.Core.Helper;
using Hoshino.Email.Entity;
using Hoshino.Email.Repository;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Hoshino.Email.Controls.SendEmailManage
{
    /// <summary>
    /// Win_NewMoreEmail.xaml 的交互逻辑
    /// </summary>
    public partial class Win_NewMoreEmail : Window
    {
        EmailAccountRepository EA_Repository = new EmailAccountRepository();
        EmailAccountCategoryRepository EAC_Repository = new EmailAccountCategoryRepository();
        public Win_NewMoreEmail()
        {
            InitializeComponent();
        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Files\\excel\\SendMail.xls";

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
                openFile.Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var (total, list) = LoadMoreData(openFile.FileName);
                    var faliTotal = 0;
                    if (list != null && list.Count > 0)
                    {
                        faliTotal = list.Count;
                    }

                    string.Format("導入總數量{0},成功{1},失敗{2}", total, total - faliTotal, faliTotal).ShowDialog();
                    this.DialogResult = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowDialog();
            }
        }

        private (int, List<FailEmailAccountEntity>) LoadMoreData(string filePath)
        {
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
                    if (row.GetCell(2) != null)
                        ea.EmailAccountPassWord = row.GetCell(2).ToString();
                    if (row.GetCell(3) != null)
                        ea.EmailAccountSMTP = row.GetCell(3).ToString();
                    if (row.GetCell(4) != null)
                        ea.EmailAccountSMTPPort = int.Parse(row.GetCell(4).ToString());
                    if (row.GetCell(5) != null)
                        ea.EmailAccountPOP3 = row.GetCell(5).ToString();
                    if (row.GetCell(6) != null)
                        ea.EmailAccountPOP3Port = int.Parse(row.GetCell(6).ToString());
                    if (row.GetCell(7) != null)
                        ea.EmailAccountIsSSL = row.GetCell(7).ToString() == "是" ? 1 : 0;
                    if (row.GetCell(8) != null)
                        ea.EmailAccountMaxEmailCount = int.Parse(row.GetCell(8).ToString());
                    if (row.GetCell(9) != null)
                        ea.EmailAccountSpace = int.Parse(row.GetCell(9).ToString());
                    if (row.GetCell(10) != null)
                        ea.SendMode = row.GetCell(10).ToString() == "發送" ? 0 : 1;
                    if (row.GetCell(11) != null)
                        ea.EmailAccountCategoryName = row.GetCell(11).ToString();
                    ea.EmailAccountCategoryID = -1;
                    ea.SendMode = 1;//密送
                    ea.EmailAccountCreateTime = DateTime.Now;
                    ea.EmailAccountLastTime = DateTime.Now;
                    LogHelper.Info(string.Format("準備導入郵箱【{0}】", ea.EmailAccountAddress));
                    if (string.IsNullOrWhiteSpace(ea.EmailAccountAddress))
                    {
                        ea.FailMessage = "郵箱地址為空";
                        failEntity.Add(ea);
                        continue;
                    }
                    if (!MailHelper.IsEmail(ea.EmailAccountAddress))
                    {
                        LogHelper.Info(string.Format("導入郵箱【{0}】地址不正確", ea.EmailAccountAddress));
                        ea.FailMessage = "郵箱地址不正確";
                        failEntity.Add(ea);
                        continue;
                    }
                    if (EA_Repository.GetByAddress(ea.EmailAccountAddress) != null)
                    {
                        LogHelper.Info(string.Format("導入郵箱【{0}】已存在", ea.EmailAccountAddress));
                        ea.FailMessage = "郵箱已存在";
                        failEntity.Add(ea);
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(ea.EmailAccountCategoryName))
                    {
                        var entity = EAC_Repository.Get(ea.EmailAccountCategoryName);
                        if (entity != null)
                        {
                            ea.EmailAccountCategoryID = entity.ID;
                        }
                        else
                        {
                            LogHelper.Info(string.Format("導入郵箱【{0}】，創建分類【{1}】", ea.EmailAccountAddress, ea.EmailAccountCategoryName));
                            EAC_Repository.Insert(ea.EmailAccountCategoryName);
                        }
                        entity = EAC_Repository.Get(ea.EmailAccountCategoryName);
                        if (entity != null)
                        {
                            ea.EmailAccountCategoryID = entity.ID;
                        }
                    }
                    EA_Repository.Insert(ea);
                    LogHelper.Info(string.Format("導入郵箱【{0}】成功", ea.EmailAccountAddress));
                }
                catch (Exception ex)
                {
                    LogHelper.Error(string.Format("導入郵箱【{0}】異常", ea.EmailAccountAddress), ex);
                    ea.FailMessage = ex.Message;
                    failEntity.Add(ea);
                }
            }
            return (total, failEntity);
        }

    }
}
