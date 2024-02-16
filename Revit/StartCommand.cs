using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitGuide.Views;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System;
using RevitGuide.Helpers;

namespace RevitGuide.Revit
{
    [Transaction(TransactionMode.Manual)]
    public class StartCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            if (!_ensureDataFolder())
            {
                return Result.Failed;
            }
            MainWindow window = new MainWindow(commandData.Application.ActiveUIDocument.Document);
            window.Show();
            return Result.Succeeded;
        }

        private bool _ensureDataFolder()
        {
            if (!Directory.Exists(App.DataFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(App.DataFolderPath);
                    
                }
                catch (Exception e)
                {
                    TaskDialog.Show("Error", "Error creating data folder: " + e.Message);
                    return false;
                }
            }
            return true;
        }
        Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            string pathAssembly = Path.Combine(App.DllPath, new AssemblyName(args.Name).Name + ".dll");
            if (File.Exists(pathAssembly))
            {
                return Assembly.LoadFrom(pathAssembly);
            }
            return null;
        }
    }
}