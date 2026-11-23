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
{ /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
    public partial class PageProfile : Page
    {
        public PageProfile() { 
            InitializeComponent();
            LoadData(); }

        private void LoadData()
        {
            if (UserSession.CurrentUser == null) { NavigationService.Navigate(new PageMain()); return; }

            UserInfoText.Text = $"Добро пожаловать, {UserSession.CurrentUser.Login}!";

            using (CinemaaDBEntities db = new CinemaaDBEntities())
            {
                TicketsList.ItemsSource = db.Ticket.Where(t => t.UserId == UserSession.CurrentUser.Id).ToList();
            }
        }

        private void GoToMain_Click(object sender, RoutedEventArgs e) { NavigationService.Navigate(new PageMain()); }
    }
}