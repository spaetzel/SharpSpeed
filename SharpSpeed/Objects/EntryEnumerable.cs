﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SharpSpeed
{
    [JsonObject]
    public class EntryEnumerable : IEnumerable<Entry>
    {

        /// <summary>
        /// The private collection of items
        /// </summary>
        [JsonProperty("entries", NullValueHandling = NullValueHandling.Ignore)]
        private Entry[] Items { get; set; }


        #region IEnumerable<T> methods
        public IEnumerator<Entry> GetEnumerator()
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
