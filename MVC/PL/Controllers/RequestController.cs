using AutoMapper;
using BLL.Interfaces;
using DAL.Dtos;
using DAL.Entities;
using DAL.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    [Authorize(Roles ="Patient, Donor")]
    public class RequestController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<RequestController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public RequestController(IUnitOfWork unitOfWork,
                               ILogger<RequestController> logger,
                               IMapper mapper,
                               UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [Authorize(Roles ="Donor")]
        public IActionResult DonorIndex(int id)
        {
            var request = unitOfWork.RequestRepository.GetRequestByDonorId(id);
            var result = mapper.Map<IEnumerable<RequestDto>>(request);
            return View(result);
        }

        [Authorize(Roles ="Patient")]
        public IActionResult PatientIndex(int id)
        {
            var request = unitOfWork.RequestRepository.GetRequestByPatientId(id);
            var result = mapper.Map<IEnumerable<RequestDto>>(request);
            return View(result);
        }

        public IActionResult Search(BloodType SearchValue)
        {
            IEnumerable<Donor> donors;

            if (SearchValue == null)
            {
                donors = unitOfWork.DonorRepository.GetAll();
            }
            else
            {
                donors = unitOfWork.DonorRepository.GetByBloodType(SearchValue);
            }

            return View(donors);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var donor = unitOfWork.DonorRepository.GetById(id);

                if (donor == null)
                    return NotFound();

                var requestDto = new RequestDto
                {
                    DonorId = id,
                    BloodType = donor.BloodType,
                    Governorate = donor.Governorate,
                    Province = donor.Province,
                };
                
                return View(requestDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }

        [Authorize(Roles ="Patient")]
        [HttpPost]
        public IActionResult Create(RequestDto input)
        {
            var userId = userManager.GetUserId(User);
            
            if (ModelState.IsValid)
            {
                var request = new Request
                {
                    PatientId = unitOfWork.PatientRepository.GetByUserId(userId).Id,
                    DonorId = input.DonorId,
                    BloodType = input.BloodType,
                    NumOfBags = input.NumOfBags,
                    Governorate = input.Governorate,
                    Province = input.Province,
                    DateTime = DateTime.Now,
                    State = RequestState.Proccesing

                };
                unitOfWork.RequestRepository.AddRequest(request);
                unitOfWork.RequestRepository.SaveChanges();
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
        [Authorize(Roles ="Patient, Donor")]
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
        [Authorize(Roles ="Patient")]
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
        [Authorize(Roles ="Patient")]
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
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View(requestDto);
        }

        [HttpPost]
        [Authorize(Roles ="Patient")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var request = unitOfWork.RequestRepository.GetById(id);

            if (request == null)
                return NotFound();

            unitOfWork.RequestRepository.Delete(request);
            unitOfWork.Complete();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles ="Donor")]
        public IActionResult Accept(int? id)
        {
            if (id == null)
                return BadRequest();

            var request = unitOfWork.RequestRepository.GetById(id);

            if (request == null)
                return NotFound();
            
            unitOfWork.RequestRepository.AcceptRequest(request);
            
            var notification = new Notification
            {
                UserId = unitOfWork.PatientRepository.GetById(request.PatientId).UserId,
                DateCreated = DateTime.Now,
                IsRead = false,
                Message = $"Your Request Has Been Accepted, Please Contact: {unitOfWork.DonorRepository.GetById(request.DonorId).PhoneNumber}"
            };
            unitOfWork.NotificationRepository.AddNotification(notification);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles ="Donor")]
        public IActionResult Refuse(int? id)
        {
            if (id == null)
                return BadRequest();

            var request = unitOfWork.RequestRepository.GetById(id);

            if (request == null)
                return NotFound();

            unitOfWork.RequestRepository.RefuseRequest(request);

            var notification = new Notification
            {
                UserId = unitOfWork.PatientRepository.GetById(request.PatientId).UserId,
                DateCreated = DateTime.Now,
                IsRead = false,
                Message = $"Your Request Has Been Refused, Request Id: {request.Id}"
            };
            unitOfWork.NotificationRepository.AddNotification(notification);

            return RedirectToAction("Index", "Home");


        }
    }
}
