using Autodesk.Revit.UI;
using System.Collections.Generic;
using RevitGuide.Helpers;
using RevitGuide.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RevitGuide.Settings
{
    public class ItemSetting
    {
        private string _key;
        public string Key
        {
            get => _key;
            set
            {
                _key = value;
            }
        }
        private string _uri;
        public string Uri
        {
            get => _uri;
            set
            {
                _uri = value;
            }
        }
        public RvtCommand RvtCommand 
        { 
            get => new RvtCommand(Key);
            set
            {

                Key = value.Name;
            }
        }

        public ItemSetting(string key = null , string uri = "")
        {
            Key = key;
            Uri = uri;
        }

    }
}
