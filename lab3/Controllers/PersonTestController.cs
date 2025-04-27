using Laboration_3___Databasdriven_webbapplikation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laboration_3___Databasdriven_webbapplikation.Controllers {
    public class PersonTestController : Controller {



        public IActionResult Index() {
            return View();
        }

        public IActionResult InsertPerson() { 
            
            PersonDetalj pd = new PersonDetalj();

            PersonMetoder pm = new PersonMetoder();

            int i = 0;
            string error = "";

            pd.Fornamn = "Alice";
            pd.Efternamn = "Karlsson";
            pd.Epost = "something@gmail.com";
            pd.Fodelsear = 1999;
            pd.Bor = 1;

            i = pm.InsertPerson(pd, out error);
            ViewBag.error = error;
            ViewBag.antal = i;

            return View(); 
        }

        [HttpGet]
        public IActionResult InsertPerson2() {
            return View();
        }

        [HttpPost]
        public IActionResult InsertPerson2(PersonDetalj pd) {
            PersonMetoder pm = new PersonMetoder();
            int i = 0;
            string error = "";

            i = pm.InsertPerson(pd, out error);
            ViewBag.error = error;
            ViewBag.antal = i;
            if (i ==1) { return RedirectToAction("SelectWithDataSet"); }
            else { return View("InsertPerson"); }
            
        }



        public IActionResult DeletePerson() {
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            int i = 0;
            i = pm.DeletePerson(out error);
            HttpContext.Session.SetString("antal", i.ToString());
            return RedirectToAction("SelectWithDataset");
        }


        public ActionResult SelectWithDataSet() {
            List<PersonDetalj> Personlist = new List<PersonDetalj>();
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            Personlist = pm.GetPersonWithDataSet(out error);
            //ViewBag.antal = HttpContext.Session.GetString("antal");
            ViewBag.error = error;
            return View(Personlist);
        }

        public ActionResult SelectWithDataReader() {
            List<PersonDetalj> Personlist = new List<PersonDetalj>();
            PersonMetoder pm = new PersonMetoder();
            string error = "";
            Personlist = pm.GetPersonWithDataSet(out error);
            ViewBag.error = error;
            return View(Personlist);
        }
    }
}
