using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UpcomingMovies.Models;

namespace UpcomingMovies.Services
{
    public interface ITMDbApiService
    {
        Task<JObject> GetUpcomingMoviesAsync(int Page);

        Task<JObject> GetGenreAsync();

        Task<JObject> GetImagesConfigurationAsync();
    }
}
