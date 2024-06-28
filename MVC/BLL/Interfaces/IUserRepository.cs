using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserRepository
    {
        ApplicationUser? GetUserById(int userId); // تعديل التعريف ليكون Nullable
        void UpdateUser(ApplicationUser user);
    }
}
