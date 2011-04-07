using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSpeed.Interfaces
{
    public interface IGeo
    {
        string Type { get; set; }
        double[] Coordinates { get; set; }

    }
}
