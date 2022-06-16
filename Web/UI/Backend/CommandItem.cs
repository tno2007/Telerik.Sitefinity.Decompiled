// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.CommandItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.Routing;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>
  /// Class for providing command item information such as Command Name, Arguments, Command Mode and etc.
  /// </summary>
  public class CommandItem
  {
    private bool uTestMode;
    private string title;
    private string description;

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.CommandItem" />.
    /// </summary>
    public CommandItem() => this.CancelNavigation = true;

    /// <summary>
    /// Specifies the name of the panel in which this item will be placed. Command panels are used to group commands and one command panel will be created per each unique name. Command panels will appear in the order they were created.
    /// </summary>
    public string PanelName { get; set; }

    /// <summary>
    /// Defines the Command Name that identifies uniquely the current item.
    /// </summary>
    public string CommandName { get; set; }

    /// <summary>Defines the arguments for the current command.</summary>
    public string CommandArgs { get; set; }

    /// <summary>Defines the parameter key to be used by the command.</summary>
    public string ParameterKey { get; set; }

    /// <summary>Defines the parameter to be passed with command.</summary>
    public string Parameter { get; set; }

    /// <summary>Defines parent pageId key to be used by the command.</summary>
    public string ParentIdKey { get; set; }

    /// <summary>Defines the parent pageId to be passed with command.</summary>
    public string ParentId { get; set; }

    /// <summary>
    /// Defines the display name of the current command.
    /// If ResourceClassId property is set this property is assumed to
    /// contain resource keys instead of actual value.
    /// </summary>
    public string Title
    {
      get
      {
        if (string.IsNullOrEmpty(this.ResourceClassId))
          return this.title ?? this.CommandName;
        if (string.IsNullOrEmpty(this.title))
          this.title = this.CommandName + nameof (Title);
        return this.uTestMode ? this.title : Res.Get(this.ResourceClassId, this.title);
      }
      set => this.title = value;
    }

    /// <summary>
    /// Provides description for the current command.
    /// The description will be displayed as additional information for the command.
    /// It should explain the implications of invoking this command.
    /// If ResourceClassId property is set this property is assumed to
    /// contain resource keys instead of actual value.
    /// </summary>
    public string Description
    {
      get
      {
        if (string.IsNullOrEmpty(this.ResourceClassId))
          return this.description;
        if (string.IsNullOrEmpty(this.description))
          this.description = this.CommandName + nameof (Description);
        return this.uTestMode ? this.description : Res.Get(this.ResourceClassId, this.description);
      }
      set => this.description = value;
    }

    /// <summary>Defines the name of the CSS class for this command.</summary>
    public string CssClass { get; set; }

    /// <summary>
    /// Gets or sets the global resource class ID to use for localized strings.
    /// If this property is set Title and Description properties are assumed to
    /// contain resource keys instead of actual values.
    /// </summary>
    public string ResourceClassId { get; set; }

    /// <summary>Gets or sets JavaScript function to call on click.</summary>
    public string ClientFunction { get; set; }

    /// <summary>
    /// Gets or sets the URL to link to when the link control is clicked.
    /// </summary>
    /// <returns>
    /// The URL to link to when the link control is clicked.
    /// The default value is an empty string ('').
    /// </returns>
    public string NavigateUrl { get; set; }

    /// <summary>Gets or set the key to retrieve route info.</summary>
    public string RouteKey { get; set; }

    /// <summary>
    /// Gets or sets a Boolean value indicating whether this command should be displayed as selected.
    /// </summary>
    public bool Selected { get; set; }

    /// <summary>
    /// Gets or sets a Boolean value indicating whether the "href" event should be canceled if
    /// there is function defined for the onclick event (ClientFunction property).
    /// </summary>
    public bool CancelNavigation { get; set; }

    /// <summary>
    /// Gets <see cref="T:System.Web.Routing.RouteValueDictionary" /> used for creating virtual path.
    /// </summary>
    /// <returns><see cref="T:System.Web.Routing.RouteValueDictionary" /></returns>
    public RouteValueDictionary GetRouteInfo()
    {
      if (string.IsNullOrEmpty(this.RouteKey))
        return (RouteValueDictionary) null;
      return new RouteValueDictionary()
      {
        {
          this.RouteKey,
          (object) this.CommandName
        }
      };
    }
  }
}
