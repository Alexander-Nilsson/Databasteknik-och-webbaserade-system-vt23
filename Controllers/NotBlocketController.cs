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

            ViewBag.search = search;
            ViewBag.sort = sort;

            //Get the Ad list
            List<Ad> Adlist = new List<Ad>();
            AdMethods pm = new AdMethods();
            string error = "";
            Adlist = pm.GetAdsWithDataSet2(sort, search, out error);
            ViewBag.error = error;
            return View(Adlist);
        }

        // Functions related to creating accounts
     
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

        //Display login view
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Profile std) {
            if (ModelState.IsValid) { //checking model state

                //update student to db
                ViewBag.error = "works";
                return RedirectToAction("Start");
            }
            ViewBag.error = "someError";
            return View(std);
            
        }


        
        [HttpGet]
        public IActionResult LoginPage() {
            return View();
        }



		
		public IActionResult Filtering() {
            ProfileMethods pm = new ProfileMethods();
            LocationMethods lm = new LocationMethods();
            AdMethods am = new AdMethods();


			string selectedValue = null;
		    if (Request.Method == "POST") {
				selectedValue = Request.Form["Category"];
			}
			ViewBag.location = selectedValue;
            
			ViewModelProfileAdsLocation myModel = new ViewModelProfileAdsLocation
            {
                ProfileList = pm.GetPersonWithDataSet(out string errormsg),
                LocationList = lm.GetLocationsWithDataSet(out string errormsg2),

                //ad list for the categories dropdown
                AdList = am.GetAdsWithDataSet(out string errormsg3),

                //seperate ad list for the view
				FilterdAdList = am.GetAdsWithDataSet(out string errormsg4, selectedValue)

			};
            ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3 + "4: " + errormsg4;


			return View(myModel);
        }

        /*
		[HttpPost]
		public IActionResult Filtering(Location loc) {
			ProfileMethods pm = new ProfileMethods();
			LocationMethods lm = new LocationMethods();
			AdMethods am = new AdMethods();

			ViewModelProfileAdsLocation myModel = new ViewModelProfileAdsLocation {
				ProfileList = pm.GetPersonWithDataSet(out string errormsg),
				LocationList = lm.GetLocationsWithDataSet(out string errormsg2),
				AdList = am.GetAdsWithDataSet(out string errormsg3)

			};
			ViewBag.error = "1: " + errormsg + "2: " + errormsg2 + "3: " + errormsg3;
            
			string selectedValue = Request.Form["Location"];
			ViewBag.location = selectedValue;


			return View(myModel);
		}
        */



		/*
        public ActionResult Create(Image img, IFormFile file) {
            if (ModelState.IsValid) {
                if (file != null) {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                          + file.FileName);
                    img.ImagePath = file.FileName;
                }
                db.Image.Add(img);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(img);
        }
        */





	}
}
