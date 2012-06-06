using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using barker.data;

namespace barker.repository.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        User GetUserByID(Guid userId);
        void InsertUser(User user);
        bool IsUserLogedin(Guid userId);
        User GetUserByName(string userName);
        void Save();
    }
}
