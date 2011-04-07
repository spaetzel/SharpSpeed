using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SharpSpeed.Interfaces;

namespace SharpSpeed
{
    [JsonObject]
    public class Entry : IEntry
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id
        {
            get;
            private set;
        }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url
        {
            get;
            private set;
        }

        [JsonProperty("at", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTimeOffset At
        {
            get;
            set;
        }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message
        {
            get;
            set;
        }

        [JsonProperty("comments", NullValueHandling = NullValueHandling.Ignore)]
        public Comment[] Comments
        {
            get;
            private set;
        }


        [JsonProperty("likes", NullValueHandling = NullValueHandling.Ignore)]
        public Like[] Likes
        {
            get;
            private set;
        }

     
    }
}
