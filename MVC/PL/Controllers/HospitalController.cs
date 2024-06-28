using AutoMapper;
using BLL.Interfaces;
using BLL.Repositories;
using DAL.Dtos.HospitalsDTO;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PL.Controllers
{
    public class HospitalController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<HospitalController> logger;
        private readonly IMapper mapper;

        public HospitalController(IUnitOfWork unitOfWork,
                                  ILogger<HospitalController> logger,
                                  IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hospitals = await unitOfWork.HospitalRepository.GetAllHospitalsAsync();
            var hospitalDtos = mapper.Map<IEnumerable<GetHospitalDTO>>(hospitals);

            return View(hospitalDtos);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                logger.LogWarning("Delete: Hospital ID is null.");
                return BadRequest();
            }

            await unitOfWork.HospitalRepository.DeleteHospitalAsync(id.Value);
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var hospital = await unitOfWork.HospitalRepository.GetHospitalDetailsAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);

        }
    }
}