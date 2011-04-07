using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpSpeed.Interfaces;
using Newtonsoft.Json;

namespace SharpSpeed
{
    [JsonObject]
    public class Geo : IGeo
    {
     
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type
        {
            get;
            set;
        }

        [JsonProperty("coordinates", NullValueHandling = NullValueHandling.Ignore)]
        public float[] Coordinates
        {
            get;
            set;
        }

        
    }
}
