using BLL.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public IPatientRepository PatientRepository { get; set; }
        public IDonorRepository DonorRepository { get; set ; }
        public IHospitalRepository HospitalRepository { get; set; }
        public IRequestRepository RequestRepository { get; set; }
        public IFollowUpFormRepository FollowUpFormRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            PatientRepository = new PatientRepository(context);
            DonorRepository = new DonorRepository(context);
            HospitalRepository = new HospitalRepository(context);
            RequestRepository = new RequestRepository(context);
            FollowUpFormRepository = new FollowUpFormRepository(context);
        }

        public int Complete()
            => context.SaveChanges();
    }
}
