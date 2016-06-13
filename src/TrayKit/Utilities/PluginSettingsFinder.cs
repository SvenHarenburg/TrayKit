using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrayKit.Base;
using TrayKit.Models.Settings;
using TrayKit.Extensions;

namespace TrayKit.Utilities
{
  internal class PluginSettingsFinder
  {
    public static PluginSettingList FindPluginSettings(ITrayKitPlugin plugin)
    {
      if (plugin == null)
      {
        throw new ArgumentNullException(nameof(plugin));
      }

      PluginSettingList result = null;
      var settingContainerInstance = GetSettingsContainer(plugin);

      if (settingContainerInstance != null)
      {
        result = new PluginSettingList();
        var settings = GetSettingsFromSettingsContainer(settingContainerInstance);

        foreach (var setting in settings)
        {
          result.Add(CreatePluginSetting(setting, settingContainerInstance, plugin));
        }
      }

      return result;
    }

    private static IEnumerable<PropertyInfo> GetSettingsFromSettingsContainer(object settingsContainer)
    {
      return settingsContainer?.GetType().GetProperties().Where(x => Attribute.IsDefined(x, typeof(Base.Settings.TrayKitPluginSettingAttribute)));
    }

    private static object GetSettingsContainer(ITrayKitPlugin plugin)
    {
      var pluginType = plugin.GetType();
      var settingsContainerProperty = pluginType.GetProperties().Where(x => Attribute.IsDefined(x, typeof(Base.Settings.TrayKitPluginSettingsContainerAttribute))).FirstOrDefault();

      if (settingsContainerProperty == null)
      {
        return null;
      }

      var settingsContainerType = settingsContainerProperty.PropertyType;
      var settingsContainerInstance = pluginType.GetProperty(settingsContainerProperty.Name).GetValue(plugin);
      return settingsContainerInstance;
    }

    private static IPluginSetting CreatePluginSetting(PropertyInfo property, object settingsContainer, ITrayKitPlugin plugin)
    {
      var propertyValue = GetSettingPropertyValue(property, settingsContainer);
      var instance = GetGenericPluginSettingInstance(property);
      var attribute = property.GetCustomAttribute(typeof(Base.Settings.TrayKitPluginSettingAttribute));

      SetInstancePropertyValues(instance, attribute, plugin, propertyValue, property, settingsContainer);

      return (IPluginSetting)instance;
    }

    private static void SetInstancePropertyValues(object instance, Attribute attribute, ITrayKitPlugin plugin, 
                                                  object propertyValue,PropertyInfo sourceProperty, object settingsContainer)
    {
      instance.GetType().GetProperty(nameof(IPluginSetting.SettingAttribute)).SetValue(instance, attribute);
      instance.GetType().GetProperty(nameof(IPluginSetting.PluginAssembly)).SetValue(instance, plugin.GetPluginAssemblyName());
      instance.GetType().GetProperty(nameof(PluginSetting<Object>.Value)).SetValue(instance, propertyValue);
      instance.GetType().GetProperty(nameof(IPluginSetting.PropertyName)).SetValue(instance, sourceProperty.Name);
      instance.GetType().GetProperty(nameof(IPluginSetting.PluginSettingsContainer)).SetValue(instance, settingsContainer);
      instance.GetType().GetProperty(nameof(IPluginSetting.SourceSettingProperty)).SetValue(instance, sourceProperty);
    }

    private static object GetGenericPluginSettingInstance(PropertyInfo property)
    {
      var genericType = typeof(PluginSetting<>);
      var specificType = genericType.MakeGenericType(property.PropertyType);
      var constructor = specificType.GetConstructor(new Type[] { });

      return constructor.Invoke(new object[] { });
    }

    private static object GetSettingPropertyValue(PropertyInfo property, object settingsContainer)
    {
      return settingsContainer.GetType().GetProperty(property.Name).GetValue(settingsContainer);
    }

  }
}
