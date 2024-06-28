using DAL.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Message { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public bool IsRead { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
