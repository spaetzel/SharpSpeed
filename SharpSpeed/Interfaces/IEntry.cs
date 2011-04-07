using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSpeed.Interfaces
{
    public interface IEntry
    {
        int Id { get; }
        string Url { get; }
        DateTimeOffset At { get; set; }
        string Message { get; set; }
    }
}
