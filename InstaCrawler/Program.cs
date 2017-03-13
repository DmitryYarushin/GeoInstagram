using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace InstaCrawler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            Console.Write("Tag: #");
            var tag = Console.ReadLine();

            Console.Write("Count: ");
            var count = int.Parse(Console.ReadLine());

            var crawler = new Crawler();
            crawler.TotalPhotoProcessedChanged += Crawler_TotalPhotoProcessedChanged;
            
            Console.WriteLine($"Поиск {count} фото с тегом #{tag}");
            var photos = crawler.Start(tag, count);
            var json = JsonConvert.SerializeObject(photos);
            SaveData("Geotager\\photos.json",json);
            Process.Start("Geotager\\index.html");
        }

        private static void Crawler_TotalPhotoProcessedChanged(int total, int withGeo)
        {
            Console.Write($"\rОбработано фото {total}. Геометка найдена: {withGeo}");
        }

        private static void SaveData(string path, string data)
        {
            File.WriteAllText(path,"var data = "+data+";");
        }
    }
}