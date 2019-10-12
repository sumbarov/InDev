using DiagramDesigner;

namespace InDev
{
    public class Window1ViewModel : INPCBase
    {
        private int? savedDiagramId; //2do
        private bool isBusy = false; //2do
        private DiagramViewModel diagramViewModel = new DiagramViewModel();

        public Window1ViewModel()
        {
            DiagramViewModel = new DiagramViewModel();

            //For future changes..
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);
            LoadDiagramCommand = new SimpleCommand(ExecuteLoadDiagramCommand);

            ConnectorViewModel.PathFinder = new OrthogonalPathFinder();
        }

        public SimpleCommand CreateNewDiagramCommand { get; private set; }
        public SimpleCommand LoadDiagramCommand { get; private set; }

        public DiagramViewModel DiagramViewModel
        {
            get
            {
                return diagramViewModel;
            }
            set
            {
                if (diagramViewModel != value)
                {
                    diagramViewModel = value;
                    NotifyChanged("DiagramViewModel");
                }
            }
        }

        //2do
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    NotifyChanged("IsBusy");
                }
            }
        }

        //2do
        public int? SavedDiagramId
        {
            get
            {
                return savedDiagramId;
            }
            set
            {
                if (savedDiagramId != value)
                {
                    savedDiagramId = value;
                    NotifyChanged("SavedDiagramId");
                }
            }
        }
                 
        //2do
        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            SavedDiagramId = null;
            DiagramViewModel.CreateNewDiagramCommand.Execute(null);
        }

        //2do
        private void ExecuteLoadDiagramCommand(object parameter)
        {
/*            IsBusy = true;
            DiagramItem wholeDiagramToLoad = null;
            if (SavedDiagramId == null)
            {
                //??
                return;
            }

            Task<DiagramViewModel> task = Task.Factory.StartNew<DiagramViewModel>(() =>
            {
                LoadPerstistDesignerItems(wholeDiagramToLoad, diagramViewModel);

                return diagramViewModel;
            });
            task.ContinueWith((ant) =>
            {
                this.DiagramViewModel = ant.Result;
                IsBusy = false;

            }, TaskContinuationOptions.OnlyOnRanToCompletion);*/
        }
    }
}
