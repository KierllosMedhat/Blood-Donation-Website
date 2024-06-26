using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        //[ForeignKey("Entity")]
        public int EntityId { get; set; }
        //public BaseEntity Entity { get; set; }
    }
}
