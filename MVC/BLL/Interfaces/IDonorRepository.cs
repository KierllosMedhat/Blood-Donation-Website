using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDonorRepository : IGenericRepository<Donor>
    {
        IEnumerable<Donor> GetByBloodType(BloodType bloodType);
        IEnumerable<Donor> GetByGovernorate(string governorate);
        IEnumerable<Donor> GetByProvince(string governorate, string province);
    }
}
