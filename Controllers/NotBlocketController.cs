using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NotBlocket2.Models;

namespace NotBlocket2.Controllers {
    public class NotBlocketController : Controller {

		[HttpGet]
		public IActionResult Start(Graphmethods gm) {

            List<string>  locationNames = new List<string>();
            List<int>  ProfileCountPerLocation = new List<int>();
            string errormsg = string.Empty;

            
            gm.GetLocationsWithDataSet(out locationNames, out ProfileCountPerLocation, out errormsg);

            string LocationNamesString = "[ ";
            foreach (var i in locationNames) { LocationNamesString = LocationNamesString + '"' + i + '"' + ',' + ' '; }
            LocationNamesString = LocationNamesString.Substring(0, LocationNamesString.Length - 2);
            LocationNamesString = LocationNamesString + ']';

            string ProfileCountString = "[ ";
            foreach (var i in ProfileCountPerLocation) {
                ProfileCountString = ProfileCountString + i  + ',' + ' '; 
            }
            ProfileCountString = ProfileCountString.Substring(0, ProfileCountString.Length - 2);
            ProfileCountString = ProfileCountString + ']';

            ViewBag.LocationNamesString = LocationNamesString;
            ViewBag.ProfileCount = ProfileCountString;

            gm.s1 = LocationNamesString;
            gm.s2 = ProfileCountString;

            return View(gm);
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
        public IActionResult CreateAccount() {
            return View();
        }

        [HttpPost]
		public IActionResult CreateAccount(Profile p) {
            //Profile p = new Profile();
            ProfileMethods pm = new ProfileMethods();
            int i = 0;
            string error = "";

            if (ModelState.IsValid) {
                i = pm.InsertProfile(p, out error);
                ViewBag.error = error;
                ViewBag.antal = i;
                return RedirectToAction("GetPersonWithDataSet");
            }
            ViewBag.error = "error: " + error;
            ViewBag.antal = i;
            return View(p);
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

        /*
        public IActionResult Editprofile(Profile p) {
            ProfileMethods pm = new ProfileMethods();
            string error = "";
            pm.UpdateProfile(p, out error);
            ViewBag.error = error;

            return RedirectToAction("GetPersonWithDataSet");
        }
        */

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
