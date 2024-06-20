using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class Hospital : BaseEntity
    {
        [Required]
        public string HospitalName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Governorate { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Address { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
