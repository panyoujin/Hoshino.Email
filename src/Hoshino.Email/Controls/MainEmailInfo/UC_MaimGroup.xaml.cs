﻿using Hoshino.Email.Controls.Common;
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

namespace Hoshino.Email.Controls.MainEmailInfo
{
    /// <summary>
    /// UC_MaimGroup.xaml 的交互逻辑
    /// </summary>
    public partial class UC_MaimGroup : UserControl
    {
        public UC_MaimGroup()
        {
            InitializeComponent();
        }


        private void BtnInsertGroup_Click(object sender, RoutedEventArgs e)
        {
            new WinDialog("提示框").Show();
        }
    }
}
