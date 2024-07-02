using AutoMapper;
using BLL.Interfaces;
using BLL.Repositories;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class FormController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<FormController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public FormController(IUnitOfWork unitOfWork,
                               ILogger<FormController> logger,
                               IMapper mapper,
                               UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public ActionResult Index(string id) 
        {
            var request = unitOfWork.FollowUpFormRepository.GetAllById(id);
            var result = mapper.Map<IEnumerable<FollowUpFormDto>>(request);
            return View(result);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var form = unitOfWork.FollowUpFormRepository.GetById(id);

                if (form == null)
                    return NotFound();

                var data = mapper.Map<FollowUpFormDto>(form);

                return View(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
                return BadRequest();

            var form = unitOfWork.FollowUpFormRepository.GetById(id);

            if (form == null)
                return NotFound();

            unitOfWork.FollowUpFormRepository.DeleteFollowUpForm(id);
            unitOfWork.Complete();

            return RedirectToAction("Forms", "Admin");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FollowUpForm form)
        {
            form.UserId = userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                unitOfWork.FollowUpFormRepository.AddForm(form);
                unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(form);
        }
    }
}
