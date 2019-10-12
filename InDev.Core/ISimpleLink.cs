using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InDev.Core
{
    public interface ISimpleLink
    {
        int Start { get; }
        int End { get; }
        int Id { get; }
    }
}
