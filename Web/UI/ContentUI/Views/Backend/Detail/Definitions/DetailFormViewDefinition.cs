// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions.DetailFormViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions
{
  /// <summary>
  /// A definition class that the determines the way in which the instance of
  /// backend master grid view will be constructed.
  /// </summary>
  public class DetailFormViewDefinition : 
    ContentViewDetailDefinition,
    IDetailFormViewDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private IWidgetBarDefinition toolbar;
    private bool? showNavigation;
    private bool? showTopToolbar;
    private string webServiceBaseUrl;
    private bool? unlockDetailItemOnExit;
    private bool? isToRenderTranslationView;
    private string alternativeTitle;
    private string itemTemplate;
    private bool? doNotUseContentItemContext;
    private MultilingualMode multilingualMode;
    private string additionalCreateCommands;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions.DetailFormViewDefinition" /> class.
    /// </summary>
    public DetailFormViewDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions.DetailFormViewDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DetailFormViewDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the toolbar.</summary>
    /// <value>The toolbar.</value>
    public IWidgetBarDefinition Toolbar => this.ResolveProperty<IWidgetBarDefinition>(nameof (Toolbar), this.toolbar);

    /// <summary>Gets or sets if a top toolbar should be displayed</summary>
    /// <value>The show top toolbar.</value>
    public bool? ShowTopToolbar
    {
      get => this.GetShowTopToolbar(this.ResolveProperty<bool?>(nameof (ShowTopToolbar), this.showTopToolbar));
      set => this.showTopToolbar = value;
    }

    /// <summary>Gets or sets the base url of the web service.</summary>
    /// <value></value>
    public string WebServiceBaseUrl
    {
      get => this.ResolveProperty<string>(nameof (WebServiceBaseUrl), this.webServiceBaseUrl);
      set => this.webServiceBaseUrl = value;
    }

    /// <summary>
    /// Gets or sets if the view will show the previous/next navigation buttons.
    /// </summary>
    public bool? ShowNavigation
    {
      get => this.ResolveProperty<bool?>(nameof (ShowNavigation), this.showNavigation);
      set => this.showNavigation = value;
    }

    /// <summary>
    /// Specify if the system will automatically unlock the currently edited item, when exiting the detail screen
    /// Exiting the screen is registered on Back to all items, browser back button and browse window close
    /// Unlocking is executed only if the screen was opened in Write mode
    /// The unlocking uses web service REST convention to call the WebServiceBaseUrl/temp/itemid with DELETE HTTP verb
    /// </summary>
    /// <value></value>
    public bool? UnlockDetailItemOnExit
    {
      get => this.ResolveProperty<bool?>(nameof (UnlockDetailItemOnExit), this.unlockDetailItemOnExit);
      set => this.unlockDetailItemOnExit = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to render the translation view.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if is to render translation view; otherwise, <c>false</c>.
    /// </value>
    public bool? IsToRenderTranslationView
    {
      get => this.ResolveProperty<bool?>(nameof (IsToRenderTranslationView), this.isToRenderTranslationView);
      set => this.isToRenderTranslationView = value;
    }

    /// <summary>
    /// Gets or sets the alternative title for the detail view.
    /// </summary>
    /// <value>The alternative title.</value>
    /// <remarks>
    /// Used in the backEnd detail form view when the edit dialog is used for creating a different language version of the item.
    /// </remarks>
    public string AlternativeTitle
    {
      get => this.ResolveProperty<string>(nameof (AlternativeTitle), this.alternativeTitle);
      set => this.alternativeTitle = value;
    }

    /// <summary>
    /// Gets or sets the item template string for the detail view (to be used to get the right item type, if required).
    /// </summary>
    /// <value>The string representing the item template.</value>
    /// <remarks>
    /// Used in the backEnd detail form view when the edit dialog is used for getting a new item.
    /// </remarks>
    public string ItemTemplate
    {
      get => this.ResolveProperty<string>(nameof (ItemTemplate), this.itemTemplate);
      set => this.itemTemplate = value;
    }

    /// <summary>
    /// If set to false, the generated JSON on the client will be the value of the Item property of an 'item context' object.
    /// If set to true, the generated JSON will be posted as is to the web service.
    /// False is required for compatilibity with ContentServiceBase. By default, it is false.
    /// </summary>
    public bool DoNotUseContentItemContext
    {
      get => this.ResolveProperty<bool?>(nameof (DoNotUseContentItemContext), this.doNotUseContentItemContext).GetValueOrDefault();
      set => this.doNotUseContentItemContext = new bool?(value);
    }

    /// <inheritdoc />
    public MultilingualMode MultilingualMode
    {
      get => this.ResolveProperty<MultilingualMode>(nameof (MultilingualMode), this.multilingualMode);
      set => this.multilingualMode = value;
    }

    /// <inheritdoc />
    public string AdditionalCreateCommands
    {
      get => this.ResolveProperty<string>(nameof (AdditionalCreateCommands), this.additionalCreateCommands);
      set => this.additionalCreateCommands = value;
    }
  }
}
