using Autodesk.Revit.UI;
using RevitGuide.Helpers;
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
        public PostableCommand? Command { get; }
        public string Name { get; }
        public string Description { get; }

        public RvtCommand(PostableCommand command)
        {
            Command = command;
            Name = command.ToString();
            Description = RvtCommandHelper.GetDescription(command.ToString());
        }

        public RvtCommand(string commandName)
        {
            Command = RvtCommandHelper.GetPostableCommandByString(commandName);
            Name = commandName;
            Description = RvtCommandHelper.GetDescription(commandName);
        }

        public override bool Equals(object obj)
        {
            if (obj is RvtCommand other)
            {
                return other.Name == Name;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
