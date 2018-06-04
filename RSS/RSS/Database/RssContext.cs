using RSS.Entities;
using System.Data.Entity;

namespace RSS.Database
{
    public class RssContext: DbContext
    {
        public RssContext():base("DbConnection")
        { }

        public DbSet<RssSource> Sources { get; set; }
        public DbSet<Novelty> News { get; set; }
    }
}
