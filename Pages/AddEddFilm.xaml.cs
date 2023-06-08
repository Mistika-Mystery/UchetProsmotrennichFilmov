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
using UchetProsmotrennichFilmov.BD;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.Win32;
using System.IO;
using System.Windows.Navigation;
using static System.Net.WebRequestMethods;

namespace UchetProsmotrennichFilmov.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEddFilm.xaml
    /// </summary>
    public partial class AddEddFilm : Window
    {
        private BD.Films row = null;
        private byte[] data = null;
        public AddEddFilm()
        {
            InitializeComponent();

        }
        public AddEddFilm(Films films)
        {
            InitializeComponent();
            row = films;
            DataContext = row;
            TxtName.Text = row.NameFilm;
            ComboOne.ItemsSource = BD.AppDB.db.Strany.ToList();
            ComboTwo.ItemsSource = BD.AppDB.db.Tip.ToList();
            ComboThr.ItemsSource = BD.AppDB.db.Rezhisers.ToList();
            TxtName1.Text = row.GodFilma.ToString();
            TxtName2.Text = row.TimeFilm.ToString();
            TxtName3.Text = row.Opisanie;
            TxtName4.Text = row.Ocenka.ToString();

            if (row.ImageFilm != null) ImageSerice.Source = new ImageSourceConverter().ConvertFrom(row.ImageFilm) as ImageSource;

        }

        private void SelectImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Multiselect = false;
            fileOpen.Filter = "Image | *.png; *.jpg; *.jpeg";
            if (fileOpen.ShowDialog() == true)
            {
                data = File.ReadAllBytes(fileOpen.FileName);
                ImageSerice.Source = new ImageSourceConverter()
                   .ConvertFrom(data) as ImageSource;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            
            if (string.IsNullOrWhiteSpace(TxtName.Text))
                errors.AppendLine("Введите Название фильма");
            if (string.IsNullOrWhiteSpace(TxtName1.Text))
                errors.AppendLine("Введите год фильма");
            if (string.IsNullOrWhiteSpace(TxtName2.Text))
                errors.AppendLine("Введите время фильма");
            if (string.IsNullOrWhiteSpace(TxtName3.Text))
                errors.AppendLine("Повторите описание фильма");
            if (string.IsNullOrWhiteSpace(TxtName4.Text))
                errors.AppendLine("Введите общий рейтинг");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (row == null)
            {
                var films = new Films();
                
                    films = new Films
                    {
                        NameFilm = TxtName.Text,
                        IdStrana = AppDB.db.Strany.Where(p => p.NameStrana == ComboOne.SelectedItem.ToString()).Select(p => p.IdStrana).FirstOrDefault(),
                        IdTip = AppDB.db.Tip.Where(p => p.NameTip == ComboTwo.SelectedItem.ToString()).Select(p => p.TipId).FirstOrDefault(),
                        IdRezhiser = AppDB.db.Rezhisers.Where(p => p.FIO == ComboThr.SelectedItem.ToString()).Select(p => p.IdRezhiser).FirstOrDefault(),
                        ImageFilm = data
                    };
            

                AppDB.db.Films.Add(films);
                AppDB.db.SaveChanges();
                MessageBox.Show("Фильм успешно добавлен", "Поздравляем!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                row.NameFilm = TxtName.Text;
                row.IdStrana = AppDB.db.Strany.Where(p => p.NameStrana == ComboOne.SelectedItem.ToString()).Select(p => p.IdStrana).FirstOrDefault();
                row.IdTip = AppDB.db.Tip.Where(p => p.NameTip == ComboTwo.SelectedItem.ToString()).Select(p => p.TipId).FirstOrDefault();
                row.IdRezhiser = AppDB.db.Rezhisers.Where(p => p.FIO == ComboThr.SelectedItem.ToString()).Select(p => p.IdRezhiser).FirstOrDefault();
                if (data != null)
                    row.ImageFilm = data;

                AppDB.db.SaveChanges();
                MessageBox.Show("Фильм успешно добавлен", "Поздравляем!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
    







        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите вернуться?\nНесохраненные данные могут быть утеряны",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
