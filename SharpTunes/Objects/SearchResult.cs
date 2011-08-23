using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpTunes.Interfaces;
using Newtonsoft.Json;

namespace SharpTunes
{
    [JsonObject]
    public class SearchResult : ISearchResult
    {
        #region ISearchResult Members

        [JsonProperty("kind", NullValueHandling = NullValueHandling.Ignore)]
        public string Kind
        {
            get;
            set;
        }

        [JsonProperty("artistId", NullValueHandling = NullValueHandling.Ignore)]
        public int? ArtistId
        {
            get;
            set;
        }

        [JsonProperty("artistName", NullValueHandling = NullValueHandling.Ignore)]
        public string ArtistName
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }

        public string Version
        {
            get;
            set;
        }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description
        {
            get;
            set;
        }

        public DateTime ReleaseDate
        {
            get;
            set;
        }

        public string SellerName
        {
            get;
            set;
        }

        public string Currency
        {
            get;
            set;

        }

        public string Features
        {
            get;
            set;
        }

        public int? TrackId
        {
            get;
            set;
        }

        [JsonProperty("trackName", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackName
        {
            get;
            set;
        }

        public string SupportedDevices
        {
            get;
            set;
        }

        public string WrapperType
        {
            get;
            set;
        }

        [JsonProperty("collectionId", NullValueHandling = NullValueHandling.Ignore)]
        public int? CollectionId
        {
            get;
            set;
        }

        [JsonProperty("collectionName", NullValueHandling = NullValueHandling.Ignore)]
        public string CollectionName
        {
            get;
            set;
        }

        public string CollectionCensoredName
        {
            get;
            set;
        }

        public string TrackCensoredName
        {
            get;
            set;
        }

        public string ArtistViewUrl
        {
            get;
            set;
        }


        [JsonProperty("collectionViewUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string CollectionViewUrl
        {
            get;
            set;
        }

        public string TrackViewUrl
        {
            get;
            set;
        }

        public string PreviewUrl
        {
            get;
            set;
        }

        public string ArtworkUrl30
        {
            get;
            set;
        }

        public string ArtworkUrl60
        {
            get;
            set;
        }

        [JsonProperty("artworkUrl100", NullValueHandling = NullValueHandling.Ignore)]
        public string ArtworkUrl100
        {
            get;
            set;
        }

        [JsonProperty("primaryGenreName", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimaryGenreName
        {
            get;
            set;
        }

        public string ShortDescription
        {
            get;
            set;
        }

        public string LongDescription
        {
            get;
            set;
        }

        #endregion
    }
}
