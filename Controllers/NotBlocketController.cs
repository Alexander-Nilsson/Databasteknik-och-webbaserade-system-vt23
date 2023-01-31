using Microsoft.AspNetCore.Mvc;
using NotBlocket2.Models;

namespace NotBlocket2.Controllers {
    public class NotBlocketController : Controller {

		[HttpGet]
		public IActionResult Start() {
            return View();
        }

        [HttpGet]
        public IActionResult SearchResults(string searchTerm) {
            ViewBag.SearchTerm = searchTerm;
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccount() {
            return View();
        }

        public IActionResult FailedToCreateAccount() {
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


    }
}
