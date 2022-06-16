// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.SingleItemSettingsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  ///  represents the list view designer
  ///  TODO: register script resources
  /// </summary>
  public class SingleItemSettingsDesignerView : ContentViewDesignerView
  {
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.SingleItemSettingsDesignerView.ascx");
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    public bool ShowTemplateSelector = true;
    public List<string> DetailTemplateCssClasses = new List<string>()
    {
      "sfDropDownListWrp"
    };
    public RenderChoicesAs ChoiceFieldRenderChoiceAs = RenderChoicesAs.DropDown;

    public string OpenInCurrentPageText
    {
      get => this.OpenInCurrentPageRadioChoice.Text;
      set => this.OpenInCurrentPageRadioChoice.Text = value;
    }

    public string OpenInNewPageText
    {
      get => this.OpenInNewPageRadioChoice.Text;
      set => this.OpenInNewPageRadioChoice.Text = value;
    }

    public string SelectedPageText { get; set; }

    /// <summary>Gets or sets the text for the TopHeaderLabel literal.</summary>
    public string TopHeaderLabelText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to render a 3rd choice in the main radio buttons.
    /// </summary>
    [DefaultValue(false)]
    public bool RenderDontDisplayAnySingleItemChoice { get; set; }

    public override string ViewName => "singleItemViewSettings";

    public override string ViewTitle => "Single Item Settings";

    /// <summary>
    /// The type name of the view used to display the designed control in details mode.
    /// </summary>
    public string DesignedDetailViewType { get; set; }

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");

    protected virtual ChoiceField DetailViewName => this.Container.GetControl<ChoiceField>("detailViewName", true);

    protected virtual HtmlGenericControl TemplateChoiceDiv => this.Container.GetControl<HtmlGenericControl>("templateChoiceDiv", true);

    protected virtual HtmlGenericControl CreateTemplateContainer => this.Container.GetControl<HtmlGenericControl>("createTemplateContainer", true);

    protected virtual HtmlGenericControl DetailTemplateHeader => this.Container.GetControl<HtmlGenericControl>("detailTemplateHeader", true);

    /// <summary>Gets the item page setting_current page.</summary>
    /// <value>The item page setting_current page.</value>
    protected RadioButton OpenInCurrentPageRadioChoice => this.Container.GetControl<RadioButton>("itemPageSetting_currentPage", true);

    protected RadioButton OpenInNewPageRadioChoice => this.Container.GetControl<RadioButton>("itemPageSetting_newPage", true);

    protected RadioButton DontOpenPageRadioChoice => this.Container.GetControl<RadioButton>("itemPageSettings_noPage", this.RenderDontDisplayAnySingleItemChoice);

    protected LinkButton PageSelectButton => this.Container.GetControl<LinkButton>("pageSelectButton", true);

    protected PagesSelector PagesSelector => this.Container.GetControl<PagesSelector>("pagesSelector", true);

    protected Label SelectedPageLabel => this.Container.GetControl<Label>("selectedPageLabel", true);

    /// <summary>
    /// Gets the ChoiceField control that lists and selects the control template for the details mode
    /// </summary>
    protected virtual ChoiceField DetailViewNameList => this.Container.GetControl<ChoiceField>("detailViewName", true);

    /// <summary>
    /// Gets the Label control representing the title of the selected control template
    /// </summary>
    protected virtual Label TemplateTitle => this.Container.GetControl<Label>("templateTitle", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected control template
    /// </summary>
    protected virtual HyperLink EditTemplateLink => this.Container.GetControl<HyperLink>("editTemplateLink", true);

    /// <summary>
    /// Gets the correct instance of the RadWindowManager class
    /// </summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>
    /// Gets the link that opens up the dialog for creating a new control template
    /// </summary>
    protected virtual HyperLink CreateTemplateLink => this.Container.GetControl<HyperLink>("createNewDetailTemplateLink", true);

    /// <summary>Gets a reference to the TopHeaderLabel literal.</summary>
    protected virtual Literal TopHeaderLabel => this.Container.GetControl<Literal>("topHeaderLabel", true);

    /// <summary>
    /// Gets a reference to the li tag that wraps the optional 3rd choice in the main radio buttons.
    /// </summary>
    protected virtual HtmlGenericControl DontDisplaySingleItemPageLi => this.Container.GetControl<HtmlGenericControl>("liDontDisplaySingleItemPage", true);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Select page" dialog
    /// </summary>
    public HtmlGenericControl SelectorTag => this.Container.GetControl<HtmlGenericControl>("selectorTag", true);

    public override void InitView(ControlDesignerBase ParentDesigner)
    {
      base.InitView(ParentDesigner);
      IContentView control = this.ParentDesigner.PropertyEditor.Control as IContentView;
      if (!(control.MasterViewDefinition.DetailsPageId != Guid.Empty))
        return;
      ContentViewDesignerBase parentDesigner = this.ParentDesigner as ContentViewDesignerBase;
      SiteMapNode siteMapNodeFromKey = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(control.MasterViewDefinition.DetailsPageId.ToString());
      if (siteMapNodeFromKey != null)
      {
        this.SelectedPageText = siteMapNodeFromKey.Title;
      }
      else
      {
        parentDesigner.TopMessageText = "Page for opening view in detail mode was deleted. Please select another page.";
        parentDesigner.TopMessageType = MessageType.Negative;
        this.SelectedPageText = "Selected page was deleted";
      }
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.DetailViewName.RenderChoicesAs = this.ChoiceFieldRenderChoiceAs;
      this.TemplateChoiceDiv.Attributes["class"] = this.DetailTemplateCssClass;
      string str = !this.ShowTemplateSelector ? "display: none;" : "";
      this.TemplateChoiceDiv.Attributes["style"] = str;
      this.CreateTemplateContainer.Attributes["style"] = str;
      this.DetailTemplateHeader.Attributes["style"] = str;
      Labels labels = Res.Get<Labels>();
      if (string.IsNullOrEmpty(this.SelectedPageText))
      {
        this.SelectedPageLabel.Style.Add("display", "none");
        this.PageSelectButton.Text = labels.SelectPageButton;
      }
      else
      {
        this.SelectedPageLabel.Text = HttpUtility.HtmlEncode(this.SelectedPageText);
        this.PageSelectButton.Text = labels.ChangePageButton;
      }
      this.TopHeaderLabel.Text = !string.IsNullOrEmpty(this.TopHeaderLabelText) ? this.TopHeaderLabelText : labels.OpenSingleItemInDotDotDot;
      if (this.RenderDontDisplayAnySingleItemChoice)
        this.DontDisplaySingleItemPageLi.Visible = true;
      IQueryable<ControlPresentation> query = PageManager.GetManager().GetPresentationItems<ControlPresentation>(SystemManager.CurrentContext.CurrentSite.Id).Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE"));
      int? nullable = new int?();
      string filterExpression = !(this.ParentDesigner is DynamicContentViewDesigner) ? string.Format("ControlType == \"{0}\"", (object) this.DesignedDetailViewType) : string.Format("ControlType == \"{0}\" AND Condition == \"{1}\"", (object) this.DesignedDetailViewType, (object) ((DynamicContentViewDesigner) this.ParentDesigner).DataItemTypeFullName);
      int? skip = new int?(0);
      int? take = new int?(0);
      ref int? local = ref nullable;
      foreach (ControlPresentation setExpression in (IEnumerable<ControlPresentation>) DataProviderBase.SetExpressions<ControlPresentation>(query, filterExpression, "Name", skip, take, ref local))
        this.DetailViewNameList.Choices.Add(new ChoiceItem()
        {
          Text = setExpression.Name,
          Value = setExpression.Id.ToString()
        });
      if (this.DetailViewNameList.Choices.Count < 2)
        this.DetailViewNameList.CssClass += " sfDisplayNone";
      if (this.ParentDesigner == null || this.ParentDesigner.PropertyEditor == null)
        return;
      string propertyValuesCulture = this.ParentDesigner.PropertyEditor.PropertyValuesCulture;
      this.PagesSelector.UICulture = propertyValuesCulture;
      string specificCultureFilter = CommonMethods.GenerateSpecificCultureFilter(propertyValuesCulture, "Title");
      if (string.IsNullOrEmpty(specificCultureFilter))
        return;
      this.PagesSelector.ConstantFilter = specificCultureFilter;
      this.PagesSelector.AppendConstantFilter = true;
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SingleItemSettingsDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (FieldControl fieldControl in this.Container.GetControls<FieldControl>().Values)
      {
        if (!string.IsNullOrEmpty(fieldControl.DataFieldName))
          dictionary.Add(fieldControl.DataFieldName, fieldControl.ClientID);
      }
      controlDescriptor.AddProperty("dataFieldNameControlIdMap", (object) dictionary);
      controlDescriptor.AddElementProperty("pageSelectButton", this.PageSelectButton.ClientID);
      controlDescriptor.AddComponentProperty("pagesSelector", this.PagesSelector.ClientID);
      controlDescriptor.AddElementProperty("selectedPageLabel", this.SelectedPageLabel.ClientID);
      controlDescriptor.AddElementProperty("templateTitleElement", this.TemplateTitle.ClientID);
      controlDescriptor.AddElementProperty("editTemplateLink", this.EditTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createTemplateLink", this.CreateTemplateLink.ClientID);
      controlDescriptor.AddComponentProperty("viewsList", this.DetailViewNameList.ClientID);
      controlDescriptor.AddProperty("_widgetEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}"));
      controlDescriptor.AddProperty("_editTemplateViewName", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName);
      controlDescriptor.AddProperty("_createTemplateViewName", (object) ControlTemplatesDefinitions.BackendInsertDetailsViewName);
      controlDescriptor.AddComponentProperty("radWindowManager", this.RadWindowManager.ClientID);
      controlDescriptor.AddElementProperty("selectorTag", this.SelectorTag.ClientID);
      controlDescriptor.AddProperty("_modifyWidgetTemplatePermission", (object) this.ModifyWidgetTemplatePermission);
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
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.SingleItemSettingsDesignerView.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources"));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    public string DetailTemplateCssClass => this.DetailTemplateCssClasses == null ? "" : string.Join(" ", (IEnumerable<string>) this.DetailTemplateCssClasses);
  }
}
