using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrayKit.Base;
using System.Drawing;

namespace TrayKit.SamplePlugin.Commands
{
  public class TwoPlusTwoCommand : ITrayKitPluginCommand
  {
    private const string CommandName = "TwoPlusTwo";

    public string Name
    {
      get
      {
        return CommandName;
      }
    }
    public ITrayKitPlugin Plugin { get; }

    public int SortPosition { get; set; }

    public TwoPlusTwoCommand(ITrayKitPlugin plugin)
    {
      Plugin = plugin;
      Image = SystemIcons.Question.ToBitmap();
    }

    public void Execute()
    {
      MessageBox.Show("2 + 2 = 4");
    }

    public Image Image { get; }
  }
}
