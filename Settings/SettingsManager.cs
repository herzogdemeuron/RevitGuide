using Autodesk.Revit.DB;
using RevitGuide.Helpers;
using System.Collections.Generic;

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
