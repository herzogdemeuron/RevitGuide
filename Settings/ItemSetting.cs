using Autodesk.Revit.UI;
using RevitGuide.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGuide.Settings
{
    public class ItemSetting
    {
        public string Key { get; set; } 
        public string Uri { get; set; }

        public ItemSetting(string key = null , string uri = "")
        {
            Key = key;
            Uri = uri;
        }
    }
}
