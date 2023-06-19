using System.Collections.Generic;
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
        //List<BD.Films> currentFilms = new List<BD.Films>();
        public Glavnaya()
        {
            InitializeComponent();

            
            var AllTip = AppDB.db.Tip.ToList();
            AllTip.Insert(0, new Tip
            {
                NameTip = "Все типы"
            });
            CBTip.ItemsSource = AllTip;
           

            CBJanr.ItemsSource = BD.AppDB.db.Janr.ToList();
            CBStrana.ItemsSource = BD.AppDB.db.Strany.ToList();
            CBactor.ItemsSource = BD.AppDB.db.Actors.ToList();
            CBRezhis.ItemsSource = BD.AppDB.db.Rezhisers.ToList();

           
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
            
            UpdFilm();


        }

        private void Glavnoe_Loaded(object sender, RoutedEventArgs e)
        {
            UpdFilm();
        }


        private void UpdFilm()
        {
            SeactWater.Visibility= Visibility.Collapsed;
            SeactWater.Text = "";
            TBoxSearch.Visibility = Visibility.Visible;
            sortBox.SelectedIndex= 0;
            CBTip.SelectedIndex= 0;
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
            Seach_Filter_Films(SeactWater.Text);
        }

        private void CBTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_Filter_Films(SeactWater.Text);
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


            Seach_Filter_Films(SeactWater.Text);
        }




        private void Seach_Filter_Films(string search = "")
        {

           
            var filmpoisk = AppDB.db.Films.ToList();
          


            switch (sortBox.SelectedIndex)
            {
                // сортировака имя, рейтинг, год

                case 1:
                    filmpoisk = filmpoisk.OrderBy(s => s.NameFilm).ToList();
                    break;
                case 2:
                    filmpoisk = filmpoisk.OrderByDescending(s => s.NameFilm).ToList();

                    break;
                case 3:
                    filmpoisk = filmpoisk.OrderByDescending(s => s.Ocenka).ToList();
                    break;
                case 4:
                    filmpoisk = filmpoisk.OrderBy(s => s.Ocenka).ToList();
                    break;
                case 5:
                    filmpoisk = filmpoisk.OrderBy(s => s.GodFilma).ToList();
                    break;
                case 6:
                    filmpoisk = filmpoisk.OrderByDescending(s => s.GodFilma).ToList();
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
          
            //if (CBTip.SelectedIndex != 0)
            //{
            //    filmpoisk = filmpoisk.Where(s => s.Tip == CBTip.SelectedValue).ToList();
            //}




            KatalogGrid.ItemsSource = filmpoisk;
        }
    }
}

