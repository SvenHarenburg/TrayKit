using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrayKit.Base;
using TrayKit.SamplePlugin.Commands;
using System.Drawing;

namespace TrayKit.SamplePlugin
{
  [Export(typeof(ITrayKitPlugin))]
  public class SamplePlugin : ITrayKitPlugin
  {
    private const string PluginName = "TrayKit SamplePlugin";
    public List<ITrayKitPluginCommand> Commands { get; }

    public Image Image { get; }

    [Base.Settings.TrayKitPluginSettingsContainer()]
    public Settings Settings { get; set; }

    public string Name { get { return PluginName; } }

    public SamplePlugin()
    {
      Settings = new Settings()
      {
        AnIntegerValue = 5,
        AStringValue = "HelloWorldSetting",
        ABoolValue = true,
        ADateTimeValue = DateTime.Now,
        ADecimalValue = 12.2m,
        ADoubleValue = 13.3,
        AFloatValue = 14.4f
      };

      Commands = new List<ITrayKitPluginCommand>()
      {
        new HelloWorldCommand(this) { SortPosition = 0 },
        new TwoPlusTwoCommand(this) { SortPosition = 1 }
      };
      Image = SystemIcons.Asterisk.ToBitmap();
    }


  }
}
