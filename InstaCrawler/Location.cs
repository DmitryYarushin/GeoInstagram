namespace InstaCrawler
{
    public class Location
    {
        public string Latitude;
        public string Longitude;
        public string Name;

        public Location(string latitude, string longitude, string name)
        {
            Latitude = latitude;
            Longitude = longitude;
            Name = name;
        }

        public Location()
        {
        }
    }
}