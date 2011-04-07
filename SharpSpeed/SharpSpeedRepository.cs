using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSpeed.Interfaces;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SharpSpeed.Properties;

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
                var requestPath = string.Format("{0}/{1}.json", _settings.PeoplePath, username);
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
                                                      string queryParams = null, string content = null)
        {
            try
            {
                var url = string.Format("{0}{1}{2}", _settings.Scheme, _settings.Domain, requestPath);
                if (!string.IsNullOrEmpty(queryParams)) url += "?" + queryParams;
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
