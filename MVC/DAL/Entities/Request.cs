using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Request
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Patient Patient { get; set; }
        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public Donor Donor { get; set; }
        [Required]
        [ForeignKey("Donor")]
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
