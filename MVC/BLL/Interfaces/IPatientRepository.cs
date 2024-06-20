using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        //Patient GetById(int? id);
        //IEnumerable<Patient patient> GetAll();
        //void Add(Patient patient);
        //void Update(Patient patient);
        //void Delete(Patient patient);
    }
}
