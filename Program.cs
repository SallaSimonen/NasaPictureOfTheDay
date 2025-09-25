using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;
using Newtonsoft.Json;

namespace NasaPictureOfTheDay;

class Program
{
    static async Task Main(string[] args)
    {
        PictureOfTheDay pictureOfTheDay = new PictureOfTheDay();

        Console.Clear();
        Console.WriteLine("NASA Picture of the Day Downloader");
        Thread.Sleep(2000);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1 Download today's picture\n2 Download yesterday's picture\n3 Download picture from day of your choice\n4 Download any random picture\n5 Exit");
            string? choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    await pictureOfTheDay.DownloadNasaPictureOfTheDayAsync(DateTime.Now.ToString("yyyy-MM-dd"));
                    break;
                case "2":
                    await pictureOfTheDay.DownloadNasaPictureOfTheDayAsync(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
                    break;
                case "3":
                    Console.Write("Enter a date (YYYY-MM-DD):");
                    string? date = Console.ReadLine();
                    await pictureOfTheDay.DownloadNasaPictureOfTheDayAsync(date);
                    break;
                case "4":

                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
