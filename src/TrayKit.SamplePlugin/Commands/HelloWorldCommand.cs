using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrayKit.Base;

namespace TrayKit.SamplePlugin.Commands
{
  public class HelloWorldCommand : ITrayKitPluginCommand
  {
    private const string CommandName = "Hello World!";

    public string Name { get { return CommandName; } }
    public ITrayKitPlugin Plugin { get; }
    public int SortPosition { get; set; }
    public Image Image { get; }

    public HelloWorldCommand(ITrayKitPlugin plugin)
    {
      Plugin = plugin;
      Image = SystemIcons.Shield.ToBitmap();
    }

    public void Execute()
    {
      MessageBox.Show("Hello World!");
    }
  }
}
