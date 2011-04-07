using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;
using SharpSpeed.Interfaces;

namespace SharpSpeed.Json
{
    public class DistanceConverter : Newtonsoft.Json.Converters.CustomCreationConverter<IDistance>
    {
        public override IDistance Create(Type objectType)
        {
            return new Distance();
        }
    }
}
