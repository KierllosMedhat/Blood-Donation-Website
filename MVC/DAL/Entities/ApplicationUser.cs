using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [NotMapped]
    public class ApplicationUser : IdentityUser
    {
        public string key { get; set; }
        public int EntityId { get; set; }
        public BaseEntity Entity { get; set; }
    }
}
