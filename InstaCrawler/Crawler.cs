using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace InstaCrawler
{
    class Crawler
    {
        private const string AccessToken = "1657958732.e029fea.35545fe0d1a9449cafdaf7a197f0ab25";

        public List<InstaPhoto> Start(string tag, int count)
        {
            var url = BuildUrl(tag, AccessToken);
            var photos = new List<InstaPhoto>();
            var totalPhoto = 0;
            var totalWithLoc = 0;

            using (var client = new WebClient())
            {
                var finished = false;
                while (!finished)
                {
                    var jObj = JObject.Parse(client.DownloadString(url));
                    var nextUrl = jObj["pagination"]["next_url"].ToString();

                    foreach (var data in jObj["data"])
                    {
                        var loc = data["location"].ToObject<Location>(); //ищем геометку
                        totalPhoto++;
                        if (loc == null)
                        {
                            TotalPhotoProcessedChanged?.Invoke(totalPhoto,totalWithLoc);
                            continue; // метки нет, пропускаем фото
                        }

                        var photo = data["images"].ToObject<InstaPhoto>(); //получаем ссылки на фото
                        photo.Location = loc;
                        photos.Add(photo);

                        totalWithLoc++;
                        TotalPhotoProcessedChanged?.Invoke(totalPhoto, totalWithLoc);

                        if (photos.Count >= count)
                        {
                            finished = true;
                            break;
                        }
                    }

                    url = nextUrl;
                }
            }

            return photos;
        }

        public event Action<int,int> TotalPhotoProcessedChanged;

        private string BuildUrl(string tag, string token, int count = 33)
        {
            return $"https://api.instagram.com/v1/tags/{tag}/media/recent?access_token={token}&count={count}";
        }
    }
}
