using BLL.Interfaces;
using BLL.Repositories;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace PL.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public AnnouncementController(IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

      
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [Authorize(Roles ="Hospital")]
        public async Task<IActionResult> Create(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = userManager.GetUserId(User);
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return BadRequest();
                }
                else
                {   
                    announcement.UserId = unitOfWork.HospitalRepository.GetByUserId(currentUserId).Id;
                    unitOfWork.AnnouncementRepository.Add(announcement);
                    unitOfWork.Complete();
                    return RedirectToAction("Index", "Home");

                }
            }

            return View(announcement);
        }

   
        public async Task<IActionResult> Index()
        {
            var announcements = await unitOfWork.AnnouncementRepository.GetAllAnnouncementsAsync();
            return View(announcements);
        }

        [HttpGet]
        [Authorize(Roles ="Hospital")]
        public IActionResult Announcements(int id)
        {
            var announcements = unitOfWork.AnnouncementRepository.GetAllAnnouncementsAsyncById(id);
            return View(announcements);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await unitOfWork.AnnouncementRepository.GetAnnouncementByIdAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        [HttpPost]
        [Authorize(Roles = "Hospital")]
        public async Task<IActionResult> Edit(int id, Announcement announcement)
        {
            if (id != announcement.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await unitOfWork.AnnouncementRepository.UpdateAnnouncementAsync(announcement);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }
            return View(announcement);
        }

        [HttpPost]
        [Authorize(Roles = "Hospital")]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }

            await unitOfWork.AnnouncementRepository.DeleteAnnouncementAsync(id.Value);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
