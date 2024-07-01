using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAnnouncementRepository
    {
        void AddAnnouncement(Announcement announcement);
        IEnumerable<Announcement> GetAll();
    }
}
