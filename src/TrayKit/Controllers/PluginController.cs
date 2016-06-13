using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using TrayKit.Base;
using TrayKit.Models;
using TrayKit.Models.Settings;
using System.Reflection;
using System.Collections.ObjectModel;
using System.IO;

namespace TrayKit.Controllers
{
  internal class PluginController
  {
    private const string PluginDirectoryName = "Plugins";
    private string PluginDirectory;

    [ImportMany(typeof(ITrayKitPlugin))]
    private ITrayKitPlugin[] RawPlugins { get; set; }

    public PluginList Plugins { get; private set; }

    public PluginController()
    {
      InitPluginDirectory();
    }

    private void InitPluginDirectory()
    {
      var executingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      PluginDirectory = Path.Combine(executingDirectory, PluginDirectoryName);
      CreatePluginDirectory();
    }

    private void CreatePluginDirectory()
    {
      if (!Directory.Exists(PluginDirectory))
      {
        Directory.CreateDirectory(PluginDirectory);
      }
    }

    public void LoadPlugins()
    {
      // Calling it again just to make sure it still exists.
      // It could've been deleted by the user or other applications while the application runs.
      CreatePluginDirectory();
      var catalog = new DirectoryCatalog(PluginDirectory);
      var container = new CompositionContainer(catalog);
      Plugins = null;
      container.ComposeParts(this);
      WrapPlugins();
    }

    private void WrapPlugins()
    {
      Plugins = new PluginList();
      foreach (var rawPlugin in RawPlugins)
      {
        var newPlugin = new Plugin(rawPlugin)
        {
          Settings = FindPluginSettings(rawPlugin)
        };
        Plugins.Add(newPlugin);
      }
    }

    public static PluginSettingList FindPluginSettings(ITrayKitPlugin plugin)
    {
      var settings = Utilities.PluginSettingsFinder.FindPluginSettings(plugin);
      return settings ?? new PluginSettingList();
    }

  }
}
