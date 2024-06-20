using AutoMapper;
using BLL.Interfaces;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class RequestController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<RequestController> logger;
        private readonly IMapper mapper;

        public RequestController(IUnitOfWork unitOfWork,
                               ILogger<RequestController> logger,
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

                var request = unitOfWork.RequestRepository.GetById(id);

                if (request == null)
                    return NotFound();

                var data = mapper.Map<RequestDto>(request);

                return View(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
                return BadRequest();

            var request = unitOfWork.RequestRepository.GetById(id);

            if (request == null)
                return NotFound();

            var data = mapper.Map<RequestDto>(request);

            return View(data);
        }

        [HttpPost]
        public IActionResult Update(int? id, RequestDto requestDto)
        {
            Request request = mapper.Map<Request>(requestDto);

            if (id != request.Id)
                return BadRequest();

            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.RequestRepository.Update(request);
                    unitOfWork.Complete();
                    return RedirectToAction("Requests", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View(requestDto);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var request = unitOfWork.RequestRepository.GetById(id);

            if (request == null)
                return NotFound();

            unitOfWork.RequestRepository.Delete(request);
            unitOfWork.Complete();

            return RedirectToAction("requests", "Admin");
        }
    }
}
