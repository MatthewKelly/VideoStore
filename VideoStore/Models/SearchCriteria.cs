using System;

namespace VideoStore.Models
{
    public class SearchCriteria
    {
        public string Cast { get; set; }
        public string Classification { get; set; }
        public string Genre { get; set; }
        public int? MovidId { get; set; }
        public int? Rating { get; set; }
        public int? ReleaseDate { get; set; }
        public string Title { get; set; }
    }
}