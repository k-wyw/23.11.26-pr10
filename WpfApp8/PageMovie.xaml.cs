using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApp8
{ /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
    public partial class PageMovie : Page
    {
        private int _movieId;

      
        public PageMovie(int movieId)
        {
            InitializeComponent();
            _movieId = movieId;
            LoadMovie();
        }

        private void LoadMovie()
        {
            using (CinemaaDBEntities db = new CinemaaDBEntities())
            {
                var movie = db.Movie.FirstOrDefault(m => m.Id == _movieId);
                if (movie == null) return;

                TitleText.Text = movie.Title;
                DescText.Text = movie.Description;

                MovieImage.Source = new BitmapImage(new Uri(@"C:\Users\243450\source\repos\WpfApp8\WpfApp8\Images\" + movie.ImagePath));

                SessionsList.ItemsSource = db.Session.Where(s => s.MovieId == _movieId).ToList();
            }
        }

        private void SelectSession_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var session = btn.Tag as Session;

            if (UserSession.CurrentUser == null)
            {
                MessageBox.Show("Войдите в аккаунт!");
                NavigationService.Navigate(new PageLogin());
                return;
            }

            NavigationService.Navigate(new PageSession(session.Id));
        }
    }
}