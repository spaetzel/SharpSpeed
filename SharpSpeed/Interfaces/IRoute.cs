using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSpeed.Interfaces
{
    public interface IRoute
    {
        int Id { get; }
        string ActivityType { get; set; }
        string Name { get; set; }
        string Location { get; set; }
        string EncodedSamples { get; }
        IDistance Distance { get; set; }
        IGeo Geo { get; set; }
    }
}
