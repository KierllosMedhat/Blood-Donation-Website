using BLL.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddAnnouncement(Announcement announcement)
        {
            _context.Announcements.Add(announcement);
            _context.SaveChanges();
        }

        public IEnumerable<Announcement> GetAll()
        {
            return _context.Announcements.ToList();
        }

        public void UpdateAnnouncement(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
            _context.SaveChanges();
        }

        public void DeleteAnnouncement(int id)
        {
            var announcement = _context.Announcements.Find(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
                _context.SaveChanges();
            }
        }
    }
}
