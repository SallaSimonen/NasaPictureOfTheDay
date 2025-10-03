using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json;

namespace NasaPictureOfTheDay;

public class PictureOfTheDay
{
    public async Task DownloadNasaPictureOfTheDayAsync(string date)
    {
        using (var client = new HttpClient())
        {
            // Käytetty DEMO_KEY:tä!
            // Ohjelma ei ehkä toimi oikein, koska pyyntöjen määrä on rajoitettu!
            var response = await client.GetAsync("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date=" + date);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Something went wrong! 1");
                Thread.Sleep(1000);
                return;
            }

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<NasaApodResponse>(json);
            Console.WriteLine(data.hdurl);

            if (data == null || data.hdurl == null)
            {
                Console.WriteLine("Something went wrong! 2");
                Thread.Sleep(1000);
                return;
            }

            var imageResponse = await client.GetAsync(data.hdurl);
            if (!imageResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Something went wrong! 3");
                Thread.Sleep(1000);
                return;
            }

            var image = await imageResponse.Content.ReadAsByteArrayAsync();

            string path = MakeDirectories(date);
            string filePath = Path.Combine(path, date + ".jpg");

            await File.WriteAllBytesAsync(filePath, image);
        }
        Console.WriteLine("Picture downloaded successfully!");
        Thread.Sleep(1000);
    }

    static string MakeDirectories(string date)
    {
        string[] dateParts = date.Split('-');

        var currentDirectory = Directory.GetCurrentDirectory();
        var firstPath = Path.Combine(currentDirectory, dateParts[0]);
        if (!Directory.Exists(firstPath))
        {
            Directory.CreateDirectory(firstPath);
        }

        var secondPath = Path.Combine(firstPath, dateParts[1]);
        if (!Directory.Exists(secondPath))
        {
            Directory.CreateDirectory(secondPath);
        }

        return secondPath;
    }
}
