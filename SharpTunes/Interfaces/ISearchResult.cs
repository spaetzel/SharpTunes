using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTunes.Interfaces
{
    public interface ISearchResult
    {
        string Kind { get; set; }
        int? ArtistId { get; set; }
        string ArtistName { get; set; }
        Double Price { get; set; }
        string Version { get; set; }
        string Description { get; set; }
        DateTime ReleaseDate { get; set; }
        String SellerName { get; set; }
        string Currency { get; set; }
        string Features { get; set; }
        int? TrackId { get; set; }
        string TrackName { get; set; }
        string SupportedDevices { get; set; }
        string WrapperType { get; set; }
        int? CollectionId { get; set; }
        string CollectionName { get; set; }
        string CollectionCensoredName { get; set; }
        string TrackCensoredName { get; set; }
        string ArtistViewUrl { get; set; }
        string CollectionViewUrl { get; set; }
        string TrackViewUrl { get; set; }
        string PreviewUrl { get; set; }
        string ArtworkUrl30 { get; set; }
        string ArtworkUrl60 { get; set; }
        string ArtworkUrl100 { get; set; }
        string PrimaryGenreName { get; set; }
        string ShortDescription { get; set; }
        string LongDescription { get; set; }


    }
}
