using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFollowUpFormRepository
    {
        FollowUpForm GetById(int id);
        void AddForm(FollowUpForm form);
        IEnumerable<FollowUpForm> GetAll();
        IEnumerable<FollowUpForm> GetAllById(string id);
        void UpdateFollowUpForm(FollowUpForm form);
        void DeleteFollowUpForm(int id);
    }
}
