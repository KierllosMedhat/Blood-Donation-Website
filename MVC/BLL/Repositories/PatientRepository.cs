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
    public class PatientRepository : GenericRepository<Patient>,IPatientRepository
    {
        private readonly ApplicationDbContext context;

        public PatientRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Patient GetByUserId(string id)
            => context.Patients.Where(patient => patient.UserId == id).FirstOrDefault();
    }
}
