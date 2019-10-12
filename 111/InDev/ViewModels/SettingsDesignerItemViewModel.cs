using DiagramDesigner;

namespace InDev
{
    public class SettingsDesignerItemViewModel : DesignerItemViewModelBase
    {
        public SettingsDesignerItemViewModel()
        {
            Init();
        }

        private void Init()
        {
            this.ShowConnectors = false;
        }
    }
}
