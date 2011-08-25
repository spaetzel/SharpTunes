using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpTunes.Interfaces;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SharpTunes.Properties;
using System.Xml.Linq;

namespace SharpTunes
{
    public class SharpTunesRepository
    {
        private static SharpTunesRepository _instance = new SharpTunesRepository();
        private static readonly Settings _settings = Settings.Default;

        private SharpTunesRepository()
        {
        }

        public static SharpTunesRepository Instance
        {
            get
            {
                return _instance;
            }
        }

        public SearchResultEnumerable SearchObjects(string term, string country = "US", string media = "all", string entity = null, string attribute = null, int limit = 50, string lang = "en_us", int version = 2, bool explicitResults = true)
        {
            var requestPath = string.Format("{0}", _settings.SearchPath);

            List<string> parameters = new List<string>(){
                String.Format("term={0}", term ),
                String.Format("country={0}", country),
                String.Format("media={0}", media),
                String.Format("limit={0}", limit),
                String.Format("lang={0}", lang),
                String.Format("version={0}", version),
                String.Format("explicit={0}", explicitResults ? "Yes" : "No" )
            };

            if( entity != null )
            {
                parameters.Add(String.Format("entity={0}", entity));
            }

            if (attribute != null)
            {
                parameters.Add(String.Format("attribute={0}", attribute));
            }

            using (var resp = ProcessRequest(requestPath, "GET", parameters))
            {
                var respContent = ReadResponseContent(resp);
                var notes = JsonConvert.DeserializeObject<SearchResultEnumerable>(respContent);

                return notes;
            }
        }

        public ISearchResult GetObjectById(int id)
        {
          //  try
            {
         
                var requestPath = string.Format("{0}", _settings.LookupPath);
                using (var resp = ProcessRequest(requestPath, "GET", new List<String>(){
                    String.Format("id={0}", id)
                }))
                {
                    var respContent = ReadResponseContent(resp);
                    var notes = JsonConvert.DeserializeObject<SearchResultEnumerable>(respContent);

                    var respNote = notes.First();
                    return respNote;
                }
            }
    /*        catch (WebException ex)
            {
                var resp = (HttpWebResponse)ex.Response;
                switch (resp.StatusCode)
                {
                    //404
                   case HttpStatusCode.NotFound:
                        throw new SharpSpeedNonExistentPersonException(username, ex);
                    //401
                    case HttpStatusCode.Unauthorized:
                        throw new SharpSpeedAuthorisationException(ex);
                    default:
                        throw;
                }
            }
            catch (Exception) { throw; } */
        }

        /// <summary>
        /// Generic method to process a request to dailymile.
        /// All publicly expose methods which interact with the store are processed though this.
        /// </summary>
        /// <param name="requestPath">The path to the request to be processed</param>
        /// <param name="method">The HTTP method for the request</param>
        /// <param name="content">The content to send in the request</param>
        /// <param name="queryParams">Queryparameters for the request</param>
        /// <returns>An HttpWebResponse continaing details returned from dailymile</returns>
        private static HttpWebResponse ProcessRequest(string requestPath, string method,
                                                      IEnumerable<string> queryParams = null, string content = null)
        {
            try
            {
                var url = string.Format("{0}{1}{2}", _settings.Scheme, _settings.Domain, requestPath);
                if (queryParams != null && queryParams.Count() > 0)
                {
                    url += "?" + string.Join("&", queryParams.ToArray());
                }

                var req = WebRequest.Create(url) as HttpWebRequest;
                req.CookieContainer = new CookieContainer();
                req.Method = method;

                if (string.IsNullOrEmpty(content)) req.ContentLength = 0;
                else
                {
                    using (var sw = new StreamWriter(req.GetRequestStream()))
                    {
                        sw.Write(content);
                    }
                }

                return (HttpWebResponse)req.GetResponse();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads the content from the response object
        /// </summary>
        /// <param name="resp">The response to be processed</param>
        /// <returns>A string of the response content</returns>
        private static string ReadResponseContent(HttpWebResponse resp)
        {
            if (resp == null) throw new ArgumentNullException("resp");
            using (var sr = new StreamReader(resp.GetResponseStream()))
            {
                return sr.ReadToEnd();

            }
        }


    }
}
