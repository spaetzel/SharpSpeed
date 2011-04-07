using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSpeed.Interfaces;
using Newtonsoft.Json;

namespace SharpSpeed
{
    [JsonObject]
    public class Route : IRoute
    {

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id
        {
            get;
            private set;
        }

        [JsonProperty("activity_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ActivityType
        {
            get;
            set;
        }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name
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

        [JsonProperty("encoded_samples", NullValueHandling = NullValueHandling.Ignore)]
        public string EncodedSamples
        {
            get;
            private set;
        }

        [JsonProperty("distance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(SharpSpeed.Json.DistanceConverter))]
        public IDistance Distance
        {
            get;
            set;
        }

        [JsonProperty("geo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(SharpSpeed.Json.GeoConverter))]
        public IGeo Geo
        {
            get;
            set;
        }

     
    }
}
