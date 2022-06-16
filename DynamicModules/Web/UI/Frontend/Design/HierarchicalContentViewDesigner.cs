// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design.HierarchicalContentViewDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Enums;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design
{
  /// <summary>
  /// Control designer for the <see cref="T:Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.HierarchicalContentView" /> widget.
  /// </summary>
  public class HierarchicalContentViewDesigner : ControlDesignerBase
  {
    private ModuleBuilderManager moduleBuilderMngr;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.HierarchicalContentViewDesigner.ascx");
    internal const string contentViewDesignerScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.HierarchicalContentViewDesigner.js";
    private Dictionary<string, ControlDesignerView> dynamicContentSelectorsViews;
    private Dictionary<string, ControlDesignerView> listSettingsViews;
    private Dictionary<string, ControlDesignerView> templateSelectorsViews;

    /// <summary>
    /// Gets or sets the message text - used to show any warnings and errors on the designers
    /// </summary>
    /// <value>The message.</value>
    public string TopMessageText { get; set; }

    /// <summary>Gets or sets the type of the top message.</summary>
    /// <value>The type of the top message.</value>
    public MessageType TopMessageType { get; set; }

    /// <summary>Gets the dynamic content selectors views.</summary>
    /// <value>The dynamic content selectors views.</value>
    public Dictionary<string, ControlDesignerView> DynamicContentSelectorsViews
    {
      get
      {
        if (this.dynamicContentSelectorsViews == null)
          this.dynamicContentSelectorsViews = new Dictionary<string, ControlDesignerView>();
        return this.dynamicContentSelectorsViews;
      }
    }

    /// <summary>Gets the list settings views.</summary>
    /// <value>The list settings views.</value>
    public Dictionary<string, ControlDesignerView> ListSettingsViews
    {
      get
      {
        if (this.listSettingsViews == null)
          this.listSettingsViews = new Dictionary<string, ControlDesignerView>();
        return this.listSettingsViews;
      }
    }

    /// <summary>Gets the template selectors view.</summary>
    /// <value>The template selectors views.</value>
    public Dictionary<string, ControlDesignerView> TemplatesSelectorsViews
    {
      get
      {
        if (this.templateSelectorsViews == null)
          this.templateSelectorsViews = new Dictionary<string, ControlDesignerView>();
        return this.templateSelectorsViews;
      }
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
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? HierarchicalContentViewDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overridden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    protected override string ScriptDescriptorTypeName => typeof (HierarchicalContentViewDesigner).FullName;

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" />.
    /// </summary>
    protected virtual ModuleBuilderManager ModuleBuilderMngr
    {
      get
      {
        if (this.moduleBuilderMngr == null)
          this.moduleBuilderMngr = ModuleBuilderManager.GetManager();
        return this.moduleBuilderMngr;
      }
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the root type of the hierarchy.</summary>
    public DynamicModuleType RootType { get; set; }

    /// <summary>
    /// Gets the repeater control that displays list settings for every type.
    /// </summary>
    protected Repeater ListSettingsRepeater => this.Container.GetControl<Repeater>("listSettingsRepeater", true);

    /// <summary>
    /// Gets the repeater control that lists dynamic content selectors for every type.
    /// </summary>
    protected Repeater DynamicContentSelectorsRepeater => this.Container.GetControl<Repeater>("dynamicContentSelectorsRepeater", true);

    /// <summary>
    /// Gets the repeater control that lists all content types.
    /// </summary>
    protected Repeater ContentTypesRepeater => this.Container.GetControl<Repeater>("contentTypesRepeater", true);

    /// <summary>
    /// Gets the repeater control that builds the full functionality option.
    /// </summary>
    protected Repeater FullFunctionalityTypesRepeater => this.Container.GetControl<Repeater>("fullFunctionalityTypesRepeater", true);

    /// <summary>
    /// Gets the repeater control that lists template selectors for every type.
    /// </summary>
    protected Repeater TemplatesRepeater => this.Container.GetControl<Repeater>("templatesRepeater", true);

    /// <summary>
    /// Gets the repeater control that lists the titles of the pages selector.
    /// </summary>
    protected Repeater PagesSelectorTitlesRepeater => this.Container.GetControl<Repeater>("pagesSelectorTitlesRepeater", true);

    /// <summary>Gets the message control.</summary>
    /// <value>The message control.</value>
    protected Message Message => this.Container.GetControl<Message>();

    /// <summary>Gets a reference to the select page button.</summary>
    protected LinkButton PageSelectButton => this.Container.GetControl<LinkButton>("pageSelectButton", true);

    /// <summary>Gets a reference to the remove page button.</summary>
    protected LinkButton RemovePageButton => this.Container.GetControl<LinkButton>("removePageButton", true);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Select page" dialog
    /// </summary>
    public HtmlGenericControl SelectorTag => this.Container.GetControl<HtmlGenericControl>("selectorTag", true);

    /// <summary>Gets a reference to the pages selector.</summary>
    protected PagesSelector PagesSelector => this.Container.GetControl<PagesSelector>("pagesSelector", true);

    /// <summary>Gets a reference to the selected page label.</summary>
    protected Label SelectedPageLabel => this.Container.GetControl<Label>("selectedPageLabel", true);

    /// <summary>Gets a reference to the selected page container.</summary>
    public HtmlGenericControl SelectedPageContainer => this.Container.GetControl<HtmlGenericControl>("selectedPageContainer", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      HierarchicalContentView control = (HierarchicalContentView) this.PropertyEditor.Control;
      if (control.CanResolveDynamicContentType())
      {
        DynamicModuleType dynamicModuleType = this.ModuleBuilderMngr.GetDynamicModuleType(control.DynamicContentTypeName);
        List<DynamicModuleType> contentTypesHierarchy = this.ModuleBuilderMngr.GetContentTypesHierarchy(dynamicModuleType);
        HierarchicalContentViewDesigner.EnsureTypesSettings(control, (ICollection<DynamicModuleType>) contentTypesHierarchy);
        this.RootType = dynamicModuleType;
        IList repeatersDataSource = HierarchicalContentViewDesigner.GetRepeatersDataSource((ICollection<DynamicModuleType>) contentTypesHierarchy);
        this.FullFunctionalityTypesRepeater.DataSource = (object) HierarchicalContentViewDesigner.GetFullFunctionalityDataSource((ICollection<DynamicModuleType>) contentTypesHierarchy);
        this.ContentTypesRepeater.DataSource = (object) repeatersDataSource;
        this.DynamicContentSelectorsRepeater.DataSource = (object) contentTypesHierarchy;
        this.DynamicContentSelectorsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.DynamicContentSelectorsRepeater_ItemDataBound);
        this.ListSettingsRepeater.DataSource = (object) repeatersDataSource;
        this.ListSettingsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ListSettingsRepeater_ItemDataBound);
        this.TemplatesRepeater.DataSource = (object) repeatersDataSource;
        this.TemplatesRepeater.ItemDataBound += new RepeaterItemEventHandler(this.TemplatesRepeater_ItemDataBound);
        this.PagesSelectorTitlesRepeater.DataSource = (object) repeatersDataSource;
        this.PagesSelectorTitlesRepeater.ItemDataBound += new RepeaterItemEventHandler(this.PagesSelectorTitlesRepeater_ItemDataBound);
        this.InitializePagesSelector(control);
      }
      else
        this.HandleInvalidContentType();
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      foreach (string key in this.ListSettingsViews.Keys)
        dictionary1.Add(key, this.ListSettingsViews[key].ClientID);
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      foreach (string key in this.DynamicContentSelectorsViews.Keys)
        dictionary2.Add(key, this.DynamicContentSelectorsViews[key].ClientID);
      Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
      foreach (string key in this.TemplatesSelectorsViews.Keys)
        dictionary3.Add(key, this.TemplatesSelectorsViews[key].ClientID);
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("rootContentType", (object) this.RootType.TypeName);
      controlDescriptor.AddProperty("dynamicContentSelectorsViewsMap", (object) dictionary2);
      controlDescriptor.AddProperty("listSettingViewsMap", (object) dictionary1);
      controlDescriptor.AddProperty("templatesSelectorsViewsMap", (object) dictionary3);
      controlDescriptor.AddComponentProperty("pagesSelector", this.PagesSelector.ClientID);
      controlDescriptor.AddElementProperty("pageSelectButton", this.PageSelectButton.ClientID);
      controlDescriptor.AddElementProperty("removePageButton", this.RemovePageButton.ClientID);
      controlDescriptor.AddElementProperty("selectorTag", this.SelectorTag.ClientID);
      controlDescriptor.AddElementProperty("selectedPageLabel", this.SelectedPageLabel.ClientID);
      controlDescriptor.AddElementProperty("selectedPageContainer", this.SelectedPageContainer.ClientID);
      Labels labels = Res.Get<Labels>();
      controlDescriptor.AddProperty("selectPageText", (object) labels.SelectPageButton);
      controlDescriptor.AddProperty("changePageText", (object) labels.ChangePageButton);
      HierarchicalContentView control = (HierarchicalContentView) this.PropertyEditor.Control;
      string str;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(control.TypesSettings.GetType()).WriteObject((Stream) memoryStream, (object) control.TypesSettings);
        str = Encoding.Default.GetString(memoryStream.ToArray());
      }
      controlDescriptor.AddProperty("typesSettings", (object) str);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = typeof (ContentViewDesignerBase).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.HierarchicalContentViewDesigner.js", assembly)
      };
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.FullFunctionalityTypesRepeater.DataBind();
      this.ContentTypesRepeater.DataBind();
      this.DynamicContentSelectorsRepeater.DataBind();
      this.ListSettingsRepeater.DataBind();
      this.TemplatesRepeater.DataBind();
      this.PagesSelectorTitlesRepeater.DataBind();
      if (string.IsNullOrEmpty(this.TopMessageText))
        return;
      this.Message.Visible = true;
      this.Message.MessageText = this.TopMessageText;
      this.Message.Status = this.TopMessageType;
    }

    private static void EnsureTypesSettings(
      HierarchicalContentView hierarchicalContentView,
      ICollection<DynamicModuleType> dynamicTypes)
    {
      if (hierarchicalContentView.ViewMode.IsNullOrEmpty())
        hierarchicalContentView.ViewMode = "Full";
      if (hierarchicalContentView.TypesSettings == null)
        hierarchicalContentView.TypesSettings = new Dictionary<string, DynamicTypeBasicSettings>();
      for (int index = 0; index < hierarchicalContentView.TypesSettings.Keys.Count; ++index)
      {
        string key = hierarchicalContentView.TypesSettings.Keys.ElementAt<string>(index);
        if (!dynamicTypes.Any<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.TypeName.Equals(key, StringComparison.InvariantCultureIgnoreCase))))
        {
          hierarchicalContentView.TypesSettings.Remove(key);
          --index;
        }
      }
      foreach (DynamicModuleType dynamicType in (IEnumerable<DynamicModuleType>) dynamicTypes)
      {
        if (!hierarchicalContentView.TypesSettings.ContainsKey(dynamicType.TypeName))
        {
          DynamicTypeBasicSettings defaultTypeSettings = hierarchicalContentView.GetDefaultTypeSettings(dynamicType.GetFullTypeName());
          hierarchicalContentView.TypesSettings[dynamicType.TypeName] = defaultTypeSettings;
        }
      }
    }

    private void DynamicContentSelectorsRepeater_ItemDataBound(
      object sender,
      RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      DynamicModuleType dataItem = e.Item.DataItem as DynamicModuleType;
      this.ModuleBuilderMngr.LoadDynamicModuleTypeGraph(dataItem, false);
      PlaceHolder control1 = e.Item.FindControl("dynamicContentSelectorsPlaceHolder") as PlaceHolder;
      string plural = PluralsResolver.Instance.ToPlural(dataItem.DisplayName.ToLower());
      string lower = dataItem.DisplayName.ToLower();
      Labels labels = Res.Get<Labels>();
      ModuleBuilderResources builderResources = Res.Get<ModuleBuilderResources>();
      DynamicContentSelectorsDesignerView child = new DynamicContentSelectorsDesignerView();
      child.ContentTitleText = string.Empty;
      child.ChooseAllText = string.Format(builderResources.AllPublishedItems, (object) plural);
      child.ChooseSingleText = string.Format(builderResources.OneParticularItemOnly, (object) lower);
      child.ChooseSimpleFilterText = string.Format(builderResources.SelectionOfItems, (object) plural);
      child.ChooseAdvancedFilterText = labels.AdvancedSelection;
      child.NoContentToSelectText = string.Format(builderResources.NoItemsHaveBeenCreatedYet, (object) plural);
      child.SingleSelectorButtonText = string.Format(builderResources.SelectItems, (object) plural);
      child.SelectedContentTitleText = string.Format(builderResources.NoItemsHaveBeenSelectedYet, (object) plural);
      child.ContentSelectorTitle = string.Format(builderResources.SelectItems, (object) plural);
      child.ContentSelectorWebServiceUrl = "~/Sitefinity/Services/DynamicModules/Data.svc/";
      child.ContentSelectorItemTypeName = dataItem.GetFullTypeName();
      child.ContentSelectorFilter = " ";
      child.ModuleType = dataItem;
      control1.Controls.Add((Control) child);
      HierarchicalContentView control2 = (HierarchicalContentView) this.PropertyEditor.Control;
      if (dataItem.TypeName.Equals(control2.ViewMode, StringComparison.InvariantCultureIgnoreCase) || dataItem.Id == this.RootType.Id && "Full".Equals(control2.ViewMode, StringComparison.InvariantCultureIgnoreCase))
      {
        child.InitView((ControlDesignerBase) this);
      }
      else
      {
        ContentViewDisplayMode contentViewDisplayMode = control2.ContentViewDisplayMode;
        control2.ContentViewDisplayMode = ContentViewDisplayMode.Master;
        child.InitView((ControlDesignerBase) this);
        control2.ContentViewDisplayMode = contentViewDisplayMode;
      }
      this.DynamicContentSelectorsViews.Add(dataItem.TypeName, (ControlDesignerView) child);
    }

    private void ListSettingsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      string key1 = DataBinder.Eval(e.Item.DataItem, "TypeName") as string;
      string str = DataBinder.Eval(e.Item.DataItem, "PluralDisplayName") as string;
      HierarchicalContentView control1 = (HierarchicalContentView) this.PropertyEditor.Control;
      string key2 = key1 + "DynamicContentMasterView";
      ModuleBuilderResources builderResources = Res.Get<ModuleBuilderResources>();
      ListBasicSettingsDesignerView control2 = e.Item.FindControl("listBasicSettingsDesignerView") as ListBasicSettingsDesignerView;
      control2.SortItemsText = string.Format(builderResources.SortItems, (object) str.ToLower());
      control2.DesignedMasterViewType = typeof (DynamicContentViewMaster).FullName;
      control2.CurrentViewName = control1.ControlDefinition.Views.Contains(key2) ? key2 : "DynamicContentMasterView";
      control2.InitView((ControlDesignerBase) this);
      this.ListSettingsViews.Add(key1, (ControlDesignerView) control2);
    }

    private void TemplatesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      string key = DataBinder.Eval(e.Item.DataItem, "TypeName") as string;
      string str1 = DataBinder.Eval(e.Item.DataItem, "DisplayName") as string;
      string str2 = DataBinder.Eval(e.Item.DataItem, "PluralDisplayName") as string;
      string str3 = DataBinder.Eval(e.Item.DataItem, "FullTypeName") as string;
      DynamicModuleResources dynamicModuleResources = Res.Get<DynamicModuleResources>();
      PlaceHolder control = e.Item.FindControl("templateSelectorsSelectorsPlaceHolder") as PlaceHolder;
      DynamicTypeTemplatesSelectorDesignerView child = new DynamicTypeTemplatesSelectorDesignerView();
      child.DetailDesignedViewType = typeof (DynamicContentViewDetail).FullName;
      child.MasterDesignedViewType = typeof (DynamicContentViewMaster).FullName;
      child.SingleItemText = string.Format(dynamicModuleResources.SingleItemTemplate, (object) str1.ToLower());
      child.ListOfItemsText = string.Format(dynamicModuleResources.ListOfTemplate, (object) str2.ToLower());
      child.DataItemTypeFullName = str3;
      child.InitView((ControlDesignerBase) this);
      control.Controls.Add((Control) child);
      this.TemplatesSelectorsViews.Add(key, (ControlDesignerView) child);
    }

    private void PagesSelectorTitlesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      string str1 = DataBinder.Eval(e.Item.DataItem, "DisplayName") as string;
      string str2 = DataBinder.Eval(e.Item.DataItem, "PluralDisplayName") as string;
      Literal control1 = e.Item.FindControl("pagesSelectorTitle") as Literal;
      Literal control2 = e.Item.FindControl("pagesSelectorDescription") as Literal;
      DynamicModuleResources dynamicModuleResources = Res.Get<DynamicModuleResources>();
      control1.Text = string.Format(dynamicModuleResources.PageFor, (object) str1);
      string str3 = string.Format(dynamicModuleResources.PagesSelectorDescription, (object) str1, (object) str2, (object) this.RootType.DisplayName);
      control2.Text = str3;
    }

    private static IList GetFullFunctionalityDataSource(
      ICollection<DynamicModuleType> dynamicModuleTypes)
    {
      List<string> list = dynamicModuleTypes.ToList<DynamicModuleType>().Select<DynamicModuleType, string>((Func<DynamicModuleType, string>) (t => PluralsResolver.Instance.ToPlural(t.DisplayName))).ToList<string>();
      if (dynamicModuleTypes.Count > 4)
      {
        list = dynamicModuleTypes.Take<DynamicModuleType>(2).Select<DynamicModuleType, string>((Func<DynamicModuleType, string>) (t => PluralsResolver.Instance.ToPlural(t.DisplayName))).ToList<string>();
        list.Add((string) null);
        list.Add(PluralsResolver.Instance.ToPlural(dynamicModuleTypes.Last<DynamicModuleType>().DisplayName));
      }
      return (IList) list;
    }

    private static IList GetRepeatersDataSource(
      ICollection<DynamicModuleType> dynamicModuleTypes)
    {
      return (IList) dynamicModuleTypes.Select(t => new
      {
        TypeName = t.TypeName,
        DisplayName = t.DisplayName,
        PluralDisplayName = PluralsResolver.Instance.ToPlural(t.DisplayName),
        FullTypeName = t.GetFullTypeName()
      }).ToList();
    }

    private void HandleInvalidContentType()
    {
      this.Message.RemoveAfter = -1;
      this.Message.ShowNegativeMessage(Res.Get<DynamicModuleResources>().DeletedModuleWarning);
    }

    private void InitializePagesSelector(HierarchicalContentView hierarchicalContentView)
    {
      string propertyValuesCulture = this.PropertyEditor.PropertyValuesCulture;
      this.PagesSelector.UICulture = propertyValuesCulture;
      string specificCultureFilter = CommonMethods.GenerateSpecificCultureFilter(propertyValuesCulture, "Title");
      if (!string.IsNullOrEmpty(specificCultureFilter))
      {
        this.PagesSelector.ConstantFilter = specificCultureFilter;
        this.PagesSelector.AppendConstantFilter = true;
      }
      string str = (string) null;
      if (hierarchicalContentView.MasterViewDefinition.DetailsPageId != Guid.Empty)
      {
        SiteMapNode siteMapNodeFromKey = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(hierarchicalContentView.MasterViewDefinition.DetailsPageId.ToString());
        if (siteMapNodeFromKey != null)
        {
          str = siteMapNodeFromKey.Title;
        }
        else
        {
          this.TopMessageText = "Page for opening view in detail mode was deleted. Please select another page.";
          this.TopMessageType = MessageType.Negative;
          str = "Selected page was deleted";
        }
      }
      Labels labels = Res.Get<Labels>();
      if (string.IsNullOrEmpty(str))
      {
        this.SelectedPageContainer.Style.Add("display", "none");
        this.PageSelectButton.Text = labels.Select;
      }
      else
      {
        this.SelectedPageLabel.Text = str;
        this.PageSelectButton.Text = labels.ChangePageButton;
      }
    }
  }
}
