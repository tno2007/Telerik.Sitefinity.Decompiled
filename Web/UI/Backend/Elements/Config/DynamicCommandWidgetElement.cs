// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.DynamicCommandWidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  public class DynamicCommandWidgetElement : 
    WidgetElement,
    IDynamicCommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.DynamicCommandWidgetElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public DynamicCommandWidgetElement(ConfigElement parent)
      : base(parent)
    {
      this.DesignTimeItems = new StringCollection();
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new DynamicCommandWidgetDefinition((ConfigElement) this);

    /// <summary>
    /// Clears all items from the widget which have not been configured design time in defintion file
    /// </summary>
    public void ClearDynamicItems()
    {
      foreach (DynamicItemElement dynamicItemElement in this.Items)
      {
        if (!this.DesignTimeItems.Contains(dynamicItemElement.GetKey()))
          this.Items.Remove(dynamicItemElement);
      }
    }

    /// <summary>
    /// Gets or sets the default size of one page of items in the list
    /// </summary>
    /// <value>The default size of one page of items in the list.</value>
    [ConfigurationProperty("pageSize", DefaultValue = 10)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandDefaultPageSizeDescription", Title = "DynamicCommandDefaultPageSizeCaption")]
    public int PageSize
    {
      get => (int) this["pageSize"];
      set => this["pageSize"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the text for the header that appears before the combo/list of commands
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("headerText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandHeaderTextDescription", Title = "DynamicCommandHeaderTextCaption")]
    public string HeaderText
    {
      get => (string) this["headerText"];
      set => this["headerText"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the css class for the control that displays the header label
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("headerTextCssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandHeaderTextCssClassDescription", Title = "DynamicCommandHeaderTextCssClassCaption")]
    public string HeaderTextCssClass
    {
      get => (string) this["headerTextCssClass"];
      set => this["headerTextCssClass"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the text for the link which needs to be clicked to show more items
    /// </summary>
    /// <value>
    /// The text for the link which needs to be clicked to show more items.
    /// </value>
    [ConfigurationProperty("moreLinkText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandMoreLinkTextDescription", Title = "DynamicCommandMoreLinkTextCaption")]
    public string MoreLinkText
    {
      get => (string) this["moreLinkText"];
      set => this["moreLinkText"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the css class of the link used to display more items when bound on the client
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("moreLinkCssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandMoreLinkCssClassDescription", Title = "DynamicCommandMoreLinkCssClassCaption")]
    public string MoreLinkCssClass
    {
      get => (string) this["moreLinkCssClass"];
      set => this["moreLinkCssClass"] = (object) value;
    }

    /// <summary>Gets or sets the text for the link showing less items</summary>
    /// <value></value>
    [ConfigurationProperty("lessLinkText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandLessLinkTextDescription", Title = "DynamicCommandLessLinkTextCaption")]
    public string LessLinkText
    {
      get => (string) this["lessLinkText"];
      set => this["lessLinkText"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the css class for the link showing less items.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("lessLinkCssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandLessLinkCssClassDescription", Title = "DynamicCommandLessLinkCssClassCaption")]
    public string LessLinkCssClass
    {
      get => (string) this["lessLinkCssClass"];
      set => this["lessLinkCssClass"] = (object) value;
    }

    /// <summary>Gets or sets the selected item pageId.</summary>
    /// <value>The selected item pageId.</value>
    [ConfigurationProperty("selectedItemCssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandSelectedItemCssClassDescription", Title = "DynamicCommandSelectedItemCssClassCaption")]
    public string SelectedItemCssClass
    {
      get => (string) this["selectedItemCssClass"];
      set => this["selectedItemCssClass"] = (object) value;
    }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    [ConfigurationProperty("baseServiceUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandWebServiceUrlDescription", Title = "DynamicCommandWebServiceUrlCaption")]
    public string BaseServiceUrl
    {
      get => (string) this["baseServiceUrl"];
      set => this["baseServiceUrl"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the URL for the service used to get child taxa
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("childItemsServiceUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandChildItemsServiceUrlDescription", Title = "DynamicCommandChildItemsServiceUrlCaption")]
    public string ChildItemsServiceUrl
    {
      get => (string) this["childItemsServiceUrl"];
      set => this["childItemsServiceUrl"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the URL for the service used to get predecessor taxa
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("predecessorServiceUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandPredecessorServiceUrlDescription", Title = "DynamicCommandPredecessorServiceUrlCaption")]
    public string PredecessorServiceUrl
    {
      get => (string) this["predecessorServiceUrl"];
      set => this["predecessorServiceUrl"] = (object) value;
    }

    /// <summary>Gets or sets the URL parameters.</summary>
    /// <value>The URL parameters.</value>
    [ConfigurationProperty("urlParameters")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandUrlParametersDescription", Title = "DynamicCommandUrlParametersCaption")]
    public ConfigValueDictionary UrlParameters => (ConfigValueDictionary) this["urlParameters"];

    /// <summary>Gets the URL parameters.</summary>
    IDictionary<string, string> IDynamicCommandWidgetDefinition.UrlParameters => (IDictionary<string, string>) this.UrlParameters;

    /// <summary>Gets or sets the data source.</summary>
    /// <value></value>
    [ConfigurationProperty("dataSource")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandDataSourceDescription", Title = "DynamicCommandDataSourceCaption")]
    [ConfigurationCollection(typeof (DynamicItemElement), AddItemName = "item")]
    public ConfigElementList<DynamicItemElement> Items => (ConfigElementList<DynamicItemElement>) this["dataSource"];

    /// <summary>
    /// Gets or sets the custom commands list. This is an additional data source that enables the DynamicCommandWidget
    /// to gave some of the items in the list fire custom commands that need special handling
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("customCommands")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandCustomCommandsDescription", Title = "DynamicCommandCustomCommandsCaption")]
    [ConfigurationCollection(typeof (DynamicItemElement), AddItemName = "item")]
    public ConfigElementList<DynamicItemElement> CustomItems => (ConfigElementList<DynamicItemElement>) this["customCommands"];

    /// <summary>
    /// Specifies how the list of commands in the dynamic command widget is rendered
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("bindTo")]
    [DefaultValue(BindCommandListTo.Client)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandBindToDescription", Title = "DynamicCommandBindToCaption")]
    public BindCommandListTo BindTo
    {
      get => (BindCommandListTo) this["bindTo"];
      set => this["bindTo"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the sort expression for the list of items
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("sortExpression")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SortExpressionDescription", Title = "SortExpressionCaption")]
    public string SortExpression
    {
      get => (string) this["sortExpression"];
      set => this["sortExpression"] = (object) value;
    }

    /// <summary>
    /// The item template used when binding on the client (in client and hierarchical mode)
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("clientItemTemplate")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandClientItemTemplateDescription", Title = "DynamicCommandClientItemTemplateCaption")]
    public string ClientItemTemplate
    {
      get => (string) this["clientItemTemplate"];
      set => this["clientItemTemplate"] = (object) value;
    }

    /// <summary>Gets or sets the type of the dynamic command widger.</summary>
    /// <value>The type of the content.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandContentTypeDescription", Title = "DynamicCommandContentTypeCaption")]
    [ConfigurationProperty("contentType")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type ContentType
    {
      get => (Type) this["contentType"];
      set => this["contentType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the dynamic type of the dynamic command widger.
    /// </summary>
    /// <value>The type of the content.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandDynamicModuleTypeIdDescription", Title = "DynamicCommandDynamicModuleTypeIdCaption")]
    [ConfigurationProperty("dynamicModuleTypeId")]
    [TypeConverter(typeof (StringGuidConverter))]
    public Guid DynamicModuleTypeId
    {
      get => (Guid) this["dynamicModuleTypeId"];
      set => this["dynamicModuleTypeId"] = (object) value;
    }

    /// <summary>The name of the command that is fired by the widget</summary>
    /// <value></value>
    [ConfigurationProperty("commandName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandCommandNameDescription", Title = "DynamicCommandCommandNameCaption")]
    public string CommandName
    {
      get => (string) this["commandName"];
      set => this["commandName"] = (object) value;
    }

    /// <summary>Gets or sets the parent key id</summary>
    /// <value></value>
    [ConfigurationProperty("parentDataKeyName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DynamicCommandParentDataKeyNameDescription", Title = "DynamicCommandParentDataKeyNameCaption")]
    public string ParentDataKeyName
    {
      get => (string) this["parentDataKeyName"];
      set => this["parentDataKeyName"] = (object) value;
    }

    IEnumerable<IDynamicItemDefinition> IDynamicCommandWidgetDefinition.Items => this.Items.Cast<IDynamicItemDefinition>();

    IEnumerable<IDynamicItemDefinition> IDynamicCommandWidgetDefinition.CustomItems => this.CustomItems.Cast<IDynamicItemDefinition>();

    /// <summary>
    /// Gets or sets the items defined at design time in a definition file
    /// </summary>
    /// <value></value>
    public StringCollection DesignTimeItems { get; set; }

    /// <summary>Gets or sets the selected item in the widget</summary>
    public string SelectedValue { get; set; }

    /// <summary>
    /// Determines this command is a filter command (e.g. a filter on the sidebar)
    /// </summary>
    [ConfigurationProperty("isFilter", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandIsFilterCommandDescription", Title = "CommandIsFilterCommandCaption")]
    public bool IsFilterCommand
    {
      get => (bool) this["isFilter"];
      set => this["isFilter"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct DynamicCommandWidgetProps
    {
      public const string pageSize = "pageSize";
      public const string headerText = "headerText";
      public const string headerTextCssClass = "headerTextCssClass";
      public const string moreLinkText = "moreLinkText";
      public const string lessLinkText = "lessLinkText";
      public const string moreLinkCssClass = "moreLinkCssClass";
      public const string lessLinkCssClass = "lessLinkCssClass";
      public const string selectedItemCssClass = "selectedItemCssClass";
      public const string title = "title";
      public const string baseServiceUrl = "baseServiceUrl";
      public const string childItemsServiceUrl = "childItemsServiceUrl";
      public const string predecessorServiceUrl = "predecessorServiceUrl";
      public const string urlParameters = "urlParameters";
      public const string dataSource = "dataSource";
      public const string customCommands = "customCommands";
      public const string dataTextField = "dataTextField";
      public const string dataValueField = "dataValueField";
      public const string bindTo = "bindTo";
      public const string clientItemTemplate = "clientItemTemplate";
      public const string commandName = "commandName";
      public const string sortExpression = "sortExpression";
      public const string parentDataKeyName = "parentDataKeyName";
      public const string isFilterCommand = "isFilter";
    }
  }
}
