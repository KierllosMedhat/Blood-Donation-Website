using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string location { get; set; }
        public string Content { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
       
        public string PhoneNumber { get; set; }

        [ForeignKey("Hospital")]
        public int? UserId { get; set; }
        public Hospital? Hospital { get; set; }


    }
}
