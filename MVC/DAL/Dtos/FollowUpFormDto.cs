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
    public class FollowUpFormDto
    {

        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string UserId { get; set; }
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
    }
}
