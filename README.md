###### NOTE: I started this project to practice and improve my programming skills. If you find yourself looking at the code thinking something like "This could've been done better.", I would appreciate it if you would tell me about it and maybe share your idea with me.


# TRAYKIT

TrayKit is a .NET desktop application designed to help with often reocurring tasks. The application runs in the system tray and uses a contextmenu accessible via right clicking to provide commands.
The commands itself are provided by plugins which will be loaded by the TrayKit application. You can create the plugins you need by creating a plugin-library and implementing the required interfaces. The process of this is explained further below.



## SETTINGS-MANAGEMENT

TrayKit allows it's plugins to publish a list of settings that will be managed by TrayKit itself. This includes saving, loading and editing(Base types only for now) of those settings. I have implemented this so not every plugin has to manage it's own settings including loading and saving them. This would probably trash the application-folder with multiple settings-files and probably create conflicts between plugins.
TrayKit stores all the settings in one single json file in the application directory. The settings get loaded when the application starts and saved back to the file when it exists.

![Settings-Editor](http://i.imgur.com/KpWXJt8.png "Settings-Editor image")

Example of a plugins settings being displayed for editing by TrayKit.


## CREATING PLUGINS

To create a plugin you need to create a new library and implement the interfaces provided by the TrayKit.Base library. To get access to the library you can either download the repository or add the Nuget-package "TrayKit.Base" to the project(NOT YET ON NUGET, WILL ADD SOON). I will explain the creation process with the SamplePlugin provided in the TrayKit-solution.

**Step 1:**
Create the plugin-class by implementing "ITrayKitPlugin" and marking the class itself with the "Export"-attribute provided by the MEF-framework:

```C#
  [Export(typeof(ITrayKitPlugin))]
  public class SamplePlugin : ITrayKitPlugin
  {
	private const string PluginName = "TrayKit SamplePlugin";
    public List<ITrayKitPluginCommand> Commands { get; }
    public Image Image { get; }

	public string Name { get { return PluginName; } }

    public SamplePlugin()
    {
      Commands = new List<ITrayKitPluginCommand>()
      {
        new HelloWorldCommand(this, Settings) { SortPosition = 0 },
        new TwoPlusTwoCommand(this) { SortPosition = 1 }
      };
      Image = SystemIcons.Asterisk.ToBitmap();
    }
  }
```


Explanation:

* **private const string PluginName = "TrayKit SamplePlugin";** - Stores the name of the plugin. Not necessary to have, just preference by me.
* **public List<ITrayKitPluginCommand> Commands { get; }** - This is where the TrayKit-application looks for commands to show. I will explain this collection further below.
* **public Image Image { get; }** - This is where the TrayKit-application looks for an image to display besides the plugin-item in the contextmenu.
* **public string Name { get { return PluginName; } }** - The string returned by this property will be used when displaying the plugin.
* **public SamplePlugin()** - Constructor where I initialized the command-collection and set the image used to display in the context-menu.

![Explanation image](http://i.imgur.com/XzF075c.png "Plugin-class explanation")

**Step 2:**
Create a command-class by implementing "ITrayKitPluginCommand":

```C#
  public class HelloWorldCommand : ITrayKitPluginCommand
  {
    private const string CommandName = "Hello World!";

    public string Name { get { return CommandName; } }
    public ITrayKitPlugin Plugin { get; }
    public int SortPosition { get; set; }
    public Image Image { get; }

    public HelloWorldCommand(ITrayKitPlugin plugin)
    {
      Plugin = plugin;
      Image = SystemIcons.Shield.ToBitmap();
    }

    public void Execute()
    {
      MessageBox.Show("Hello World!");
    }
  }
```
		
Explanation:

* **private const string CommandName = "Hello World!";** - Stores the name of the command. Not necessary to have, just a preference by me.
* **public string Name { get { return CommandName; } }** - The string returned by this property will be used when displaying the command.
* **public ITrayKitPlugin Plugin { get; }** - Needs to return the plugin which the command belongs to.
* **public int SortPosition { get; set; }** - Is used to define the order the commands will be displayed in the contextmenu.
* **public Image Image { get; }** - This is where the TrayKit-application looks for an image to display besides the command-item in the contextmenu.
* **public HelloWorldCommand(ITrayKitPlugin plugin)** - Constructor where I set the reference to the plugin and the image to be displayed.
* **public void Execute(){..}** - This is the function which will be called when a command is clicked on in the contextmenu.

![Explanation image](http://i.imgur.com/JySiOaR.png	 "Command-class explanation")
	
	
**Step 3(Optional):**
Using the settings-feature provided by TrayKit. To use the settings-management feature provided by TrayKit(which is explained further up), you need to create a public property on your ITrayKitPlugin-implementation and mark it with the TrayKit.Base.Settings.TrayKitPluginSettingsContainer-Attribute. In the SamplePlugin I created the following property on the Plugin-class:

```C#
[Base.Settings.TrayKitPluginSettingsContainer()]
public Settings Settings { get; set; }
```
	
	
By applying the attribute, TrayKit knows that it should look into the marked property and manage settings for the plugin. While looking into the property the application looks for members marked with the TrayKit.Base.Settings.TrayKitPluginSetting-Attribute. By applying the attribute, the properties value will be managed by TrayKit. An implementation could look like this:
	
```C#	
public class Settings
  {
    [TrayKitPluginSetting]
    public bool ABoolValue { get; set; }
    [TrayKitPluginSetting]
    public decimal ADecimalValue { get; set; }
    [TrayKitPluginSetting]
    public double ADoubleValue { get; set; }
    [TrayKitPluginSetting]
    public float AFloatValue { get; set; }
    [TrayKitPluginSetting(Description ="This is a setting that stores an integer value.")]
    public int AnIntegerValue { get; set; }
    [TrayKitPluginSetting]
    public string AStringValue { get; set; }    
    [TrayKitPluginSetting]
    public DateTime ADateTimeValue { get; set; }
  }
```	
	
	
Notice that the Setting-Attribute provides several properties like "MaxValue" or "MinValue". Not all of these are used by TrayKit yet.
	
# LICENSE

MIT License

Copyright (c) [2016] [Sven Harenburg]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
