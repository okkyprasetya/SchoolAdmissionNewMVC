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
                TempData["message"] = @"<div class='alert alert-success'>Login <strong>Success</strong> !</div>";
                return RedirectToAction("Index", "Home",userDatas);
            }
            catch (Exception ex)
            {
                TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
                return RedirectToAction("Index", "User");
            }
            
        }
    }
}
