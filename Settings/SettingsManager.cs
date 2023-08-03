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
        private SettingsHelper _settingsHelper;
        public List<ItemSetting> TabSettings { get; set; }
        public List<ItemSetting> TriggerSettings { get; set; }

        public SettingsManager(Document doc)
        {
            _settingsHelper = new SettingsHelper(doc);
            TabSettings = _settingsHelper.GetTabSettings();
            TriggerSettings = _settingsHelper.GetTriggerSettings();
        }

        public void UpdateSettings(List<ItemSetting> tabSettings, List<ItemSetting> triggerSettings)
        {
            TabSettings = tabSettings;
            TriggerSettings = triggerSettings;
            App.ExEventHandler.Raise(SetSettings);
        }
        private void SetSettings()
        {
            _settingsHelper.UpdateAllSettings(TabSettings, TriggerSettings);
        }
    }
}
