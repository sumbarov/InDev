using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InDev.Core
{
    public interface ISimpleCollection
    {
        List<SimpleObject> Objects { get; }
        List<SimpleLink> Links { get; }
        DateTime Actual { get; }

        void Load(string objectsPath, string linksPath);
    }
}
