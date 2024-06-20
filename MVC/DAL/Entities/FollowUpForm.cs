using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class FollowUpForm
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public Request Request { get; set; }
        [Required]
        [ForeignKey("Request")]
        public int RequestId { get; set; }

        public ApplicationUser User { get; set; }
        [ForeignKey("Patient")]
        public string UserId { get; set; } //  Patient, Hospital Or Donor

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [Required]
        public string Question1 { get; set; }
        [Required]
        public string Question2 { get; set; }
        [Required]
        public string Question3 { get; set; }
        [Required]
        public string Question4 { get; set; }
        [Required]
        public string Feedback { get; set; }
        [Required]
        [Range(0,5)]
        public int Rating { get; set; }

    }
}
