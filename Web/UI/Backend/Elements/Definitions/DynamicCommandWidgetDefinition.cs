// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DynamicCommandWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>Definition class for DynamicCommandWidget</summary>
  public class DynamicCommandWidgetDefinition : 
    WidgetDefinition,
    IDynamicCommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    private int pageSize;
    private string headerText;
    private string headerTextCssClass;
    private string moreLinkText;
    private string lessLinkText;
    private string moreLinkCssClass;
    private string lessLinkCssClass;
    private string selectedItemCssClass;
    private string baseServiceUrl;
    private string childItemsServiceUrl;
    private string predecessorServiceUrl;
    private IDictionary<string, string> urlParameters;
    private List<IDynamicItemDefinition> items;
    private List<IDynamicItemDefinition> customItems;
    private BindCommandListTo bindTo;
    private string clientItemTemplate;
    private Type contentType;
    private Guid dynamicModuleTypeId;
    private string commandName;
    private string parentDataKeyName;
    private string selectedValue;
    private string sortExpression;
    private bool isFilterCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public DynamicCommandWidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DynamicCommandWidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public DynamicCommandWidgetDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets the default size of one page of items in the list
    /// </summary>
    /// <value>The default size of one page of items in the list.</value>
    public int PageSize
    {
      get => this.ResolveProperty<int>(nameof (PageSize), this.pageSize);
      set => this.pageSize = value;
    }

    /// <summary>
    /// Gets or sets the text for the header that appears before the combo/list of commands
    /// </summary>
    /// <value></value>
    public string HeaderText
    {
      get => this.ResolveProperty<string>(nameof (HeaderText), this.headerText);
      set => this.headerText = value;
    }

    /// <summary>
    /// Gets or sets the css class for the control that displays the header label
    /// </summary>
    /// <value></value>
    public string HeaderTextCssClass
    {
      get => this.ResolveProperty<string>(nameof (HeaderTextCssClass), this.headerTextCssClass);
      set => this.headerTextCssClass = value;
    }

    /// <summary>
    /// Gets or sets the text for the link which needs to be clicked to show more items
    /// </summary>
    /// <value>
    /// The text for the link which needs to be clicked to show more items.
    /// </value>
    public string MoreLinkText
    {
      get => this.ResolveProperty<string>(nameof (MoreLinkText), this.moreLinkText);
      set => this.moreLinkText = value;
    }

    /// <summary>
    /// Gets or sets the css class of the link used to display more items when bound on the client
    /// </summary>
    /// <value></value>
    public string MoreLinkCssClass
    {
      get => this.ResolveProperty<string>(nameof (MoreLinkCssClass), this.moreLinkCssClass);
      set => this.moreLinkCssClass = value;
    }

    /// <summary>Gets or sets the text for the link showing less items</summary>
    /// <value></value>
    public string LessLinkText
    {
      get => this.ResolveProperty<string>(nameof (LessLinkText), this.lessLinkText);
      set => this.lessLinkText = value;
    }

    /// <summary>
    /// Gets or sets the css class for the link showing less items.
    /// </summary>
    /// <value></value>
    public string LessLinkCssClass
    {
      get => this.ResolveProperty<string>(nameof (LessLinkCssClass), this.lessLinkCssClass);
      set => this.lessLinkCssClass = value;
    }

    /// <summary>Gets or sets the selected item pageId.</summary>
    /// <value>The selected item pageId.</value>
    public string SelectedItemCssClass
    {
      get => this.ResolveProperty<string>(nameof (SelectedItemCssClass), this.selectedItemCssClass);
      set => this.selectedItemCssClass = value;
    }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public string BaseServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (BaseServiceUrl), this.baseServiceUrl);
      set => this.baseServiceUrl = value;
    }

    /// <summary>
    /// Gets or sets the URL for the service used to get child taxa
    /// </summary>
    /// <value></value>
    public string ChildItemsServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (ChildItemsServiceUrl), this.childItemsServiceUrl);
      set => this.childItemsServiceUrl = value;
    }

    /// <summary>
    /// Gets or sets the URL for the service used to get predecessor taxa
    /// </summary>
    /// <value></value>
    public string PredecessorServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (PredecessorServiceUrl), this.predecessorServiceUrl);
      set => this.predecessorServiceUrl = value;
    }

    /// <summary>Gets or sets the URL parameters.</summary>
    /// <value>The URL parameters.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public IDictionary<string, string> UrlParameters => this.ResolveProperty<IDictionary<string, string>>(nameof (UrlParameters), this.urlParameters);

    /// <summary>Gets or sets the data source.</summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IDynamicItemDefinition> Items
    {
      get
      {
        if (this.items == null)
        {
          this.items = new List<IDynamicItemDefinition>();
          if (this.ConfigDefinition != null)
          {
            foreach (IDefinition definition in ((IDynamicCommandWidgetDefinition) this.ConfigDefinition).Items)
              this.items.Add((IDynamicItemDefinition) definition.GetDefinition());
          }
        }
        return this.items;
      }
    }

    /// <summary>
    /// Gets or sets the custom commands list. This is an additional data source that enables the DynamicCommandWidget
    /// to gave some of the items in the list fire custom commands that need special handling
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IDynamicItemDefinition> CustomItems
    {
      get
      {
        if (this.customItems == null)
        {
          this.customItems = new List<IDynamicItemDefinition>();
          if (this.ConfigDefinition != null)
          {
            foreach (IDefinition customItem in ((IDynamicCommandWidgetDefinition) this.ConfigDefinition).CustomItems)
              this.customItems.Add((IDynamicItemDefinition) customItem.GetDefinition());
          }
        }
        return this.customItems;
      }
    }

    /// <summary>
    /// Specifies how the list of commands in the dynamic command widget is rendered
    /// </summary>
    /// <value></value>
    public BindCommandListTo BindTo
    {
      get => this.ResolveProperty<BindCommandListTo>(nameof (BindTo), this.bindTo);
      set => this.bindTo = value;
    }

    /// <summary>Sort expression for the dynamic command widget</summary>
    /// <value></value>
    public string SortExpression
    {
      get => this.ResolveProperty<string>(nameof (SortExpression), this.sortExpression);
      set => this.sortExpression = value;
    }

    /// <summary>
    /// The item template used when binding on the client (in client and hierarchical mode)
    /// </summary>
    /// <value></value>
    public string ClientItemTemplate
    {
      get => this.ResolveProperty<string>(nameof (ClientItemTemplate), this.clientItemTemplate);
      set => this.clientItemTemplate = value;
    }

    /// <summary>The content type</summary>
    /// <value></value>
    public Type ContentType
    {
      get => this.ResolveProperty<Type>(nameof (ContentType), this.contentType);
      set => this.contentType = value;
    }

    /// <summary>The dynamic module type Id</summary>
    public Guid DynamicModuleTypeId
    {
      get => this.ResolveProperty<Guid>(nameof (DynamicModuleTypeId), this.dynamicModuleTypeId);
      set => this.dynamicModuleTypeId = value;
    }

    /// <summary>The command name that the widget fires</summary>
    public string CommandName
    {
      get => this.ResolveProperty<string>(nameof (CommandName), this.commandName);
      set => this.commandName = value;
    }

    /// <summary>Gets or sets the parent data key name</summary>
    /// <value></value>
    public string ParentDataKeyName
    {
      get => this.ResolveProperty<string>(nameof (ParentDataKeyName), this.parentDataKeyName);
      set => this.parentDataKeyName = value;
    }

    /// <summary>Gets or sets the selected item in the widget.</summary>
    public string SelectedValue
    {
      get => this.ResolveProperty<string>(nameof (SelectedValue), this.selectedValue);
      set => this.selectedValue = value;
    }

    /// <summary>
    /// Determines this command is a filter command (e.g. a filter on the sidebar)
    /// </summary>
    public bool IsFilterCommand
    {
      get => this.ResolveProperty<bool>(nameof (IsFilterCommand), this.isFilterCommand);
      set => this.isFilterCommand = value;
    }

    IEnumerable<IDynamicItemDefinition> IDynamicCommandWidgetDefinition.Items => this.Items.Cast<IDynamicItemDefinition>();

    IEnumerable<IDynamicItemDefinition> IDynamicCommandWidgetDefinition.CustomItems => this.CustomItems.Cast<IDynamicItemDefinition>();
  }
}
