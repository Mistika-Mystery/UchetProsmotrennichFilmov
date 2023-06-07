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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UchetProsmotrennichFilmov.BD;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;

namespace UchetProsmotrennichFilmov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registracia.xaml
    /// </summary>
    public partial class Registracia : Window
    {
        private Users _currenUser = new Users();
        public Registracia()
        {
            InitializeComponent();
            DataContext = _currenUser;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnRegistr_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currenUser.Login))
            errors.AppendLine("Введите Логин");
            if (string.IsNullOrWhiteSpace(_currenUser.Pass))
             errors.AppendLine("Введите Пароль");
            if (string.IsNullOrWhiteSpace(_currenUser.Pass2))
            errors.AppendLine("Повторите Пароль");
            if (string.IsNullOrWhiteSpace(_currenUser.Email))
            errors.AppendLine("Введите e-mail");

            if (errors.Length > 0)
            {   
                MessageBox.Show(errors.ToString());
                return;
            }
            
            var Email = AppDB.db.Users.FirstOrDefault(x => x.Email == _currenUser.Email.ToString());
            if (Email != null)
            {
                errors.AppendLine("Этот email уже заригистрирован");
            }

            var Log = AppDB.db.Users.FirstOrDefault(x => x.Login == _currenUser.Login.ToString());
            if (Log != null)
            {
                errors.AppendLine("Такой логин уже существует, выберите другой");
            }
            if (_currenUser.Pass != _currenUser.Pass2)
            {
                errors.AppendLine("Пароли не совпадают!");
            }
            if (!_currenUser.Email.Contains("@"))
            {
                errors.AppendLine("Не верный формат почты");
            }
            if (_currenUser.DR == DateTime.Today || _currenUser.DR > DateTime.Today || _currenUser.DR == null || _currenUser.DR < tbdr.DisplayDateStart)
            {
                errors.AppendLine("Неправильно введена дата рождения");
            }

            if (IsEmailAllowed(tblogin.Text.Trim()) == false)
            {
                errors.AppendLine("Не верный формат почты: англ буквы + @ + англ буквы + точка + англ буквы");
            }
            if (IsPasswordAllowed(tbpass1.Text.Trim()) == false)
            {
                errors.AppendLine("Пароль должен содеражать: минимум 8 символов, цифры, заглавные и строчные буквы латиницы");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            else if (Log == null || Email == null ||  _currenUser.IdUser == 0)
            {

                _currenUser.RolId = 2;


                AppDB.db.Users.Add(_currenUser);

                try
                {
                    AppDB.db.SaveChanges();
                    MessageBox.Show(_currenUser.Login + ", Поздравляем! \n Вы Успешно Зарегистрировались! \n Теперь можете авторизироваться");
                    AppDB.db.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private static bool IsEmailAllowed(string text)
        {
            bool blnValidEmail = false;
            Regex regEMail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            if (text.Length > 0)
            {
                blnValidEmail = regEMail.IsMatch(text);
            }

            return blnValidEmail;
        }
        private static bool IsPasswordAllowed(string text)
        {
            bool blnValidPassword = false;
            Regex regPassword = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$");
            if (text.Length > 0)
            {
                blnValidPassword = regPassword.IsMatch(text);
            }

            return blnValidPassword;
        }

    }
}
