using SampleAPI;
using System;
using System.Net.Http;

namespace CallApiAutorest
{
    class Program
    {
        private const string baseUrl = "http://localhost:21951";
     
        static void Main(string[] args)
        {
            Console.WriteLine("Autorest");
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            var weatherForcast = new WheatherForcast(httpClient, true);
            var data = weatherForcast.WeatherForecast.GetWithHttpMessagesAsync().Result;
            foreach (var message in data.Body)
            {
                Console.WriteLine($"{message.Summary} {message.TemperatureC} {message.Date}");
            }
            Console.WriteLine();
            Console.WriteLine("NSwag!");
            //NSwag
            var nswag = new MyServiceClient(httpClient);
            var response = nswag.GetAsync().Result;
            foreach (var message in response)
            {
                Console.WriteLine($"{message.Summary} {message.TemperatureC} {message.Date}");
            }
        }
    }
}
