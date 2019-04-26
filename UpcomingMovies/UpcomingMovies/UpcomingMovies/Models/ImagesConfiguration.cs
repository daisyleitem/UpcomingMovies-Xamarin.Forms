namespace UpcomingMovies.Models
{
    public class ImagesConfiguration
    {
        public string BaseUrl { get; set; }

        public string[] BackdropSizes { get; internal set; }
        public string[] PosterSizes { get; internal set; }
        public string[] LogoSizes { get; internal set; }
    }
}

