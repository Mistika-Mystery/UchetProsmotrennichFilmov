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

namespace UchetProsmotrennichFilmov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
            MyFrame.Navigate(new Pages.Avtorizac());
        }

        private void BtnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
                MyFrame.GoBack();
        }
            private void MainFrame_ContRend(object sender, EventArgs e)
            {
                if (MyFrame.CanGoBack)
                {
                    BtnBack.Visibility = Visibility.Visible;
                }
                else
                {
                    BtnBack.Visibility = Visibility.Hidden;
                }
            }
        
    }
}
