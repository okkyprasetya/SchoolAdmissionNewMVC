using Microsoft.AspNetCore.Mvc;
using SchoolAdmission.BLL;
using SchoolAdmission.BLL.DTOs.ApplicantDTO;
using SchoolAdmission.BLL.DTOs.VerificatorDTO;
using SchoolAdmission.BLL.Interfaces;

namespace SchoolAdmissionNewMVC.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IAdministrator _adminBLL;

        public ApplicantController(IAdministrator adminBLL)
        {
            _adminBLL = adminBLL;
        }
        public IActionResult Index()
        {
            if (TempData["message"] != null)
            {
                ViewData["message"] = TempData["message"];
            }

            var models = _adminBLL.GetAllApplicants();
            return View(models);
        }

        [HttpGet]
        public IActionResult GetAcademicDataDetails(int UGDataID)
        {
            try
            {
                AcademicDataDTO academicData = _adminBLL.getAcademicDataByID(UGDataID);
                if (academicData != null)
                {
                    return Json(new
                    {
                        Success = true,
                        AcademicData = academicData
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        ErrorMessage = "Data not found"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult GetPersonalDataDetails(int UGDataID)
        {
            try
            {
                PersonalDataDTO personalData = _adminBLL.getPersonalDataByID(UGDataID);
                if (personalData != null)
                {
                    return Json(new
                    {
                        Success = true,
                        PersonalData = personalData
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        ErrorMessage = "Data not found"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult GetAchievementRecords(int UGDataID)
        {
            try
            {
                var achievementRecords = _adminBLL.getAchievementRecordsById(UGDataID);

                if (achievementRecords != null && achievementRecords.Count > 0)
                {
                    return Json(new
                    {
                        Success = true,
                        AchievementRecords = achievementRecords
                    });
                }
                else
                {
                    return Json(new
                    {
                        Success = false,
                        ErrorMessage = "Achievement records not found for the specified UGDataID"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [Route("Applicant/VerifyAcademicData/{UGDataID}")]
        public IActionResult VerifyAcademicData(int UGDataID)
        {
            int VerID = 25;
            try
            {
                _adminBLL.verifyAcademicData(VerID,UGDataID);
                TempData["message"] = @"<div class='alert alert-success'>Data applicant with ID: "+UGDataID+" has been verified by Verificator (ID : " + VerID + ") <strong>Success</strong> !</div>";
            }
            catch (Exception ex)
            {

                TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
            }
            return RedirectToAction("Index");
        }

        [Route("Applicant/VerifyPersonalData/{UGDataID}")]
        public IActionResult VerifyPersonalData(int UGDataID)
        {
            int VerID = 25;
            try
            {
                _adminBLL.verifyPersonalData(VerID, UGDataID);
                TempData["message"] = @"<div class='alert alert-success'>Personal Data applicant with ID: " + UGDataID + " has been verified by Verificator (ID : " + VerID + ") <strong>Success</strong> !</div>";
            }
            catch (Exception ex)
            {

                TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
            }
            return RedirectToAction("Index");
        }
    }
}
