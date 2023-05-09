using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAssist.RevitGuide
{
    public class RevitGuideApp : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            RevitApp.UIControlledApp = application;
            CommandBinder.Register();
            RibbonMaker ribbonMaker = new RibbonMaker();
            ribbonMaker.Create(application);
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
