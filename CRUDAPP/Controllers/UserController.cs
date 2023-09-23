using CRUDAPP.Models;
using CRUDAPP.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CRUDAPP.Controllers
{
    public class UserController : Controller
    {
        private UsersRepo userRepo;

        public UserController()
        {
            userRepo = new UsersRepo();
        }

        public ActionResult GetAllUserDetails()
        {
            UsersRepo usersRepo = new UsersRepo();
            return View(usersRepo.GetUserDetails());
        }


        private bool IsImage(HttpPostedFileBase ProfilePicture)
        {
            if (ProfilePicture != null && ProfilePicture.ContentLength > 0)
            {
                string[] allowedImageTypes = { "image/jpeg", "image/png", "image/gif" };
                string contentType = ProfilePicture.ContentType;

                return allowedImageTypes.Contains(contentType);
            }
            return false;
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User user, HttpPostedFileBase image)
        {
            UsersRepo usersRepo=new UsersRepo();
            userRepo.AddNewUser(user, image);
            return RedirectToAction("GetAllUserDetails");
        }



        public ActionResult DeleteUser(int id,UsersRepo del)
        {
            try
            {
                UsersRepo repo = new UsersRepo();
                if (repo.DeleteUser(id))
                {
                    ViewBag.alert = "sucess";
                }
                return RedirectToAction("GetAllUserDetails");
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = "An error Occured while deleting the record: " + ex.Message;

                return RedirectToAction("GetAllUserDetails");

            }
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
           UsersRepo edit = new UsersRepo();
            return View(edit.GetUserDetails().Find(obj=>obj.UserID==id));
        }

        [HttpPost]
        public ActionResult EditUser(User obj, HttpPostedFileBase image)
            {
            try
            {
                UsersRepo edit = new UsersRepo();
                edit.UpdateUser(obj, image);
                return RedirectToAction("GetAllUserDetails");
            }
            catch
            {
                return View();
            }
   
}
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            UsersRepo usersRepo = new UsersRepo();
            bool loginResult = usersRepo.Login(user); // Capture the login result
            if (loginResult)
            {
                return RedirectToAction("GetAllUserDetails");
            }
            else
            {
                ViewBag.msg = "Invalid username or password";
                return View();
            }
        }

    }
}
