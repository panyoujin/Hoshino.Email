using Hoshino.Email.Controls.Common;
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

    }
}
