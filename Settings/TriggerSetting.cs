using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGuide.Settings
{
    public class TriggerSetting
    {
        public PostableCommand RvtCommand { get; set; }
        public string TabUri { get; set; }

        public TriggerSetting(PostableCommand rvtCommand, string uri = "")
        {

            RvtCommand = rvtCommand;
            TabUri = uri;
        }
    }
}
