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
using System.Windows.Shapes;

namespace CurseWork.Views
{
    /// <summary>
    /// Логика взаимодействия для AddressWindow.xaml
    /// </summary>
    public partial class AddressWindow : Window
    {
        public AddressWindow()
        {
            InitializeComponent();
        }

        public void Close_Click(object sender, RoutedEventArgs e)
        {
            int res = -1;
            if (!int.TryParse(tbHouse.Text, out res))
            {
                MessageBox.Show("значение, номер дома не число");
                tbHouse.Text = "";
                return;
            }
            Close();
        }
    }
}
