using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RequestApp.Controllers
{
    public class AnnouncementtsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AnnouncementtsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Create(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.AnnouncementRepository.AddAnnouncement(announcement);
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        public IActionResult Index()
        {
            var announcements = unitOfWork.AnnouncementRepository.GetAll();
            return View(announcements);
        }
    }
}

