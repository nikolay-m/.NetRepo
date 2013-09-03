using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Malenko_new.Models;

namespace Malenko_new.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            DataContext _db = new DataContext();
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex);
                }
            }
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            DataContext _db = new DataContext();
            var res = _db.Users;
            return View(res);
        }
    }
}
