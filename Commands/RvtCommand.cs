using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitGuide.Commands
{
    public class RvtCommand
    {
        public PostableCommand Command { get; }
        public string ToolTip { get => CommandDescriptions.GetDescription(Command); }
        public RvtCommand(PostableCommand command)
        {
            Command = command;
        }
    }
}
