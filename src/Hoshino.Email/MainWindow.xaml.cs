using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hoshino.Email
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Menu
        {
            public string Name { set; get; }
        }
        public MainWindow()
        {
            InitializeComponent();
            List<Menu> MenuList = new List<Menu>();
            MenuList.Add(new Menu { Name = "首頁" });
            MenuList.Add(new Menu { Name = "通訊錄" });
            MenuList.Add(new Menu { Name = "郵件管理" });
            MenuList.Add(new Menu { Name = "新增郵件" });
            MenuList.Add(new Menu { Name = "郵件篩選" });

            ItemsListBox.ItemsSource = MenuList;
        }

        #region 控件事件
        private void ItemsListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);

        }
        #endregion
    }
}
