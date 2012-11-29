using System;
using System.Xml.Linq;
using System.Linq;
using System.Net;

namespace SharpTunes
{
	public static class Podcasts
	{
		public static Uri GetPodcastFeedUrl(int podcastId ){
			string plist = DownloadiTunesUrl(String.Format("https://buy.itunes.apple.com/WebObjects/MZFinance.woa/wa/com.apple.jingle.app.finance.DirectAction/subscribePodcast?id={0}&wasWarnedAboutPodcasts=true", podcastId));
			
			if( plist.Length > 0 ){
				XDocument doc = XDocument.Parse(plist);
				
				XElement subscribeNode = (from p in doc.Elements("plist").First().Elements("dict").First().Elements("key")
				                          where p.Value == "subscribe-podcast"
				                          select p).FirstOrDefault();
				
				if (subscribeNode != null)
				{
					var dict = subscribeNode.ElementsAfterSelf().First();
					
					var feedNode = (from n in dict.Elements("key")
					                where n.Value == "feedURL"
					                select n).First();
					
					var feedUrlValue = feedNode.ElementsAfterSelf().First().Value;
					
					return new Uri(feedUrlValue);
				}
				else
				{
					throw new Exception("Cannot find subscribe podcast node");
				}
			}else{
				 throw new Exception("Invalid plist returned");
			}
		}

		private static string DownloadiTunesUrl(string url)
		{
			WebClient client = new WebClient();
			client.Headers["user-agent"] = "iTunes/9.1.1";
			
			try{
				var page = client.DownloadString(url);
				
				return page;
			}catch{
				return "";
			}
		}
	}
}

