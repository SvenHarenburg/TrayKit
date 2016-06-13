using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrayKit.Base;

namespace TrayKit.ViewModels.DesignTimeClasses
{
  internal class TrayKitCommand : ITrayKitPluginCommand
  {
    public string Name
    {
      get
      {
        return "PluginCommand1";
      }
    }

    public int SortPosition { get; set; }

    public ITrayKitPlugin Plugin { get; private set; }

    public TrayKitCommand(ITrayKitPlugin plugin)
    {
      Plugin = plugin;
    }

    public void Execute()
    {
      throw new NotImplementedException();
    }

    public Image Image { get; }
  }
}
