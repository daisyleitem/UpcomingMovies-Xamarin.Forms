using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace UpcomingMovies.Models.Factory
{
    internal class ImagesConfigurationFromTMDbApiServiceFactory
    {

        internal static ImagesConfiguration BuildImageConfiguration(JToken Json)
        {
            ImagesConfiguration ImageConfigurationReturn = new ImagesConfiguration();
            try
            {
                if (Json.HasValues)
                {
                    ImageConfigurationReturn.BaseUrl = Json["images"]["base_url"].ToString();

                    #region BackdropSizes

                    int backdrop_sizesCount = Json["images"]["backdrop_sizes"][0].Parent.Count;
                    string[] backdrop_sizes = new string[backdrop_sizesCount];
                    if (backdrop_sizesCount > 0)
                    {
                        for (int i = 0; i < backdrop_sizesCount; i++)
                        {
                            backdrop_sizes[i] = Json["images"]["backdrop_sizes"][i].ToString();
                        }
                    }

                    ImageConfigurationReturn.BackdropSizes = backdrop_sizes;

                    #endregion

                    #region PosterSizes

                    int poster_sizesCount = Json["images"]["poster_sizes"][0].Parent.Count;
                    string[] poster_sizes = new string[poster_sizesCount];
                    if (poster_sizesCount > 0)
                    {
                        for (int i = 0; i < poster_sizesCount; i++)
                        {
                            poster_sizes[i] = Json["images"]["poster_sizes"][i].ToString();
                        }
                    }

                    ImageConfigurationReturn.PosterSizes = backdrop_sizes;

                    #endregion

                    #region logo_sizes

                    int logo_sizesCount = Json["images"]["still_sizes"][0].Parent.Count;
                    string[] logo_sizes = new string[logo_sizesCount];
                    if (logo_sizesCount > 0)
                    {
                        for (int i = 0; i < logo_sizesCount; i++)
                        {
                            logo_sizes[i] = Json["images"]["still_sizes"][i].ToString();
                        }
                    }

                    ImageConfigurationReturn.LogoSizes = logo_sizes;

                    #endregion

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {0}", ex.Message);
            }

            return ImageConfigurationReturn;
        }

    }
}
