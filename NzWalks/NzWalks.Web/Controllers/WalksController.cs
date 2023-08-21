using Microsoft.AspNetCore.Mvc;

namespace NzWalks.Web.Controllers
{
    public class WalksController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public WalksController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7111/api/Walks");
                httpResponseMessage.EnsureSuccessStatusCode();
                string body = await httpResponseMessage.Content.ReadAsStringAsync();
                ViewBag.Body = body;
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }
    }
}
