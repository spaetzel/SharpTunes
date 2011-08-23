using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace SharpTunesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            client.Headers["user-agent"] = "iTunes/9.1.1";

            var page = client.DownloadString("http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewRoom?fcId=374710547&genreIdString=1301&mediaTypeString=Podcasts");

            string regex = "subscribePodcast\\?id=([0-9]*)";

            var matches = Regex.Matches(page, regex);

            var ids = from Match m in matches
                      select Convert.ToInt32 (m.Groups[1].Value);



            var respository = SharpTunes.SharpTunesRepository.Instance;

            foreach (var curId in ids.Take(10))
            {
                var result = respository.GetObjectById(curId);

                Console.WriteLine("{0} {1} {2}", curId, result.CollectionName, result.PrimaryGenreName);
            }

            Console.ReadLine();
        }
    }
}
