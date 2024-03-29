﻿using Microsoft.AspNetCore.Mvc;
using SchoolAdmission.BLL.Interfaces;
using static Dapper.SqlMapper;

namespace SchoolAdmissionNewMVC.Controllers
{
    public class RankController : Controller
    {
        private readonly IAdministrator _adminBLL;

        public RankController(IAdministrator adminBLL)
        {
            _adminBLL = adminBLL;

           
        }
        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetInt32("RoleID");
            var userName = HttpContext.Session.GetString("UserName");
            var roleName = HttpContext.Session.GetString("UserRole");

            if (userRole is null || userRole == 2)
            {
                return RedirectToAction("Index", "User");
            }

            var model = _adminBLL.GetRank();
            return View(model);
        }

        [HttpPost]
        public IActionResult finalizeRank(int quota)
        {
            try
            {
                _adminBLL.finalizeLeaderboard(quota);
                TempData["message"] = @"<div class='alert alert-success'>Finalize leaderboard <strong>Success</strong> !</div>";

            }
            catch (Exception ex)
            {
                TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
            }
            return RedirectToAction("Index");
        }
    }
}
