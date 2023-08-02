using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGuide.Commands
{
    public class CommandManager
    {
        public string CommandStr { get; set; }

        public void Execute(string commandStr)
        {
            CommandStr = commandStr;
            App.ExEventHandler.RaiseWithUIApp(ExecuteCommand);
        }

        private void ExecuteCommand(UIApplication uiApp)
        {
            RevitCommandId commandId = FindPostableCommand(CommandStr);
            if (commandId != null)
            {
                uiApp.PostCommand(commandId);
            }
        }

        private RevitCommandId FindPostableCommand(string commandStr)
        {
            string command = ParseCommand(commandStr);
            try
            {
                PostableCommand postableCommand = (PostableCommand)Enum.Parse(typeof(PostableCommand), command);
                RevitCommandId commandId = RevitCommandId.LookupPostableCommandId(postableCommand);
                return commandId;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }            
        }

        private string ParseCommand(string commandStr)
        {
            // a typical command str: http://revitcommand-override_by_element.run/
            // subString is between 'revitcommand-' and '.run'
            string prefix = "revitcommand-";
            string suffix = ".run";
            string subString = ExtractCommandString(commandStr, prefix, suffix);
            return Capitalize(subString);
        }

        private string ExtractCommandString(string uri, string prefix, string suffix)
        {
            // get subString between prefix and suffix
            int startIndex = uri.IndexOf(prefix) + prefix.Length;
            int endIndex = uri.IndexOf(suffix);
            string subString = uri.Substring(startIndex, endIndex - startIndex);
            return subString;
        }

        private string Capitalize(string text)
        {
            string[] words = text.Split('_');
            string result = "";
            foreach (string word in words)
            {
                result += word.Substring(0, 1).ToUpper() + word.Substring(1);
            }
            return result;
        }
    }
}
