using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Linq;

namespace SharpTunesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
          
            var page =DownloadiTunesUrl("http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewRoom?fcId=374710547&genreIdString=1301&mediaTypeString=Podcasts");

            string regex = "subscribePodcast\\?id=([0-9]*)";

            var matches = Regex.Matches(page, regex);

            var ids = from Match m in matches
                      select Convert.ToInt32 (m.Groups[1].Value);



            var respository = SharpTunes.SharpTunesRepository.Instance;

            foreach (var curId in ids.Take(10))
            {
                var result = respository.GetObjectById(curId);

                Console.WriteLine("{0}: {1} - {2} - {3}", curId, result.CollectionName, result.PrimaryGenreName, GetPodcastFeedUrl(curId ) );
            }

            Console.ReadLine();
        }

        private static string DownloadiTunesUrl(string url)
        {
            WebClient client = new WebClient();
            client.Headers["user-agent"] = "iTunes/9.1.1";

            var page = client.DownloadString(url);
            
            return page;
        }

        private static string GetPodcastFeedUrl(int podcastId)
        {
            string plist = DownloadiTunesUrl(String.Format("https://buy.itunes.apple.com/WebObjects/MZFinance.woa/wa/com.apple.jingle.app.finance.DirectAction/subscribePodcast?id={0}&wasWarnedAboutPodcasts=true", podcastId));

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

                return feedUrlValue;
            }
            else
            {
                return "";
            }

      
        }

    
    }
}
