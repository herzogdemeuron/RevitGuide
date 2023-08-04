using RevitGuide.Helpers;
using RevitGuide.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RevitGuide.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private SettingsManager _settingsManager;
        public bool IsTabSettingsActive { get; set; } = true;

        public ObservableCollection<ItemSetting> ActiveSettings
        {
            get=> IsTabSettingsActive ? TabSettings : TriggerSettings;
        }

        private ObservableCollection<ItemSetting> _tabSettings;  
        public ObservableCollection<ItemSetting> TabSettings
        {
            get => _tabSettings;
            set
            {
                _tabSettings = value;
                OnPropertyChanged(nameof(TabSettings));
            }
        }
        private ObservableCollection<ItemSetting> _triggerSettings;
        public ObservableCollection<ItemSetting> TriggerSettings
        {
            get => _triggerSettings;
            set
            {
                _triggerSettings = value;
                OnPropertyChanged(nameof(TriggerSettings));
            }
        }

        public SettingsViewModel(SettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            TabSettings = new ObservableCollection<ItemSetting>(_settingsManager.TabSettings);
            TriggerSettings = new ObservableCollection<ItemSetting>(_settingsManager.TriggerSettings);
        }

        public void HandleNewItem(List<int> indices)
        {

            int index = ActiveSettings.Count;
            if (indices.Count > 0)
            {
                index = indices.Max() + 1;
            }
            ActiveSettings.Insert(index, new ItemSetting());
        }

        public void HandleRemoveItems(List<int> indices)
        {
            indices.Sort();
            indices.Reverse();
            foreach (int index in indices)
            {
                ActiveSettings.RemoveAt(index);
            }
        }

        public void HandleMoveItems(List<int> indices, bool dir)
        {
            // dir: true for moving up, false for moving down
            indices.Sort();
            int delta;
            if (dir)
            {
                if (indices.Count > 0 && indices[0] == 0)
                {
                    return;
                }
                delta = -1;

            }
            else
            {
                indices.Reverse();
                if (indices.Count > 0 && indices[0] == ActiveSettings.Count - 1)
                {
                    return;
                }
                delta = 1;
            }
            
            List<ItemSetting> selectedTabs = new List<ItemSetting>();
            foreach (int index in indices)
            {
                ItemSetting itemSetting = ActiveSettings[index];
                ActiveSettings.RemoveAt(index);
                ActiveSettings.Insert(index + delta, itemSetting);
                selectedTabs.Add(itemSetting);
            }
            //let ui restore selection
            Mediator.Broadcast("UpdateDataGridSelection", selectedTabs);
        }


        public void HandleConfirm()
        {
            _settingsManager.UpdateSettings(TabSettings.ToList(), TriggerSettings.ToList());
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

