using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IPatientRepository PatientRepository { get; set; }
        public IDonorRepository DonorRepository { get; set; }
        public IHospitalRepository HospitalRepository { get; set; }
        public IRequestRepository RequestRepository { get; set; }
        public IFollowUpFormRepository FollowUpFormRepository { get; set; }
        public int Complete();
    }
}
