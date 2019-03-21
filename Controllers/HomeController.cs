using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingPlannerContext dbContext;
        public HomeController(WeddingPlannerContext context){
            dbContext = context;
        }
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/create")]
        [HttpPost]
        public IActionResult Register(User newUser){
            if(ModelState.IsValid){
                if(dbContext.Users.Any(u => u.Email == newUser.Email)){
                    ModelState.AddModelError("Email","Email already in use!");
                    return View("Index");
                }
                else{
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    dbContext.Add(newUser);
                    dbContext.SaveChanges();
                    int Id = newUser.UserId;
                    HttpContext.Session.SetInt32("Id",Id);
                    return RedirectToAction("Registry");
                }
            }
            else{
                return View("Index");
            }
        }
        [Route("/login")]
        [HttpGet]
        public IActionResult Login(){
            return View();
        }
        [Route("/verify")]
        [HttpPost]
        public IActionResult Verify(LoginUser userSubmission){
            if(ModelState.IsValid){
                var userInDB = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
                if(userInDB == null){
                    ModelState.AddModelError("Email","Invalid Email/Password");
                    return View("Login");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDB.Password,userSubmission.Password);
                if(result == 0){
                    ModelState.AddModelError("Email","Invalid Email/Password");
                    return View("Login");
                }
                else{
                    int Id = userInDB.UserId;
                    HttpContext.Session.SetInt32("Id",Id);
                    return RedirectToAction("Registry");
                }
            
            }
            else{
                return View("Login");
            }
        }


        [Route("/registry")]
        [HttpGet]
        public IActionResult Registry(){
            if(HttpContext.Session.GetInt32("Id")==null){
                return RedirectToAction("Index");
            }
            else{
                List<Wedding> AllWeddings = dbContext.Weddings.Include(w => w.venue).Include(w => w.Guests).ThenInclude(g=> g.guest).ToList();
                foreach(var w in AllWeddings){
                    if(w.UserId==HttpContext.Session.GetInt32("Id")){
                        w.Status=1;
                        System.Console.WriteLine("it's a match");
                    }
                    else if(w.Guests.Any(g=>g.UserId==HttpContext.Session.GetInt32("Id"))){
                        w.Status=2;
                    }
                    else{
                        w.Status=3;
                    }
                }
                return View(AllWeddings);
            }
        }
        [Route("/logout")]
        [HttpGet]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [Route("/newwedding")]
        [HttpGet]
        public IActionResult NewWedding(){
            List<Location> list = dbContext.Locations.ToList();
            ViewBag.venues=list;
            string[] states = {"AK", "AL","AR","AZ","CA","CO","CT","DC","DE","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA","MA","MD","ME","MO","MN","MO","MT","NC","ND","NE","NH","NJ","NM","NV","NY","OH","OK","OR","PA","RI","SC","SD","TN","TX","UT","VA","VT","WA","WI","WV","WY"};
            ViewBag.states= states;
            return View();

        }
        [Route("/createwedding")]
        [HttpPost]
        public IActionResult CreateWedding(Wedding newWedding){
            if(ModelState.IsValid){
                newWedding.UserId=(int)HttpContext.Session.GetInt32("Id");
                dbContext.Add(newWedding);
                dbContext.SaveChanges();
                return RedirectToAction("Registry");
            }
            System.Console.WriteLine(newWedding.Wedder1);
            System.Console.WriteLine("lol no form data");
            List<Location> list = dbContext.Locations.ToList();
                ViewBag.venues=list;
            return View("NewWedding");

        }
        [Route("/newlocation")]
        [HttpGet]
        public IActionResult NewLocation(){
            string[] states = {"AK", "AL","AR","AZ","CA","CO","CT","DC","DE","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA","MA","MD","ME","MO","MN","MO","MT","NC","ND","NE","NH","NJ","NM","NV","NY","OH","OK","OR","PA","RI","SC","SD","TN","TX","UT","VA","VT","WA","WI","WV","WY"};
            ViewBag.states= states;
            return PartialView("LocationPartial");
        }
        [Route("/createlocation")]
        [HttpPost]
        public IActionResult CreateLocation(Location newLocation){
            if(ModelState.IsValid){
                dbContext.Add(newLocation);
                dbContext.SaveChanges();
                List<Location> list = dbContext.Locations.ToList();
                ViewBag.venues=list;
                System.Console.WriteLine("error in create route");
                return PartialView("WeddingPartial");
            }
            string[] states = {"AK", "AL","AR","AZ","CA","CO","CT","DC","DE","FL","GA","HI","ID","IL","IN","IA","KS","KY","LA","MA","MD","ME","MO","MN","MO","MT","NC","ND","NE","NH","NJ","NM","NV","NY","OH","OK","OR","PA","RI","SC","SD","TN","TX","UT","VA","VT","WA","WI","WV","WY"};
            ViewBag.states= states;
            System.Console.WriteLine("error in redirect");
            return PartialView("LocationPartial");
        }
        [Route("/rsvp")]
        [HttpPost]
        public IActionResult rsvp(int wedding){
            int ID = (int)HttpContext.Session.GetInt32("Id");
            Rsvp newRSVP = new Rsvp(ID, wedding);
            dbContext.Add(newRSVP);
            dbContext.SaveChanges();
            return RedirectToAction("Registry");
        }
        [Route("/unrsvp")]
        [HttpPost]
        public IActionResult unrsvp(int wedding){
            int ID = (int)HttpContext.Session.GetInt32("Id");
            Rsvp targeted = dbContext.Rsvps.Where(r => r.UserId == ID && r.WeddingId == wedding).FirstOrDefault();
            dbContext.Rsvps.Remove(targeted);
            dbContext.SaveChanges();
            return RedirectToAction("Registry");
        }
        [Route("/delete")]
        [HttpPost]
        public IActionResult delete(int wedding){
            Wedding targeted = dbContext.Weddings.Include(w => w.planner).Include(w => w.venue).Include(w => w.Guests).ThenInclude(g=> g.guest).Where(w => w.WeddingId == wedding).FirstOrDefault();
            dbContext.Weddings.Remove(targeted);
            dbContext.SaveChanges();
            return RedirectToAction("Registry");
        }
        [Route("/weddings/{num}")]
        [HttpGet]
        public IActionResult Wedding(int num){
            Wedding displayed = dbContext.Weddings.Include(w => w.planner).Include(w => w.venue).Include(w => w.Guests).ThenInclude(g => g.guest).Where(w => w.WeddingId == num).FirstOrDefault();
            return View(displayed);
        }
    }
}
