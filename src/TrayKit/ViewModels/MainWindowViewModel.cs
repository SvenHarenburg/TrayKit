using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrayKit.Controllers;

namespace TrayKit.ViewModels
{
  internal class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
  {
    public PluginController PluginController { get; private set; }
    public SettingsController SettingsController { get; private set; }

    public event EventHandler PluginsLoaded;
    protected virtual void OnPluginsLoaded(EventArgs e)
    {
      PluginsLoaded?.Invoke(this, new EventArgs());
    }
    
    public MainWindowViewModel()
    {
      PluginController = new PluginController();
      LoadPlugins();
      InitializeSettingsController();
      RefreshPluginsCommand = new ActionCommand(OnRefreshPluginsExecuted, OnRefreshPluginsCanExecute);
    }

    private void InitializeSettingsController()
    {
      SettingsController = new SettingsController();
      SettingsController.LoadSettings();

      var settings = SettingsController.Settings.PluginSettings;

      foreach (var plugin in PluginController.Plugins)
      {
        var savedpluginSettings = (from Models.Settings.IPluginSetting q in settings
                              where q.PluginAssembly == plugin.TrayKitPlugin.GetType().Assembly.GetName().Name
                              select q);

        foreach (var setting in plugin.Settings)
        {
          var savedSetting = (from Models.Settings.IPluginSetting q in savedpluginSettings
                              where q.PropertyName == setting.PropertyName
                              select q).FirstOrDefault();

          if (savedSetting != null)
          {
            var value = savedSetting.GetType().GetProperty("Value").GetValue(savedSetting);
            setting.GetType().GetProperty("Value").SetValue(setting, value);
          }
          else
          {
            settings.Add(setting);
          }
        }
      }

      settings.Clear();

      var allSettings = (from p in PluginController.Plugins
                         from s in p.Settings
                         select s);
      settings.AddRange(allSettings);
    }

    private void LoadPlugins()
    {
      PluginController.LoadPlugins();      
      OnPluginsLoaded(new EventArgs());
    }

    public string AppVersion
    {
      get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
    }

    public void OnWindowClosing(object sender, CancelEventArgs e)
    {
      SettingsController?.SaveSettings();
    }



    public ICommand RefreshPluginsCommand { get; set; }
    

    private void OnRefreshPluginsExecuted(object parameter)
    {
      LoadPlugins();
    }

    private bool OnRefreshPluginsCanExecute(object parameter)
    {
      return true;
    }

  }

}

