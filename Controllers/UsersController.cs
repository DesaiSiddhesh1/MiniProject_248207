using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject_248207.Models;
using NuGet.Protocol;

namespace MiniProject_248207.Controllers
{
    public class UsersController : Controller
    {
        // GET: HomeController1
        public ActionResult Register()
        {
          ViewBag.Cities = City.GetCities();
          return View();
        }



        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users user)

        {
            try
            {
                Users.RegisterUser(user);
                TempData["SuccessMessage"] = "Registration successful. Please log in.";
                return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult ViewAll()
        {
            var userdisplay = UserDisplay.GetAllUser();
            return View(userdisplay);
        }

        
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(string loginName, string password)
        {
            // Authenticate the user
            var user = Users.Authenticate(loginName, password);
            if (user != null)
            {
                HttpContext.Session.SetString("FullName", user.FullName);
                return RedirectToAction("Home");
            }

                // Set error message if authentication fails
            ViewBag.ErrorMessage = "Invalid LoginName or Password.";
            return View();
        }
        public ActionResult Home()
        {
            var fullName = HttpContext.Session.GetString("FullName");

            ViewBag.FullName = fullName;
            return View();

        }


        
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index))
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
   
}

