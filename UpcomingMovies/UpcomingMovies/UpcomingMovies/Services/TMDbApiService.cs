using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using UpcomingMovies.Models.Factory;

namespace UpcomingMovies.Services
{
    public class TMDbApiService : ITMDbApiService
    {
        private readonly string _apiKey = "1f54bd990f1cdfb230adb312546d765d";
        private readonly string _urlApiBase = "https://api.themoviedb.org/3/";

        private readonly string _language = CultureInfo.CurrentCulture.Name; // use local language instead, not work
        private HttpClient _client;


        public TMDbApiService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        public async Task<JObject> GetImagesConfigurationAsync()
        {
            string UrlApi = $"{_urlApiBase}configuration?api_key={_apiKey}";

            JObject jObjectReturn = new JObject();
            try
            {
                var response = await _client.GetAsync(UrlApi).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    jObjectReturn = JObject.Parse(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {0}", ex.Message);
            }

            return jObjectReturn; 
        }

        public async Task<JObject> GetGenreAsync()
        {
            string UrlApi = $"{_urlApiBase}genre/movie/list?api_key={_apiKey}&language={_language}";

            JObject jObjectReturn = new JObject();

            try
            {
                var response = await _client.GetAsync(UrlApi).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    jObjectReturn = JObject.Parse(content);                  
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {0}", ex.Message);
            }

            return jObjectReturn;
        }

        public async Task<JObject> GetUpcomingMoviesAsync(int Page)
        {
            string UrlApi = $"{_urlApiBase}movie/upcoming?api_key={_apiKey}&language={_language}&page={Page.ToString()}";
            JObject jObjectReturn = new JObject();

            try
            {
                var response = await _client.GetAsync(UrlApi).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    jObjectReturn = JObject.Parse(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {0}", ex.Message);
            }

            return jObjectReturn;
        }

    }
}