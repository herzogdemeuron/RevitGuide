using Autodesk.Revit.UI;
using RevitGuide.Commands;
using RevitGuide.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Linq;

namespace RevitGuide.Settings
{
    public class ItemSetting : INotifyPropertyChanged
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

        public PostableCommand? PostableCommand
        {
            get => RvtCommand.Command;
        }

        public Uri ValidatedUri
        {
            get => UriHelper.StringToUri(Uri);
        }


        private ObservableCollection<RvtCommand> _allRvtCommands;
        public ObservableCollection<RvtCommand> AllRvtCommands
        {
            get { return _allRvtCommands; }
            set
            {
                if(value != _allRvtCommands)
                {
                    _allRvtCommands = value;
                    OnPropertyChanged(nameof(AllRvtCommands));
                }
            }
        }


        private ObservableCollection<RvtCommand> _filteredRvtCommands;
        public ObservableCollection<RvtCommand> FilteredRvtCommands
        {
            get { return _filteredRvtCommands; }
            set
            {
                if(value != _filteredRvtCommands)
                {
                    _filteredRvtCommands = value;
                    OnPropertyChanged(nameof(FilteredRvtCommands));
                }
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if(value != _searchText)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilteredRvtCommands = new ObservableCollection<RvtCommand>(AllRvtCommands.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())));
                }
            }
        }

        public ItemSetting(string key = null , string uri = "")
        {
            Key = key;
            Uri = uri;
            AllRvtCommands = new ObservableCollection<RvtCommand>(RvtCommandHelper.AllRvtCommands);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
