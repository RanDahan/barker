using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using barker.data;
using barker.repository.Interfaces;
using barker.repository.LinqToSql;

namespace barker.repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public User GetUserByID(Guid userId)
        {
            return context.Users.Find(userId);
        }       

        public void InsertUser(User user)
        {
            context.Users.Add(user);
        }

        public bool IsUserLogedin(Guid userId)
        {
            return context.Users.Find(userId) != null;
        }

        public User GetUserByName(string userName)
        {
            return context.Users.FirstOrDefault(u => u.Username == userName);
        }
       
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public  List<UserFriend> FollowersCount(User user)
        {            
            return context.Friends.Where(f => f.FriendId == user.ID).ToList();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<User> GetUserFriends(string p)
        {
            var user = GetUserByName(p);
            return context.Friends.Where(f => f.UserId == user.ID).Select(s=>s.Friend).ToList();
        }
    }
}
