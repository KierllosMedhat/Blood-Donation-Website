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
            this.context = context;
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync()
        {
            return await context.Announcements.ToListAsync();
        }
        public async Task<Announcement> GetAnnouncementByIdAsync(int id)
        {
            return await context.Announcements.FindAsync(id);
        }

        public async Task CreateAnnouncementAsync(Announcement announcement)
        {
            context.Announcements.Add(announcement);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAnnouncementAsync(Announcement announcement)
        {
            context.Entry(announcement).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAnnouncementAsync(int id)
        {
            var announcement = await context.Announcements.FindAsync(id);
            if (announcement != null)
            {
                context.Announcements.Remove(announcement);
                await context.SaveChangesAsync();
            }
        }

        public IEnumerable<Announcement> GetAllAnnouncementsAsyncById(int id)
            => context.Announcements.Where(ann => ann.UserId == id);
    }
}
