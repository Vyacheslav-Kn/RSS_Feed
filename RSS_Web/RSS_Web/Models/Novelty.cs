using System;

namespace RSS_Web.Models
{
    public class Novelty
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public virtual RssSource Source { get; set; }
        public int? RssSourceId { get; set; }
    }
}
