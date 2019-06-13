using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace AIT.DomainUtilities
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        TContext Context { get; }
        void Save();
        void Dispose();
    }
}
