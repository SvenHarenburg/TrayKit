using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayKit.Controls
{
  /// <summary>
  /// Contains information about the MenuItem that was clicked on.
  /// </summary>
  internal class MenuItemClickedEventArgs : EventArgs
  {
    
    /// <summary>
    /// The Name of the MenuItem that was clicked on.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The tag of the MenuItem that was clicked on.
    /// </summary>
    public string Tag { get; set; }


  }
}
