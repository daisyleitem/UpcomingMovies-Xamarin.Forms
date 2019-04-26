using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.BO;
using UpcomingMovies.Models;
using UpcomingMovies.Services;
using UpcomingMovies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UpcomingMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpcomingMoviesView : ContentPage
    {
        private UpcomingMoviesViewModel ViewModel => BindingContext as UpcomingMoviesViewModel;

        public UpcomingMoviesView()
        {
            InitializeComponent();
            BindingContext = new UpcomingMoviesViewModel();

            Task.Run(async () => { await GetUpcomingMovies(1); }).Wait();
            lvMovies.ItemsSource = MoviesList;

            if (MoviesList != null)
            {
                foreach (Movie item in MoviesList)
                {
                    if (MoviesList == null)
                        MoviesList = new List<Movie>();
                    ViewModel.MoviesList.Add(item);
                }
            }
        }
        private List<Movie> MoviesList;
        private async Task<List<Movie>> GetUpcomingMovies(int GetPage)
        {
            ConsumeTMDbApiServiceBO bo = new ConsumeTMDbApiServiceBO();
            List<Movie> MoviesReturn = new List<Movie>();

            MoviesReturn = await bo.GetUpcomingMoviesAsync(GetPage);

            if (MoviesList == null)
                MoviesList = new List<Movie>();

            MoviesList.AddRange(MoviesReturn);

            return MoviesReturn;
        }

















        //private UpcomingMoviesViewModel ViewModel => BindingContext as UpcomingMoviesViewModel;

        //public UpcomingMoviesView()
        //{
        //    InitializeComponent();
        //    BindingContext = new UpcomingMoviesViewModel();

        //    MoviesList = new List<Movie>();
        //    Task.Run(async () => { await GetMovieListFromService(1); }).Wait();
        //    lv2.ItemsSource = MoviesList;

        //    if (MoviesList != null)
        //    {
        //        foreach (Movie item in MoviesList)
        //        {
        //            if (MoviesList == null)
        //                MoviesList = new List<Movie>();
        //            ViewModel.MoviesList.Add(item);
        //        }
        //    }
        //}
        //private List<Movie> MoviesList;

        //private async Task<List<Movie>> GetMovieListFromService(int GetPage)
        //{
        //    TMDbApiService _service = new TMDbApiService();
        //    List<Movie> MoviesReturn = new List<Movie>();


        //    //MoviesReturn = await _service.GetUpcomingMoviesAsync(GetPage);

        //    foreach (Movie item in MoviesReturn)
        //    {
        //        if (MoviesList == null)
        //            MoviesList = new List<Movie>();
        //        MoviesList.Add(item);
        //    }

        //    return MoviesReturn;
        //}
    }
}