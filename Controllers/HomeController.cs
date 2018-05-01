using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using login_registration.Models;

namespace login_registration.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;

        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        [HttpPost]
        [Route("/signup")]
        public IActionResult Signup(User newUser)
        {
            if (ModelState.IsValid)
            {
                _dbConnector.Execute($"INSERT INTO users (first_name, last_name, email, password) VALUES ('{newUser.first_name}', '{newUser.last_name}', '{newUser.email}', '{newUser.password}');");
                // HttpContext.Session.SetString("email", newUser.email);
                // TempData["user"] = newUser.first_name;
                var user = _dbConnector.Query($"SELECT * FROM users WHERE email = '{newUser.email}'");
                HttpContext.Session.SetString("username", (string)user[0]["first_name"]);
                // var currentUser = user[0]["id"];
                HttpContext.Session.SetInt32("userId", (int)_dbConnector.Query($"SELECT * FROM users WHERE email = '{newUser.email}'")[0]["id"]);
                return RedirectToAction("WallDisplay", "Wall");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login(User newUser)
        {
            var user = _dbConnector.Query($"SELECT * FROM users WHERE email = '{newUser.email}'");
            if(user.Count > 0)
            {
                if(user[0]["password"].ToString() == newUser.password)
                {
                    // TempData["log_error"] = "";
                    // TempData["user"] = user[0]["first_name"];
                    // var currentUser = user[0]["id"];
                    HttpContext.Session.SetInt32("userId", (int)user[0]["id"]);
                    HttpContext.Session.SetString("username", (string)user[0]["first_name"]);
                    return RedirectToAction("WallDisplay", "Wall");
                }
                else
                {
                    TempData["psw_error"] = "Password is incorrect";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["log_error"] = "Please check your email otherwie go to register";
                return RedirectToAction("Index");
            }
        }
    }
}
