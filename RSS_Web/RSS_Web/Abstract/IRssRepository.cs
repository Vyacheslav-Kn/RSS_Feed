using System.Linq;
using RSS_Web.Models;

namespace RSS_Web.Abstract
{
    public interface IRssRepository
    {
        IQueryable<Novelty> News { get; }
        IQueryable<RssSource> Sources { get; }

        void AddSource(RssSource entity);
        void AddNovelty(Novelty entity);
    }
}
