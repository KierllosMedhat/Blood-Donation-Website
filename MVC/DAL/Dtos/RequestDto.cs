using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dtos
{
    public class RequestDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DonorId { get; set; }

        [Required]
        public BloodType BloodType { get; set; }
        [Required]
        public int NumOfBags { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [Required]
        public string Governorate { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public RequestState State { get; set; } = RequestState.Proccesing;
    }
}
