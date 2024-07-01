using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class NotificationController : Controller
    {
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationService _notificationService;
    private readonly IUserRepository _userRepository;
    private readonly IRequestRepository _requestRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public NotificationController(INotificationRepository notificationRepository,
                                  INotificationService notificationService,
                                  IUserRepository userRepository,
                                  IRequestRepository requestRepository,
                                  IUnitOfWork unitOfWork,
                                  UserManager<ApplicationUser> userManager)
    {
        _notificationRepository = notificationRepository;
        _notificationService = notificationService;
        _userRepository = userRepository;
        _requestRepository = requestRepository;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

    [HttpGet]
    public IActionResult Index()
    {
        var userId = userManager.GetUserId(User); 
        var notifications = _notificationRepository.GetNotificationsForUser(userId);
        return View(notifications);
    }

    [HttpPost]
    public IActionResult MarkAsRead(int notificationId)
    {
        _notificationRepository.MarkAsRead(notificationId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult PatientRequest(int patientId, int donorId)
    {
        // إرسال إشعار إلى المتبرع
        var userId = unitOfWork.DonorRepository.GetById(donorId).UserId;
        _notificationService.SendNotification("You have a new donation request.", userId);

        // حفظ الطلب في قاعدة البيانات
        var request = new Request
        {
            PatientId = patientId,
            DonorId = donorId,
        };
        _requestRepository.AddRequest(request);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RespondToRequest(int requestId, bool isAccepted)
    {
        var request = _requestRepository.GetRequestById(requestId);
        if (request == null) return NotFound();

        var userId = unitOfWork.DonorRepository.GetById(request.PatientId).UserId;
        
        if (isAccepted)
        {
                // قبول الطلب

            _notificationService.SendNotification("Your request has been accepted.",userId);

            // كشف معلومات الاتصال
            RevealContactInfo(request.PatientId, request.DonorId);

            // حذف الطلبات الأخرى
            DeleteOtherRequests(request.DonorId, requestId);

            // منع المتبرع من تلقي طلبات جديدة لفترة محددة
            BlockDonor(request.DonorId, 30); // فترة تعليق لمدة 30 يوما
        }
        else
        {
            // رفض الطلب
            _notificationService.SendNotification("Your request has been declined.", userId);

            // حذف الطلب
            _requestRepository.RemoveRequest(requestId);
        }

        _requestRepository.SaveChanges();
        return RedirectToAction("Index");
    }

    private void RevealContactInfo(int patientId, int donorId)
    {
        // تنفيذ كشف معلومات الاتصال
        var patient = _userRepository.GetUserById(patientId);
        var donor = _userRepository.GetUserById(donorId);

        if (patient != null && donor != null)
        {
            // إرسال المعلومات إلى كل من المريض والمتبرع
            var patientUserId = unitOfWork.PatientRepository.GetById(patientId).UserId;
            _notificationService.SendNotification($"Donor contact info: {donor.PhoneNumber}", patientUserId);
            var donorUserId = unitOfWork.PatientRepository.GetById(donorId).UserId;
            _notificationService.SendNotification($"Patient contact info: {patient.PhoneNumber}", donorUserId);
        }
    }

    private void DeleteOtherRequests(int donorId, int acceptedRequestId)
    {
        var otherRequests = _requestRepository.GetRequestsForDonor(donorId).Where(r => r.Id != acceptedRequestId).ToList();
        foreach (var request in otherRequests)
        {
            _requestRepository.RemoveRequest(request.Id);
        }
    }

    private void BlockDonor(int donorId, int days)
    {
        var donor = unitOfWork.DonorRepository.GetById(donorId);
        if (donor != null)
        {
            donor.LastDonationDate = DateTime.Now;
        }
    }
    }
}
