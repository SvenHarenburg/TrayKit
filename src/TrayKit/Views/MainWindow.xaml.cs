using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using TrayKit.ViewModels;
using System.Diagnostics;
using TrayKit.Base;
using System.Collections.Specialized;

namespace TrayKit.Views
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private IMainWindowViewModel viewModel;

    // Had to use the Windows.Forms ContextMenu because WPF does not have a NotifyIcon
    // and the Windows.Forms NotifyIcon does not accept the WPF ContextMenu.
    private Controls.TrayKitContextMenu ctmNotifyIcon;
    private NotifyIcon notifyIcon;

    public MainWindow()
    {
      InitializeComponent();
      try
      {
        InitViewModel();
        InitWindowEvents();

        InitializeContextMenu();
        InitializeNotifyIcon();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.ToString());
        System.Windows.MessageBox.Show(ex.ToString());
      }
    }

    private void InitWindowEvents()
    {
      Closing += viewModel.OnWindowClosing;
    }

    private void InitViewModel()
    {
      viewModel = (IMainWindowViewModel)Resources[nameof(MainWindowViewModel)];
      if (viewModel == null)
      {
        throw new Exception("ViewModel could not be found.");
      }
      viewModel.PluginsLoaded += ViewModel_PluginsLoaded;
      AddPluginEventHandlers();
    }

    private void ViewModel_PluginsLoaded(object sender, EventArgs e)
    {
      ctmNotifyIcon.Reset();
      AddPluginsToContextMenu();
      AddPluginEventHandlers();
    }

    private void AddPluginEventHandlers()
    {
      foreach (var plugin in viewModel.PluginController.Plugins)
      {
        plugin.EnabledChanged -= Plugin_EnabledChanged;
        plugin.EnabledChanged += Plugin_EnabledChanged;

        plugin.CommandCollectionChanged -= Plugin_CommandCollectionChanged;
        plugin.CommandCollectionChanged += Plugin_CommandCollectionChanged;

      }
    }

    private void Plugin_CommandCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      var plugin = (Models.Plugin)sender;

      /* This is kind of a hack. Instead of checking what changed and only do an update
       * on the plugin in the contextmenu, I reimport the whole plugin.
       * I chose not to implement new functions for updating because it would take time I currently don't have
       * and reimporting the plugins shouldn't take much resources.  
       */
      ctmNotifyIcon.RemovePlugin(plugin.TrayKitPlugin);
      ctmNotifyIcon.ImportPlugin(plugin.TrayKitPlugin);
    }

    private void Plugin_EnabledChanged(object sender, EventArgs e)
    {
      var plugin = (Models.Plugin)sender;
      if (plugin.Enabled)
      {
        ctmNotifyIcon.ImportPlugin(plugin.TrayKitPlugin);
      }
      else
      {
        ctmNotifyIcon.RemovePlugin(plugin.TrayKitPlugin);
      }
    }
        
    protected override void OnStateChanged(EventArgs e)
    {
      if (WindowState == WindowState.Minimized)
      {
        Hide();
      }

      base.OnStateChanged(e);
    }

    private void InitializeNotifyIcon()
    {
      notifyIcon = new System.Windows.Forms.NotifyIcon
      {
        Icon = System.Drawing.SystemIcons.Information,
        Visible = true,
        ContextMenuStrip = ctmNotifyIcon
      };

      notifyIcon.DoubleClick +=
          delegate
          {
            Show();
            WindowState = WindowState.Normal;
          };
    }

    #region ctmNotifyIcon

    private void InitializeContextMenu()
    {
      ctmNotifyIcon = new Controls.TrayKitContextMenu();
      AddPluginsToContextMenu();
      ctmNotifyIcon.MenuItemClicked += CtmNotifyIcon_MenuItemClicked;
    }

    private void CtmNotifyIcon_MenuItemClicked(object sender, Controls.MenuItemClickedEventArgs e)
    {
      if (e.Tag == Controls.TrayKitContextMenu.BASE_ITEM_TAG)
      {
        if (e.Name == Controls.ContextMenuBaseItem.OpenTrayKit.ToString())
        {
          Show();
          WindowState = WindowState.Normal;
        }
        else if (e.Name == Controls.ContextMenuBaseItem.ExitTrayKit.ToString())
        {
          Close();
        }
      }
      else
      {
        var command = (from p in viewModel.PluginController.Plugins
                       from cmd in p.TrayKitPlugin.Commands
                       where cmd.GetFullCommandKey() == e.Tag
                       select cmd).FirstOrDefault();
        if (command == null)
        {
          throw new Exception($"Command not found: {e.Tag}");
        }
        command.Execute();
      }
    }
    
    private void AddPluginsToContextMenu()
    {
      foreach (var plugin in viewModel.PluginController.Plugins)
      {
        ctmNotifyIcon.ImportPlugin(plugin.TrayKitPlugin);
      }
    }    
    #endregion
  }
}
