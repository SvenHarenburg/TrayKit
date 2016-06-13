using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrayKit.Base;

namespace TrayKit.Models
{
  /// <summary>
  /// Wraps an ITrayKitPlugin-instance and provides additional functionality.
  /// </summary>
  internal class Plugin
  {
    public delegate void EnabledChangedEventHandler(object sender, EventArgs e);
    public event EnabledChangedEventHandler EnabledChanged;
    
    public ITrayKitPlugin TrayKitPlugin { get; }

    public Settings.PluginSettingList Settings { get; set; }

    private bool enabled;
    public bool Enabled
    {
      get
      {
        return enabled;
      }
      set
      {
        if (!enabled == value)
        {
          enabled = value;
          OnEnabledChanged(new EventArgs());
        }
      }
    }

    public Plugin(ITrayKitPlugin trayKitPlugin)
    {
      if (trayKitPlugin == null)
      {
        throw new ArgumentNullException(nameof(trayKitPlugin));
      }
      TrayKitPlugin = trayKitPlugin;
      Enabled = true;
    }

    protected virtual void OnEnabledChanged(EventArgs e)
    {
      EnabledChanged?.Invoke(this, new EventArgs());
    }

  }
}
