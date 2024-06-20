using BLL.Interfaces;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class FollowUpFormRepository : GenericRepository<FollowUpForm>, IFollowUpFormRepository
    {
        private readonly ApplicationDbContext context;

        public FollowUpFormRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
