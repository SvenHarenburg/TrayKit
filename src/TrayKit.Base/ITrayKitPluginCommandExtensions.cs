using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrayKit.Base;

namespace TrayKit.Base
{
  public static class ITrayKitPluginCommandExtensions
  {

    /// <summary>
    /// Returns the full-key of the <see cref="ITrayKitPluginCommand"/>.
    /// </summary>
    public static string GetFullCommandKey(this ITrayKitPluginCommand command)
    {
      return $"{command.Plugin.Name}{command.Name}";
    }

  }
}
