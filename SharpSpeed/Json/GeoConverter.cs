using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSpeed.Json
{
    public class GeoConverter : Newtonsoft.Json.Converters.CustomCreationConverter<Geo>
    {
        public override Geo Create(Type objectType)
        {
            return new Geo();
        }
    }
}
