using Autodesk.Revit.UI;
using Newtonsoft.Json;
using RevitGuide.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using System.Reflection;


namespace RevitGuide.Helpers
{
    public class RvtCommandHelper
    {

        public static ObservableCollection<RvtCommand> _allRvtCommands;
        public static ObservableCollection<RvtCommand> AllRvtCommands
        {
            get
            {
                if (_allRvtCommands == null)
                {
                    _allRvtCommands = new ObservableCollection<RvtCommand>();
                    foreach (string commandName in SourceDict.Keys.OrderBy(key => key))
                    {
                        var command = new RvtCommand(commandName);
                        _allRvtCommands.Add(command);
                    }
                }
                return _allRvtCommands;
            }
        }

        private static Dictionary<string, string> _sourceDict;
        public static Dictionary<string, string> SourceDict
        {
            get
            {
                if (_sourceDict == null)
                {
                    _sourceDict = GetPostableCommandDict23();
                }
                return _sourceDict;
            }
        }

/*        private static List<RvtCommand> GetAllRvtCommands()
        {
            var commands = new List<RvtCommand>();
            foreach (string commandName in DescriptionDict.Keys.OrderBy(key => key))
            {
                var command = new RvtCommand(commandName);
                commands.Add(command);
            }
            return commands;
        }
*/
        public static PostableCommand? GetPostableCommandByString(string commandName)
        {
            if (Enum.TryParse(commandName, out PostableCommand command))
            {
                return command;
            }
            return null;
        }

        public static string GetDescription(string commandName)
        {
            if(commandName == null)
            {
                return "";
            }
            return SourceDict.TryGetValue(commandName, out string toolTip) ? toolTip : "";
        }
        private static Dictionary<string, string> GetPostableCommandDict23()
        {
            string dataDirectory = App.DataFolderPath23 + "/PostableCommands2023.json";
            string json = File.ReadAllText(dataDirectory);
            Dictionary<string, string> commands = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return commands;
        }
    }
}
