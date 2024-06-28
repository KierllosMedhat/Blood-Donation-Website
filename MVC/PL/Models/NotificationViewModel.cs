using DAL.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{

    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; }
        public int UserId { get; set; }
    }
}
