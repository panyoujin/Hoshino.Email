using Hoshino.Email.Controls.Common;
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

namespace Hoshino.Email.Controls.ContactsForm
{
    /// <summary>
    /// UC_ContactsGroup.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ContactsGroup : UserControl
    {
        public UC_ContactsGroup()
        {
            InitializeComponent();
        }

        private void BtnInsertGroup_Click(object sender, RoutedEventArgs e)
        {
            new Win_NewContactsGroup().Show();
        }
    }
}
