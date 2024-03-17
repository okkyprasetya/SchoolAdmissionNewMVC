using Microsoft.AspNetCore.Mvc;
using SchoolAdmission.BLL.DTOs.VerificatorDTO;
using SchoolAdmission.BLL.Interfaces;
using SchoolAdmission.DAL.BOs;
using SchoolAdmissionNewMVC.Models;
using System.Diagnostics;

namespace SchoolAdmissionNewMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdministrator _adminBLL;

        public HomeController(IAdministrator adminBLL)
        {
            _adminBLL = adminBLL;
        }

        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetInt32("RoleID");
            var userName = HttpContext.Session.GetString("UserName");
            var roleName = HttpContext.Session.GetString("UserRole");


            if(userRole is null || userRole == 2)
            {
                return RedirectToAction("Index", "User");
            }
            if(userRole == 1)
            {
                return RedirectToAction("Index", "Applicant");
            }

            if (TempData["message"] != null)
            {
                ViewData["message"] = TempData["message"];
            }

            var models = _adminBLL.getAllVerificator();
            return View(models);
        }

        [HttpPost]
        public IActionResult InsertVerificator(CreateVerificatorDTO entity)
        {
            try
            {
                _adminBLL.addVerificator(entity);
                TempData["message"] = @"<div class='alert alert-success'>Add Verificator <strong>Success</strong> !</div>";

            }
            catch (Exception ex)
            {
                TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
            }
            return RedirectToAction("Index");
        }

        [Route("Home/Delete/{userId}")]
        public IActionResult Delete(int userId)
        {
            try
            {
                _adminBLL.deleteVerificator(userId);
                TempData["message"] = @"<div class='alert alert-success'>Delete Verificator (UID : "+userId+") <strong>Success</strong> !</div>";
            }
            catch(Exception ex)
            {

                TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetVerificatorDetails(int verificatorId)
        {
            try
            {
                // Retrieve verificator details by ID using the verificator service
                VerificatorDTO verificator = _adminBLL.getVerificator(verificatorId);

                // Check if verificator exists
                if (verificator != null)
                {
                    // Return JSON response with verificator details
                    return Json(new
                    {
                        Success = true,
                        Verificator = verificator
                    });
                }
                else
                {
                    // Return JSON response with error message if verificator does not exist
                    return Json(new
                    {
                        Success = false,
                        ErrorMessage = "Verificator not found"
                    });
                }
            }
            catch (Exception ex)
            {
                // Return JSON response with error message if an exception occurs
                return Json(new
                {
                    Success = false,
                    ErrorMessage = ex.Message // You can customize the error message as needed
                });
            }
        }

        [HttpPost]
        public IActionResult EditVerificator(UpdateVerificatorDTO verificator)
        {
            try
            {
                // Call the update method in the BLL to update the verificator
                _adminBLL.updateVerificator(verificator);

                // Return a JSON response indicating success
                TempData["message"] = @"<div class='alert alert-success'>Edit Verificator (Email : " + verificator.Users.UserEmail + ") <strong>Success</strong> !</div>";
            }
            catch (Exception ex)
            {
                // Return a JSON response indicating failure with the error message
                TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
            }
            return RedirectToAction("Index");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
