using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                HttpContext.Session.SetString("LoginName", user.LoginName);
                return RedirectToAction("Home");
            }

                // Set error message if authentication fails
            ViewBag.ErrorMessage = "Invalid LoginName or Password.";
            return View();
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ActionResult Home()
        {
            var loginName = HttpContext.Session.GetString("LoginName");
            if (string.IsNullOrEmpty(loginName))
            {
                return RedirectToAction("Login");
            }

            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            return View();
        }


        
        public ActionResult Edit()
        {
            var loginName = HttpContext.Session.GetString("LoginName");
            var user = Users.GetUserByLoginName(loginName);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var cities = City.GetCities();
            ViewBag.Cities = new SelectList(cities, "CityId", "CityName");
            return View(user);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users user)
        {
            try
            {

                Users.UpdateUser(user);
                TempData["SuccessfullMessage"] = "Updated Successfully.";
                return RedirectToAction("Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: LogOut
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
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

