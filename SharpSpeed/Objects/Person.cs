using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSpeed.Interfaces;
using Newtonsoft.Json;

namespace SharpSpeed
{
    [JsonObject]
    public class Person : IPerson
    {

        [JsonProperty("goal", NullValueHandling = NullValueHandling.Ignore)]
        public string Goal
        {
            get;
            set;
        }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public string Location
        {
            get;
            set;
        }

        [JsonProperty("time_zone", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone
        {
            get;
            set;
        }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url
        {
            get;
            set;
        }

        [JsonProperty("photo_url", NullValueHandling = NullValueHandling.Ignore)]
        public string PhotoUrl
        {
            get;
            set;
        }

        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName
        {
            get;
            set;
        }



        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username
        {
            get;
            private set;
        }

     
    }
}
