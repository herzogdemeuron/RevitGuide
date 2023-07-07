using GongSolutions.Wpf.DragDrop;
using RevitGuide.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

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

        public SettingsViewModel()
        {
            _settingsManager = new SettingsManager();
            TabSettings = new ObservableCollection<TabSetting>(_settingsManager.GetTabSettings());
        }

        public void HandleNewItem()
        {
            TabSettings.Add(new TabSetting());
            OnPropertyChanged(nameof(TabSettings));
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

