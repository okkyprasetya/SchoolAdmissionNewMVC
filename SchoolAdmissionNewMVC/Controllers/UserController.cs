using Microsoft.AspNetCore.Mvc;
using SchoolAdmission.BLL.Interfaces;

namespace SchoolAdmissionNewMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IAdministrator _adminBLL;

        public UserController(IAdministrator adminBLL)
        {
            _adminBLL = adminBLL;


        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(string email, string password)
        {
            try
            {
                var userDatas = _adminBLL.login(email,password);
                HttpContext.Session.SetString("UserRole", userDatas.Role.RoleName);
                HttpContext.Session.SetInt32("RoleID", userDatas.Role.RoleId);
                HttpContext.Session.SetString("UserName", userDatas.FirstName);
                TempData["message"] = @"<div class='alert alert-success'>Login <strong>Success</strong> !</div>";
                return RedirectToAction("Index", "Home",userDatas);
            }
            catch (Exception ex)
            {
                TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
                return RedirectToAction("Index", "User");
            }
            
        }

        public IActionResult logout()
        {
            HttpContext.Session.Remove("RoleID");
            HttpContext.Session.Remove("UserRole");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "User");
        }
    }
}
