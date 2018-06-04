using System.Data.Entity;
using RSS_Web.Models;

namespace RSS_Web.Database
{
    public class RssContext: DbContext
    {
        public RssContext():base("DbConnection")
        { }

        public DbSet<RssSource> Sources { get; set; }
        public DbSet<Novelty> News { get; set; }
    }
}
