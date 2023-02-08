using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NotBlocket2.Models;

namespace NotBlocket2.Controllers {
    public class NotBlocketController : Controller {

		[HttpGet]
		public IActionResult Start() {

            List<string>  locationNames = new List<string>();
            List<int>  ProfileCountPerLocation = new List<int>();
            string errormsg = string.Empty;

            Graphmethods gm = new Graphmethods();

            gm.GetLocationsWithDataSet(out locationNames, out ProfileCountPerLocation, out errormsg);

            ViewBag.LocationNames = locationNames;
            ViewBag.ProfileCountPerLocation = ProfileCountPerLocation;
            return View();
        }

        [HttpGet]
        public IActionResult SearchResults(string search, string sort) {
           
            //Dealing with the null-values
            if (sort == null) { sort = "Name";  }
            if (search != null) { 

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
            return RedirectToAction("Start");
        }



        // Functions related to creating accounts
        [HttpGet]
        public IActionResult CreateAccount() {
            return View();
        }

        [HttpPost]
		public IActionResult CreateAccount(Profile p) {
            ProfileMethods pm = new ProfileMethods();
            int i = 0;
            string error = "";

            if (!ModelState.IsValid) {
                ViewBag.error = "error: " + error;
                ViewBag.antal = i;
                return View(p);
            }

            i = pm.InsertProfile(p, out error);
            ViewBag.error = error;
            ViewBag.antal = i;
            return RedirectToAction("GetPersonWithDataSet");

        }


        [HttpGet]
        public IActionResult Deleteprofile(int id) {
            // Remove the row with the specified ID from the database
            ProfileMethods pm = new ProfileMethods();
            string error = "";
            pm.DeleteProfile(id, out error);
            ViewBag.error = error;

            return RedirectToAction("GetPersonWithDataSet");
        }

		[HttpGet]
		public IActionResult DeleteAd(int id) {
			// Remove the row with the specified ID from the database
			AdMethods am = new AdMethods();
			string error = "";
			am.DeleteAd(id, out error);
			ViewBag.error = error;

			return RedirectToAction("Filtering");
		}

		[HttpGet]
        public IActionResult Editprofile(int id) {
            //Profile p = new Profile();
            string error = "";
            ProfileMethods pm = new ProfileMethods();
            Profile p = pm.GetProfileById(id, out error);
            return View(p);
        }
		[HttpPost]
		public ActionResult Editprofile(Profile pd) {
			if (ModelState.IsValid) {
				// updt databse and go back to list
				ProfileMethods pm = new ProfileMethods();
				string error = "";

				pm.UpdateProfile(pd, out error);
				ViewBag.error = error;
				return RedirectToAction("GetPersonWithDataSet");
				//return View(pd);
			}
			return View(pd);
			//return RedirectToAction("Editprofile");
		}

		[HttpGet]
		public IActionResult EditAd(int Id) {
			//Profile p = new Profile();
			string error = "";
			AdMethods pm = new AdMethods();
			Ad ad = pm.GetAdById(Id, out error);
            ViewBag.id = Id;

            /*
            ViewBag.Description = ad.Description;
            ViewBag.category = ad.Category;
            ViewBag.id = ad.Id;
            ViewBag.price = ad.Price;
            ViewBag.Profile_id = ad.Profile_Id;
            */

            ViewBag.error = error;
			return View(ad);
		}

		[HttpPost]
	    public ActionResult EditAd(Ad pd) {
			if (ModelState.IsValid) {
				// updt databse and go back to list
				AdMethods pm = new AdMethods();
				string error = "";

				pm.UpdateAd(pd, out error);
				ViewBag.error = error;
				return RedirectToAction("Filtering");
				//return View(pd);
			}


				return View(pd);
            //return RedirectToAction("Editprofile");
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
    
    }
}
