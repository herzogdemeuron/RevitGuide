using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitGuide.Revit;
using System.Reflection;

namespace RevitGuide
{
    public class App : IExternalApplication
    {

        public static Document Doc { get; set; }
        public static UIControlledApplication UICtrlApp;
        public static ExternalEventHandler ExEventHandler = new ExternalEventHandler();
        public readonly static string DataFolderPath23 = @"C:\ProgramData\Autodesk\Revit\Addins\2023\RevitGuide\Data\";
        public Result OnStartup(UIControlledApplication uiCtrlApp)
        {

                UICtrlApp = uiCtrlApp;
                RibbonMaker.Create(uiCtrlApp, "HdM", "HOME");
                uiCtrlApp.ControlledApplication.DocumentOpened += OnDocumentOpened;
                return Result.Succeeded;
            
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        private void OnDocumentOpened(object sender, DocumentOpenedEventArgs args)
        {
            Doc = args.Document;
            //CommandBinder.Register();
        }

        private void OnDocumentOpening(object sender, DocumentOpeningEventArgs args)
        {
            //args.PathName;
            //CommandBinder.Register();
        }


        private void OnDocumentChanged(object sender, DocumentChangedEventArgs args)
        {
            //CommandBinder.Register();
        }
    }
}
