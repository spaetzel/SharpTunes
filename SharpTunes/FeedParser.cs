using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace SharpTunes
{
	public class FeedParser
	{
		private static XNamespace itunesNamespace = "http://itunes.apple.com/rss";

		private Uri feedUri;

		public Uri FeedUri {
			get {
				return feedUri;
			}
		}

		public FeedParser (Uri feedUri)
		{
			this.feedUri= feedUri;
		}

		/**
		 * Loops through the feed and finds the iTunes ids for all objects in the list
		 */
		public IEnumerable<long> GetItunesIds(){
			string FeedText = DownloadFeed(feedUri);
			XDocument doc = XDocument.Parse(FeedText);

			var feed = doc.Elements("feed").First();

			var entries = feed.Elements("entry");

			var ids = from XElement e in entries
				select Convert.ToInt64( e.Elements("id").First().Attributes(itunesNamespace+ "id" ).First().Value );

			return ids;
		}

		private static string DownloadFeed(Uri url)
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

			request.Timeout = 10000;
			
			var response = request.GetResponse();
			
			StreamReader reader = new StreamReader(response.GetResponseStream());
			
			return reader.ReadToEnd();
		}
	}
}

