using AutoMapper;
using BLL.Interfaces;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PatientController> logger;
        private readonly IMapper mapper;

        public PatientController(IUnitOfWork unitOfWork,
                               ILogger<PatientController> logger,
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

                var patient = unitOfWork.PatientRepository.GetById(id);

                if (patient == null)
                    return NotFound();

                var data = mapper.Map<PatientDto>(patient);

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

            var patient = unitOfWork.PatientRepository.GetById(id);

            if (patient == null)
                return NotFound();

            var data = mapper.Map<PatientDto>(patient);

            return View(data);
        }

        [HttpPost]
        public IActionResult Update(int? id, PatientDto patientDto)
        {
            Patient patient = mapper.Map<Patient>(patientDto);

            if (id != patient.Id)
                return BadRequest();

            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.PatientRepository.Update(patient);
                    unitOfWork.Complete();
                    return RedirectToAction("Patients", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View(patientDto);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var patient = unitOfWork.PatientRepository.GetById(id);

            if (patient == null)
                return NotFound();

            unitOfWork.PatientRepository.Delete(patient);
            unitOfWork.Complete();

            return RedirectToAction("patients", "Admin");
        }

    }
}
