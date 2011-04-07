using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSpeed.Interfaces;
using Newtonsoft.Json;

namespace SharpSpeed
{
    [JsonObject]
    public class Distance : IDistance
    {
        #region IDistance Members

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        float IDistance.Value
        {
            get;
            set;
        }

        [JsonProperty("units", NullValueHandling = NullValueHandling.Ignore)]
        string IDistance.Units
        {
            get;
            set;
        }

        #endregion
    }
}
