using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrayKit.Base;

namespace TrayKit.SamplePlugin.Commands
{
  public class AddNewCommand : ITrayKitPluginCommand
  {
    private const string CommandName = "Add another Command";

    public string Name { get { return CommandName; } }
    public ITrayKitPlugin Plugin { get; }
    public int SortPosition { get; set; }
    public Image Image { get; }

    public AddNewCommand(ITrayKitPlugin plugin)
    {
      Plugin = plugin;
    }

    public void Execute()
    {
      var newCommand = new AddNewCommand(Plugin);
      newCommand.SortPosition = SortPosition + 1;
      Plugin.Commands.Add(newCommand);
    }
  }
}
