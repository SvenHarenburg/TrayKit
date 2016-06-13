using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrayKit.Base.Settings;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Reflection;

namespace TrayKit.Models.Settings
{
  internal class PluginSetting<T> : IPluginSetting, INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    [JsonIgnore()]
    public TrayKitPluginSettingAttribute SettingAttribute { get; set; }

    public string PluginAssembly { get; set; }
    public string PropertyName { get; set; }

    [JsonProperty()]
    private T internalValue;

    [JsonIgnore()]
    public T Value
    {
      get
      {
        return GetValue();

      }
      set
      {
        SetValue(value);
        OnPropertyChanged();
      }
    }

    private T GetValue()
    {
      return internalValue;
    }

    private void SetValue(T value)
    {
      internalValue = value;
      if (!(PluginSettingsContainer == null || SourceSettingProperty == null))
      {
        PluginSettingsContainer.GetType().GetProperty(SourceSettingProperty.Name).SetValue(PluginSettingsContainer, value);
      }
    }

    [JsonIgnore()]
    public object PluginSettingsContainer { get; set; }

    [JsonIgnore()]
    public PropertyInfo SourceSettingProperty { get; set; }



    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

  }
}
