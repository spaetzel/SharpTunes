using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SharpTunes
{
    [JsonObject]
    public class SearchResultEnumerable : IEnumerable<SearchResult>
    {
        /// <summary>
        /// The private collection of items
        /// </summary>
       [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        private SearchResult[] Items { get; set; }

        [JsonProperty("resultCount", NullValueHandling = NullValueHandling.Ignore)]
        public int ResultCount { get; set; }
        

        #region IEnumerable<T> methods
        public IEnumerator<SearchResult> GetEnumerator()
        {
            return Items.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion    
    }
}
