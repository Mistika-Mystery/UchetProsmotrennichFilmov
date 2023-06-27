using System;
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
        private int PagesCount;
        private int NumberOfPage = 0;
        private int maxItemShow = 5;
        List<Films> currentTasks = AppDB.db.Films.ToList();
        public Glavnaya()
        {
            InitializeComponent();

            
            var AllTip = AppDB.db.Tip.ToList();
            AllTip.Insert(0, new Tip
            {
                NameTip = "Все типы"
            });
            CBTip.ItemsSource = AllTip;

            var AllJanr = AppDB.db.Janr.ToList();
            AllJanr.Insert(0, new Janr
            {
                NameJanr = "Все жанры"
            });
            CBJanr.ItemsSource = AllJanr;

            var AllStrana= AppDB.db.Strany.ToList();
            AllStrana.Insert(0, new Strany
            {
                NameStrana = "Все страны"
            });
            CBStrana.ItemsSource = AllStrana;

            var AllActor = AppDB.db.Actors.ToList();
            AllActor.Insert(0, new Actors
            {
                FIO = "Все актёры"
            });
            CBactor.ItemsSource = AllActor;

            var AllRezh = AppDB.db.Rezhisers.ToList();
            AllRezh.Insert(0, new Rezhisers
            {
                FIO = "Все режиссёры"
            });
            CBRezhis.ItemsSource = AllRezh;


            //RezhGrid.ItemsSource = AppDB.db.Rezhisers.ToList();

            PagesCount = Convert.ToInt16(Math.Floor(((double)currentTasks.Count / maxItemShow) - 0.0000001));
            CheckPages();
            KatalogGrid.ItemsSource = currentTasks.Skip(maxItemShow * NumberOfPage).Take(maxItemShow).ToList();

            if (AppDB.CurrentUser == null || AppDB.CurrentUser.RolId == 2)
            {
                BtnAddFilm.Visibility = Visibility.Collapsed;
                BtnDel.Visibility = Visibility.Collapsed;


                //BtnAddRezh.Visibility = Visibility.Collapsed;
                //BtnDelRezh.Visibility = Visibility.Collapsed;
            }

            List<> currentTasks = new List<TaskNames>();
            foreach (var item in AppData.db.CompletedTaskUser.ToList().Where(c => c.UserID == AppData.CurrentUser.ID).ToList())
                currentTasks.Add(AppData.db.TaskNames.ToList().Where(c => c.ID == item.CompletedTaskID && item.UserID == AppData.CurrentUser.ID).First());
            LBMyTasks.ItemsSource = currentTasks;
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
            CBJanr.SelectedIndex= 0;
            CBStrana.SelectedIndex= 0;
            CBactor.SelectedIndex= 0;
            var currentTasks = AppDB.db.Films.ToList();
       

            PagesCount = Convert.ToInt16(Math.Floor(((double)currentTasks.Count / maxItemShow) - 0.0000001));
            CheckPages();
            KatalogGrid.ItemsSource = currentTasks.Skip(maxItemShow * NumberOfPage).Take(maxItemShow).ToList();

            //var upRezh = AppDB.db.Rezhisers.ToList();
            //RezhGrid.ItemsSource = upRezh;

        }
        private void CheckPages()
        {
            if(NumberOfPage == 0)
            {
                TBNextPage.Visibility = Visibility.Collapsed;
                vperimg.Visibility = Visibility.Collapsed;
                TBPrevPage.Visibility = Visibility.Collapsed;
                backim.Visibility = Visibility.Collapsed;



            }

             else if (NumberOfPage > 0)
            {
                TBPrevPage.Text = (NumberOfPage).ToString();
                TBPrevPage.Visibility = Visibility.Visible;
                backim.Visibility = Visibility.Visible;
            }
            else
            {
                TBPrevPage.Visibility = Visibility.Collapsed;
                backim.Visibility = Visibility.Collapsed;


            }
            if (NumberOfPage < PagesCount)
            {
                TBNextPage.Text = (NumberOfPage + 2).ToString();
                TBNextPage.Visibility = Visibility.Visible;
                vperimg.Visibility = Visibility.Visible;
               
            }
            else
            {
                TBNextPage.Visibility = Visibility.Collapsed;
              vperimg.Visibility = Visibility.Collapsed;


            }
            
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
            Seach_Filter_Films(SeactWater.Text);
        }

        private void CBStrana_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_Filter_Films(SeactWater.Text);
        }

        private void CBactor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_Filter_Films(SeactWater.Text);
        }

        private void CBRezhis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seach_Filter_Films(SeactWater.Text);
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

            // комбобоксы
            if (CBTip == null)
            {
                return;
            }
            if (CBTip.SelectedIndex != 0)
            {
                filmpoisk = filmpoisk.Where(s => s.Tip == CBTip.SelectedValue).ToList();
            }
           
            if (CBJanr.SelectedIndex > 0)
            {
                filmpoisk = filmpoisk.Where(s => s.Janr.Contains(CBJanr.SelectedItem as Janr)).ToList();
            }

            if (CBStrana.SelectedIndex != 0)
            {
                filmpoisk = filmpoisk.Where(p => p.Strany == CBStrana.SelectedValue).ToList();
            }

            if (CBactor.SelectedIndex > 0)
            {
                filmpoisk = filmpoisk.Where(s => s.Actors.Contains(CBactor.SelectedItem as Actors)).ToList();
            }

            if (CBRezhis.SelectedIndex != 0)
            {
                filmpoisk = filmpoisk.Where(p => p.Rezhisers == CBRezhis.SelectedValue).ToList();
            }

            KatalogGrid.ItemsSource = filmpoisk;
            PagesCount = Convert.ToInt16(Math.Floor(((double)filmpoisk.Count / maxItemShow) - 0.0000001));
            CheckPages();
            KatalogGrid.ItemsSource = filmpoisk.Skip(maxItemShow * NumberOfPage).Take(maxItemShow).ToList();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Seach_Filter_Films(SeactWater.Text);
            if (NumberOfPage < PagesCount)
            {
               
                NumberOfPage++;
                TBCurrentPage.Text = (NumberOfPage + 1).ToString();
                CheckPages();
                
                
            }
        }

        private void backim_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Seach_Filter_Films(SeactWater.Text);
            if (NumberOfPage > 0)
            {
                
                NumberOfPage--;
                TBCurrentPage.Text = (NumberOfPage + 1).ToString();
                CheckPages();
                
                
            }
        }

        //private void BtnAddRezh_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void BtnDelRezh_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void RezhGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    var row = (sender as DataGridRow).DataContext as Rezhisers;
        //    AddRezhWindow addRezhWindow = new AddRezhWindow(row);
        //    addRezhWindow.Show();
        //}
    }
}

