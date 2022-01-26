using System;
using System.Net.Http;

namespace CallApiAutorest
{
    class Program
    {
        private const string baseUrl = "https://petstore.swagger.io/v2";

        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            //NSwag
            var petClient = new PetStoreClient.PetStoreClient(httpClient);
            var pet = petClient.GetPetByIdAsync(1).Result;

            Console.WriteLine($"{pet.Id} {pet.Name} {pet.Status} {pet.Category.Name}");
        }
    }
}
