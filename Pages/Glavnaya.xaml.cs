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
            sortBox.SelectedIndex = -1;

            
            CBTip.SelectedIndex = -1;

           

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

        }

        private void CBTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        }




        //private void Seach_Filter_Films(string search = "", string sort = "По имени, от А до Я", string filter = "Все породы")
        //{

        //   // var materialList = DB.AppData.db.kotiki.ToList();
        //    var filmpoisk = AppDB.db.Films.ToList();
        //    switch (sort)
        //    {
        //        //наименование, остаток на складе и стоимость 

        //        case "По имени, от А до Я":
        //            materialList = materialList.OrderByDescending(s => s.name).ToList();
        //            break;
        //        case "По имени, от Я до А":
        //            materialList = materialList.OrderBy(s => s.name).ToList();

        //            break;
        //        case "Вначале моложе":
        //            materialList = materialList.OrderBy(s => s.dr).ToList();
        //            break;
        //        case "Вначале старее":
        //            materialList = materialList.OrderByDescending(s => s.dr).ToList();
        //            break;
        //        case "По убыванию цены":
        //            materialList = materialList.OrderBy(s => s.price).ToList();
        //            break;
        //        case "По возрастанию цены":
        //            materialList = materialList.OrderByDescending(s => s.price).ToList();
        //            break;
        //        default:
        //            break;
        //    }
        //    if (!string.IsNullOrEmpty(search) || !string.IsNullOrWhiteSpace(search))
        //    {
        //        //по наименованию и описанию материала 

        //        materialList = materialList.Where(s => s.name.ToLower().Contains(search.ToLower())
        //        || (s.poroda ?? "").ToLower().Contains(search.ToLower())).ToList();
        //    }
        //    if (filter != "Все породы")
        //    {
        //        materialList = materialList.Where(s => s.poroda.ToLower().Contains(filter.ToLower())).ToList();
        //    }
        //    materialsGrid.ItemsSource = materialList;
        //}
    }
}

