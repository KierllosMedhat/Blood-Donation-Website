using BLL.Interfaces;
using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class FollowUpFormRepository : IFollowUpFormRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowUpFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddForm(FollowUpForm form)
        {
            _context.FollowUpForms.Add(form);
            _context.SaveChanges();
        }

        public IEnumerable<FollowUpForm> GetAll()
        {
            return _context.FollowUpForms.ToList();
        }

        public void UpdateFollowUpForm(FollowUpForm form)
        {
            _context.FollowUpForms.Update(form);
            _context.SaveChanges();
        }

        public void DeleteFollowUpForm(int id)
        {
            var form = _context.FollowUpForms.Find(id);
            if (form != null)
            {
                _context.FollowUpForms.Remove(form);
                _context.SaveChanges();
            }
        }

        public FollowUpForm GetById(int id)
            => _context.FollowUpForms.Find(id);

        public IEnumerable<FollowUpForm> GetAllById(string id)
            => _context.FollowUpForms.Where(form => form.UserId == id);
    }
}
