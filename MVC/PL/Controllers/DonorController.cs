using AutoMapper;
using BLL.Interfaces;
using DAL.Dtos;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DonorController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<DonorController> logger;
        private readonly IMapper mapper;

        public DonorController(IUnitOfWork unitOfWork,
                               ILogger<DonorController> logger,
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

                var donor = unitOfWork.DonorRepository.GetById(id);

                if (donor == null)
                    return NotFound();

                var data = mapper.Map<DonorDto>(donor);

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

            var donor = unitOfWork.DonorRepository.GetById(id);

            if (donor == null)
                return NotFound();

            var data = mapper.Map<DonorDto>(donor);

            return View(data);
        }
        
        [HttpPost]
        public IActionResult Update(int? id, DonorDto donorDto)
        {
            Donor donor = mapper.Map<Donor>(donorDto);
            
            if (id != donor.Id)
                return BadRequest();

            try
            {
                if(ModelState.IsValid)
                {
                    unitOfWork.DonorRepository.Update(donor);
                    unitOfWork.Complete();
                    return RedirectToAction("Donors", "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View(donorDto);
        }
        
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var donor = unitOfWork.DonorRepository.GetById(id);

            if (donor == null)
                return NotFound();

            unitOfWork.DonorRepository.Delete(donor);
            unitOfWork.Complete();

            return RedirectToAction("Donors", "Admin");
        }


    }
}
