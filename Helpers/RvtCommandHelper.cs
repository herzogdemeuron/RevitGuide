using Autodesk.Revit.UI;
using RevitGuide.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGuide.Helpers
{
    public class RvtCommandHelper
    {
        public static PostableCommand GetPostableCommandByString(string commandName)
        {
            try
            {
                return (PostableCommand)Enum.Parse(typeof(PostableCommand), commandName);
            }
            catch (Exception)
            {
                throw new Exception($"Cannot find command {commandName}");
            }
        }

        public static string GetDescription(string commandName)
        {
            return DescriptionDict.TryGetValue(commandName, out string toolTip) ? toolTip : "";
        }
        public static readonly Dictionary<string, string> DescriptionDict = new Dictionary<string, string>
        {
              { "Room", "Creates a room bounded by model elements (such as walls, floors, and ceilings) and separation lines." },
              { "Wall", "Creates foundations hosted by walls." },
        };
    }
}
