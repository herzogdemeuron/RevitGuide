using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGuide.Settings
{
    public class TabSetting
    {
        public string TabName { get; set; }
        public string TabUri { get; set; }

        public TabSetting(string name = "", string uri = "")
        {
            TabName = name;
            TabUri = uri;
        }
    }
}
