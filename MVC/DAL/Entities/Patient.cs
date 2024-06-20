using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Patient : BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public BloodType BloodType { get; set; }
        [Required]
        public string Governorate { get; set; }
        [Required]
        public string Province { get; set; }

        public List<Request> Requests { get; set; }
        
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
