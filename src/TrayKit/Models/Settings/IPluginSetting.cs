using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrayKit.Base.Settings;
using System.Reflection;

namespace TrayKit.Models.Settings
{
  internal interface IPluginSetting
  {
    TrayKitPluginSettingAttribute SettingAttribute { get; }
    string PluginAssembly { get; set; }
    string PropertyName { get; set; }

    /// <summary>
    /// Holds a reference to the object holding the settings in the plugin itself.
    /// </summary>
    object PluginSettingsContainer { get; set; }

    /// <summary>
    /// The property in the SettingsContainer of the plugin.
    /// </summary>
    PropertyInfo SourceSettingProperty { get; set; }
  }
}
