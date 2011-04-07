using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SharpSpeed.Interfaces;

namespace SharpSpeed
{
    [JsonObject]
    public class Like : ILike
    {
        #region IComment Members

     
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTimeOffset CreatedAt
        {
            get;
            private set;
        }

        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public Person User
        {
            get;
            set;
        }

        #endregion
    }
}
