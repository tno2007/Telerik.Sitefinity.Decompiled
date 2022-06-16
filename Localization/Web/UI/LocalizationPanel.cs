// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.LocalizationPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>Represents ControlPanel for global resources.</summary>
  public class LocalizationPanel : ProviderControlPanel<Page>, IPartialRouteHandler
  {
    private static RouteValueDictionary emptyValues = new RouteValueDictionary()
    {
      {
        "Filter",
        (object) null
      },
      {
        "Params",
        (object) null
      }
    };
    private static RouteValueDictionary defaultValues = new RouteValueDictionary()
    {
      {
        "Filter",
        (object) "AllLabels"
      },
      {
        "Params",
        (object) null
      }
    };
    private static readonly string LabelsName = Res.Get<Labels>().FilterByResourceEntryType;
    private const string PermissionsName = "Permissions";

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Web.UI.LocalizationPanel" />.
    /// </summary>
    public LocalizationPanel()
      : base(false)
    {
      this.ResourceClassId = typeof (PageResources).Name;
      this.Title = Res.Get<PageResources>().LabelsAndMassages;
    }

    /// <summary>
    /// When overridden this method returns a list of standard Command Panels.
    /// </summary>
    /// <param name="viewMode">The view mode.</param>
    /// <param name="commandsInfo">
    /// A list of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.CommandItem">ControlPanel.CommandItem</see> objects.
    /// </param>
    /// <param name="commandPanels">
    /// A list of Command Panels for this Control Panel.
    /// This list can be used to add and remove Command Panels.
    /// </param>
    /// <remarks>
    /// This method will automatically create Command Panels
    /// based on the information passed with CommandItem classes.
    /// One command panel will be created per each
    /// unique panel name specified in the collection of command items.
    /// Command panels will appear in the order they were created.
    /// </remarks>
    protected override void CreateStandardCommandPanels(
      string viewMode,
      IList<CommandItem> commandsInfo,
      IList<ICommandPanel> commandPanels)
    {
      if (commandsInfo == null)
        commandsInfo = (IList<CommandItem>) new List<CommandItem>();
      string virtualPath = RouteHelper.GetVirtualPath((IPartialRouteHandler) this, LocalizationPanel.emptyValues, UrlResolveOptions.Rooted | UrlResolveOptions.AppendTrailingSlash);
      if (this.PartialRequestContext == null)
        PageHelper.SetPartialRouteHandler((object) this, this.GetRequestContext(), "Params");
      RouteValueDictionary values = this.PartialRequestContext.RouteData.Values;
      string str;
      if (string.Equals(this.PartialRequestContext.RouteHandlerName, LocalizationPanel.LabelsName, StringComparison.OrdinalIgnoreCase))
      {
        str = (string) values["Filter"];
        if (string.IsNullOrEmpty(str))
          str = "AllLabels";
      }
      else
        str = string.Empty;
      commandsInfo.Add(new CommandItem()
      {
        PanelName = LocalizationPanel.LabelsName,
        CommandName = "AllLabels",
        ResourceClassId = this.ResourceClassId,
        ClientFunction = "LoadData(this)",
        NavigateUrl = virtualPath + "AllLabels",
        Selected = str.Equals("AllLabels", StringComparison.OrdinalIgnoreCase)
      });
      foreach (ObjectInfoAttribute allClassInfo in Res.GetManager(this.ProviderName).GetAllClassInfos(CultureInfo.InvariantCulture))
      {
        CommandItem commandItem = new CommandItem();
        commandItem.PanelName = LocalizationPanel.LabelsName;
        string url = string.IsNullOrEmpty(allClassInfo.Name) ? allClassInfo.ResourceClassId : allClassInfo.Name;
        commandItem.CommandName = url;
        commandItem.NavigateUrl = virtualPath + ServiceUtility.EncodeServiceUrl(url);
        commandItem.ClientFunction = string.Format("LoadData(this, [\"ClassId\",\"{0}\"])", (object) ServiceUtility.EncodeServiceUrl(url));
        commandItem.Selected = str.Equals(url, StringComparison.OrdinalIgnoreCase);
        if (!string.IsNullOrEmpty(allClassInfo.Title))
        {
          commandItem.Title = allClassInfo.Title;
          commandItem.Description = allClassInfo.Description;
        }
        else
          commandItem.ResourceClassId = allClassInfo.ResourceClassId;
        commandsInfo.Add(commandItem);
      }
      base.CreateStandardCommandPanels(viewMode, commandsInfo, commandPanels);
    }

    /// <summary>
    /// Returns the name of the default data provider for the module.
    /// </summary>
    /// <returns>
    /// A string representing the name of the default data provider for the module.
    /// </returns>
    protected override string GetDefaultProviderName() => Res.DefaultProviderName;

    /// <summary>
    /// When overridden this method returns a string array containing the names of
    /// available data providers.
    /// </summary>
    /// <returns>
    /// A string array containing the names of available data providers.
    /// </returns>
    protected override string[] GetProviderNames()
    {
      List<string> stringList = new List<string>(Res.DataProviders.Count);
      foreach (DataProviderBase dataProvider in (Collection<ResourceDataProvider>) Res.DataProviders)
        stringList.Add(dataProvider.Name);
      return stringList.ToArray();
    }

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews() => this.AddView<LabelsView>();

    /// <summary>
    /// Gets a collection of objects that derive from the
    /// <see cref="T:System.Web.Routing.RouteBase" /> class.
    /// </summary>
    /// <returns>
    /// An object that contains all the routes in the collection.
    /// </returns>
    public virtual RouteCollection CreateRoutes() => new RouteCollection()
    {
      {
        "Permissions",
        (RouteBase) new Route("Permissions/{*Params}", (IRouteHandler) new RouteHandlerBase("Permissions"))
      },
      {
        LocalizationPanel.LabelsName,
        (RouteBase) new Route("{Filter}", LocalizationPanel.defaultValues, (IRouteHandler) new RouteHandlerBase(LocalizationPanel.LabelsName))
      }
    };

    /// <summary>Registers child partial route handlers.</summary>
    /// <param name="list">A list of <see cref="T:Telerik.Sitefinity.Web.RouteInfo" /> objects.</param>
    public virtual void RegisterChildRouteHandlers(IList<RouteInfo> list)
    {
    }

    /// <summary>
    /// An object that encapsulates information about the requested route.
    /// </summary>
    public PartialRequestContext PartialRequestContext { get; set; }

    /// <summary>Gets or sets the parent route handler.</summary>
    public IPartialRouteHandler ParentRouteHandler { get; set; }
  }
}
