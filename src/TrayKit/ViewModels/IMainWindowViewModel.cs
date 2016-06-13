using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrayKit.Controllers;

namespace TrayKit.ViewModels
{
  internal interface IMainWindowViewModel
  {
    //Events:
    event EventHandler PluginsLoaded;

    //Properties:
    string AppVersion { get; }
    SettingsController SettingsController { get; }
    PluginController PluginController { get; }

    //Functions:
    void OnWindowClosing(object sender, CancelEventArgs e);

    //Commands:
    ICommand RefreshPluginsCommand { get; set; }

  }
}
