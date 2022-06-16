// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DynamicCommandWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// A widget that displays a list of items and fires a command whenever one of them is selected
  /// </summary>
  public class DynamicCommandWidget : SimpleScriptView, IWidget
  {
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.DynamicCommandWidget.ascx");

    /// <summary>Configures the specified definition.</summary>
    /// <param name="definition">The definition.</param>
    public void Configure(IWidgetDefinition definition)
    {
      IDynamicCommandWidgetDefinition widgetDefinition = (IDynamicCommandWidgetDefinition) definition;
      this.Definition = definition;
      this.BindTo = widgetDefinition.BindTo;
      this.Items = widgetDefinition.Items;
      this.CustomItems = widgetDefinition.CustomItems;
      this.ContentType = widgetDefinition.ContentType;
      this.DynamicModuleTypeId = widgetDefinition.DynamicModuleTypeId;
      this.ResourceClassId = widgetDefinition.ResourceClassId;
      this.CommandName = widgetDefinition.CommandName;
      this.SortExpression = widgetDefinition.SortExpression;
      this.ParentDataKeyName = widgetDefinition.ParentDataKeyName;
      this.IsFilterCommand = widgetDefinition.IsFilterCommand;
    }

    /// <summary>Gets or sets the definition.</summary>
    /// <value>The definition.</value>
    public IWidgetDefinition Definition { get; set; }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
    {
      this.GetDynamicWidgetScriptDescriptor()
    };

    /// <summary>Gets the dynamic widget script descriptor.</summary>
    /// <returns></returns>
    protected ScriptBehaviorDescriptor GetDynamicWidgetScriptDescriptor()
    {
      ScriptBehaviorDescriptor scriptDescriptor = new ScriptBehaviorDescriptor(this.GetType().FullName, this.ClientID);
      IDynamicCommandWidgetDefinition definition = this.Definition as IDynamicCommandWidgetDefinition;
      Dictionary<string, string> dictionary = new Dictionary<string, string>(definition.UrlParameters);
      if (dictionary.ContainsKey("provider") && !string.IsNullOrEmpty(this.Page.Request.QueryString["provider"]))
        dictionary["provider"] = this.Page.Request.QueryString["provider"];
      switch (this.BindTo)
      {
        case BindCommandListTo.ComboBox:
          List<string> stringList1 = new List<string>();
          List<string> stringList2 = new List<string>();
          foreach (IDynamicItemDefinition customItem in this.CustomItems)
          {
            string itemTitle = this.GetItemTitle(customItem);
            stringList1.Add(customItem.Value);
            stringList2.Add(itemTitle);
          }
          scriptDescriptor.AddProperty("customCommandItemValues", (object) stringList1.ToArray());
          scriptDescriptor.AddProperty("customCommandItemTexts", (object) stringList2.ToArray());
          scriptDescriptor.AddProperty("_dropDownId", (object) this.DropDown.ClientID);
          break;
        case BindCommandListTo.Client:
          scriptDescriptor.AddProperty("_binderId", (object) this.Binder.ClientID);
          scriptDescriptor.AddProperty("urlParameters", (object) dictionary);
          scriptDescriptor.AddProperty("_moreLinkId", (object) this.MoreLink.ClientID);
          scriptDescriptor.AddProperty("_lessLinkId", (object) this.LessLink.ClientID);
          scriptDescriptor.AddProperty("_commandName", (object) this.CommandName);
          break;
        case BindCommandListTo.HierarchicalData:
          scriptDescriptor.AddProperty("_binderId", (object) this.HierarchicalBinder.ClientID);
          scriptDescriptor.AddProperty("urlParameters", (object) dictionary);
          scriptDescriptor.AddProperty("_commandName", (object) this.CommandName);
          scriptDescriptor.AddProperty("_moreLinkId", (object) this.MoreLink.ClientID);
          scriptDescriptor.AddProperty("_lessLinkId", (object) this.LessLink.ClientID);
          break;
      }
      scriptDescriptor.AddElementProperty("headerTextControl", ((Control) this.HeaderTextControl).ClientID);
      scriptDescriptor.AddProperty("_bindTo", (object) this.BindTo);
      scriptDescriptor.AddProperty("selectedValue", (object) this.SelectedValue);
      scriptDescriptor.AddProperty("_selectedItemCssClass", (object) definition.SelectedItemCssClass);
      scriptDescriptor.AddProperty("pageSize", (object) definition.PageSize);
      scriptDescriptor.AddProperty("name", (object) definition.Name);
      scriptDescriptor.AddProperty("cssClass", (object) definition.CssClass);
      scriptDescriptor.AddProperty("isSeparator", (object) definition.IsSeparator);
      scriptDescriptor.AddProperty("wrapperTagId", (object) definition.WrapperTagId);
      scriptDescriptor.AddProperty("wrapperTagName", (object) definition.WrapperTagKey);
      scriptDescriptor.AddProperty("hidden", (object) this.Hidden);
      scriptDescriptor.AddProperty("_isFilterCommand", (object) this.IsFilterCommand);
      return scriptDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (DynamicCommandWidget).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new ScriptReference[3]
      {
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.SitefinityJS.UI.IWidget.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.SitefinityJS.UI.ICommandWidget.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.DynamicCommandWidget.js"
        }
      };
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DynamicCommandWidget.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the selected item pageId.</summary>
    /// <value>The selected item pageId.</value>
    public string SelectedValue { get; set; }

    /// <summary>
    /// Specifies how the list of commands in the dynamic command widget is databound
    /// </summary>
    public BindCommandListTo BindTo { get; set; }

    /// <summary>Gets or sets the data source.</summary>
    /// <value>The data source.</value>
    public IEnumerable<IDynamicItemDefinition> Items { get; set; }

    /// <summary>
    /// Gets or sets the custom commands list. This is an additional data source that enables the DynamicCommandWidget
    /// to gave some of the items in the list fire custom commands that need special handling
    /// </summary>
    public IEnumerable<IDynamicItemDefinition> CustomItems { get; set; }

    /// <summary>Gets or sets the content type</summary>
    /// <value>The content type.</value>
    public Type ContentType { get; set; }

    /// <summary>Gets or sets the type of the dynamic module.</summary>
    /// <value>The type of the dynamic module.</value>
    public Guid DynamicModuleTypeId { get; set; }

    /// <summary>Gets or sets the resource class id.</summary>
    /// <value>The resource class id.</value>
    public string ResourceClassId { get; set; }

    /// <summary>The name of the command fired by the widget</summary>
    public string CommandName { get; set; }

    /// <summary>Gets or sets the parent key id</summary>
    public string ParentDataKeyName { get; set; }

    internal bool Hidden { get; set; }

    /// <summary>Specifies the sort expression for the image albums.</summary>
    public string SortExpression { get; set; }

    /// <summary>
    /// Determines this command is a filter command (e.g. a filter on the sidebar)
    /// </summary>
    protected virtual bool IsFilterCommand { get; set; }

    /// <summary>Gets the binder used to bind the list of items</summary>
    /// <value>The binder.</value>
    protected virtual ClientBinder Binder => this.Container.GetControl<ClientBinder>("itemsBinder", true);

    /// <summary>
    /// Gets the binder used to bind the hierarchical list of items in Hierarchical mode
    /// </summary>
    protected virtual ClientBinder HierarchicalBinder => this.Container.GetControl<ClientBinder>("itemsTreeBinder", true);

    /// <summary>Gets the link showing next page of items</summary>
    /// <value>The link showing next page of items.</value>
    protected virtual LinkButton MoreLink
    {
      get
      {
        LinkButton moreLink;
        switch (this.BindTo)
        {
          case BindCommandListTo.Client:
            moreLink = this.Container.GetControl<LinkButton>("moreLink", true);
            break;
          case BindCommandListTo.HierarchicalData:
            moreLink = this.Container.GetControl<LinkButton>("moreLink_hierarchicalData", true);
            break;
          default:
            moreLink = (LinkButton) null;
            break;
        }
        return moreLink;
      }
    }

    /// <summary>Gets the link showing one page less items.</summary>
    /// <value>The link showing one page less items.</value>
    protected virtual LinkButton LessLink
    {
      get
      {
        LinkButton lessLink;
        switch (this.BindTo)
        {
          case BindCommandListTo.Client:
            lessLink = this.Container.GetControl<LinkButton>("lessLink", true);
            break;
          case BindCommandListTo.HierarchicalData:
            lessLink = this.Container.GetControl<LinkButton>("lessLink_hierarchicalData", true);
            break;
          default:
            lessLink = (LinkButton) null;
            break;
        }
        return lessLink;
      }
    }

    /// <summary>Gets the drop down.</summary>
    /// <value>The drop down.</value>
    protected virtual DropDownList DropDown => this.Container.GetControl<DropDownList>("ddlDynamicCommands", this.BindTo == BindCommandListTo.ComboBox);

    /// <summary>
    /// Gets the control used to display the header text for the widget
    /// </summary>
    protected virtual ITextControl HeaderTextControl
    {
      get
      {
        ITextControl headerTextControl;
        switch (this.BindTo)
        {
          case BindCommandListTo.ComboBox:
            headerTextControl = this.Container.GetControl<ITextControl>("headerTextCombo", true);
            break;
          case BindCommandListTo.Client:
            headerTextControl = this.Container.GetControl<ITextControl>("headerTextClient", true);
            break;
          case BindCommandListTo.HierarchicalData:
            headerTextControl = this.Container.GetControl<ITextControl>("headerTextHierarchical", true);
            break;
          default:
            headerTextControl = (ITextControl) null;
            break;
        }
        return headerTextControl;
      }
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      IDynamicCommandWidgetDefinition definition = (IDynamicCommandWidgetDefinition) this.Definition;
      switch (definition.BindTo)
      {
        case BindCommandListTo.ComboBox:
          if (this.Items == null)
            throw new ArgumentNullException("When in ComboBox mode, the DataSource should be set");
          if (!string.IsNullOrEmpty(definition.HeaderText))
            this.HeaderTextControl.Text = this.GetLabel(definition.ResourceClassId, definition.HeaderText);
          if (!string.IsNullOrEmpty(definition.HeaderTextCssClass))
            ((WebControl) this.HeaderTextControl).CssClass = definition.HeaderTextCssClass;
          this.DropDown.DataSource = (object) this.GetItems(this.Items);
          this.DropDown.AutoPostBack = false;
          this.DropDown.DataTextField = "Title";
          this.DropDown.DataValueField = "Value";
          if (!this.Page.IsPostBack)
            this.DropDown.DataBind();
          if (this.CustomItems != null)
          {
            using (IEnumerator<IDynamicItemDefinition> enumerator = this.CustomItems.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                IDynamicItemDefinition current = enumerator.Current;
                this.DropDown.Items.Add(new ListItem(this.GetItemTitle(current), current.Value));
              }
              break;
            }
          }
          else
            break;
        case BindCommandListTo.Client:
          if (string.IsNullOrEmpty(this.CommandName))
            throw new ArgumentNullException("The CommandName property is not set");
          this.InitializeMoreLessLinks(definition);
          if (!string.IsNullOrEmpty(definition.HeaderText))
            this.HeaderTextControl.Text = this.GetLabel(definition.ResourceClassId, definition.HeaderText);
          if (!string.IsNullOrEmpty(definition.HeaderTextCssClass))
            ((WebControl) this.HeaderTextControl).CssClass = definition.HeaderTextCssClass;
          this.Binder.ServiceUrl = definition.BaseServiceUrl;
          if (!string.IsNullOrEmpty(definition.SortExpression))
            this.Binder.DefaultSortExpression = this.SortExpression;
          this.Binder.Containers[0].Markup = definition.ClientItemTemplate;
          break;
        case BindCommandListTo.HierarchicalData:
          this.InitializeMoreLessLinks(definition);
          if (!string.IsNullOrEmpty(definition.HeaderText))
            this.HeaderTextControl.Text = this.GetLabel(definition.ResourceClassId, definition.HeaderText);
          this.HierarchicalBinder.ServiceUrl = definition.BaseServiceUrl;
          if (!string.IsNullOrEmpty(definition.SortExpression))
            this.HierarchicalBinder.DefaultSortExpression = this.SortExpression;
          if (!string.IsNullOrEmpty(definition.ParentDataKeyName))
            ((RadTreeBinder) this.HierarchicalBinder).ParentDataKeyName = this.ParentDataKeyName;
          ((RadTreeBinder) this.HierarchicalBinder).ServiceChildItemsBaseUrl = definition.ChildItemsServiceUrl;
          ((RadTreeBinder) this.HierarchicalBinder).ServicePredecessorBaseUrl = definition.PredecessorServiceUrl;
          this.HierarchicalBinder.Containers[0].Markup = definition.ClientItemTemplate;
          break;
      }
      if (definition.SelectedValue == null)
        return;
      this.SelectedValue = definition.SelectedValue;
    }

    /// <summary>
    /// Overridden. Calls Evaluate on the conditional template container to correctly use the controls inside of the templates
    /// </summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    protected internal override GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = base.CreateContainer(template);
      ConditionalTemplateContainer control = container.GetControl<ConditionalTemplateContainer>();
      if (control == null)
        return container;
      control.Evaluate((object) this);
      return container;
    }

    private IEnumerable<DynamicCommandWidget.DynamicItem> GetItems(
      IEnumerable<IDynamicItemDefinition> items)
    {
      IList<DynamicCommandWidget.DynamicItem> items1 = (IList<DynamicCommandWidget.DynamicItem>) new List<DynamicCommandWidget.DynamicItem>();
      foreach (IDynamicItemDefinition dynamicItemDefinition in items)
      {
        string itemTitle = this.GetItemTitle(dynamicItemDefinition);
        items1.Add(new DynamicCommandWidget.DynamicItem()
        {
          Title = itemTitle,
          Value = dynamicItemDefinition.Value
        });
      }
      return (IEnumerable<DynamicCommandWidget.DynamicItem>) items1;
    }

    /// <summary>Gets the item title.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    private string GetItemTitle(IDynamicItemDefinition item)
    {
      string resourceValue;
      return !string.IsNullOrEmpty(item.ResourceClassId) && Res.TryGet(item.ResourceClassId, item.Title, out resourceValue) || !string.IsNullOrEmpty(this.ResourceClassId) && Res.TryGet(this.ResourceClassId, item.Title, out resourceValue) ? resourceValue : item.Title;
    }

    private IDictionary<string, string> GetDictionaryFromCollection(NameValueCollection c)
    {
      Dictionary<string, string> dictionaryFromCollection = new Dictionary<string, string>();
      foreach (string key in c.Keys)
        dictionaryFromCollection.Add(key, c[key]);
      return (IDictionary<string, string>) dictionaryFromCollection;
    }

    private void InitializeMoreLessLinks(IDynamicCommandWidgetDefinition definition)
    {
      string label1 = this.GetLabel(definition.ResourceClassId, definition.MoreLinkText);
      if (!string.IsNullOrEmpty(label1))
      {
        this.MoreLink.Text = string.Format(label1, (object) definition.PageSize);
        this.MoreLink.CssClass = definition.MoreLinkCssClass;
      }
      else
        this.MoreLink.Visible = false;
      string label2 = this.GetLabel(definition.ResourceClassId, definition.LessLinkText);
      if (!string.IsNullOrEmpty(label2))
      {
        this.LessLink.Text = string.Format(label2, (object) definition.PageSize);
        this.LessLink.CssClass = definition.LessLinkCssClass;
      }
      else
        this.LessLink.Visible = false;
    }

    /// <summary>
    /// Sortable item used when databinding to sortable collection.
    /// </summary>
    public class DynamicItem
    {
      public string Title { get; set; }

      public string Value { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Scripts
    {
      public const string DynamicCommandWidget = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.DynamicCommandWidget.js";
    }
  }
}
