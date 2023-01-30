using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotBlocket2.Models;

namespace NotBlocket2.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index() { return View(); }



         [HttpPost]
         public IActionResult SearchResults() {
             // Perform the search and retrieve the results
             //var results = PerformSearch(searchTerm);

             //ViewBag.Search = searchTerm;

             // Pass the results to the view
             return View();
         }
        


         /*[HttpGet]
         public IActionResult Index(IFormCollection Search) {

             //Save serch to session variable
             string s = JsonConvert.SerializeObject(Search);
             HttpContext.Session.SetString("SearchSession", s);

             // Send search to the SearchList View
             return View(Search);

         }
         */

        [HttpGet]
        public IActionResult SearchList() {

            return View();
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}