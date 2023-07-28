using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGuide.Commands
{
    public class RvtCommandManager
    {
        public static List<RvtCommand> RvtCommands { get; } = new List<RvtCommand>
        {
            new RvtCommand(PostableCommand.Room),
            new RvtCommand(PostableCommand.Wall),
        };
    }
}
