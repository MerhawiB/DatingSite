using Dejtinghemsida.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dejtinghemsida.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(IndexViewModel model)
        {
            model.Requests = new List<ApplicationUser>();

            var ListofExamples = new List<ApplicationUser>();
            model.ExampleProfiles = ListofExamples;
            var user = _context.Users.Single(u => u.UserName == "hejhej@gmail.com");
            var user2 = _context.Users.Single(u => u.UserName == "merinho_93@hotmail.com");
            var user3 = _context.Users.Single(u => u.UserName == "merinho_933@hotmail.com");
            var user4 = _context.Users.Single(u => u.UserName == "riyaa@gmail.com");
            model.ExampleProfiles.Add(user);
            model.ExampleProfiles.Add(user2);
            model.ExampleProfiles.Add(user3);
            model.ExampleProfiles.Add(user4);
            return View(model);
        }
    }
}