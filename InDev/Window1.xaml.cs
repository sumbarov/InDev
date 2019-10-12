using System.Windows;
using System.Configuration;
using DiagramDesigner;
using System.Linq;
using InDev.Core;
using System;
using System.Text;
using System.Windows.Threading;

namespace InDev
{
    public partial class Window1 : Window
    { 
        private Window1ViewModel window1ViewModel;
        private ISimpleCollection simpleCollection = null;
        private string objectsPath = ConfigurationManager.AppSettings["fileObjects"];
        private string linksPath = ConfigurationManager.AppSettings["fileLinks"];
        private DispatcherTimer timer = null;
        private int step = 100;
        private int start = 99;
        private int raws = 1;
        private int maxId = 10000; //NB!!!

        public Window1()
        {
            InitializeComponent();

/*            Generate it..
 
            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();
            for (int i = 1; i < 101; i++)
            {
                s1.AppendLine(i.ToString());
            }
            Random random = new Random();
            int l1 = 0, l2 = 0;
            for (int i = 1; i < 3001; i++)
            {
                l1 = random.Next(3, 94);
                l2 = random.Next(3, 94);
                s2.AppendLine(String.Format("{0};{1}", l1, l2));
            }*/

            window1ViewModel = new Window1ViewModel();
            this.DataContext = window1ViewModel;
            this.Loaded += new RoutedEventHandler(Window1_Loaded);

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Update);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }

        void AddNewLink(SimpleLink newLink)
        {
            SettingsDesignerItemViewModel firstObj = null;
            SettingsDesignerItemViewModel secondObj = null;
            try
            {
                firstObj = (SettingsDesignerItemViewModel)window1ViewModel.DiagramViewModel.Items.First(f => f.Id == newLink.Start);
                secondObj = (SettingsDesignerItemViewModel)window1ViewModel.DiagramViewModel.Items.First(s => s.Id == newLink.End);
            }
            catch (Exception)
            {
                return; //:)
            }
            if (null != firstObj && null != secondObj)
            {
                ConnectorViewModel con1 = new ConnectorViewModel(firstObj.RightConnector, secondObj.TopConnector);
                con1.Parent = window1ViewModel.DiagramViewModel;
                con1.Id = newLink.Id;
                window1ViewModel.DiagramViewModel.Items.Add(con1);
            }

        }

        void DeleteOldLink(SimpleLink oldLink)
        {
            try
            {
                ConnectorViewModel firstObj = (ConnectorViewModel)window1ViewModel.DiagramViewModel.Items.First(f => f.Id == oldLink.Id);
                if (null != firstObj) window1ViewModel.DiagramViewModel.Items.Remove(firstObj);
            }
            catch (Exception) //***
            {

            }
        }

        void AddNew(SimpleObject newObj)
        {
            SettingsDesignerItemViewModel box = new SettingsDesignerItemViewModel();
            box.Parent = window1ViewModel.DiagramViewModel;
            box.Left = start;
            box.Top = start + step * raws++;
            box.Id = newObj.Id;
            window1ViewModel.DiagramViewModel.Items.Add(box);
        }

        void DeleteOld(SimpleObject oldObj)
        {
            SettingsDesignerItemViewModel firstObj = (SettingsDesignerItemViewModel)window1ViewModel.DiagramViewModel.Items.First(f => f.Id == oldObj.Id);
            window1ViewModel.DiagramViewModel.Items.Remove(firstObj);
        }

        void Update(object sender, EventArgs e)
        {
            ISimpleCollection newCollection = new SimpleCollection(objectsPath, linksPath, false);

            if (simpleCollection.Actual < newCollection.Actual)
            {
                newCollection.Load(objectsPath, linksPath);
                //Add new
                foreach (SimpleObject obj in newCollection.Objects)
                {
                    SimpleObject obj2 = null;
                    try
                    {
                        obj2 = simpleCollection.Objects.First(f => f.Id == obj.Id);
                    }
                    catch (Exception) //Doesn't matter
                    {
                        AddNew(obj);
                    }
                }
                //Delete old
                foreach (SimpleObject obj in simpleCollection.Objects)
                {
                    try
                    {
                        newCollection.Objects.First(f => f.Id == obj.Id);
                    } catch (Exception) //Doesn't matter
                    {
                        DeleteOld(obj);
                    }
                }
                //Add new links
                foreach (SimpleLink link in newCollection.Links)
                {
                    SimpleLink link2 = null;
                    try
                    {
                        link2 = simpleCollection.Links.First(f => f.Id == link.Start*maxId + link.End);
                    }
                    catch (Exception) //Doesn't matter
                    {
                        AddNewLink(link);
                    }
                }
                //Delete old links
                foreach (SimpleLink link in simpleCollection.Links)
                {
                    try
                    {
                        newCollection.Links.First(f => f.Id == link.Id);
                    }
                    catch (Exception) //Doesn't matter
                    {
                        DeleteOldLink(link);
                    }
                }

                simpleCollection = newCollection;
            }
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            simpleCollection = new SimpleCollection(objectsPath, linksPath, true);

            int tric = (int)Math.Sqrt(simpleCollection.Objects.Count);
            int x = -1, y = 0;
            foreach (SimpleObject obj in simpleCollection.Objects)
            {
                SettingsDesignerItemViewModel box = new SettingsDesignerItemViewModel();
                box.Parent = window1ViewModel.DiagramViewModel;
                box.Left = start + step*++x;
                box.Top = start + step*y;
                if (x >= tric) { x = -1; y++; raws++; }
                box.Id = obj.Id;
                window1ViewModel.DiagramViewModel.Items.Add(box);
            }
            foreach (SimpleLink link in simpleCollection.Links)
            {
                SettingsDesignerItemViewModel firstObj = null;
                SettingsDesignerItemViewModel secondObj = null;
                try
                {
                    firstObj = (SettingsDesignerItemViewModel)window1ViewModel.DiagramViewModel.Items.First(f => f.Id == link.Start);
                    secondObj = (SettingsDesignerItemViewModel)window1ViewModel.DiagramViewModel.Items.First(s => s.Id == link.End);
                }
                catch (Exception)
                {
                    continue; //:)
                }
                if (null != firstObj && null != secondObj)
                {
                    ConnectorViewModel con1 = new ConnectorViewModel(firstObj.RightConnector, secondObj.TopConnector);
                    con1.Parent = window1ViewModel.DiagramViewModel;
                    con1.Id = maxId * firstObj.Id + secondObj.Id; //:)
                    window1ViewModel.DiagramViewModel.Items.Add(con1);
                }
            }
        }
    }
}
