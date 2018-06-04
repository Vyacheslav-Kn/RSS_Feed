using RSS.Abstract;
using RSS.Entities;
using System.Linq;

namespace RSS.Database
{
    class RssRepository: IRssRepository
    {
        private RssContext context = new RssContext();
        public IQueryable<Novelty> News
        {
            get { return context.News; }
        }
        public IQueryable<RssSource> Sources
        {
            get { return context.Sources; }
        }
        public void AddNovelty(Novelty entity)
        {
            context.News.Add(entity);
            context.SaveChanges();
        }
        public void AddSource(RssSource entity)
        {
            context.Sources.Add(entity);
            context.SaveChanges();
        }
    }
}
