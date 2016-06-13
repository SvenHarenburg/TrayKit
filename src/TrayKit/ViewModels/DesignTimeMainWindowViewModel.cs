//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using TrayKit.Controllers;

//namespace TrayKit.ViewModels
//{
//  internal  class DesignTimeMainWindowViewModel : IMainWindowViewModel
//  {
//    public event EventHandler PluginsLoaded;
//    public ObservableCollection<Models.Plugin> Plugins { get; }

//    public DesignTimeMainWindowViewModel()
//    {
//      Plugins = new ObservableCollection<Models.Plugin>()
//      {
//        new Models.Plugin(new DesignTimeClasses.TrayKitPlugin()),
//        new Models.Plugin(new DesignTimeClasses.TrayKitPlugin())
//      };
//    }

//    public string AppVersion
//    {
//      get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
//    }

//    public ICommand RefreshPluginsCommand
//    {
//      get
//      {
//        throw new NotImplementedException();
//      }

//      set
//      {
//        throw new NotImplementedException();
//      }
//    }

//    public SettingsController SettingsController
//    {
//      get
//      {
//        throw new NotImplementedException();
//      }
//    }

//    protected virtual void OnPluginsLoaded(EventArgs e)
//    {
//      PluginsLoaded?.Invoke(this, new EventArgs());
//    }

//  }
//}
