// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterDetailView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI
{
  /// <summary>MasterDetailView for Forms module.</summary>
  public class FormsMasterDetailView : MasterDetailView
  {
    private List<Control> formDescriptionControls;
    private string formsPageUrl;
    private string exportServiceUrl = "~/Sitefinity/Content/FormEntries/Export";
    private string entryPageIndexServiceUrl = "~/Sitefinity/Services/Forms/FormsService.svc/entryindex";
    private const string script = "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.FormsMasterDetailView.js";
    public new static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentUI.FormsMasterDetailView.ascx");

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the name of the form.</summary>
    /// <value>The name of the form.</value>
    public string FormName { get; set; }

    /// <summary>Gets or sets the title of the form.</summary>
    /// <value>The title of the form.</value>
    public string FormTitle { get; set; }

    /// <summary>Gets or sets the form description.</summary>
    /// <value>The form description.</value>
    public FormDescription FormDescription { get; set; }

    /// <summary>Gets the form field controls.</summary>
    /// <value>The form field controls.</value>
    public virtual List<Control> FormDescriptionControls
    {
      get
      {
        if (this.formDescriptionControls == null)
          this.LoadFormFieldControls();
        return this.formDescriptionControls;
      }
    }

    /// <summary>Gets or sets the url of the forms page.</summary>
    /// <value>The forms page URL.</value>
    public virtual string FormsPageUrl
    {
      get
      {
        if (this.formsPageUrl == null)
          this.formsPageUrl = RouteHelper.ResolveUrl(PageManager.GetManager().GetPageNode(FormsModule.FormsPageGroupId).GetUrl(), UrlResolveOptions.Rooted);
        return this.formsPageUrl;
      }
      set => this.formsPageUrl = value;
    }

    /// <summary>Gets or sets the export service URL.</summary>
    /// <value>The export service URL.</value>
    public string ExportServiceUrl
    {
      get => VirtualPathUtility.ToAbsolute(this.exportServiceUrl);
      set => this.exportServiceUrl = value;
    }

    /// <summary>Gets or sets the entry page index service URL.</summary>
    /// <value>The entry page index service URL.</value>
    public string EntryPageIndexServiceUrl
    {
      get => VirtualPathUtility.ToAbsolute(this.entryPageIndexServiceUrl);
      set => this.entryPageIndexServiceUrl = value;
    }

    /// <summary>
    /// Gets the splitter bar between the Master and the Detail view.
    /// </summary>
    /// <value>The splitter bar.</value>
    protected RadSplitBar SplitterBar => this.Container.GetControl<RadSplitBar>("splitterBar", true);

    /// <summary>
    /// Gets the detail view <see cref="T:Telerik.Web.UI.RadPane" />.
    /// </summary>
    /// <value>The detail view pane.</value>
    protected RadPane DetailViewPane => (RadPane) this.DetailViewPlaceHolder;

    /// <summary>Gets the back to forms link control.</summary>
    public HyperLink BackToFormsLink => this.Container.GetControl<HyperLink>("backToForms", true);

    /// <summary>Get the view title control</summary>
    public ITextControl ViewTitle => this.Container.GetControl<ITextControl>("viewTitle", true);

    public FlatSiteSelector FlatSiteSelector => this.Container.GetControl<FlatSiteSelector>("flatSiteSelector", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      if (!(definition is IContentViewMasterDetailDefinition detailDefinition))
        throw new NotSupportedException("The definition is not of type IContentViewMasterDetailDefinition.");
      this.InitializeUrlParams();
      this.InitializeFormDescription();
      this.InitializeHeaderContent();
      this.InitializeSiteSelector();
      this.AddDynamicItemConfiguration(detailDefinition);
      base.InitializeControls(container, (IContentViewDefinition) detailDefinition);
    }

    /// <summary>
    /// Gets the default name of the layout template.
    /// It will be used if there is no LayoutTemplateName in the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewDefinition" /> definition.
    /// </summary>
    /// <value>The default name of the layout template.</value>
    public override string DefaultLayoutTemplateName => FormsMasterDetailView.layoutTemplateName;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FormsMasterDetailView.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void InitializeSiteSelector()
    {
      SiteItemLink[] formSites = this.GetFormsManager().Provider.GetSiteFormLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == this.FormDescription.Id)).ToArray<SiteItemLink>();
      this.FlatSiteSelector.Sites = this.FlatSiteSelector.Sites.Where<ISite>((Func<ISite, bool>) (s => ((IEnumerable<SiteItemLink>) formSites).Any<SiteItemLink>((Func<SiteItemLink, bool>) (l => l.SiteId == s.Id))));
    }

    private void InitializeHeaderContent()
    {
      this.SetupBackToFormsLink();
      this.SetupTitleText();
    }

    private void SetupBackToFormsLink()
    {
      this.BackToFormsLink.NavigateUrl = this.FormsPageUrl;
      this.BackToFormsLink.Text = Res.Get<FormsResources>().BackToForms;
    }

    private void SetupTitleText() => this.ViewTitle.Text = string.Format("{0} <i>{1}</i>", (object) Res.Get<FormsResources>().ResponsesFor, (object) this.FormTitle);

    protected void InitializeUrlParams()
    {
      object[] values;
      this.EvaluateUrl("Forms", "", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix, out values);
      if (values.Length < 2)
        this.RedirectToFormsPage();
      if (values.Length == 0)
        return;
      this.ProviderName = values[0] as string;
      this.FormName = values[1] as string;
      RouteHelper.SetUrlParametersResolved();
    }

    private void InitializeFormDescription()
    {
      this.FormDescription = this.GetFormsManager().Provider.GetSiteItems<FormDescription>(SystemManager.CurrentContext.CurrentSite.Id).Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Name == this.FormName)).SingleOrDefault<FormDescription>();
      if (this.FormDescription != null)
      {
        if (this.FormDescription.IsGranted("Forms", "ViewResponses"))
        {
          this.FormTitle = (string) this.FormDescription.Title;
          return;
        }
      }
      this.RedirectToFormsPage();
    }

    protected FormsManager GetFormsManager() => FormsManager.GetManager(this.ProviderName);

    /// <summary>
    /// Modifies the definitions adding the dynamic information.
    /// </summary>
    /// <param name="masterDetailDefinition">The master detail definition.</param>
    private void AddDynamicItemConfiguration(
      IContentViewMasterDetailDefinition masterDetailDefinition)
    {
      this.SetContentType();
      this.ConfigureMasterDefinition(masterDetailDefinition.MasterDefinition);
      this.ConfigureDetailDefintition(masterDetailDefinition.DetailDefinition);
    }

    private void SetContentType()
    {
      FormsManager formsManager = this.GetFormsManager();
      this.Host.ControlDefinition.ContentType = formsManager.GetEntryType(string.Format("{0}.{1}", (object) formsManager.Provider.FormsNamespace, (object) this.FormName));
    }

    private void LoadFormFieldControls()
    {
      this.formDescriptionControls = new List<Control>();
      foreach (FormControl control1 in (IEnumerable<FormControl>) this.FormDescription.Controls)
      {
        Control control2 = this.GetFormsManager().LoadControl((ObjectData) control1, (CultureInfo) null);
        if (control2 != null)
          this.formDescriptionControls.Add(control2);
      }
    }

    private void ConfigureMasterDefinition(
      IContentViewMasterDefinition masterContentDefinition)
    {
      masterContentDefinition.WebServiceBaseUrl += this.FormName;
      if (!(masterContentDefinition is IMasterViewDefinition masterDefinition))
        return;
      this.ConfigureSearchFields(masterDefinition);
      this.ConfigureEntryDetailDialog(masterDefinition);
      this.ConfigureSortingWidgetBar(masterDefinition);
      if (!(masterDefinition.ViewModes["Grid"] is IGridViewModeDefinition viewMode))
        return;
      this.ConfigureColumnsDefnintion(viewMode);
    }

    private void ConfigureEntryDetailDialog(IMasterViewDefinition masterDefinition)
    {
      foreach (IDialogDefinition dialogDefinition in masterDefinition.Dialogs.Where<IDialogDefinition>((Func<IDialogDefinition, bool>) (d => d.Name == typeof (FormEntryEditDialog).Name)))
      {
        if (dialogDefinition.Name == typeof (FormEntryEditDialog).Name)
          dialogDefinition.Parameters = "?" + "formName=" + this.FormName + "&viewName=" + dialogDefinition.OpenOnCommandName;
      }
    }

    private void ConfigureSearchFields(IMasterViewDefinition masterDefinition)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IControlBehaviorResolver controlBehaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
      string allowedClrType = typeof (string).FullName;
      IEnumerable<string> values = this.FormDescriptionControls.Select<Control, IFormFieldControl>((Func<Control, IFormFieldControl>) (c => controlBehaviorResolver.GetBehaviorObject(c) as IFormFieldControl)).Where<IFormFieldControl>((Func<IFormFieldControl, bool>) (c => c != null && c.MetaField != null && c.MetaField.ClrType == allowedClrType && !c.MetaField.Hidden)).Select<IFormFieldControl, string>((Func<IFormFieldControl, string>) (c => c.MetaField.FieldName));
      masterDefinition.SearchFields = string.Join(",", values);
    }

    private void ConfigureColumnsDefnintion(IGridViewModeDefinition definition)
    {
      if (!SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        IColumnDefinition columnDefinition = definition.Columns.FirstOrDefault<IColumnDefinition>((Func<IColumnDefinition, bool>) (p => p.Name == "Language"));
        if (columnDefinition != null)
          definition.Columns.Remove(columnDefinition);
      }
      IControlBehaviorResolver behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
      foreach (Control descriptionControl in this.FormDescriptionControls)
      {
        if (descriptionControl != null && behaviorResolver.GetBehaviorObject(descriptionControl) is IFormFieldControl behaviorObject && behaviorObject.MetaField != null && !behaviorObject.MetaField.FieldName.IsNullOrEmpty())
        {
          IMetaField metaField = behaviorObject.MetaField;
          if (!metaField.Hidden)
          {
            DataColumnDefinition columnDefinition = new DataColumnDefinition();
            if (metaField.ClrType == typeof (ContentLink[]).FullName)
            {
              StringBuilder stringBuilder = new StringBuilder();
              stringBuilder.Append("{{");
              stringBuilder.Append(metaField.FieldName);
              stringBuilder.Append(".length}} ");
              stringBuilder.Append("{{");
              stringBuilder.AppendFormat("({0}.length>1||{0}.length==0)?'{1}':'{2}'", (object) metaField.FieldName, (object) Res.Get<FormsResources>().AttachedFiles, (object) Res.Get<FormsResources>().AttachedFile);
              stringBuilder.Append("}}");
              columnDefinition.ClientTemplate = stringBuilder.ToString();
            }
            else
              columnDefinition.ClientTemplate = string.Format("{{{{{0}}}}}", (object) metaField.FieldName);
            columnDefinition.BoundPropertyName = metaField.FieldName;
            columnDefinition.Name = metaField.FieldName;
            string str1 = (string) null;
            if (descriptionControl is FieldControl)
              str1 = (descriptionControl as FieldControl).Title;
            else if (behaviorObject.MetaField != null)
              str1 = behaviorObject.MetaField.Title != null ? behaviorObject.MetaField.Title : behaviorObject.MetaField.FieldName;
            string str2 = this.AdjustTextForUi(str1 ?? behaviorObject.GetType().Name, 50);
            columnDefinition.HeaderText = str2;
            definition.Columns.Add((IColumnDefinition) columnDefinition);
          }
        }
      }
    }

    private void ConfigureDetailDefintition(IContentViewDetailDefinition detailDefinition)
    {
      FormsDetailViewDefinition detailViewDefinition = (FormsDetailViewDefinition) detailDefinition;
      detailViewDefinition.FormName = this.FormName;
      detailViewDefinition.WebServiceBaseUrl = string.Format("{0}{1}/", (object) detailViewDefinition.WebServiceBaseUrl, (object) this.FormName);
    }

    private void ConfigureSortingWidgetBar(IMasterViewDefinition masterDefinition)
    {
      if (!(masterDefinition.Toolbar is WidgetBarElement toolbar))
        return;
      WidgetBarSectionElement barSectionElement = toolbar.WidgetSections.Find((Predicate<WidgetBarSectionElement>) (item => item.GetKey() == "FormEntriesToolbar"));
      if (barSectionElement == null || !(barSectionElement.Items.First<WidgetElement>((Func<WidgetElement, bool>) (item => item.GetKey() == "FormEntriesSorting")) is DynamicCommandWidgetElement widget))
        return;
      widget.ClearDynamicItems();
      this.AddDynamicSortingCommandItems(widget);
    }

    private void AddDynamicSortingCommandItems(DynamicCommandWidgetElement widget)
    {
      NameValueCollection duplicateControlIndexes = new NameValueCollection();
      foreach (Control descriptionControl in this.FormDescriptionControls)
      {
        if (this.IsSortingOptionNeeded(descriptionControl.GetType()))
        {
          IFormFieldControl formFieldControl = (IFormFieldControl) descriptionControl;
          if (formFieldControl.MetaField != null)
          {
            IMetaField metaField = formFieldControl.MetaField;
            if (!metaField.Hidden && descriptionControl is FieldControl)
            {
              FieldControl formControl = descriptionControl as FieldControl;
              this.AddSortingCommandItem(formControl, widget, duplicateControlIndexes, metaField, Telerik.Sitefinity.Modules.SortDirection.Ascending);
              this.AddSortingCommandItem(formControl, widget, duplicateControlIndexes, metaField, Telerik.Sitefinity.Modules.SortDirection.Descending);
            }
          }
        }
      }
    }

    private void AddSortingCommandItem(
      FieldControl formControl,
      DynamicCommandWidgetElement widget,
      NameValueCollection duplicateControlIndexes,
      IMetaField field,
      Telerik.Sitefinity.Modules.SortDirection direction)
    {
      int num1 = 2;
      string str1 = this.AdjustTextForUi(formControl.Title, 50);
      string str2 = ContentHelper.FormatSortText(str1, direction);
      DynamicItemElement dynamicItemElement;
      if (widget.Items.Contains(str2))
      {
        if (duplicateControlIndexes.Get(str2) != null)
        {
          int num2 = int.Parse(duplicateControlIndexes.GetValues(str2)[0]);
          NameValueCollection nameValueCollection = duplicateControlIndexes;
          string name = str2;
          int num3 = num2;
          num1 = num3 + 1;
          string str3 = num3.ToString();
          nameValueCollection.Set(name, str3);
        }
        else
          duplicateControlIndexes.Add(str2, num1.ToString());
        if (str1.EndsWith("..."))
          str1 = str1.Substring(0, str1.Length - 4);
        string title = ContentHelper.FormatSortText(string.Format("{0}_{1}", (object) str1, (object) num1), direction);
        dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) widget.Items, title, ContentHelper.FormatSortValue(field.FieldName, direction), (string) null, (NameValueCollection) null);
      }
      else
        dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) widget.Items, str2, ContentHelper.FormatSortValue(field.FieldName, direction), (string) null, (NameValueCollection) null);
      widget.Items.Add(dynamicItemElement);
    }

    private bool IsSortingOptionNeeded(Type formControlType) => typeof (IFormFieldControl).IsAssignableFrom(formControlType) && formControlType != typeof (FormCheckboxes) && formControlType != typeof (FormFileUpload);

    private void RedirectToFormsPage() => this.Page.Response.Redirect(this.FormsPageUrl);

    /// <summary>
    /// Strips the html tags and shrotens the text to the number of words
    /// </summary>
    /// <param name="text">text to be processed</param>
    /// <param name="numberOfWords">number of words which to remain</param>
    /// <param name="addElipsis">if set to <c>true</c> adds elipsis at the end.</param>
    /// <returns></returns>
    private string AdjustTextForUi(string text, int maxCharacters)
    {
      string valueToTruncate = HtmlStripper.StripTagsRegexCompiled(text);
      return valueToTruncate.Length <= maxCharacters ? valueToTruncate : valueToTruncate.TruncateString(maxCharacters, SitefinityExtensions.TruncateOptions.AllowLastWordToGoOverMaxLength | SitefinityExtensions.TruncateOptions.IncludeEllipsis);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("splitterBar", this.SplitterBar.ClientID);
      controlDescriptor.AddComponentProperty("detailViewPane", this.DetailViewPane.ClientID);
      controlDescriptor.AddComponentProperty("flatSiteSelector", this.FlatSiteSelector.ClientID);
      controlDescriptor.AddProperty("_exportServiceUrl", (object) this.ExportServiceUrl);
      controlDescriptor.AddProperty("_entryPageIndexServiceUrl", (object) this.EntryPageIndexServiceUrl);
      controlDescriptor.AddProperty("_formName", (object) this.FormName);
      controlDescriptor.AddProperty("_itemType", (object) this.Host.ControlDefinition.ContentType.FullName);
      controlDescriptor.AddProperty("_isShared", (object) (this.GetFormsManager().GetSiteFormLinks().Count<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == this.FormDescription.Id)) > 1));
      controlDescriptor.AddProperty("_isPublished", (object) (bool) (!this.FormDescription.Visible ? 0 : (this.FormDescription.Status == ContentLifecycleStatus.Live ? 1 : 0)));
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.FormsMasterDetailView.js", typeof (FormsMasterDetailView).Assembly.FullName)
    };
  }
}
