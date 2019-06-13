using System;
using System.Data.Entity;

namespace AIT.DomainUtilities
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable
        where TContext : DbContext, new()
    {
        private readonly TContext _context;

        public UnitOfWork()
        {
            _context = new TContext();
        }

        TContext IUnitOfWork<TContext>.Context
        {
            get { return _context; }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
