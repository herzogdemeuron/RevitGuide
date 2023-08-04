using Autodesk.Revit.UI;
using RevitGuide.Commands;
using RevitGuide.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

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
               Key = value?.Name??null;
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

        private ICollectionView _allRvtCmdCollection;
        public ICollectionView AllRvtCmdCollection
        {
            get
            {
                if (_allRvtCmdCollection == null && _allRvtCommands != null)
                {
                    _allRvtCmdCollection = CollectionViewSource.GetDefaultView(_allRvtCommands);
                }

                return _allRvtCmdCollection;
            }
        }
        public ItemSetting(string key = null , string uri = "")
        {
            Key = key;
            Uri = uri;
            _allRvtCommands = new ObservableCollection<RvtCommand>(RvtCommandHelper.AllRvtCommands);
        }   
    }
}
