using Autodesk.Revit.DB;
using RevitGuide.Helpers;
using RevitGuide.Settings;
using RevitGuide.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RevitGuide.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private SettingsManager _settingsManager;

        private ObservableCollection<TabItemViewModel> _tabs;
        public ObservableCollection<TabItemViewModel> Tabs
        {
            get => _tabs;
            set
            {
                _tabs = value;
                OnPropertyChanged(nameof(Tabs));
            }
        }
        private TabItemViewModel _selectedTab;
        public TabItemViewModel SelectedTab
        {
            get => _selectedTab;
            set
            {
                _selectedTab = value;
                OnPropertyChanged(nameof(SelectedTab));
            }
        }

        public MainViewModel(Document doc)
        {
            _settingsManager = new SettingsManager(doc);
            UpdateTabs();
        }

        private void UpdateTabs()
        {
            
            List<ItemSetting> tabSettings = _settingsManager.TabSettings;
            List<ItemSetting> triggerSettings = _settingsManager.TriggerSettings;
            ClearAllTabs();
            
            foreach (ItemSetting tabSetting in tabSettings)
            {
                if (tabSetting.Key == null)
                {
                    continue;
                }
                AddTab(Tabs, tabSetting.Key, tabSetting.ValidatedUri);
            }
            if(triggerSettings.Count > 0)
            {
                //add the live guide tab
                AddTab(Tabs, "LIVE", UriHelper.LiveGuidePageUri, true);
            }
            CleanDataFolders();
            SelectedTab = Tabs.FirstOrDefault();
        }

        private void CleanDataFolders()
        {
            if (!Directory.Exists(App.DataFolderPath)) return;

            List<string> activeDataFolders = Tabs.Select(tab => tab.FolderPath).ToList();
            string[] existingDataFolders = Directory.GetDirectories(App.DataFolderPath);
            foreach (string folder in existingDataFolders)
            {
                if (!activeDataFolders.Contains(folder))
                {
                    try
                    {
                        Directory.Delete(folder, true);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }
        }

        private void AddTab(ObservableCollection<TabItemViewModel> tabs, string header, Uri uri, bool isLive = false)
        {
            TabItemViewModel tab = new TabItemViewModel
            {
                FolderPath = App.DataFolderPath,
                Title = header,
                Uri = uri,
                IsLive = isLive
            };
            tab.InitWbv2();
            tabs.Add(tab);
        }

        public void ClearAllTabs()
        {
            Dispose();

            Tabs = new ObservableCollection<TabItemViewModel>();
        }

        public void Dispose()
        {
            if (Tabs != null)
            {
                foreach (TabItemViewModel tab in Tabs)
                {
                    tab.Dispose();
                }
            }
        }

        public void HandleConfigClicked()
        {
            SettingsWindow settingsWindow = new SettingsWindow(_settingsManager);
            bool? result = settingsWindow.ShowDialog();
            if (result == true)
            {
                UpdateTabs(); 
            }
        }

        public void HandleOpenClicked()
        {
            if (SelectedTab == null) return;
            var currentUri = SelectedTab.GetCurrentUri();
            if (currentUri == null) return;
            try
            {
                Process.Start(currentUri.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void HandleBackClicked()
        {
            if (SelectedTab == null) return;
            SelectedTab.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
