using System.Linq;
using RSS.Entities;

namespace RSS.Abstract
{
    public interface IRssRepository
    {
        IQueryable<Novelty> News { get; }
        IQueryable<RssSource> Sources { get; }

        void AddSource(RssSource entity);
        void AddNovelty(Novelty entity);
    }
}
