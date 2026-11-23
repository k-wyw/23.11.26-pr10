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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        public PageMain()
        {
            InitializeComponent(); LoadMovies();
        }

        private void LoadMovies()
        {
            using (CinemaaDBEntities db = new CinemaaDBEntities())
            {
                var movies = db.Movie.ToList();

                if (!string.IsNullOrWhiteSpace(SearchBox.Text))
                    movies = movies.Where(m => m.Title.ToLower().Contains(SearchBox.Text.ToLower())).ToList();

                movies = (SortBox.SelectedIndex == 0) ? movies.OrderBy(m => m.Title).ToList() : movies.OrderByDescending(m => m.Rating).ToList();

             
                foreach (var m in movies)
                    m.ImagePath = @"C:\Users\243450\source\repos\WpfApp8\WpfApp8\Images\" + m.ImagePath;

                MoviesList.ItemsSource = movies;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) { if (IsLoaded) LoadMovies(); }
        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { if (IsLoaded) LoadMovies(); }

        private void Movie_Click(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var movie = border.Tag as Movie;
            if (movie != null) NavigationService.Navigate(new PageMovie(movie.Id));
        }

        private void GoToProfile_Click(object sender, RoutedEventArgs e)
        {
            if (UserSession.CurrentUser == null) { MessageBox.Show("Войдите в аккаунт!"); NavigationService.Navigate(new PageLogin()); }
            else NavigationService.Navigate(new PageProfile());
        }

        private void GoToLogin_Click(object sender, RoutedEventArgs e) { NavigationService.Navigate(new PageLogin()); }
    }
}