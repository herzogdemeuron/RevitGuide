using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAssist
{
    [Transaction(TransactionMode.ReadOnly)]
    public class MainWindowButton : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            MainControl window = new MainControl();
            window.Show();
            return Result.Succeeded;
        }
    }



}