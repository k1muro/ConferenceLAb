using ConferenceLAb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceLAb.Controllers
{
    public class ConferenseController : Controller
    {
        ConferensContext context;
        [BindProperty]
        public User? user { get; set; }

        public ConferenseController(ConferensContext db)
        {
            this.context = db;

        }

        public IActionResult Index()
        {
            Role role = new Role() { Name = "admin" };
            Role role1 = new Role() { Name = "user" };
            user = new User() { Name = "Dima", Email = "Dima@gmail.com", Phone = "0966438556", Password = "1111" };
            context.Roles.Add(role1);
            context.Roles.Add(role);
            context.SaveChanges();
            context.Users.Add(user);
            context.SaveChanges();
            return View();
        }
    }
}
