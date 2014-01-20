using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoStore.Models
{
    public class SearchCriteria
    {
        public string Cast { get; set; }
        public string Classification { get; set; }
        public string Genre { get; set; }
        public int? MovidId { get; set; }
        public int? Rating { get; set; }
        public DateTime? FromReleaseDate { get; set; }
        public DateTime? ToReleaseDate { get; set; }
        public string Title { get; set; }
    }
}