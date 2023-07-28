using RevitGuide.Settings;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Security.Policy;
using RevitGuide.Views;
using System.IO;

namespace RevitGuide.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private SettingsManager _settingsManager;
        private string DataFolderPath = App.DataFolderPath23; 
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

        public MainViewModel()
        {
            _settingsManager = new SettingsManager();
            UpdateTabs();
        }

        private void UpdateTabs()
        {
            
            List<TabSetting> tabSettings = _settingsManager.TabSettings;
            ClearAllTabs();
            
            foreach (TabSetting tabSetting in tabSettings)
            {
                if (tabSetting.TabName == "")
                {
                    continue;
                }
                AddTab(Tabs, tabSetting.TabName, tabSetting.TabUrl);
            }
            CleanDataFolders();
            SelectedTab = Tabs.FirstOrDefault();
        }

        private void CleanDataFolders()
        {
            if (!Directory.Exists(DataFolderPath)) return;

            List<string> activeDataFolders = Tabs.Select(tab => tab.FolderPath).ToList();
            string[] existingDataFolders = Directory.GetDirectories(DataFolderPath);
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

        private void AddTab(ObservableCollection<TabItemViewModel> tabs, string header, string url)
        {
            TabItemViewModel tab = new TabItemViewModel
            {
                FolderPath = DataFolderPath,
                Title = header,
                Url = Validate(url),
            };
            tab.InitWbv2();
            tabs.Add(tab);
        }

        public void ClearAllTabs()
        {
            if (Tabs != null)
            {
                foreach (TabItemViewModel tab in Tabs)
                {
                    tab.Dispose();
                }
            }
            Tabs = new ObservableCollection<TabItemViewModel>();
        }


        private Uri Validate(string uri)
        {

            if (File.Exists(uri))
            {
                return new Uri(uri);
            }

            if (uri == "")
            {
                return new Uri(App.DataFolderPath23 + "first_page.html");
            }
            else if (Uri.TryCreate(uri, UriKind.Absolute, out Uri result))
            {
                return result;
            }
            else if (Uri.TryCreate("http://" + uri, UriKind.Absolute, out result))
            {
                return result;
            }
            else if (Uri.TryCreate("https://" + uri, UriKind.Absolute, out result))
            {
                return result;
            }
            else
            {
                return new Uri(App.DataFolderPath23 + "invalid_page.html");
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



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
