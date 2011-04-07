using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SharpSpeed
{
    [JsonObject]
    public class RouteEnumerable : IEnumerable<Route>
    {

        /// <summary>
        /// The private collection of items
        /// </summary>
        [JsonProperty("routes", NullValueHandling = NullValueHandling.Ignore)]
        private Route[] Items { get; set; }


        #region IEnumerable<T> methods
        public IEnumerator<Route> GetEnumerator()
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
