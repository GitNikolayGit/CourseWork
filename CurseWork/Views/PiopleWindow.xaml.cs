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
    /// Логика взаимодействия для PiopleWindow.xaml
    /// </summary>
    public partial class PiopleWindow : Window
    {
        public PiopleWindow()
        {
            InitializeComponent();
        }

        public void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
