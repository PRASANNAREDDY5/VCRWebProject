using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel _user)
        {
            if (ModelState.IsValid)
            {

                var apiRes = _user.UserRegistration(_user);
                if (apiRes.IsSuccessful)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Register Failed";
                }
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin uLogin)
        {
            UserModel uObj = new UserModel();
            if (ModelState.IsValid)
            {
                var apiRes = uObj.LoginDetails(uLogin);
                if (apiRes.IsSuccessful)
                {
                    var resJ = apiRes.ContentLength;
                    if (resJ > 2)
                    {
                        Session["UserName"] = uLogin.UserName.ToString();
                        //if (resJ.Success)
                        //{

                        //}
                        return RedirectToAction("Index");
                    }
                    else {
                        ViewBag.error = "Invalid User";
                        return View();
                    }
                   
                }
                else
                {
                    ViewBag.error = "Login Failed";
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
    }
}