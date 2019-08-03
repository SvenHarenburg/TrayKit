using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrayKit.Base;

namespace TrayKit.Controls
{
  internal class TrayKitContextMenu : ContextMenuStrip
  {
    public const string BASE_ITEM_TAG = "base";

    public delegate void MenuItemClickedEventHandler(object sender, MenuItemClickedEventArgs e);
    public event MenuItemClickedEventHandler MenuItemClicked;

    protected virtual void OnMenuItemClicked(MenuItemClickedEventArgs e)
    {
      MenuItemClicked?.Invoke(this, e);
    }

    /// <summary>
    /// Determines the starting index from where plugin-menu-items will be added.
    /// </summary>
    private int pluginStartingIndex;

    public TrayKitContextMenu() : base()
    {
      AddBaseItems();
    }

    private void AddBaseItems()
    {
      AddItem(ContextMenuBaseItem.OpenTrayKit.ToString(), "Open TrayKit", BASE_ITEM_TAG, null, true);
      AddItem(ContextMenuBaseItem.ExitTrayKit.ToString(), "Exit TrayKit", BASE_ITEM_TAG, null, true);

      // Set to 1 because Plugin-Items should be added after the "Open TrayKit"-Item which is at index 0.
      pluginStartingIndex = 1;
    }

    private ToolStripMenuItem AddItem(string name, string text, object tag, Image image, bool addClickEventHandler)
    {
      var newItem = new ToolStripMenuItem()
      {
        Name = name,
        Text = text,
        Tag = tag,
        Image = image
      };
      if (addClickEventHandler) newItem.Click += Item_Click;
      Items.Add(newItem);

      return newItem;
    }

    private ToolStripMenuItem AddItem(string name, string text, object tag, Image image, bool addClickEventHandler, int index)
    {
      var newItem = new ToolStripMenuItem()
      {
        Name = name,
        Text = text,
        Tag = tag,
        Image = image
      };
      if (addClickEventHandler) newItem.Click += Item_Click;
      Items.Insert(index, newItem);

      return newItem;
    }

    /// <summary>
    /// Removes all non-base-MenuItems.
    /// </summary>
    public void Reset()
    {
      var nonBaseItems = (from ToolStripMenuItem q in Items
                          where q.Tag.ToString() != BASE_ITEM_TAG
                          select q).ToList();

      foreach (var item in nonBaseItems)
      {
        Items.Remove(item);
        item.Dispose();
      }
    }

    // TODO: Refactor this. Maybe create a ContextMenuItemCreator-Class which creates the items when given a plugin.
    /// <summary>
    /// Creates menu-items for a plugin.
    /// </summary>
    /// <param name="plugin">The plugin which the menu-items should be created for.</param>
    public void ImportPlugin(ITrayKitPlugin plugin)
    {
      if (plugin == null)
      {
        throw new ArgumentNullException(nameof(plugin));
      }

      var isFirstPlugin = !(from ToolStripMenuItem q in Items
                            where q.Tag.ToString() != BASE_ITEM_TAG
                            select q).Any();

      var newItemIndex = pluginStartingIndex;

      if (!isFirstPlugin)
      {
        newItemIndex = (from ToolStripMenuItem q in Items
                        where q.Tag.ToString() != BASE_ITEM_TAG
                        select Items.IndexOf(q)).Max() + 1; // + 1 because the new item has to be placed behind the last plugin-item already in the list.
      }

      if (!plugin.Commands.Any())
      {
        AddItem(plugin.Name, plugin.Name, plugin.Name, plugin.Image, true, newItemIndex);
      }
      else if (plugin.Commands.Count == 1)
      {
        var command = plugin.Commands[0];
        AddItem(command.Name, command.Name, command.GetFullCommandKey(), command.Image, true, newItemIndex);
      }
      else
      {
        var pluginItem = AddItem(plugin.Name, plugin.Name, plugin.Name, plugin.Image, false, newItemIndex);
        foreach (var command in (from q in plugin.Commands
                                 orderby q.SortPosition ascending
                                 select q))
        {
          var commandItem = new ToolStripMenuItem()
          {
            Name = command.Name,
            Text = command.Name,
            Tag = command.GetFullCommandKey(),
            Image = command.Image
          };
          commandItem.Click += Item_Click;
          pluginItem.DropDownItems.Add(commandItem);
        }
      }
    }

    public void RemovePlugin(ITrayKitPlugin plugin)
    {
      ToolStripMenuItem menuItem = null;

      if (plugin.Commands.Count == 1)
      {
        var command = plugin.Commands[0];
        menuItem = (from ToolStripMenuItem q in Items
                    where q.Tag.ToString() == command.GetFullCommandKey()
                    select q).FirstOrDefault();
      }
      else
      {
        menuItem = (from ToolStripMenuItem q in Items
                    where q.Tag.ToString() == plugin.Name
                    select q).FirstOrDefault();
      }

      if (menuItem != null)
      {
        Items.Remove(menuItem);
      }

    }

    private void Item_Click(object sender, EventArgs e)
    {
      var menuItem = (ToolStripMenuItem)sender;
      OnMenuItemClicked(new MenuItemClickedEventArgs()
      {
        Name = menuItem.Name,
        Tag = menuItem.Tag.ToString()
      });
    }

  }
}

