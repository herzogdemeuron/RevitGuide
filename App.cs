using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using RevitGuide.Commands;
using RevitGuide.Revit;

namespace RevitGuide
{
    public class App : IExternalApplication
    {
        private bool _isLiveGuideSet = false;
        public static UIControlledApplication UICtrlApp;
        public static CommandBinder CommandBinder;
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
            if(!_isLiveGuideSet)
            {
                _isLiveGuideSet = true;
                CommandBinder = new CommandBinder(args.Document);
                CommandBinder.Register();
            }
        }
    }
}
