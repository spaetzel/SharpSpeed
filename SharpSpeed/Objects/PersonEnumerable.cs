using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SharpSpeed
{
    [JsonObject]
    public class PersonEnumerable : IEnumerable<Person>
    {

        /// <summary>
        /// The private collection of items
        /// </summary>
        [JsonProperty("friends", NullValueHandling = NullValueHandling.Ignore)]
        private Person[] Items { get; set; }


        #region IEnumerable<T> methods
        public IEnumerator<Person> GetEnumerator()
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
