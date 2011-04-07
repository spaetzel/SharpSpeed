using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSpeed.Interfaces
{
    public interface IComment
    {
        string Body { get; set; }
        DateTimeOffset CreatedAt { get; }
    }
}
