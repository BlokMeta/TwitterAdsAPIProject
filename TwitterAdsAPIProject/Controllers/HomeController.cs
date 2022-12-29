using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using TwitterAdsAPIProject.Models;

namespace TwitterAdsAPIProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Aşağıdaki Action methodu, bir sayfada Twitter Ads API tarafından dönen verileri gösterir
        // (henüz sadece Twitter API için, ad bilgilerimiz mevcut değil.
        public async Task<IActionResult> Index()
        {
            // API endpoint'ine gönderilecek URL
            string apiUrl = "https://api.twitter.com/2/users/291676449/followers";

            // HTTP isteği oluşturuyoruz
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAABRokwEAAAAAbF56KsqxkypAeQTBoa5KTHX30jE%3Dy4yDI8QiioYVM8eQvvZoGuRPrd6ZT9njGK427afGcJ2a4m6dNn");

            // API'ye istek gönderiyoruz
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            // Cevabı alıyoruz
            string responseJson = await response.Content.ReadAsStringAsync();

            // Cevabı çözümlüyoruz
            dynamic responseData = JsonConvert.DeserializeObject(responseJson);

            // lineItem verilerini alıyoruz
            dynamic lineItems = responseData.data;

            // lineItem verilerini modelde depolama
            List<LineItem> lineItemList = new List<LineItem>();
            foreach (var item in lineItems)
            {
                LineItem lineItem = new LineItem
                {
                    Id = item.id,
                    Name = item.name,
                    Username = item.username
                };
                lineItemList.Add(lineItem);
            }

            // lineItemList verilerini görüntüleme
            return View(lineItemList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}