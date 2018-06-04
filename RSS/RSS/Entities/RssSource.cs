using System.Collections.Generic;

namespace RSS.Entities
{
    public class RssSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual ICollection<Novelty> News { get; set; }

        public RssSource()
        {
            News = new List<Novelty>();
        }
    }
}
