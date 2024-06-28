
using BLL.Interfaces;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
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

        public HospitalRepository(ApplicationDbContext context) : base(context) { this.context = context; }

        public async Task DeleteHospitalAsync(int id)
        {
            var result = await context.Hospitals.Include(h => h.User).FirstOrDefaultAsync(h => h.Id == id);
            context.Hospitals.Remove(result);
            context.SaveChanges();

        }

        public async Task<IEnumerable<Hospital>> GetAllHospitalsAsync()
        {
            try
            {
                return await context.Hospitals.Include(h => h.User).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all hospitals.", ex);
            }
        }

        public async Task<Hospital> GetHospitalByIdAsync(int id)
        {
            try
            {
                return await context.Hospitals.Include(h => h.User).FirstOrDefaultAsync(h => h.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the hospital with ID {id}.", ex);
            }
        }

        public Task<Hospital> GetHospitalByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Hospital> GetHospitalDetailsAsync(int id)
        {
            return await context.Hospitals.Include(h => h.User).FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
