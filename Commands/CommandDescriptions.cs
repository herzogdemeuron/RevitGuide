using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGuide.Commands
{
    public class CommandDescriptions
    {
        private static Dictionary<PostableCommand, string> _currentDict = _descriptions2023;

        private static readonly Dictionary<PostableCommand, string> _descriptions2023 = new Dictionary<PostableCommand, string>
        {
              { PostableCommand.Room, "Creates a room bounded by model elements (such as walls, floors, and ceilings) and separation lines." },
              { PostableCommand.Wall, "Creates foundations hosted by walls." },
        };

        public static string GetDescription(PostableCommand command)
        {
            //if the command is not in the dictionary, return an empty string
            if (!_currentDict.ContainsKey(command))
                return string.Empty;
            return _currentDict[command];
        }
    }
}
