using System;

namespace InDev.Core
{ 
    public class SimpleObject : ISimpleObject
    {
        public int Id { get; }
        public SimpleObject(string id)
        {
            try
            {
                Id = Convert.ToInt32(id);
            }
            catch (Exception)
            {
                Id = -1; //
            }
        }
    }
}
