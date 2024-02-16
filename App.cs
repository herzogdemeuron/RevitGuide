using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using RevitGuide.Commands;
using RevitGuide.Revit;
using System;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace RevitGuide
{
    public class App : IExternalApplication
    {
        private bool _isLiveGuideSet = false;
        public static UIControlledApplication UICtrlApp;
        public static CommandBinder CommandBinder;
        public static ExternalEventHandler ExEventHandler = new ExternalEventHandler();
        public readonly static string DllPath = "C:\\HdM-DT\\RevitCSharpExtensions\\RevitGuide\\bin";
        public readonly static string DataFolderPath = Path.Combine(DllPath, "Data");
        public Result OnStartup(UIControlledApplication uiCtrlApp)
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
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
        Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            string directoryDLLs = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string pathAssembly = Path.Combine(directoryDLLs, new AssemblyName(args.Name).Name + ".dll");
            if (File.Exists(pathAssembly))
            {
                return Assembly.LoadFrom(pathAssembly);
            }
            return null;
        }
    }
}
