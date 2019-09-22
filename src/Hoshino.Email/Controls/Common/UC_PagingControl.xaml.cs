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
        public Action ChangePageAction;
        private int _PageIndex;
        public int PageIndex
        {
            get { return _PageIndex; }
            set
            {
                if (_PageIndex != value)
                {
                    _PageIndex = value;
                    ChangePageAction?.Invoke();
                }
            }
        }
        private int _PageSize;
        public int PageSize
        {
            get { return _PageSize; }
            set
            {
                if (_PageSize != value)
                {
                    _PageSize = value;
                    this.PageIndex = 1;
                    ChangePageAction?.Invoke();
                }
            }
        }
        public int PageTotal;
        public UC_PagingControl()
        {
            InitializeComponent();
            this.PageIndex = 0;
            this.PageSize = 10;
        }
        public void InitData(int total)
        {
            this.PageTotal = (int)Math.Ceiling((decimal)total / this.PageSize);
            this.lblTotal.Content = total + "";
            this.lblPage.Content = string.Format("{0}/{1}", this.PageIndex, this.PageTotal);
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            this.PageIndex = 1;
        }

        private void BtnPrePage_Click(object sender, RoutedEventArgs e)
        {
            if (this.PageIndex > 1)
            {
                this.PageIndex--;
            }
        }

        private void BtnLastPage_Click(object sender, RoutedEventArgs e)
        {
            this.PageIndex = this.PageTotal;
        }

        private void BtnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (this.PageIndex < this.PageTotal)
            {
                this.PageIndex++;
            }
        }

        public void RefreshList(bool gotoHome = true)
        {
            if (gotoHome)
            {
                _PageIndex = 1;
            }
            ChangePageAction?.Invoke();
        }
    }
}
