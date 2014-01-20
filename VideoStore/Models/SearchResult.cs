using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoStore.Models
{
    public class SearchResult
    {
        public List<Movie> Results { get; set; }
        public int Count { get; set; }
        public int StartingFrom { get; set; }
        public int TotalSize { get; set; }
    }
}