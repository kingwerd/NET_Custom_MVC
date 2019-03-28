using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CustomMVC.Models;

namespace CustomMVC.Controllers {

    public class UserController : Controller {

        private Context dbContext;

        public UserController(Context context) {
            dbContext = context;
        }

        public IActionResult Index() {
            if (UserIdInSession()) {
                SetViewBag();
            }
            return View();
        }

        public IActionResult SignIn() {
            return View();
        }

        public IActionResult LoginUser(LoginUser userSubmission) {
            if (ModelState.IsValid) {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
                if (userInDb == null) {
                    ModelState.AddModelError("Email", "Ivalid Email/Password");
                    return View("SignUp");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);
                if (result == 0) {
                    ModelState.AddModelError("Password", "Incorrect password!");
                    return View("SignUp");
                }
                SetSession(userInDb.UserId, userInDb.UserLevel);
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public IActionResult SignUp() {
            return View();
        }

        public IActionResult CreateUser(User user) {
            if (ModelState.IsValid) {
                if (dbContext.Users.Any(u => u.Email == user.Email)) {
                    ModelState.AddModelError("Email", "Email already in use!");
                    if (!UserIdInSession()) {
                        return View("SignUp");
                    } else {
                        return View("NewUser");
                    }
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                dbContext.Add(user);
                // User UserJustAdded = dbContext.Users.OrderByDescending(u => u.CreatedAt).Take(1).SingleOrDefault();
                dbContext.SaveChanges();
                if (!UserIdInSession()) {
                    SetSession(user.UserId, user.UserLevel);
                }
                return RedirectToAction("Dashboard");
            }
            else {
                return View("SignUp");
            }
        }

        public IActionResult Logout() {
            ClearUserIdFromSession();
            return RedirectToAction("Index");
        }

        public IActionResult DataTable() {
            return View();
        }

        public IActionResult Posts() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Gets the user object with the userid that is currently in session
        private User GetUserInSession() {
            User user = dbContext.Users.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();
            return user;
        }

        // Sets the session whenever a user is registered
        private void SetSession(int UserId, int UserLevel) {
            HttpContext.Session.SetInt32("UserId", UserId);
            HttpContext.Session.SetInt32("UserLevel", UserLevel);
        }

        // Returns a boolean value based on whether there is a user in the session or not
        private bool UserIdInSession() {
            if (HttpContext.Session.GetInt32("UserId") != null) {
                return true;
            } else {
                return false;
            }
        }

        // Called when the user logs out
        private void ClearUserIdFromSession() {
            HttpContext.Session.Clear();
        }

        // Helper function that is only called when there is a user in the session
        private void SetViewBag() {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
        }
    }
}
