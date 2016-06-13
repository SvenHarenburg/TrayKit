using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayKit.Base.Settings
{
  /// <summary>
  /// Placed on a method to mark the method as a TrayKitPluginSetting so it will be tracked by the TrayKitSettingsController.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class TrayKitPluginSettingAttribute : Attribute, ITrayKitPluginSetting
  { 
    public string DisplayName { get; set; }
    public bool Visible { get; set; }
    public bool ReadOnly { get; set; }
    public string Description { get; set; }
    public int MaxLength { get; set; }
    public int MaxValue { get; set; }
    public int MinValue { get; set; }
  }
}
