using BLL.Repositories;
using DAL.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BLL.Interfaces
{
    public interface IAnnouncementRepository : IGenericRepository<Announcement>
    {
      
        Task<IEnumerable<Announcement>> GetAllAnnouncementsAsync();
        IEnumerable<Announcement> GetAllAnnouncementsAsyncById(int id);
        Task<Announcement> GetAnnouncementByIdAsync(int id);
        Task UpdateAnnouncementAsync(Announcement announcement);
        Task DeleteAnnouncementAsync(int id);
    }
}
