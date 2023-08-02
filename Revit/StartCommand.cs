using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitGuide.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGuide.Revit
{
    [Transaction(TransactionMode.Manual)]
    public class StartCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            App.Doc = commandData.Application.ActiveUIDocument.Document;
            MainWindow window = new MainWindow();
            window.Show();
            return Result.Succeeded;
        }
    }
}