using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrayKit.Base;

namespace TrayKit.Extensions
{
  internal static class ITrayKitPluginExtensions
  {
    public static Assembly GetPluginAssembly(this ITrayKitPlugin plugin)
    {
      return plugin?.GetType().Assembly;
    }
    
    public static string GetPluginAssemblyName(this ITrayKitPlugin plugin)
    {
      return plugin?.GetType().Assembly.GetName().Name;
    }
  }
}
