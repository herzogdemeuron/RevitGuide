using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace RevitGuide.Revit
{
    public class RibbonMaker
    {
        public static void Create(UIControlledApplication uiCtrlApp, string tabName, string panelName)
        {
            CreateTab(uiCtrlApp, tabName);
            RibbonPanel panel = CreatePanel(uiCtrlApp, tabName, panelName);
            PushButtonData buttonData = new PushButtonData(
                "RevitGuideButton",
                "Revit Guide",
                Assembly.GetExecutingAssembly().Location,
                "RevitGuide.Revit.StartCommand");
            PushButton button = panel.AddItem(buttonData) as PushButton;
            Uri uriImage = new Uri("pack://application:,,,/RevitGuide;component/Resources/icon.png", UriKind.Absolute);
            button.LargeImage = new BitmapImage(uriImage);
        }


        private static void CreateTab(UIControlledApplication uiCtrlApp, string tabName)
        {
            try
            {
                uiCtrlApp.CreateRibbonTab(tabName);
            }
            catch (Autodesk.Revit.Exceptions.ArgumentException)
            {
                // Tab already exists
            }
        }

        private static RibbonPanel CreatePanel(UIControlledApplication uiCtrlApp, string tabName, string panelName)
        {
            return uiCtrlApp.GetRibbonPanels(tabName).Where(x => x.Name == panelName)?.FirstOrDefault() ?? uiCtrlApp.CreateRibbonPanel(tabName, panelName);
        }
    }
}
