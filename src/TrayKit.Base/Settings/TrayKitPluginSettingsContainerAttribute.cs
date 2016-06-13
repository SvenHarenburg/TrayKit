using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayKit.Base.Settings
{
  /// <summary>
  /// Placed on a property of <see cref="ITrayKitPlugin"/> to mark a member that contains 
  /// settings to be tracked by the TrayKitSettingsController.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class TrayKitPluginSettingsContainerAttribute : Attribute
  {    
  }
}
