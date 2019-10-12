using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InDev.Core
{
    public class SimpleCollection : ISimpleCollection
    {
        public List<SimpleObject> Objects { get; private set; }
        public List<SimpleLink> Links { get; private set; }
        public DateTime Actual { get; }

        public SimpleCollection(string objectsPath, string linksPath, bool newTime)
        {
            if (newTime) {
                Actual = DateTime.Now;
                Load(objectsPath, linksPath);
            } else
            {
                DateTime t1 = File.GetLastWriteTime(objectsPath);
                DateTime t2 = File.GetLastWriteTime(linksPath);
                Actual = t1 > t2 ? t1 : t2;
                //NoLoad
            }
        }

        public void Load(string objectsPath, string linksPath)
        {
            Objects = File.ReadLines(objectsPath).Select(line => new SimpleObject(line)).ToList();
            Links = File.ReadLines(linksPath).Select(line => new SimpleLink(line)).ToList();
        }
    }
}
