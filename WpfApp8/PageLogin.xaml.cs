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

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            using (CinemaaDBEntities db = new CinemaaDBEntities())
            {
                var user = db.User.FirstOrDefault(u => u.Login == LoginBox.Text && u.Password == PassBox.Password);
                if (user != null)
                {
                    UserSession.CurrentUser = user;
                    MessageBox.Show("Успешный вход!");
                    NavigationService.Navigate(new PageMain());
                }
                else MessageBox.Show("Неверный логин или пароль!");
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginBox.Text) || string.IsNullOrWhiteSpace(PassBox.Password)) { MessageBox.Show("Заполните поля!"); return; }
            using (CinemaaDBEntities db = new CinemaaDBEntities())
            {
                if (db.User.Any(u => u.Login == LoginBox.Text)) { MessageBox.Show("Логин занят!"); return; }
                db.User.Add(new User { Login = LoginBox.Text, Password = PassBox.Password });
                db.SaveChanges(); MessageBox.Show("Аккаунт создан! Теперь войдите.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e) { NavigationService.Navigate(new PageMain()); }
    }
}