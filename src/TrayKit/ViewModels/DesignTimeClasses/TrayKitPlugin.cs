using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrayKit.Base;

namespace TrayKit.ViewModels.DesignTimeClasses
{
  internal class TrayKitPlugin : ITrayKitPlugin
  {
    public ObservableCollection<ITrayKitPluginCommand> Commands
    {
      get
      {
        return new ObservableCollection<ITrayKitPluginCommand>()
        {
          new DesignTimeClasses.TrayKitCommand(this)
        };
      }
    }

    public Image Image { get; }

    public string Name
    {
      get
      {
        return "TestPlugin";
      }
    }
  }
}
