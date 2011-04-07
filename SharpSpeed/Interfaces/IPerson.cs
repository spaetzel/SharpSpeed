using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSpeed.Interfaces
{
    public interface IPerson
    {
        string Goal { get; set; }
        string Location { get; set; }
        string TimeZone { get; set; }
        string Url { get; set; }
        string PhotoUrl { get; set; }
        string DisplayName { get; set; }
        string Username { get; }
    }
}
