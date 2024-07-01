using BLL.Interfaces;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        private readonly ApplicationDbContext context;

        public AnnouncementRepository(ApplicationDbContext context)
            : base(context) 
        {
            this._context = context;
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync()
        {
            return await _context.Announcements.ToListAsync();
        }
        public async Task<Announcement> GetAnnouncementByIdAsync(int id)
        {
            return await _context.Announcements.FindAsync(id);
        }

        public async Task CreateAnnouncementAsync(Announcement announcement)
        {
            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAnnouncementAsync(Announcement announcement)
        {
            _context.Entry(announcement).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnnouncementAsync(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
                await _context.SaveChangesAsync();
            }
        }
    }
}
