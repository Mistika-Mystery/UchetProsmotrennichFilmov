using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
using UchetProsmotrennichFilmov.BD;

namespace UchetProsmotrennichFilmov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Glavnaya.xaml
    /// </summary>
    public partial class Glavnaya : Window
    {
        public Glavnaya()
        {
            InitializeComponent();
            CBTip.ItemsSource = BD.AppDB.db.Tip.ToList();
            CBJanr.ItemsSource = BD.AppDB.db.Janr.ToList();
            CBStrana.ItemsSource = BD.AppDB.db.Strany.ToList();
            CBactor.ItemsSource = BD.AppDB.db.Actors.ToList();
            CBRezhis.ItemsSource = BD.AppDB.db.Rezhisers.ToList();
            KatalogGrid.ItemsSource = BD.AppDB.db.Films.ToList();
        }



        private void TxtLogo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите сменить пользователя?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window.GetWindow(this).Close();
            }
        }

        private void TBdev_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите сменить пользователя?",
               "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window.GetWindow(this).Close();
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = (sender as DataGridRow).DataContext as Films;
            AddEddFilm addEddFilm = new AddEddFilm(row);
            addEddFilm.Show();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AppDB.db.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            KatalogGrid.ItemsSource = AppDB.db.Films.ToList();
        }



        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            AddEddFilm addEddFilm = new AddEddFilm(null);
            addEddFilm.Show();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var delFilm = KatalogGrid.SelectedItems.Cast<Films>().ToList();

            if (MessageBox.Show($"Вы дейстиветльно хотите удалить эти {delFilm.Count()} элемента!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)

            {
                AppDB.db.Films.RemoveRange(delFilm);
                AppDB.db.SaveChanges();
                //UpdatePotion();
            }
        }
    }
}

