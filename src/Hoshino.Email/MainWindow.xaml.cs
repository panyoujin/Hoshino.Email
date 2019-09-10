﻿using Hoshino.Email.Controls.SendEmailManage;
using Hoshino.Email.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            public SysFunction SysFunction { set; get; }

            public object Content { set; get; }
        }

        private List<Menu> MenuList = new List<Menu>();
        public MainWindow()
        {
            InitializeComponent();
            Init();
            ItemsListBox.ItemsSource = MenuList;
            MainContent.Content = new UC_SendEmailManage();
        }

        private void Init()
        {
            MenuList.Add(new Menu { Name = "發送郵件管理", SysFunction = SysFunction.HomePage ,Content=new UC_SendEmailManage()});
            MenuList.Add(new Menu { Name = "通訊錄", SysFunction = SysFunction.AddressBook });
            MenuList.Add(new Menu { Name = "接收郵件管理", SysFunction = SysFunction.MailManagement });
            MenuList.Add(new Menu { Name = "新增郵件", SysFunction = SysFunction.NewMail });
            MenuList.Add(new Menu { Name = "郵件篩選", SysFunction = SysFunction.MailScreening });
        }

        #region 控件事件
        private void ItemsListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var menuItem = (sender as ListBox).SelectedItem as Menu;
            if (menuItem != null)
            {
                MainContent.Content = menuItem.Content;
                LeftMenu.IsLeftDrawerOpen = false;
            }
        }
        #endregion
    }
}
