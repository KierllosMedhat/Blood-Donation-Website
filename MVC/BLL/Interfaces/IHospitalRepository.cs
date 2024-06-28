using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHospitalRepository : IGenericRepository<Hospital>
    {
        Task<IEnumerable<Hospital>> GetAllHospitalsAsync();
        Task<Hospital> GetHospitalByIdAsync(int id);
        Task<Hospital> GetHospitalByNameAsync(string name);
        Task DeleteHospitalAsync(int id);
        Task<Hospital> GetHospitalDetailsAsync(int id);
    }
}
