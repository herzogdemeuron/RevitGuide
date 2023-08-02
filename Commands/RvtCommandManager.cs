using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitGuide.Helpers;
namespace RevitGuide.Commands
{
    public class RvtCommandManager
    {
        public static List<RvtCommand> AllRvtCommands { get => GetRvtCommands(); }


        private static List<RvtCommand> GetRvtCommands()
        {
            
            List<RvtCommand> rvtCommands = new List<RvtCommand>();
            foreach (PostableCommand command in mockCommandList)
            {
                
                rvtCommands.Add(new RvtCommand(command));
            }
            return rvtCommands;
        }
        private static List<PostableCommand> mockCommandList = new List<PostableCommand>
        {
            PostableCommand.Room,
            PostableCommand.Wall,
        };
    }
}
