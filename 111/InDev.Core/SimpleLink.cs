using System;

namespace InDev.Core
{
    public class SimpleLink : ISimpleLink
    {
        public int Start { get; }
        public int End { get; }
        public int Id { get; }

        public SimpleLink(string line)
        {
            var split = line.Split(';');
            try
            {
                Start = Convert.ToInt32(split[0]);
                End = Convert.ToInt32(split[1]);
                Id = Start * 10000 + End;
            }
            catch (Exception)
            {
                //Hm...Shit happends.
                Start = -1;
                End = -1;
                Id = -1;
            }
        }
    }
}
