using Microsoft.AspNetCore.Mvc;
using NotBlocket2.Models;

namespace NotBlocket2.Controllers {
    public class NotBlocketController : Controller {

		[HttpGet]
		public IActionResult Start() {
            return View();
        }

        [HttpGet]
        public IActionResult SearchResults(string search, string sort) {
           
            //Dealing with the null-values
            if (sort == null) { sort = "Name";  }
            if (search == null) {search = "volvo"; }


            ViewBag.SearchTerm = search;
            ViewBag.Sort = sort;

            //Get the Ad list
            List<Ad> Adlist = new List<Ad>();
            AdMethods pm = new AdMethods();
            string error = "";
            Adlist = pm.GetAdsWithDataSet2(sort, search, out error);
            ViewBag.error = error;
            return View(Adlist);
        }

        [HttpGet]
        public IActionResult FailedToCreateAccount() {
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccount() {
            return View();
        }

        [HttpPost]
		public IActionResult CreateAccount(Profile p) {
            //Profile p = new Profile();
            ProfileMethods pm = new ProfileMethods();
            int i = 0;
            string error = "";

            i = pm.InsertProfile(p, out error);
            ViewBag.error = error;
            ViewBag.antal = i;

            //if i = 1 success, else failed to add profile
            // TODO change this so you get login view after loging in instead of db view
            if (i == 1) { return RedirectToAction("GetPersonWithDataSet"); }
            else { return View("FailedToCreateAccount"); }
        }

        [HttpGet]
        public ActionResult GetPersonWithDataSet() {
            List<Profile> Profilelist = new List<Profile>();
            ProfileMethods pm = new ProfileMethods();
            string error = "";
            Profilelist = pm.GetPersonWithDataSet(out error);
            ViewBag.error = error;
            return View(Profilelist);
        }

        public IActionResult Login() {
            return View();
        }
        
        [HttpGet]
        public IActionResult LoginPage() {
            return View();
        }

        [HttpGet]
        public IActionResult Filtering() {
            ProfileMethods pm = new ProfileMethods();
            LocationMethods lm = new LocationMethods();
            AdMethods am = new AdMethods();

            ViewModelProfileAdsLocation myModel = new ViewModelProfileAdsLocation
            {
                ProfileList = pm.GetPersonWithDataSet(out string errormsg),
                LocationList = lm.GetLocationsWithDataSet(out string errormsg2),
                AdList = am.GetAdsWithDataSet(out string errormsg3)
            
            };
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;

            return View(myModel);
        }

    }
}
