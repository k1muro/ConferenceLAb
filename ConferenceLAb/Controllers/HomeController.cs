using ConferenceLAb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace ConferenceLAb.Controllers
{
    public class HomeController : Controller
    {

        ConferensContext context;
        [BindProperty]
        public User user { get; set; }
        public Conferense conferense { get; set; }
        public HomeController(ConferensContext db)
        {
            this.context = db;

        }
        [HttpPost]
        public async Task<ActionResult> Index(string email, string password)
        {
         
            user = context.Users.Where(o => o.Email.Equals(email) && o.Password.Equals(password)).FirstOrDefault();
            if (user != null)
            {
                if (user.RoleId == 2)
                {
                    return Redirect("/ConferensZap/Index");
                }
                if (user.RoleId == 1)
                {
                    return Redirect("/ConferenceForRegistered/Index");
                }
            }
            return View();
        }
        [HttpPost]

        public async Task<ActionResult> Regist([Bind] User user2)
        {
           
            user2.RoleId = context.Roles.Where(o => o.Name.Equals("user")).FirstOrDefault().Id;
            context.Users.Add(user2);
            await context.SaveChangesAsync();
            Random rand = new Random();
            var num = rand.Next(1000, 9999);
            var fromAddress = new MailAddress("conferencetesting1lab@gmail.com", "Conferens");
            const string fromPassword = "hjeokrj23";
            var toAddress = new MailAddress(user2.Email, $"{user2.Name}");
            const string subject = "Change Password";
            string body = $"<h1>Hello this is the message to reset you're password!\nHere is you're code: [{num}]</h1>";
            var smtp = new SmtpClient { Host = "smtp.gmail.com", Port = 587, EnableSsl = true, DeliveryMethod = SmtpDeliveryMethod.Network, Credentials = new NetworkCredential(fromAddress.Address, fromPassword), Timeout = 20000 };
            using (var message = new MailMessage(fromAddress, toAddress)
            { Subject = subject, Body = body })
            {
                smtp.Send(message);
            };
            return RedirectToAction("Thanks", "Home", new { number = num });
        }
        public async Task<ActionResult> CodСonfirmation(string cod, string rait)
        {
            if (cod.Equals(rait))
            {
                return Redirect("/ConferencClient/Index");
            }
            return Redirect("/Home/Thanks");
        }
        public IActionResult Index()
        {
            return View(user);
        }
        public IActionResult Thanks(int number)
        {
            ViewData["number"] = number;
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}