using AutoMapper;
using BLL.Interfaces;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    [Authorize(Roles = "User, Patient, Donor")]
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

        public IActionResult DonorIndex(int id)
        {
            var request = unitOfWork.RequestRepository.GetRequestByDonorId(id);
            var result = mapper.Map<IEnumerable<RequestDto>>(request);
            return View(result);
        }

        public IActionResult PatientIndex(int id)
        {
            var request = unitOfWork.RequestRepository.GetRequestByPatientId(id);
            var result = mapper.Map<IEnumerable<RequestDto>>(request);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RequestDto input)
        {
            if (ModelState.IsValid)
            {
                var request = mapper.Map<Request>(input);
                request.State = RequestState.Proccesing;
                unitOfWork.RequestRepository.AddRequest(request);
                var notification = new Notification{
                    UserId = unitOfWork.DonorRepository.GetById(request.DonorId).UserId,
                    DateCreated = DateTime.Now,
                    IsRead = false,
                    Message = $"You Have A New Request: {request.Id}"
                };
                unitOfWork.NotificationRepository.AddNotification(notification);
                return RedirectToAction("Index", "Home");
            }
            return View(input);
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
