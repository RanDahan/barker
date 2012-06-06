using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace barker.repository.Interfaces
{
    public interface ICRUDRepository<T>
    {
        IQueryable<T> GetAll();
        T GetById(Guid id);
        Guid Insert(T model);
        Guid Update(T model);
        void Delete(T model);
    }
}
