using System.Data;
using System.Data.Entity;
using System.Linq;

namespace AIT.DomainUtilities.Database
{
    public class BaseContext<TContext> : DbContext where TContext : DbContext
    {
        static BaseContext()
        {
            System.Data.Entity.Database.SetInitializer<TContext>(null);
        }

        protected BaseContext()
            : base("name=WeddingTimeConnection")
        {
        }
    }
}