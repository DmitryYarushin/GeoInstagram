using Newtonsoft.Json;

namespace InstaCrawler
{
    public class InstaPhoto
    {
        [JsonProperty(PropertyName = "low_resolution")]
        public ImageInfo LowResolution;
        [JsonProperty(PropertyName = "standard_resolution")]
        public ImageInfo StandardResolution;
        public ImageInfo Thumbnail;

        public Location Location;
    }

    public class ImageInfo
    {
        public int Width;
        public int Height;
        public string Url;
    }
}