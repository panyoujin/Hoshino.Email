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

namespace Hoshino.Email.Controls.EmailInfoManage
{
    /// <summary>
    /// UC_EmailInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UC_EmailInfo : UserControl
    {
        EmailInfoRepository EI_Repository = new EmailInfoRepository();
        EmailSendBccAccountRepository ESBAR_Repository = new EmailSendBccAccountRepository();

        List<EmailInfoEntity> EmailInfoList = new List<EmailInfoEntity>();

        public UC_EmailInfo()
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


        private void GetList()
        {
            var (list, total) = EI_Repository.GetList(this.tbEmailAccount.Text, this.ucPage.PageIndex, this.ucPage.PageSize);
            dgEmail.ItemsSource = list;
            EmailInfoList = list.ToList();
            this.ucPage.InitData(total);
        }



        private void BtnReceiptList_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (bt != null && bt.Tag != null)
            {
                new Win_ReceiptList((int)bt.Tag).Show();
            }

        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            this.ucPage.RefreshList();
        }

        private void BtnSentList_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (bt != null && bt.Tag != null)
            {
                new Win_SentList((int)bt.Tag).Show();
            }

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            "是否刪除該郵件,以及該郵件相關數據".ShowYesOrNoDialog(yesAction: () =>
            {
                Button bt = sender as Button;
                if (bt != null && bt.Tag != null)
                {
                    EI_Repository.Delete((int)bt.Tag);
                    GetList();
                }
            });

        }

        private void BtnOperation_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (bt != null && bt.Tag != null)
            {
                var emailInfo = EmailInfoList.FirstOrDefault(p => p.EmailID == (int)bt.Tag);
                if (emailInfo != null && emailInfo.EmailID > 0)
                {
                    EmailInfoEntity entity = new EmailInfoEntity();
                    entity.EmailID = emailInfo.EmailID;
                    switch (emailInfo.EmailState)
                    {
                        case 0:
                            //設置停止發送
                            entity.EmailState = 4;
                            //修改密送記錄的郵件狀態
                            ESBAR_Repository.Update(emailInfo.EmailID, 2, 0);
                            break;
                        case 2:
                            //設置開始發送
                            entity.EmailState = 0;
                            break;
                        case 4:
                            //設置繼續發送
                            entity.EmailState = 0;
                            //修改密送記錄的郵件狀態
                            ESBAR_Repository.Update(emailInfo.EmailID, 0, 2);
                            break;
                    }
                    EI_Repository.Update(entity);
                    GetList();
                }
            }
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmail.SelectedItem == null)
            {
                "請選擇導出的郵件信息".ShowDialog();
                return;
            }
            var selectItem = dgEmail.SelectedItem as EmailInfoEntity;
            Win_ExportExcel win = new Win_ExportExcel(selectItem.EmailID);
            win.Show();

        }

        private void BtnSendFail_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmail.SelectedItem == null)
            {
                "請選擇重新發送的的郵件信息".ShowDialog();
                return;
            }
            var selectItem = dgEmail.SelectedItem as EmailInfoEntity;
            EI_Repository.Update(new EmailInfoEntity { EmailID = selectItem.EmailID, EmailState = 0,FailQty=0 });

            //修改密送記錄的郵件狀態
            ESBAR_Repository.Update(selectItem.EmailID, 0, -1);
            ESBAR_Repository.Update(selectItem.EmailID, 0, 2);

            "狀態修改成功".ShowDialog();
            GetList();
        }
    }
}
