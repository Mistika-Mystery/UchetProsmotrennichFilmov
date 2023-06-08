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
            ComboOne.SelectedIndex = row.IdStrana - 1;
            ComboTwo.SelectedIndex = row.IdTip - 1;
            ComboThr.SelectedIndex = row.IdRezhiser - 1;
            TxtName1.Text = row.GodFilma.ToString();
            TxtName2.Text = row.TimeFilm.ToString();
            TxtName3.Text = row.Opisanie;

            //ImageSerice.Source = new ImageSourceConverter().ConvertFrom(row.ImageFilm) as ImageSource;

        }

        private void SelectImageBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
