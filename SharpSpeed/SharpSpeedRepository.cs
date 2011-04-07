using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSpeed.Interfaces;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SharpSpeed.Properties;
using System.Xml.Linq;

namespace SharpSpeed
{
    public class SharpSpeedRepository
    {
        private static SharpSpeedRepository _instance = new SharpSpeedRepository();
        private static readonly Settings _settings = Settings.Default;

        private SharpSpeedRepository()
        {
        }

        public static SharpSpeedRepository Instance
        {
            get
            {
                return _instance;
            }
        }

        public IPerson GetPerson(string username)
        {
            try
            {
                StringParamCheck("username", username);
                var requestPath = string.Format("{0}{1}{2}", _settings.PersonPath, username, _settings.PersonSuffix);
                using (var resp = ProcessRequest(requestPath, "GET", null))
                {
                    var respContent = ReadResponseContent(resp);
                    var respNote = JsonConvert.DeserializeObject<Person>(respContent);
                    return respNote;
                }
            }
            catch (WebException ex)
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
            catch (Exception) { throw; }
        }

        public IEnumerable<double[]> GetRoute(int id)
        {
            string gpx = GetRouteGpx(id);

            return ParseGpx(gpx);
        }

        private IEnumerable<double[]> ParseGpx(string gpx)
        {
            XDocument doc = XDocument.Parse(gpx);

            XNamespace ns = "http://www.topografix.com/GPX/1/1";

            var segments = doc.Element(ns + "gpx").Element(ns+ "trk").Elements(ns + "trkseg");

            return from pt in segments.Elements(ns + "trkpt")
                   select new double[] { Convert.ToDouble(pt.Attribute("lon").Value), Convert.ToDouble(pt.Attribute("lat").Value) };
        }

        public string GetRouteGpx(int id)
        {
             string requestPath = string.Format("{0}{1}{2}", _settings.RoutePath, id, _settings.RouteSuffix);

             using (var resp = ProcessRequest(requestPath, "GET", null))
             {
                 var respContent = ReadResponseContent(resp);

                 return respContent;
             }

        }

        /// <summary>
        /// Gets the index of routes for a person
        /// </summary>
        /// <returns>An INoteEnumerable of T notes</returns>
        public IEnumerable<Route> GetRoutes(string username)
        {
            try
            {
                StringParamCheck("username", username);

                string requestPath = string.Format("{0}{1}{2}", _settings.RoutesPath, username, _settings.RoutesSuffix);

                using (var resp = ProcessRequest(requestPath, "GET", null))
                {
                    var respContent = ReadResponseContent(resp);
                    var notes = JsonConvert.DeserializeObject<RouteEnumerable>(respContent);
                    return notes;
                }
            }
            catch (WebException ex)
            {
                var resp = (HttpWebResponse)ex.Response;
                switch (resp.StatusCode)
                {
                    default:
                        throw;
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Gets the index of entries for a person
        /// </summary>
        /// <param name="page">page of results to return, starting with 1</param>
        /// <param name="since">Fetch all entries with a unix timestamp greater than the given since.</param>
        /// <param name="until">Fetch all entries with a unix timestamp less than or equal to the given until.</param>
        /// <returns>The most recent entries for the user</returns>
        public IEnumerable<Entry> GetEntries(string username, int? page = null, int? until = null, int? since = null)
        {
            StringParamCheck("username", username);

            return GetEntriesStream(username, page, until, since);
        }


        /// <summary>
        /// Gets the index of public entries
        /// </summary>
        /// <param name="page">page of results to return, starting with 1</param>
        /// <param name="since">Fetch all entries with a unix timestamp greater than the given since.</param>
        /// <param name="until">Fetch all entries with a unix timestamp less than or equal to the given until.</param>
        /// <returns>The most recent entries for all users</returns>
        public IEnumerable<Entry> GetEntries( int? page = null, int? until = null, int? since = null)
        {
          

            return GetEntriesStream(null, page, until, since);
        }




        private IEnumerable<Entry> GetEntriesStream(string username, int? page = null, int? until = null, int? since = null)
        {
            try
            {
                
                string requestPath;

                if (!string.IsNullOrEmpty(username))
                {
                    requestPath = string.Format("{0}{1}{2}", _settings.EntriesPath, username, _settings.EntriesSuffix);
                }
                else
                {
                    requestPath = string.Format("{0}", _settings.PublicEntriesPath);
                }


                List<string> queryParams = new List<string>();

                if (page != null)
                {
                    queryParams.Add(String.Format("page={0}", page));
                }

                if (until != null)
                {
                    queryParams.Add(String.Format("until={0}", until));
                }

                if (since != null)
                {
                    queryParams.Add(String.Format("since={0}", since));
                }



                using (var resp = ProcessRequest(requestPath, "GET", queryParams ))
                {
                    var respContent = ReadResponseContent(resp);
                    var notes = JsonConvert.DeserializeObject<EntryEnumerable>(respContent);
                    return notes;
                }
            }
            catch (WebException ex)
            {
                var resp = (HttpWebResponse)ex.Response;
                switch (resp.StatusCode)
                {
                    default:
                        throw;
                }
            }
            catch (Exception) { throw; }
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
                    url += "?" + string.Join( "&", queryParams.ToArray() );
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

        /// <summary>
        /// String parameter helper method.
        /// Checks for null or empty, throws ArgumentNullException if true
        /// </summary>
        /// <param name="paramName">The name of the paramter being checked</param>
        /// <param name="value">The value to check</param>
        private void StringParamCheck(string paramName, string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(paramName, "Value must not be null or string.Empty");
        }



    }
}
