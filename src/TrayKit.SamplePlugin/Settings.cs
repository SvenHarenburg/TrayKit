using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrayKit.Base.Settings;

namespace TrayKit.SamplePlugin
{
  public class Settings
  {
    [TrayKitPluginSetting]
    public bool ABoolValue { get; set; }
    [TrayKitPluginSetting]
    public decimal ADecimalValue { get; set; }
    [TrayKitPluginSetting]
    public double ADoubleValue { get; set; }
    [TrayKitPluginSetting]
    public float AFloatValue { get; set; }
    [TrayKitPluginSetting(Description ="This is a setting that stores an integer value.")]
    public int AnIntegerValue { get; set; }
    [TrayKitPluginSetting]
    public string AStringValue { get; set; }    
    [TrayKitPluginSetting]
    public DateTime ADateTimeValue { get; set; }
  }
}
