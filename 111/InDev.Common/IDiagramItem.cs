using System.Collections.Generic;

namespace InDev.Common
{
    public interface IDiagramItem 
    {
        List<DiagramItemData> DesignerItems { get; set; }
        List<int> ConnectionIds { get; set; }
    }
}
