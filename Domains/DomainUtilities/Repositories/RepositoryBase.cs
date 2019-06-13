using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace AIT.DomainUtilities.Repositories
{
    public abstract class RepositoryBase<T, TContext> : IRepositoryBase<T>
        where T : class
        where TContext : DbContext
    {
        protected TContext Context { get; private set; }                    //do I really need it? - yes for eager loading

        protected RepositoryBase(IUnitOfWork<TContext> unitOfWork)
        {
            Context = unitOfWork.Context;
        }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();            
        }

        //public T Find(int id)
        //{
        //    return Context.Set<T>().Find(id);
        //}

        public void Insert(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        //public void Update(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        //public void Delete(int id)
        //{
        //    var entity = Find(id);
        //    Context.Set<T>().Remove(entity);
        //}
    }
}