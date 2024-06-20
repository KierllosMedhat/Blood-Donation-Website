using BLL.Interfaces;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        private readonly ApplicationDbContext context;

        public RequestRepository(ApplicationDbContext context) : base(context)
        {
        }


        public IEnumerable<Request> GetRequestByBloodType(BloodType bloodType)
            => context.Requests.Where(req => req.BloodType == bloodType);

        public IEnumerable<Request> GetRequestByLocation(string governorate, string province)
            => context.Requests.Where(req => req.Governorate == governorate &&
                                      req.Province == province);

        public void AcceptRequest(Request request)
        {
            request.State = RequestState.Accepted;
            Donor donor = context.Donors.Find(request.DonorId);
            // Delete the rest of the requests for the donor
            var proccesingRequests = donor.Requests.Where(request => request.State == RequestState.Proccesing);
            foreach (var item in proccesingRequests)
                 donor.Requests.Remove(item);
            // Update the last donation date for the donor
            donor.LastDonationDate = DateTime.Now;
        }

        public void RefuseRequest(Request request)
        {
            request.State = RequestState.Refused;
        }
    }
}
