using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RequestApp.Controllers
{
    public class FollowUpFormsController : Controller
    {
        private readonly IFollowUpFormRepository _followUpFormRepository;
        private readonly IUnitOfWork unitOfWork;

        public FollowUpFormsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var forms = unitOfWork.FollowUpFormRepository.GetAll().ToList();
            return View(forms);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FollowUpForm form)
        {
            if (ModelState.IsValid)
            {
                _followUpFormRepository.AddForm(form);
                return RedirectToAction("Index");
            }
            return View(form);
        }

        public IActionResult Edit(int id)
        {
            var form = _followUpFormRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }

        [HttpPost]
        public IActionResult Edit(FollowUpForm form)
        {
            if (ModelState.IsValid)
            {
                _followUpFormRepository.UpdateFollowUpForm(form);
                return RedirectToAction("Index");
            }
            return View(form);
        }

        public IActionResult Delete(int id)
        {
            var form = _followUpFormRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _followUpFormRepository.DeleteFollowUpForm(id);
            return RedirectToAction("Index");
        }
    }
}

