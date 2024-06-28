using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        IEnumerable<Request> GetRequestByBloodType(BloodType bloodType);
        IEnumerable<Request> GetRequestByLocation(string governorate, string province);
        
        IEnumerable<Request> GetRequestByDonorId(int donor);
        IEnumerable<Request> GetRequestByPatientId(int patient);
        
        void AcceptRequest(Request request);
        void RefuseRequest(Request request);


        Request? GetRequestById(int requestId);
        IEnumerable<Request> GetRequestsForDonor(int donorId);
        void AddRequest(Request request);
        void RemoveRequest(int requestId);
        void SaveChanges();


    }
}
