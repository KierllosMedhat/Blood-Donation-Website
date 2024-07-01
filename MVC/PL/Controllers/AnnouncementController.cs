using BLL.Interfaces;
using BLL.Repositories;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace PL.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AnnouncementController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

      
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Announcement announcement)
        {
            if (ModelState.IsValid)
            {

                 string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(currentUserId))
                {
                    return BadRequest();
                }
                else
                {
                    unitOfWork.AnnouncementRepository.Add(announcement);

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
        [ValidateAntiForgeryToken]
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

        [HttpGet]
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
