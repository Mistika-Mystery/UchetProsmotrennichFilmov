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
using System.Xml.Linq;
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
            if (AppDB.CurrentUser == null || AppDB.CurrentUser.RolId == 2)
            {
                BtnAddFilm.Visibility = Visibility.Collapsed;
                BtnDel.Visibility = Visibility.Collapsed;
            }
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



 

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var delFilm = KatalogGrid.SelectedItems.Cast<Films>().ToList();

            if (MessageBox.Show($"Вы дейстиветльно хотите удалить фильмов: {delFilm.Count()} шт!?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)

            {
                AppDB.db.Films.RemoveRange(delFilm);
                AppDB.db.SaveChanges();
                UpdFilm();
            }
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            //sortBox.SelectedIndex = -1;

            
            //CBTip.SelectedIndex = -1;
            UpdFilm();


        }

        private void Glavnoe_Loaded(object sender, RoutedEventArgs e)
        {
            UpdFilm();
        }


        private void UpdFilm()
        {
            var Upfilm = AppDB.db.Films.ToList();
            KatalogGrid.ItemsSource = Upfilm;
        }

        private void BtnAddFilm_Click(object sender, RoutedEventArgs e)
        {
            AddEddFilm addEddFilm = new AddEddFilm(null);
            addEddFilm.Show();
        }

        private void Glavnoe_Activated(object sender, System.EventArgs e)
        {
            UpdFilm();
        }


        private void sortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_Filter_Films(SeactWater.Text, sortBox.Text, CBTip.Text);
        }

        private void CBTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_Filter_Films(SeactWater.Text, sortBox.Text, CBTip.SelectedValue.ToString());
        }

        private void CBJanr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CBStrana_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CBactor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CBRezhis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TBoxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TBoxSearch.Visibility = Visibility.Collapsed;
            SeactWater.Visibility = Visibility.Visible;
            SeactWater.Focus();
        }

        private void SeactWater_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SeactWater.Text))
            {
                SeactWater.Visibility = Visibility.Collapsed;
                TBoxSearch.Visibility = Visibility.Visible;
            }
        }

        private void SeactWater_TextChanged(object sender, TextChangedEventArgs e)
        {


            Seach_Filter_Films(SeactWater.Text, sortBox.Text, CBTip.Text);
        }




        private void Seach_Filter_Films(string search = "", string sort = "", string filter = "")
        {

           
            var filmpoisk = AppDB.db.Films.ToList();
            
            switch (sort)
            {
                // сортировака имя, рейтинг, год

                case "По имени, от А до Я":
                    filmpoisk = filmpoisk.OrderByDescending(s => s.NameFilm).ToList();
                    break;
                case "По имени, от Я до А":
                    filmpoisk = filmpoisk.OrderBy(s => s.NameFilm).ToList();

                    break;
                case "По рейтингу, вначале высокий":
                    filmpoisk = filmpoisk.OrderBy(s => s.Ocenka).ToList();
                    break;
                case "По рейтингу, вначале низкий":
                    filmpoisk = filmpoisk.OrderByDescending(s => s.Ocenka).ToList();
                    break;
                case "По году, вначале старее":
                    filmpoisk = filmpoisk.OrderByDescending(s => s.GodFilma).ToList();
                    break;
                case "По году, вначале новее":
                    filmpoisk = filmpoisk.OrderBy(s => s.GodFilma).ToList();
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(search) || !string.IsNullOrWhiteSpace(search))
            {
                //по наименованию и описанию 

                filmpoisk = filmpoisk.Where(s => s.NameFilm.ToLower().Contains(search.ToLower())
                || (s.Opisanie ?? "").ToLower().Contains(search.ToLower())).ToList();
            }
            if (filter != "")
            {

                filmpoisk = filmpoisk.Where(s => s.IdTip.ToString().Contains(filter.ToLower())).ToList();
            }


            KatalogGrid.ItemsSource = null;
            KatalogGrid.ItemsSource = filmpoisk;
        }
    }
}

