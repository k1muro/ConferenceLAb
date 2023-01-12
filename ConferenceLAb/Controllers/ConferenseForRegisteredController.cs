using ConferenceLAb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceLAb.Controllers
{
    public class ConferenseForRegisteredController : Controller
    {
        ConferensContext context;
        public Conferense conferens { get; set; }
        public ConferenseForRegisteredController(ConferensContext db)
        {
            this.context = db;

        }
        public IActionResult Index()
        {
            return View(context.Conferense.ToList());
        }
        public IActionResult Zapolnenia()
        {
            return Redirect("/ConferenceZap/Index");
        }
    }
}
