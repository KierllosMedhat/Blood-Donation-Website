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
    public class DonorRepository : GenericRepository<Donor>, IDonorRepository
    {
        private readonly ApplicationDbContext context;

        public DonorRepository(ApplicationDbContext context) : base(context) { }

        public IEnumerable<Donor> GetByBloodType(BloodType bloodType)
            => context.Donors.Where(donor => donor.BloodType == bloodType);

        public IEnumerable<Donor> GetByGovernorate(string governorate)
            => context.Donors.Where(donor => donor.Governorate == governorate);

        public IEnumerable<Donor> GetByProvince(string governorate, string province)
            => context.Donors.Where(donor => donor.Governorate == governorate && donor.Province == province);
    }
}
