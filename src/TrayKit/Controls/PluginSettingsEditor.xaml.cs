using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrayKit.Models.Settings;

namespace TrayKit.Controls
{
  internal partial class PluginSettingsEditor : UserControl
  {
    public PluginSettingList PluginSettings
    {
      get { return (PluginSettingList)GetValue(PluginSettingsProperty); }
      set { SetValue(PluginSettingsProperty, value); }
    }

    public static readonly DependencyProperty PluginSettingsProperty = DependencyProperty.Register(
                "PluginSettings",
                typeof(PluginSettingList),
                typeof(PluginSettingsEditor),
                new FrameworkPropertyMetadata(null,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
    );


    public PluginSettingsEditor()
    {
      InitializeComponent();
    }

  
  }
}
