using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSpeed.Interfaces;

namespace SharpSpeed.Json
{
    public class GeoConverter : Newtonsoft.Json.Converters.CustomCreationConverter<IGeo>
    {
        public override IGeo Create(Type objectType)
        {
            return new Geo();
        }
    }
}
