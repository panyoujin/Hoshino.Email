using Hoshino.Email.Controls.Common;
using Hoshino.Email.Core;
using Hoshino.Email.Core.Helper;
using Hoshino.Email.Entity;
using Hoshino.Email.Helper;
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

namespace Hoshino.Email.Controls.EmailInfoManage
{
    /// <summary>
    /// Win_NewMoreEmail.xaml 的交互逻辑
    /// </summary>
    public partial class Win_ExportExcel : Window
    {
        EmailSendBccAccountRepository ESBR_Repository = new EmailSendBccAccountRepository();

        public int EmailID;
        public Win_ExportExcel(int EmailID)
        {
            InitializeComponent();
            this.EmailID = EmailID;
        }

        private void BtnSuccess_Click(object sender, RoutedEventArgs e)
        {
            ExportExcel(1);
        }

        private void BtnFail_Click(object sender, RoutedEventArgs e)
        {
            ExportExcel(-1);
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            ExportExcel(2);
        }

        private void BtnNoStart_Click(object sender, RoutedEventArgs e)
        {
            ExportExcel(0);
        }

        public void ExportExcel(int state)
        {
            List<EmailBccAccountEntity> bccAccountList = ESBR_Repository.GetExportEmailBccAccountList(EmailID, state).ToList();
            if (bccAccountList == null || bccAccountList.Count <= 0)
            {
                "不可以導出空數據".ShowDialog();
                return;
            }
            ExportBccAccountHelper helper = new ExportBccAccountHelper();
            helper.Export(bccAccountList);
        }

    }
}
