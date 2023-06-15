using Microsoft.Win32;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using UchetProsmotrennichFilmov.BD;
using System.Text.RegularExpressions;

namespace UchetProsmotrennichFilmov.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEddFilm.xaml
    /// </summary>
    public partial class AddEddFilm : Window
    {
        private Films row = new Films();
        private byte[] data = null;
        Regex regex = new Regex(@"^[0-9]+$");
        Regex namefl = new Regex(@"^\w*([А-яА-яA-Za-z0-9 \.,?!]){1,300}$");
        MatchCollection match;

        public AddEddFilm(Films films)
        {
            InitializeComponent();
            if (films != null)
            {
                row = films;
            }

            AppDB.db = new UchetFilmofEntities();
            DataContext = row;
       
            ComboOne.ItemsSource = BD.AppDB.db.Strany.ToList();
            ComboTwo.ItemsSource = BD.AppDB.db.Tip.ToList();
            ComboThr.ItemsSource = BD.AppDB.db.Rezhisers.ToList();


            if (row.ImageFilm != null) ImageSerice.Source = new ImageSourceConverter().ConvertFrom(row.ImageFilm) as ImageSource;

        }

        private void SelectImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Multiselect = false;
            fileOpen.Filter = "Image | *.png; *.jpg; *.jpeg";
            if (fileOpen.ShowDialog() == true)
            {
                data = System.IO.File.ReadAllBytes(fileOpen.FileName);
                ImageSerice.Source = new ImageSourceConverter().ConvertFrom(data) as ImageSource;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(row.NameFilm)) errors.AppendLine("Введите Название фильма");
            match = namefl.Matches(TxtName4.Text);
            if (match.Count == 0) errors.AppendLine("Название может содержать: только буквы латиницы, кирилицы, цифры, точки, запятые, нижнее подчеркивание, пробел, вопрос, восклицание");
            if (string.IsNullOrWhiteSpace(row.Opisanie)) errors.AppendLine("Введите описание фильма");
            if (row.GodFilma == null) errors.AppendLine("Введите год фильма");
            match = regex.Matches(TxtName1.Text);
            if (match.Count == 0) errors.AppendLine("Год: Только цифры!");
            if (row.Ocenka == null) errors.AppendLine("Введите общий рейтинг фильма");
            match = regex.Matches(TxtName2.Text);
            if (match.Count == 0) errors.AppendLine("Длительность: Только цифры!");
            if ((row.Ocenka) < 0 || (row.Ocenka) > 10) errors.AppendLine("Рейтинг должен быть от 1 до 10");
            if (row.TimeFilm == null) errors.AppendLine("Введите продолжительность фильма");
            match = regex.Matches(TxtName4.Text);
            if (match.Count == 0) errors.AppendLine("Рейтинг: Только цифры!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

          
            if (row.IdFilm == 0)
            {
           
                var stran = ComboOne.SelectedItem as Strany;
                var tip = ComboTwo.SelectedItem as Tip;
                var rezhis = ComboThr.SelectedItem as Rezhisers;

                var films = new BD.Films
                {
                    NameFilm = TxtName.Text,
                    IdStrana = stran.IdStrana,
                    IdTip = tip.TipId,
                    IdRezhiser = rezhis.IdRezhiser,
                    ImageFilm = data,
                    TimeFilm = Convert.ToInt32(TxtName2.Text),
                    GodFilma = Convert.ToInt32(TxtName1.Text),
                    Opisanie = TxtName3.Text,
                    Ocenka = Convert.ToInt32(TxtName4.Text),
                    IdUser = 2
     


                };


                AppDB.db.Films.Add(films);
                AppDB.db.SaveChanges();
                MessageBox.Show("Фильм успешно добавлен", "Поздравляем!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                row.NameFilm = TxtName.Text;
                if (data != null)
                {
                    row.ImageFilm = data;
                }
                row.TimeFilm = Convert.ToInt32(TxtName2.Text);
                row.GodFilma = Convert.ToInt32(TxtName1.Text);
                row.Opisanie = TxtName3.Text;
                row.Ocenka = Convert.ToInt32(TxtName4.Text);


                AppDB.db.SaveChanges();
                MessageBox.Show("Фильм успешно отредактированн", "Поздравляем!", MessageBoxButton.OK, MessageBoxImage.Information);
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
