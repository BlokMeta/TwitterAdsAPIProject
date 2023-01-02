using Newtonsoft.Json;
using System.Net.Http.Headers;
using TwitterAdsAPIProject.Models;

namespace TwitterAdsAPIProject.Services
{
    public class TwitterAdsService : ITwitterAdsService
    {
        private readonly string _bearerToken= "AAAAAAAAAAAAAAAAAAAAABRokwEAAAAAbF56KsqxkypAeQTBoa5KTHX30jE%3Dy4yDI8QiioYVM8eQvvZoGuRPrd6ZT9njGK427afGcJ2a4m6dNn";
        private readonly string _accountId= "291676449";

        public TwitterAdsService(string bearerToken, string accountId)
        {
            _bearerToken = "AAAAAAAAAAAAAAAAAAAAABRokwEAAAAAbF56KsqxkypAeQTBoa5KTHX30jE%3Dy4yDI8QiioYVM8eQvvZoGuRPrd6ZT9njGK427afGcJ2a4m6dNn";
            _accountId = "291676449";


        }

        public async Task<List<LineItem>> GetLineItems()
        {
            // API endpoint URL
            string apiUrl = $"https://api.twitter.com/2/users/{_accountId}/followers";

            // HTTP istek oluştur
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            // HTTP cevabının JSON içeriğini oku
            string responseJson = await response.Content.ReadAsStringAsync();

            // JSON verisini deserialize et
            dynamic responseData = JsonConvert.DeserializeObject(responseJson);

            // Metrik verilerini al
            string lineItems = responseData.data;

            // Line item verilerini listeye dönüştür
            List<LineItem> lineItemList = JsonConvert.DeserializeObject<List<LineItem>>(lineItems);

            return lineItemList;
        }
    }

}
