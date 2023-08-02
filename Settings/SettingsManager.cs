using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.UI;
using RevitGuide.Helpers;

namespace RevitGuide.Settings
{
    public class SettingsManager
    {
        private SettingsHelper SettingsHelper = new SettingsHelper();
        public List<ItemSetting> TabSettings { get; set; }
        public List<ItemSetting> TriggerSettings { get; set; }

        public SettingsManager()
        {
            TabSettings = SettingsHelper.GetTabSettings();
            TriggerSettings = SettingsHelper.GetTriggerSettings();
        }
    /*    public List<ItemSetting> GetTabSettings()
        {
            return SettingsHelper.GetTabSettings();
        }

        public List<ItemSetting> GetTriggerSettings()
        {
            return SettingsHelper.GetTriggerSettings();
        }*/

        public void UpdateSettings(List<ItemSetting> tabSettings, List<ItemSetting> triggerSettings)
        {
            TabSettings = tabSettings;
            TriggerSettings = triggerSettings;
            App.ExEventHandler.Raise(SetSettings);
        }
        private void SetSettings()
        {
            SettingsHelper.UpdateAllSettings(TabSettings, TriggerSettings);
        }
    }
}
