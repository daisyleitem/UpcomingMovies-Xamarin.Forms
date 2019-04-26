using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using UpcomingMovies.Models.Factory;
using UpcomingMovies.Services;

namespace UpcomingMovies.BO
{
    internal class ConsumeTMDbApiServiceBO
    {
        #region TMDbApiService
        private TMDbApiService _service;
        public TMDbApiService Service
        {
            get
            {
                if (_service == null)
                    _service = new TMDbApiService();

                return _service;
            }
        }
        #endregion


        #region Cache ImagesConfiguration
        private ImagesConfiguration _imagesConfigurationCache;
        private ImagesConfiguration ImagesConfigurationCache
        {
            get
            {
                if (_imagesConfigurationCache == null)
                    SetImagesConfigurationCash();

                return _imagesConfigurationCache;
            }
        }

        private void SetImagesConfigurationCash()
        {
            ImagesConfiguration returImagesConfiguration = new ImagesConfiguration();
            try
            {
                Task.Run(async () =>
                                {
                                    returImagesConfiguration =
                                    ImagesConfigurationFromTMDbApiServiceFactory.BuildImageConfiguration(await Service.GetImagesConfigurationAsync());

                                }).Wait();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {0}", ex.Message);
            }

            if (returImagesConfiguration != null)
                _imagesConfigurationCache = returImagesConfiguration;
            else
                _imagesConfigurationCache = new ImagesConfiguration();
        }

        #endregion

        #region Cache List of Genres
        private List<Genre> _genresCache;
        private List<Genre> GenresCache
        {
            get
            {
                if (_genresCache == null)
                    SetGenresCache();

                return _genresCache;
            }
        }

        private void SetGenresCache()
        {
            List<Genre> returGenreList = new List<Genre>();
            try
            {
                Task.Run(async () =>
                                { returGenreList = GenreFromTMDbApiServiceFactory.BuildListGenre(await Service.GetGenreAsync()); }
                                ).Wait();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {0}", ex.Message);
            }

            if (returGenreList != null)
                _genresCache = returGenreList;
            else
                _genresCache = new List<Genre>();
        }
        #endregion
        
        public async Task<List<Movie>> GetUpcomingMoviesAsync(int Page)
        {
            List<Movie> returMovieList = new List<Movie>();
            try
            {
                returMovieList = BuildListMovie(await Service.GetUpcomingMoviesAsync(Page));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {0}", ex.Message);
            }

            return returMovieList;
        }

        #region Construct Movie
        private Movie BuildMovie(JToken Json)
        {
            Movie MovieReturn = new Movie();
            try
            {
                if (Json.HasValues)
                {
                    MovieReturn.Id = Convert.ToInt32(Json["id"]);
                    MovieReturn.Title = Json["title"].ToString();
                    MovieReturn.Poster_path = Json["poster_path"].ToString();

                    MovieReturn.Backdrop_path = Json["backdrop_path"].ToString();
                    MovieReturn.Overview = Json["overview"].ToString();
                    MovieReturn.Release_date = Json["release_date"].ToString();

                    #region Genre
                    #region Genre_ids
                    int genreCount = Json["genre_ids"].Count();
                    List<int> genres = new List<int>();
                    if (genreCount > 0)
                    {
                        for (int i = 0; i < genreCount; i++)
                        {
                            genres.Add(Convert.ToInt32(Json["genre_ids"][i]));
                        }

                        MovieReturn.Genre_ids= genres;
                    }

                    MovieReturn.Genre_ids = genres;
                    #endregion

                    #region Genres
                    MovieReturn.Genres = GetGenresByListID(MovieReturn.Genre_ids);
                    #endregion
                    #endregion

                    MovieReturn.Poster_path_Img= $"{ImagesConfigurationCache.BaseUrl}{ImagesConfigurationCache.PosterSizes[0]}/{MovieReturn.Poster_path}";

                    MovieReturn.Backdrop_path_Img= $"{ImagesConfigurationCache.BaseUrl}{ImagesConfigurationCache.LogoSizes[0]}/{MovieReturn.Backdrop_path}";

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {0}", ex.Message);
            }

            return MovieReturn;
        }

        private List<Movie> BuildListMovie(JObject Json)
        {
            List<Movie> MoviesReturn = new List<Movie>();

            try
            {
                var jsonMovies = Json["results"];

                if (jsonMovies.HasValues)
                {
                    foreach (JToken item in jsonMovies)
                    {
                        Movie NewMovie = BuildMovie(item);
                        MoviesReturn.Add(NewMovie);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {0}", ex.Message);
            }

            return MoviesReturn;
        }

        private List<Genre> GetGenresByListID(List<int> Genre_ids)
        {
            var GenresFind = from cache in GenresCache
                             join param in Genre_ids on cache.Id equals param
                             select cache;

            return GenresFind.ToList();// .ToList();
        }
        #endregion
    }

}
