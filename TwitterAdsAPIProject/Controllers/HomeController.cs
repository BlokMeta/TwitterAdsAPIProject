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
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        // Aşağıdaki Action methodu, bir sayfada Twitter Ads API tarafından dönen verileri gösterir
        // (henüz sadece Twitter API için, ad bilgilerimiz mevcut değil.
        public async Task<IActionResult> Index()
        {

            
            // API endpoint'ine gönderilecek URL
            //string apiUrl = "https://api.twitter.com/2/users/291676449/followers";

            string apiUrl = _config.GetSection("TwitterAPIConnections:apiUrl").Value;
            string bearerKey = _config.GetSection("TwitterAPIConnections:bearerKey").Value;


            // HTTP isteği oluşturuyoruz
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", bearerKey);

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