using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using barker.data;

namespace barker.repository.Interfaces
{
    public interface IBarkRepository : IDisposable
    {
        IEnumerable<Bark> GetAllBarks();
        IEnumerable<Bark> GetBarksByUser(Guid userID);
        Bark GetBarkByID(Guid barkId);
        void InsertBark(Bark bark);
        void DeleteBark(Guid barkId);
        void UpdateBark(Bark bark);
        void Save();
    }
}
