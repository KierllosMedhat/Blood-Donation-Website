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
    public class HospitalRepository : GenericRepository<Hospital>, IHospitalRepository
    {
        private readonly ApplicationDbContext context;

        public HospitalRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
