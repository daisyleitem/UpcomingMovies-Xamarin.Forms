using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.BO;
using UpcomingMovies.Models;
using UpcomingMovies.Services;
using UpcomingMovies.Views;
using Xamarin.Forms;

namespace UpcomingMovies.ViewModels
{
    public class UpcomingMoviesViewModel : BaseViewModel
    {
        internal ObservableCollection<Movie> MoviesList;
        internal int PageCount;


        public UpcomingMoviesViewModel()
        {
            MoviesList = new ObservableCollection<Movie>();
            PageCount = 1;

            GetMoreUpcomingMovies = new Command(ExecuteGetMoreUpcomingMovies);
        }


        #region Button

        public Command GetMoreUpcomingMovies { get; }

        private async void ExecuteGetMoreUpcomingMovies(object obj)
        {
            List<Movie> NewMoviesReturn = new List<Movie>();

            ConsumeTMDbApiServiceBO bo = new ConsumeTMDbApiServiceBO();
            NewMoviesReturn = await bo.GetUpcomingMoviesAsync(PageCount);

            foreach (Movie item in NewMoviesReturn)
            {
                if (MoviesList == null)
                    MoviesList = new ObservableCollection<Movie>();

                MoviesList.Add(item);
            }

            PageCount++;
        }

        #endregion


        #region Movies

        //private int PageCount;

        

        //private async Task<List<Movie>> GetMovieListFromService(int GetPage)
        //{
        //    List<Movie> MoviesReturn = new List<Movie>();

        //    //MoviesReturn = await _service.GetUpcomingMoviesAsync(GetPage);

        //    foreach (Movie item in MoviesReturn)
        //    {
        //        if (MoviesList == null)
        //            MoviesList = new ObservableCollection<Movie>();
        //        MoviesList.Add(item);
        //    }

        //    return MoviesReturn;
        //}

        //public async void GetMoreMovies(int GetPage)
        //{
        //    List<Movie> MoviesToAdd = new List<Movie>();
        //    MoviesToAdd = await GetMovieListFromService(GetPage);

        //    foreach (Movie item in MoviesToAdd)
        //    {
        //        MoviesList.Add(item);
        //    }
        //}


        #endregion


        

        





































        //private readonly TMDbApiService _service;

        //#region ImagesConfiguration
        //private ImagesConfiguration _imagesConfiguration;

        //private ImagesConfiguration ImagesConfiguration
        //{
        //    get
        //    {
        //        if (_imagesConfiguration == null)
        //            GetImagesConfiguration();

        //        return _imagesConfiguration;
        //    }
        //}

        //private async void GetImagesConfiguration()
        //{
        //    _imagesConfiguration = new ImagesConfiguration();
        //    //_imagesConfiguration = await _service.GetImagesConfigurationAsync();
        //}
        //#endregion

        //public UpcomingMoviesViewModel()
        //{
        //    _service = new TMDbApiService();
        //    MoviesList = new ObservableCollection<Movie>();
        //    PageCount = 1;


        //    TesteCommand = new Command(ExecuteTesteCommand);

        //}


        //#region Movies

        //private int PageCount;

        //internal ObservableCollection<Movie> MoviesList;

        //private async Task<List<Movie>> GetMovieListFromService(int GetPage)
        //{
        //    List<Movie> MoviesReturn = new List<Movie>();

        //    //MoviesReturn = await _service.GetUpcomingMoviesAsync(GetPage);

        //    foreach (Movie item in MoviesReturn)
        //    {
        //        if (MoviesList == null)
        //            MoviesList = new ObservableCollection<Movie>();
        //        MoviesList.Add(item);
        //    }

        //    return MoviesReturn;
        //}

        //public async void GetMoreMovies(int GetPage)
        //{
        //    List<Movie> MoviesToAdd = new List<Movie>();
        //    MoviesToAdd = await GetMovieListFromService(GetPage);

        //    foreach (Movie item in MoviesToAdd)
        //    {
        //        MoviesList.Add(item);
        //    }
        //}


        //#endregion


        //#region Botao

        //public Command TesteCommand { get; }

        //private async void ExecuteTesteCommand(object obj)
        //{
        //    List<Movie> MoviesReturn = new List<Movie>();

        //    //MoviesReturn = await _service.GetUpcomingMoviesAsync(1);

        //    foreach (Movie item in MoviesReturn)
        //    {
        //        if (MoviesList == null)
        //            MoviesList = new ObservableCollection<Movie>();
        //        MoviesList.Add(item);
        //    }

        //    Filme = ((Movie)MoviesList[0]);
        //}

        //#endregion

        //#region Label
        //private Movie _filme;
        //public Movie Filme
        //{
        //    get { return _filme; }
        //    set
        //    {
        //        SetProperty(ref _filme, value);
        //    }
        //}

        //#endregion
    }
}
