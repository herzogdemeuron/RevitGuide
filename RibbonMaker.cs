using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

namespace RevitAssist.RevitGuide
{
    public class RibbonMaker
    {
        string TabName = "HdM";


        public void Create(UIControlledApplication uiCtrlApp)
        {
            List<RibbonPanel> panelList = uiCtrlApp.GetRibbonPanels(TabName);
            if (panelList == null || panelList.Count == 0)
            {
               uiCtrlApp.CreateRibbonTab(TabName);
            }
            RibbonPanel panel = uiCtrlApp.CreateRibbonPanel(TabName, "GENERAL");
            PushButtonData buttonData = new PushButtonData(
                "RevitAssistButton",
                "Revit Guide",
                Assembly.GetExecutingAssembly().Location,
                "RevitAssist.RevitGuide.MainWindowButton");
            PushButton button = panel.AddItem(buttonData) as PushButton;
            Uri uriImage = new Uri("pack://application:,,,/RevitGuide;component/Resources/icon.png", UriKind.Absolute);
            button.LargeImage = new BitmapImage(uriImage);

        }

    }
}
