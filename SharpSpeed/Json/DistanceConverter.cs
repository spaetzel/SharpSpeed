using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;

namespace SharpSpeed.Json
{
    public class DistanceConverter : Newtonsoft.Json.Converters.CustomCreationConverter<Distance>
    {
        public override Distance Create(Type objectType)
        {
            return new Distance();
        }
    }
}
