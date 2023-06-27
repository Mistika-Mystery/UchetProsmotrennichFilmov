using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace UchetProsmotrennichFilmov.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddRezhWindow.xaml
    /// </summary>
    public partial class AddRezhWindow : Window
    {
        private Rezhisers row = new Rezhisers();
        private byte[] data = null;
        
        Regex namefl = new Regex(@"^\w*([А-яА-яA-Za-z0-9 \.,?!]){1,300}$");
        MatchCollection match;

        public AddRezhWindow(Rezhisers rezhisers)
        {
            InitializeComponent();
            if (rezhisers != null)
            {
                row = rezhisers;
            }

            AppDB.db = new UchetFilmofEntities();
            DataContext = row;

            ComboOne.ItemsSource = BD.AppDB.db.Strany.ToList();
          


            if (row.ImageRezhiser != null) ImageSerice1.Source = new ImageSourceConverter().ConvertFrom(row.ImageRezhiser) as ImageSource;

        }

        private void SelectImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Multiselect = false;
            fileOpen.Filter = "Image | *.png; *.jpg; *.jpeg";
            if (fileOpen.ShowDialog() == true)
            {
                data = System.IO.File.ReadAllBytes(fileOpen.FileName);
                ImageSerice1.Source = new ImageSourceConverter().ConvertFrom(data) as ImageSource;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(row.FIO)) errors.AppendLine("Введите Название фильма");
            match = namefl.Matches(TxtName.Text);
           
            if (row.DR == null) errors.AppendLine("Введите др режиссера");
           
           

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            if (row.IdRezhiser == 0)
            {

                var stran = ComboOne.SelectedItem as Strany;


                var rezhisers = new BD.Rezhisers
                {
                    FIO = TxtName.Text,
                    StranaId = stran.IdStrana,

                    ImageRezhiser = data,

                    DR = DateTime.Now



                };


                AppDB.db.Rezhisers.Add(rezhisers);
                AppDB.db.SaveChanges();
                MessageBox.Show("Режиссер успешно добавлен", "Поздравляем!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                row.FIO = TxtName.Text;
                if (data != null)
                {
                    row.ImageRezhiser = data;
                }
                
                //row.DR = tbdr.SelectedDate.Value;




                AppDB.db.SaveChanges();
                MessageBox.Show("Режиссер успешно отредактированн", "Поздравляем!", MessageBoxButton.OK, MessageBoxImage.Information);
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
