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
    public partial class PageSession : Page
    {
        private int _sessionId;
        private List<SeatModel> _seats = new List<SeatModel>();
        private SeatModel _selectedSeat = null;

        public PageSession(int sessionId) {
            InitializeComponent();
            _sessionId = sessionId; 
            LoadSessionData(); }

        private void LoadSessionData()
        {
            using (CinemaaDBEntities db = new CinemaaDBEntities())
            {
                var session = db.Session.FirstOrDefault(s => s.Id == _sessionId);
                if (session != null) SessionInfoText.Text = $"{session.Movie.Title} | {session.Hall.Name} | {session.StartTime:dd.MM.yyyy HH:mm}";

              
                var booked = db.Ticket.Where(t => t.SessionId == _sessionId).ToList();

               
                for (int r = 1; r <= 5; r++)
                {
                    for (int s = 1; s <= 8; s++)
                    {
                        _seats.Add(new SeatModel { Row = r, SeatNumber = s, IsBooked = booked.Any(t => t.RowNumber == r && t.SeatNumber == s) });
                    }
                }
                SeatsGrid.ItemsSource = _seats;
            }
        }

        private void Seat_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var seat = btn.Tag as SeatModel;

            if (seat.IsBooked) { MessageBox.Show("Место занято!"); return; }

            if (_selectedSeat != null) _selectedSeat.IsSelected = false; 
            _selectedSeat = seat;
            _selectedSeat.IsSelected = true; 

            
            SeatsGrid.ItemsSource = null;
            SeatsGrid.ItemsSource = _seats;
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSeat == null) { MessageBox.Show("Выберите место!"); return; }
            NavigationService.Navigate(new PageTicket(_sessionId, _selectedSeat.Row, _selectedSeat.SeatNumber));
        }
    }
}