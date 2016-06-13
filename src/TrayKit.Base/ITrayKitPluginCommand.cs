using System.Drawing;

namespace TrayKit.Base
{
  /// <summary>
  /// Defines a command that can be used by a <see cref="ITrayKitPlugin"/>.
  /// </summary>
  public interface ITrayKitPluginCommand
  {   
     
    /// <summary>
    /// The name of the command.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The position the command will have in the TrayKit contextmenu.
    /// </summary>
    int SortPosition { get; set; }

    /// <summary>
    /// The Plugin which will expose the command.
    /// </summary>
    ITrayKitPlugin Plugin { get; }

    /// <summary>
    /// The function that will be called by the TrayKit-application when the command will be clicked on.
    /// </summary>
    void Execute();

    /// <summary>
    /// The image that will be shown besides the command in the contextmenu.
    /// </summary>
    Image Image { get; }
  }
}