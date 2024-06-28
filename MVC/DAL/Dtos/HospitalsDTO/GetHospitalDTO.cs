using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dtos.HospitalsDTO
{
    public class GetHospitalDTO
    {
        public int Id { get; set; }
        public string HospitalName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Governorate { get; set; }

        public string Province { get; set; }

        public string Address { get; set; }
    }
}