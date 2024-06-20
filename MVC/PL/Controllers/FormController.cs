using AutoMapper;
using BLL.Interfaces;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class FormController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<FormController> logger;
        private readonly IMapper mapper;

        public FormController(IUnitOfWork unitOfWork,
                               ILogger<FormController> logger,
                               IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Details(int? id)
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

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var form = unitOfWork.FollowUpFormRepository.GetById(id);

            if (form == null)
                return NotFound();

            unitOfWork.FollowUpFormRepository.Delete(form);
            unitOfWork.Complete();

            return RedirectToAction("Forms", "Admin");
        }

    }
}
