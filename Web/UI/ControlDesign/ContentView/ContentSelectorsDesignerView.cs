// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelectorsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Enums;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  ///  Represents the Content Selectors tab
  ///  TODO: register script resources
  /// </summary>
  public class ContentSelectorsDesignerView : ContentViewDesignerView
  {
    private bool? addCultureToFilter;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.ContentSelectorsDesignerView.ascx");
    protected internal const string CategoriesQueryDataName = "Categories";
    protected internal const string TagsQueryDataName = "Tags";
    private const string defaultContentSelectorWebServiceUrl = "~/Sitefinity/Services/Content/NewsItemService.svc/";
    private const string contentSelectorItemFilter = "Visible == true AND Status == Live";
    private string noSingleItemSelectedLabel;
    private string contentSelectorWebServiceUrl;
    private bool isEmptyContent;

    /// <summary>Gets or sets the choose all text.</summary>
    /// <value>The radio choice all.</value>
    public string ChooseAllText { get; set; }

    /// <summary>Gets or sets the text for single item choice.</summary>
    /// <value>The radio choice single.</value>
    public string ChooseSingleText { get; set; }

    /// <summary>
    /// Gets or sets the predefined simple filter choice name.
    /// </summary>
    /// <value>The radio choice select.</value>
    public string ChooseSimpleFilterText { get; set; }

    /// <summary>Gets or sets the advanced filter choice name.</summary>
    /// <value>The radio choice advanced.</value>
    public string ChooseAdvancedFilterText { get; set; }

    /// <summary>Gets or sets the content selector title text.</summary>
    /// <value>The content title text.</value>
    public string ContentTitleText { get; set; }

    /// <summary>Gets or sets the single selector button text.</summary>
    /// <value>The single selector button text.</value>
    public string SingleSelectorButtonText { get; set; }

    /// <summary>Gets or sets the selected content title text.</summary>
    /// <value>The selected content title text.</value>
    public string SelectedContentTitleText { get; set; }

    /// <summary>Gets or sets the no content to select text.</summary>
    /// <value>The no content to select text.</value>
    public string NoContentToSelectText { get; set; }

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => "contentSelectorsViewSettings";

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => "Content";

    /// <summary>
    /// Gets or sets a value indicating whether to add the culture of the property editor to the filter.
    /// </summary>
    /// <value><c>true</c> if the culture will be added to the filter; otherwise, <c>false</c>.</value>
    /// <remarks>
    /// If not set a value, the property will return the value of the <see cref="M:Telerik.Sitefinity.Model.IAppSettings.Multilingual" />.;
    /// </remarks>
    public bool AddCultureToFilter
    {
      get
      {
        if (!this.addCultureToFilter.HasValue)
          this.addCultureToFilter = new bool?(SystemManager.CurrentContext.AppSettings.Multilingual);
        return this.addCultureToFilter.Value;
      }
      set => this.addCultureToFilter = new bool?(value);
    }

    public string ContentSelectorTitle { get; set; }

    public string ContentSelectorWebServiceUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.contentSelectorWebServiceUrl))
          this.contentSelectorWebServiceUrl = "~/Sitefinity/Services/Content/NewsItemService.svc/";
        return this.contentSelectorWebServiceUrl;
      }
      set => this.contentSelectorWebServiceUrl = value;
    }

    public string ContentSelectorItemTypeName { get; set; }

    public string ContentSelectorFilter { get; set; }

    /// <summary>
    /// Gets the single item content selector control. example - news selector or events selector
    /// </summary>
    /// <value>The content selector.</value>
    public ContentSelector ContentSelector => this.Container.GetControl<ContentSelector>("selector", false, TraverseMethod.DepthFirst);

    protected virtual string ScriptDescriptorType => this.GetType().FullName;

    /// <summary>Gets the radio choice for 'show all items'.</summary>
    /// <value>The radio choice all.</value>
    protected RadioButton RadioChoiceAll => this.Container.GetControl<RadioButton>("contentSelect_AllItems", false);

    /// <summary>Gets the radio choice for 'sjow singleitem'.</summary>
    /// <value>The radio choice single.</value>
    protected RadioButton RadioChoiceSingle => this.Container.GetControl<RadioButton>("contentSelect_OneItem", false);

    /// <summary>Gets the radio choice for simple predefined filters.</summary>
    /// <value>The radio choice simple filter.</value>
    protected RadioButton RadioChoiceSimpleFilter => this.Container.GetControl<RadioButton>("contentSelect_SimpleFilter", false);

    /// <summary>Gets the radio choice for advanced filter.</summary>
    /// <value>The radio choice advanced filter.</value>
    protected RadioButton RadioChoiceAdvancedFilter => this.Container.GetControl<RadioButton>("contentSelect_AdvancedFilter", false);

    /// <summary>Gets the select signle item button.</summary>
    /// <value>The select content button.</value>
    protected LinkButton SelectContentButton => this.Container.GetControl<LinkButton>("btnSelectSingleItem", false);

    /// <summary>Gets the radio choices title literal.</summary>
    /// <value>The choices title literal.</value>
    protected Literal ChoicesTitleLiteral => this.Container.GetControl<Literal>("choicesTitle", false);

    /// <summary>Gets the selected content label.</summary>
    /// <value>The selected content label.</value>
    protected Label SelectedContentLabel => this.Container.GetControl<Label>("selectedContentTitle", false);

    /// <summary>
    /// Gets a reference to btnSelectSingleItemWrapper control.
    /// </summary>
    protected HtmlGenericControl SelectContentButtonWrapper => this.Container.GetControl<HtmlGenericControl>("btnSelectSingleItemWrapper", false);

    /// <summary>Gets a reference to the filter selector control.</summary>
    protected virtual ContentFilterSelector ContentFilterSelector => this.Container.GetControl<ContentFilterSelector>("contentFilterSelector", true);

    /// <summary>Gets a reference to the filter selector control.</summary>
    protected virtual FilterSelector FilterSelector => this.Container.GetControl<FilterSelector>("filterSelector", true);

    /// <summary>Gets the button for narrow selection.</summary>
    protected virtual LinkButton NarrowSelectionButton => this.Container.GetControl<LinkButton>("btnNarrowSelection", false);

    /// <summary>Gets the narrow selection container.</summary>
    /// <value>The narrow selection container.</value>
    protected virtual Control NarrowSelectionContainer => this.Container.GetControl<Control>("narrowSelection", false);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Choose item" dialog
    /// </summary>
    public HtmlGenericControl SelectorTag => this.Container.GetControl<HtmlGenericControl>("selectorTag", false);

    /// <summary>
    /// Gets the reference to the flat taxon selector results view.
    /// </summary>
    protected virtual FlatTaxonSelectorResultView FlatTaxonSelector
    {
      get
      {
        FilterSelectorItem filterSelectorItem = this.ContentFilterSelector.FilterSelector.Items.Find((Predicate<FilterSelectorItem>) (i => i.ActualSelectorResultView is FlatTaxonSelectorResultView));
        return filterSelectorItem != null ? filterSelectorItem.ActualSelectorResultView as FlatTaxonSelectorResultView : (FlatTaxonSelectorResultView) null;
      }
    }

    /// <summary>Gets the providers selector control.</summary>
    /// <value>The providers selector control.</value>
    protected ProvidersSelector ProvidersSelector => this.Container.GetControl<ProvidersSelector>("providersSelector", false);

    /// <summary>
    /// No single item selected label, shown when there is no item selected in front of the single item selector
    /// </summary>
    public string NoSingleItemSelectedLabel
    {
      get
      {
        if (string.IsNullOrEmpty(this.noSingleItemSelectedLabel))
          this.noSingleItemSelectedLabel = Res.Get<Labels>("NoContentSelected");
        return this.noSingleItemSelectedLabel;
      }
      set => this.noSingleItemSelectedLabel = value;
    }

    /// <summary>Checks if there is no content to select.</summary>
    /// <returns>true if there is not content to select</returns>
    protected virtual bool CheckForEmptyContent()
    {
      IContentView currentContentView = this.CurrentContentView;
      try
      {
        int? totalCount = new int?(0);
        if (this.ContentManager != null)
        {
          if (currentContentView != null)
          {
            this.ContentManager.GetItems(currentContentView.ControlDefinition.ContentType, (string) null, (string) null, 0, 1, ref totalCount);
            int? nullable = totalCount;
            int num = 0;
            if (nullable.GetValueOrDefault() == num & nullable.HasValue)
            {
              this.isEmptyContent = true;
              return true;
            }
          }
        }
      }
      catch (ConfigurationErrorsException ex)
      {
      }
      this.isEmptyContent = false;
      return false;
    }

    /// <summary>Gets the detail item that was selected</summary>
    /// <returns>null if the item was deleted</returns>
    protected virtual IDataItem GetSelectedDetailItem()
    {
      IContentView currentContentView = this.CurrentContentView;
      try
      {
        return this.ContentManager.GetItem(currentContentView.ControlDefinition.ContentType, currentContentView.DetailViewDefinition.DataItemId) as IDataItem;
      }
      catch
      {
        return (IDataItem) null;
      }
    }

    /// <summary>Initializes the content manager</summary>
    protected new virtual void InitializeContentManager() => base.InitializeContentManager();

    /// <summary>Initializes the view.</summary>
    public override void InitView(ControlDesignerBase designer)
    {
      base.InitView(designer);
      this.InitializeContentManager();
      if (!this.IsControlDefinitionProviderCorrect)
      {
        string message = Res.Get<Labels>("DefinedProviderNotAvailable");
        this.SetTopErrorMessageToDesigner(designer, message);
      }
      if (this.CheckForEmptyContent())
        this.SelectedContentTitleText = this.NoContentToSelectText;
      if (this.CurrentContentView == null || this.CurrentContentView.ContentViewDisplayMode != ContentViewDisplayMode.Detail || !(this.CurrentContentView.DetailViewDefinition.DataItemId != Guid.Empty) || !this.IsControlDefinitionProviderCorrect)
        return;
      switch (this.GetSelectedDetailItem())
      {
        case null:
          string selectAnotherItem = Res.Get<Labels>().SelectedItemWasDeletedSelectAnotherItem;
          this.SetTopErrorMessageToDesigner(designer, selectAnotherItem);
          this.SelectedContentTitleText = Res.Get<Labels>().SelectedItemWasDeleted;
          break;
        case IContent content:
          this.SelectedContentTitleText = (string) content.Title;
          break;
        case DynamicContent component:
          PropertyDescriptor typeMainProperty = ModuleBuilderManager.GetTypeMainProperty(component.GetType());
          if (typeMainProperty is LstringPropertyDescriptor)
          {
            string name = this.GetUICulture() ?? CultureInfo.InvariantCulture.Name;
            this.SelectedContentTitleText = ((LstringPropertyDescriptor) typeMainProperty).GetString((object) component, CultureInfo.GetCultureInfo(name), false);
            break;
          }
          object obj = typeMainProperty.GetValue((object) component);
          if (obj == null)
            break;
          this.SelectedContentTitleText = obj.ToString();
          break;
      }
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.IsControlDefinitionProviderCorrect || this.isEmptyContent)
        this.HideSelectContentButtonWrapper();
      if (this.ChooseAllText != null)
        this.RadioChoiceAll.Text = this.ChooseAllText;
      if (this.ChooseSingleText != null)
        this.RadioChoiceSingle.Text = this.ChooseSingleText;
      if (this.ChooseSimpleFilterText != null)
        this.RadioChoiceSimpleFilter.Text = this.ChooseSimpleFilterText;
      if (this.ChooseAdvancedFilterText != null)
        this.RadioChoiceAdvancedFilter.Text = this.ChooseAdvancedFilterText;
      if (this.ContentTitleText != null)
        this.ChoicesTitleLiteral.Text = this.ContentTitleText;
      if (this.SelectContentButton != null && this.SingleSelectorButtonText != null)
        this.SelectContentButton.Text = this.SingleSelectorButtonText;
      if (this.SelectedContentLabel != null && this.SelectedContentTitleText != null)
        this.SelectedContentLabel.Text = HttpUtility.HtmlEncode(this.SelectedContentTitleText);
      this.SetFilterTaxonomyIds();
      string uiCulture = this.GetUICulture();
      if (this.ContentSelector != null)
      {
        if (this.ContentManager != null)
          this.ContentSelector.ProviderName = this.ContentManager.Provider.Name;
        this.ContentSelector.ServiceUrl = this.ContentSelectorWebServiceUrl;
        if (!string.IsNullOrEmpty(this.ContentSelectorTitle))
          this.ContentSelector.TitleText = this.ContentSelectorTitle;
        if (!string.IsNullOrEmpty(this.ContentSelectorItemTypeName))
          this.ContentSelector.ItemType = this.ContentSelectorItemTypeName;
        try
        {
          this.ContentFilterSelector.ProviderTypeName = this.ProvidersSelector.SelectedProviderName;
        }
        catch
        {
        }
        this.ContentFilterSelector.ItemTypeName = this.CurrentContentView.ControlDefinition.ContentType.FullName;
        this.ContentFilterSelector.UICulture = uiCulture;
        string str = "Visible == true AND Status == Live";
        this.ContentSelector.UICulture = uiCulture;
        if (string.IsNullOrEmpty(this.ContentSelectorFilter))
        {
          if (this.AddCultureToFilter && !string.IsNullOrEmpty(uiCulture))
            str += string.Format(" AND Culture == {0}", (object) uiCulture);
          this.ContentSelector.ItemsFilter = str;
        }
        else
          this.ContentSelector.ItemsFilter = this.ContentSelectorFilter.Length <= 1 ? string.Empty : this.ContentSelectorFilter;
      }
      if (this.ProvidersSelector != null)
      {
        if (this.ParentDesigner != null && this.ContentManager != null)
        {
          this.ProvidersSelector.Manager = this.ContentManager;
          string providerName = this.CurrentContentView.ControlDefinition.ProviderName;
          if (this.CurrentContentView != null && !string.IsNullOrEmpty(providerName))
            this.ProvidersSelector.SelectedProviderName = providerName;
          else if (this.ProvidersSelector.Providers.Count > 0)
            this.ProvidersSelector.SelectedProviderName = this.ProvidersSelector.Providers[0].Name;
        }
        else
          this.ProvidersSelector.Visible = false;
      }
      if (this.FlatTaxonSelector == null)
        return;
      this.FlatTaxonSelector.UICulture = uiCulture;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ContentSelectorsDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Hides the select content button wrapper.</summary>
    public virtual void HideSelectContentButtonWrapper()
    {
      if (this.SelectContentButtonWrapper == null)
        return;
      this.SelectContentButtonWrapper.Style.Add("display", "none");
    }

    private void SetTopErrorMessageToDesigner(ControlDesignerBase designer, string message)
    {
      if (designer is ContentViewDesignerBase viewDesignerBase)
      {
        viewDesignerBase.TopMessageText = message;
        viewDesignerBase.TopMessageType = MessageType.Negative;
      }
      else
      {
        HierarchicalContentViewDesigner contentViewDesigner = designer as HierarchicalContentViewDesigner;
        contentViewDesigner.TopMessageText = message;
        contentViewDesigner.TopMessageType = MessageType.Negative;
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorType, this.ClientID);
      if (this.SelectContentButton != null)
        controlDescriptor.AddProperty("_selectContentButton", (object) this.SelectContentButton.ClientID);
      if (this.SelectedContentLabel != null)
        controlDescriptor.AddElementProperty("selectedContentTitle", this.SelectedContentLabel.ClientID);
      if (this.SelectContentButtonWrapper != null)
        controlDescriptor.AddElementProperty("selectContentButtonWrapper", this.SelectContentButtonWrapper.ClientID);
      if (this.ContentSelector != null)
        controlDescriptor.AddComponentProperty("contentSelector", this.ContentSelector.ClientID);
      controlDescriptor.AddComponentProperty("filterSelector", this.ContentFilterSelector.FilterSelector.ClientID);
      if (this.SelectorTag != null)
        controlDescriptor.AddElementProperty("selectorTag", this.SelectorTag.ClientID);
      if (this.NarrowSelectionButton != null)
        controlDescriptor.AddElementProperty("btnNarrowSelection", this.NarrowSelectionButton.ClientID);
      if (this.NarrowSelectionContainer != null)
        controlDescriptor.AddElementProperty("narrowSelection", this.NarrowSelectionContainer.ClientID);
      if (this.ProvidersSelector != null && this.ProvidersSelector.Visible)
        controlDescriptor.AddComponentProperty("providersSelector", this.ProvidersSelector.ClientID);
      controlDescriptor.AddProperty("noSingleItemSelectedLabel", (object) this.NoSingleItemSelectedLabel);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
      string assembly = typeof (ContentSelectorsDesignerView).Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.ContentSelectorsDesignerView.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.FilterSelectorHelper.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected virtual void SetFilterTaxonomyIds()
    {
      this.ContentFilterSelector.FilterSelector.SetTaxonomyId("Categories", TaxonomyManager.CategoriesTaxonomyId);
      this.ContentFilterSelector.FilterSelector.SetTaxonomyId("Tags", TaxonomyManager.TagsTaxonomyId);
    }
  }
}
