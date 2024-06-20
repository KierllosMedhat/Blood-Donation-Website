using AutoMapper;
using BLL.Interfaces;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AdminController(IUnitOfWork unitOfWork,
                               IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Donors()
        {
            var donors = unitOfWork.DonorRepository.GetAll();
            var data = mapper.Map<IEnumerable<DonorDto>>(donors);
            return View(data);
        }
        
        public IActionResult Patients()
        {
            var patients = unitOfWork.PatientRepository.GetAll();
            var data = mapper.Map<IEnumerable<PatientDto>>(patients);
            return View(data);
        }

        public IActionResult Requests()
        {
            var requests = unitOfWork.RequestRepository.GetAll();
            var data = mapper.Map<IEnumerable<RequestDto>>(requests);
            return View(data);
        }

        public IActionResult Forms()
        {
            var forms = unitOfWork.FollowUpFormRepository.GetAll();
            var data = mapper.Map<IEnumerable<FollowUpFormDto>>(forms);
            return View(data);
        }
    }
}
