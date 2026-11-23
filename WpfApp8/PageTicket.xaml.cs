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
    public partial class PageTicket : Page
    {
        private int _sessionId, _row, _seat;

        public PageTicket(int sessionId, int row, int seat)
        {
            InitializeComponent();
            _sessionId = sessionId;
            _row = row;
            _seat = seat;
            LoadInfo();
        }

        private void LoadInfo()
        {
            using (CinemaaDBEntities db = new CinemaaDBEntities())
            {
                var session = db.Session.FirstOrDefault(s => s.Id == _sessionId);
                if (session == null) return;

                MovieTitleText.Text = session.Movie.Title;
                SessionInfoText.Text = $"Зал: {session.Hall.Name} | {session.StartTime:dd.MM.yyyy HH:mm}";
                SeatInfoText.Text = $"Ряд {_row}, Место {_seat}";
                PriceText.Text = $"К оплате: {session.Price:N0} руб.";
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            using (CinemaaDBEntities db = new CinemaaDBEntities())
            {
                // Сохраняем билет в БД
                db.Ticket.Add(new Ticket
                {
                    SessionId = _sessionId,
                    UserId = UserSession.CurrentUser.Id,
                    RowNumber = _row,
                    SeatNumber = _seat
                });
                db.SaveChanges();
            }
            MessageBox.Show("Билет успешно куплен!");
            NavigationService.Navigate(new PageMain());
        }
    }
}