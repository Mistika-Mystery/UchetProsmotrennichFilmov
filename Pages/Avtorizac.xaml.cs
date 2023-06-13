using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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

            if (Log != null)
            {
                AppDB.CurrentUser = Log;
                MessageBox.Show("Рады видеть Вас, " + AppDB.CurrentUser.Login + "!");
                Glavnaya glavnaya = new Glavnaya();
                glavnaya.Show();
                Window.GetWindow(this).Close();
            }

            else
            {
                MessageBox.Show("Такого пользователя \n      не существует\nПопробуйте еще раз\n           ИЛИ\nЗарегестрируйтесь");
            }
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            Registracia registracia = new Registracia();
            registracia.Show();


        }
    }
}
