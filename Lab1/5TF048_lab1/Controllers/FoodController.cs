using System;
using System.Diagnostics;
using _5TF048_lab1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;

namespace _5TF048_lab1.Controllers
{
    public class FoodController : Controller
    {
        [HttpGet]
        public IActionResult Start(){
            return View();
        }

        [HttpPost]
        public IActionResult Recept(IFormCollection col) {

            Dinner D = new Dinner();
            D.Name = col["Name"];
            D.NumberofPortions = Convert.ToInt32(col["NumberofPortions"]);
            D.Date = Convert.ToDateTime(col["date"]);
            D.Calculate();

            string s = JsonConvert.SerializeObject(D);
            HttpContext.Session.SetString("Foodsession", s);

            return View(D);
        }

        [HttpGet]
        public IActionResult Evaluate() {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "uruselt", Value = "0" });
            list.Add(new SelectListItem { Text = "Dåligt", Value = "1" });
            list.Add(new SelectListItem { Text = "INTE BRA", Value = "2" });
            list.Add(new SelectListItem { Text = "ok", Value = "3" });
            list.Add(new SelectListItem { Text = "inte dåligt", Value = "4" });
            list.Add(new SelectListItem { Text = "bra", Value = "5" });

            return View(list);
        }

        [HttpPost]
        public ActionResult Finish(IFormCollection col) {
            Dinner m = new Dinner();
            string s = HttpContext.Session.GetString("Foodsession");
            m = JsonConvert.DeserializeObject<Dinner>(s);
            int review = Convert.ToInt32(col["Review"]);

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "uruselt", Value = "0" });
            list.Add(new SelectListItem { Text = "Dåligt", Value = "1" });
            list.Add(new SelectListItem { Text = "INTE BRA", Value = "2" });
            list.Add(new SelectListItem { Text = "ok", Value = "3" });
            list.Add(new SelectListItem { Text = "inte dåligt", Value = "4" });
            list.Add(new SelectListItem { Text = "bra", Value = "5" });

            m.Review = list[review].Text;

            s = JsonConvert.SerializeObject(m);
            HttpContext.Session.SetString("Foodsession", s);

            return View(m);
        }

    }
}
