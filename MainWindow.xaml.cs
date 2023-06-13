using System.Windows;

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
        // кнопка назад, скрытая при невозможности - сейчас не нужна
        //private void BtnBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

        //        MyFrame.GoBack();
        //}
        //    private void MainFrame_ContRend(object sender, EventArgs e)
        //    {
        //        if (MyFrame.CanGoBack)
        //        {
        //            BtnBack.Visibility = Visibility.Visible;
        //        }
        //        else
        //        {
        //            BtnBack.Visibility = Visibility.Hidden;
        //        }
        //    }

    }
}
