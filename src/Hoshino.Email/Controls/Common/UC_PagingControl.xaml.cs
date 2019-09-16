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

namespace Hoshino.Email.Controls.Common
{
    /// <summary>
    /// UC_PagingControl.xaml 的交互逻辑
    /// </summary>
    public partial class UC_PagingControl : UserControl
    {
        public Action<int> ChangePageAction;
        private int PageIndex;
        private int PageTotal;
        public UC_PagingControl()
        {
            InitializeComponent();
        }
        public void InitData(int index, int total, int size)
        {
            this.PageIndex = index;
            this.PageTotal = (int)Math.Ceiling((decimal)total / size);
            this.lblTotal.Content = total + "";
            this.lblPage.Content = string.Format("{0}/{1}", this.PageIndex, this.PageTotal);
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            ChangePageAction?.Invoke(1);
        }

        private void BtnPrePage_Click(object sender, RoutedEventArgs e)
        {
            if (this.PageIndex > 1)
            {
                ChangePageAction?.Invoke(--this.PageIndex);
            }
        }

        private void BtnLastPage_Click(object sender, RoutedEventArgs e)
        {
            ChangePageAction?.Invoke(this.PageTotal);
        }

        private void BtnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (this.PageIndex < this.PageTotal)
            {
                ChangePageAction?.Invoke(++this.PageIndex);
            }
        }
    }
}
