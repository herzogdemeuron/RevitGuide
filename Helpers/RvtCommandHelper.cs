using Autodesk.Revit.UI;
using RevitGuide.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace RevitGuide.Helpers
{
    public class RvtCommandHelper
    {

        public static List<RvtCommand> _allRvtCommands;
        public static List<RvtCommand> AllRvtCommands
        {
            get
            {
                if (_allRvtCommands == null)
                {
                    _allRvtCommands = GetAllRvtCommands();
                }
                return _allRvtCommands;
            }
        }

        private static Dictionary<string, string> _descriptionDict;
        public static Dictionary<string, string> DescriptionDict
        {
            get
            {
                if (_descriptionDict == null)
                {
                    _descriptionDict = GetPostableCommandDict23();
                }
                return _descriptionDict;
            }
        }

        private static List<RvtCommand> GetAllRvtCommands()
        {
            var commands = new List<RvtCommand>();
            foreach (string commandName in DescriptionDict.Keys.OrderBy(key => key))
            {
                var command = new RvtCommand(commandName);
                commands.Add(command);
            }
            return commands;
        }

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
            return DescriptionDict.TryGetValue(commandName, out string toolTip) ? toolTip : "";
        }
        private static Dictionary<string, string> GetPostableCommandDict23()
        {
            string path = App.DataFolderPath23 + "PostableCommands2023.json";
            string json = File.ReadAllText(path);
            Dictionary<string, string> commands = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return commands;
        }
    }
}
