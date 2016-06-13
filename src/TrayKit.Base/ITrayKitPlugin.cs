using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayKit.Base
{
  /// <summary>
  /// Defines a TrayKitPlugin wich will be loaded by the TrayKit-application.
  /// </summary>
  public interface ITrayKitPlugin
  {
    /// <summary>
    /// The name of the plugin.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The commands provided by the plugin.
    /// </summary>
    List<ITrayKitPluginCommand> Commands { get; }
     
    /// <summary>
    /// The image that will be shown besides the plugin in the contextmenu.
    /// </summary>
    Image Image { get; }

  }
}
