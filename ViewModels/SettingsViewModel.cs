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

        private ObservableCollection<TabSetting> _tabSettings;  
        public ObservableCollection<TabSetting> TabSettings
        {
            get => _tabSettings;
            set
            {
                _tabSettings = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel(SettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            TabSettings = new ObservableCollection<TabSetting>(_settingsManager.GetTabSettings());
        }

        public void HandleNewItem(List<int> indices)
        {

            int index = TabSettings.Count;
            if (indices.Count > 0)
            {
                index = indices.Max() + 1;
            }
            TabSettings.Insert(index, new TabSetting());
        }

        public void HandleRemoveItems(List<int> indices)
        {
            indices.Sort();
            indices.Reverse();
            foreach (int index in indices)
            {
                TabSettings.RemoveAt(index);
            }
        }

        public void HandleMoveItems(List<int> indices, bool dir)
        {
            // dir is true for move up, false for move down
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
                if (indices.Count > 0 && indices[0] == TabSettings.Count - 1)
                {
                    return;
                }
                delta = 1;
            }
            
            List<TabSetting> selectedTabs = new List<TabSetting>();
            foreach (int index in indices)
            {
                TabSetting tabSetting = TabSettings[index];
                TabSettings.RemoveAt(index);
                TabSettings.Insert(index + delta, tabSetting);
                selectedTabs.Add(tabSetting);
            }
            Mediator.Broadcast("UpdateDataGridSelection", selectedTabs);
        }


        public void HandleConfirm()
        {
            _settingsManager.UpdateTabSettings(TabSettings.ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

