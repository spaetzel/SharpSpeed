using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSpeed.Interfaces
{
    public interface IDistance
    {
        float Value { get; set; }
        string Units { get; set; }
    }
}
