using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using barker.data;
using barker.repository.Interfaces;
using barker.repository.LinqToSql;

namespace barker.repository.Repositories
{
    public class BarkRepository : IBarkRepository
    {
        private DataContext context;

        public BarkRepository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Bark> GetAllBarks()
        {
            return context.Barks.ToList();            
        }

        public IEnumerable<Bark> GetBarksByUser(Guid userID)
        {            
            return
            context.Barks.Where(t =>
                                 context.Users.FirstOrDefault(u => u.ID == userID).Friends.Select(uf => uf.FriendId).
                                     Contains(t.UserID) || t.UserID == userID).ToList();

            //return context.Barks.Where(t => t.UserID == userID|| user.Friends.Select(g=>g.FriendId).Contains(t.UserID)).ToList();
        }

        public Bark GetBarkByID(Guid barkId)
        {
            return context.Barks.Find(barkId);
        }

        public void InsertBark(Bark bark)
        {
            if (bark.Message.Length > 140)
                bark.Message = bark.Message.Substring(0, 140);                       
            context.Barks.Add(bark);
        }

        public void DeleteBark(Guid barkId)
        {
            Bark bark = context.Barks.Find(barkId);
            context.Barks.Remove(bark);
        }

        public void UpdateBark(Bark bark)
        {
            context.Entry(bark).State = EntityState.Modified;
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
