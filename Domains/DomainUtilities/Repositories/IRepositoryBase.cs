using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIT.DomainUtilities.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll();
        //T Find(int id);
        void Insert(T entity);
        //void Update(T entity);
        void Delete(T entity);
        //void Delete(int id);
    }
}