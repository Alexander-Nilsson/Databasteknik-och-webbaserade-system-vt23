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

        [HttpPost]
		public IActionResult CreateAccount() {
            Profile pm = new Profile();
            int i = 0;
            string error = "";

            i = pm.InsertPerson(pm, out error);
            ViewBag.error = error;
            ViewBag.antal = i;

            //if i = 1 success, else failed to add profile
            if (i == 1) { return RedirectToAction("SelectWithDataSet"); }
            else { return View("InsertPerson"); }
        }

        [HttpGet]
        public ActionResult SelectWithDataReader() {
            List<Profile> Personlist = new List<Profile>();
            Profile pm = new Profile();
            string error = "";
            Personlist = pm.GetProfileWithDataReader(out error);
            ViewBag.error = error;
            return View(Personlist);
        }



        public IActionResult Login() {
            return View();
        }


    }
}
