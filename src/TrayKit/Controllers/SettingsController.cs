using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrayKit.Models.Settings;
using Newtonsoft.Json;
using System.IO;

namespace TrayKit.Controllers
{
  internal class SettingsController
  {
    private const string SettingsFileName = "Settings.json";
    public Settings Settings { get; set; }

    public void SaveSettings()
    {
      if (Settings != null)
      {
        var json = JsonConvert.SerializeObject(Settings, Formatting.Indented, new JsonSerializerSettings
        {
          TypeNameHandling = TypeNameHandling.Objects,
          TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
        });
        // TODO: Should check if the file is locked before attempting to write it.
        File.WriteAllText(GetSettingsPath(), json);
      }
    }

    public void LoadSettings()
    {
      var path = GetSettingsPath();
      if (!File.Exists(path))
      {
        Settings = new Settings()
        {
          TrayKitSettings = new TrayKitSettings(),
          PluginSettings = new PluginSettingList()
        };
      }
      else
      {
        // TODO: Should check if the file is locked before attempting to read it.
        Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path), new JsonSerializerSettings
        {
          TypeNameHandling = TypeNameHandling.Objects
        });
      }
    }

    private string GetSettingsPath()
    {
      var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsFileName);
      return path;
    }

  }
}
