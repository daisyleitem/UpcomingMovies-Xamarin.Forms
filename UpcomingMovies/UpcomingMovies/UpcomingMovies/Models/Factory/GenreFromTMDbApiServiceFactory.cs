using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UpcomingMovies.Models.Factory
{
    internal class GenreFromTMDbApiServiceFactory
    {
        private static Genre BuildGenre(JToken Json)
        {
            Genre GenreReturn = new Genre();
            try
            {
                if (Json.HasValues)
                {
                    GenreReturn.Id = Convert.ToInt32(Json["id"]);
                    GenreReturn.Name = Json["name"].ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {0}", ex.Message);
            }

            return GenreReturn;
        }

        internal static List<Genre> BuildListGenre(JObject Json)
        {
            List<Genre> GenresReturn = new List<Genre>();

            try
            {
                var jsonGenres = Json["genres"];

                if (jsonGenres.HasValues)
                {
                    foreach (JToken item in jsonGenres)
                    {
                        Genre NewGenre = BuildGenre(item);
                        GenresReturn.Add(NewGenre);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {0}", ex.Message);
            }

            return GenresReturn;
        }
    }
}
