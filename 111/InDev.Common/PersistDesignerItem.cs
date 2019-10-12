﻿namespace InDev.Common
{
    public class PersistDesignerItem : DesignerItemBase
    {
        public PersistDesignerItem(int id, double left, double top, double itemWidth, double itemHeight, string hostUrl) 
            : base(id, left, top, itemWidth, itemHeight)
        {
            this.HostUrl = hostUrl;
        }

        public string HostUrl { get; set; }

    }
}
