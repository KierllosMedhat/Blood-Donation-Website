using BLL.Interfaces;
using DAL.Dtos.HospitalsDTO;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IUnitOfWork unitOfWork,
                                 ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [HttpGet]
        public  IActionResult SignUp()
        {
            return View(new SignUpViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = input.UserName,
                    Email = input.Email,
                    PhoneNumber = input.PhoneNumber,
                    
                };
                var result = await userManager.CreateAsync(user, input.Password);

                if (input.Role == "donor")
                {
                    var donor = new Donor
                    {
                        UserName = input.UserName,
                        Email = input.Email,
                        PhoneNumber = input.PhoneNumber,
                        BloodType = input.BloodType,
                        Gender = input.Gender,
                        DateOfBirth = input.DateOfBirth,
                        LastDonationDate = input.LastDonationDate,
                        Governorate = input.Governorate,
                        Province = input.Province,
                        User = user,
                        UserId = user.Id
                    };
                    unitOfWork.DonorRepository.Add(donor);
                    user.EntityId = donor.Id;
                    await userManager.AddToRoleAsync(user, "Donor");
                }
                else if (input.Role == "patient")
                {
                    var patient = new Patient
                    {
                        UserName = input.UserName,
                        Email = input.Email,
                        PhoneNumber = input.PhoneNumber,
                        BloodType = input.BloodType,
                        Gender = input.Gender,
                        DateOfBirth = input.DateOfBirth,
                        Governorate = input.Governorate,
                        Province = input.Province,
                        User = user,
                        UserId = user.Id
                    };
                    unitOfWork.PatientRepository.Add(patient);
                    user.EntityId = patient.Id;
                    await userManager.AddToRoleAsync(user, "Patient");
                }


                if (result.Succeeded)
                {
                    unitOfWork.Complete();
                    return RedirectToAction("Login");
                }
                
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description); 
                    //logger.LogError(error.Description);   
            }
            return View(input);
        }


        [HttpGet]
        public IActionResult RegisterHospital()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterHospital(HospitalDTO hospitalDTO)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = hospitalDTO.HospitalName, Email = hospitalDTO.Email };
  
                var result = await userManager.CreateAsync(user, hospitalDTO.Password);

                await userManager.AddToRoleAsync(user, "Hospital");

                if (result.Succeeded)
                {
                    var hospital = new Hospital
                    {
                        HospitalName = hospitalDTO.HospitalName,
                        Email = hospitalDTO.Email,
                        PhoneNumber = hospitalDTO.PhoneNumber,
                        Governorate = hospitalDTO.Governorate,
                        Province = hospitalDTO.Province,
                        Address = hospitalDTO.Address,
                        UserId = user.Id,

                    };
                    unitOfWork.HospitalRepository.Add(hospital);
                    unitOfWork.Complete();

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(hospitalDTO);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(input.Email);

                if (user == null)
                    ModelState.AddModelError("", "Email Does not exist!");

                if (user != null && await userManager.CheckPasswordAsync(user, input.Password))
                {
                    var result = await signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, false);
                    
                    if (result.Succeeded)
                       return RedirectToAction("Index", "Home");
                }
            }

            return View(input);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
