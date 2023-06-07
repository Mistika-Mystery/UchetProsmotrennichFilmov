using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для Avtorizac.xaml
    /// </summary>
    public partial class Avtorizac : Page
    {
        public Avtorizac()
        {
            InitializeComponent();
           
        }

        private void BtnVhod_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TBLogo.Text))
                errors.AppendLine("Укажите почту");
            if (string.IsNullOrWhiteSpace(PBPass.Password))
                errors.AppendLine("Укажите пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var Log = BD.AppDB.db.Users.FirstOrDefault(x => x.Login == TBLogo.Text && x.Pass == PBPass.Password.ToString());
            var isRoot = BD.AppDB.db.Users.FirstOrDefault(x => x.RolId == 1);
            if (Log != null && isRoot == Log)
            {
                AppDB.CurrentUser = Log;
                MessageBox.Show("Рады видеть Вас, " + AppDB.CurrentUser.Login + "!");
                NavigationService.Navigate(new Pages.Katalog());
            }
            else if (Log != null)
                {
                AppDB.CurrentUser = Log;
                    MessageBox.Show("Рады видеть Вас, " + AppDB.CurrentUser.Login + "!");
                    NavigationService.Navigate(new Pages.Katalog());
                }
            else
            {
                MessageBox.Show("Такого пользователя \n      не существует\nПопробуйте еще раз\n           ИЛИ\nЗарегестрируйтесь");
            }
        }
    }
}
