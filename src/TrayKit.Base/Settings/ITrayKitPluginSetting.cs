using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayKit.Base.Settings
{
  public interface ITrayKitPluginSetting
  {
    string DisplayName { get; set; }
    bool Visible { get; set; }
    bool ReadOnly { get; set; }
    string Description { get; set; }
    int MaxLength { get; set; }
    int MaxValue { get; set; }
    int MinValue { get; set; }
  }
}
