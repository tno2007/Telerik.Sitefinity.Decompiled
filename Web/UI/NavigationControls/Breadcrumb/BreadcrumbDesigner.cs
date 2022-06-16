// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.BreadcrumbDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb
{
  /// <summary>Designer for the Breadcrumb control.</summary>
  public class BreadcrumbDesigner : ControlDesignerBase
  {
    private string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.NavigationControls.BreadcrumbDesigner.ascx");
    private string scriptReference = "Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.Scripts.BreadcrumbDesigner.js";
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    private IDictionary<string, string> embeddedTemplateMap;
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";

    /// <summary>
    /// Gets or sets the layout template path of the Breadcrumb's desinger.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => this.layoutTemplatePath;
      set => this.layoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the layout template name of the Breadcrumb's desinger.
    /// </summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>Gets the page selector control.</summary>
    /// <value>The page selector control.</value>
    protected internal virtual PagesSelector PageSelectorControl => this.Container.GetControl<PagesSelector>("pageSelector", true);

    /// <summary>Gets the correct instance of RadWindowManager.</summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected control template.
    /// </summary>
    protected virtual HyperLink EditTemplateLink => this.Container.GetControl<HyperLink>("editTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for creating a new control template.
    /// </summary>
    protected virtual HyperLink CreateTemplateLink => this.Container.GetControl<HyperLink>("createNewTemplateLink", true);

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");

    /// <summary>
    /// The type name of the view used to display the designed control in master mode.
    /// </summary>
    public string DesignedMasterViewType { get; set; }

    /// <summary>
    /// Gets the choice field listing all possible layout templates.
    /// </summary>
    protected virtual ChoiceField MasterViewNameList => this.Container.GetControl<ChoiceField>("masterViewName", true);

    /// <summary>
    /// Gets the Label control representing the title of the selected control template.
    /// </summary>
    protected virtual Label TemplateTitle => this.Container.GetControl<Label>("templateTitle", true);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Choose page" dialog
    /// </summary>
    public HtmlGenericControl SelectorTag => this.Container.GetControl<HtmlGenericControl>("selectorTag", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initialize the controls of the Breadcrumb's designer.</summary>
    /// <param name="container"></param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.DesignerMode = ControlDesignerModes.Simple;
      this.TemplateTitle.Style.Add("display", "none");
      PageManager manager = PageManager.GetManager();
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      IQueryable<ControlPresentation> queryable = (multisiteContext == null ? manager.GetPresentationItems<ControlPresentation>() : manager.GetPresentationItems<ControlPresentation>(multisiteContext.CurrentSite.Id)).Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE" && tmpl.ControlType == typeof (Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.Breadcrumb).FullName));
      this.embeddedTemplateMap = (IDictionary<string, string>) new Dictionary<string, string>();
      foreach (ControlPresentation controlPresentation in (IEnumerable<ControlPresentation>) queryable)
      {
        Collection<ChoiceItem> choices = this.MasterViewNameList.Choices;
        ChoiceItem choiceItem = new ChoiceItem();
        choiceItem.Text = controlPresentation.Name;
        Guid id = controlPresentation.Id;
        choiceItem.Value = id.ToString();
        choices.Add(choiceItem);
        if (!string.IsNullOrEmpty(controlPresentation.EmbeddedTemplateName))
        {
          IDictionary<string, string> embeddedTemplateMap = this.embeddedTemplateMap;
          string embeddedTemplateName = controlPresentation.EmbeddedTemplateName;
          id = controlPresentation.Id;
          string str = id.ToString();
          embeddedTemplateMap.Add(embeddedTemplateName, str);
        }
      }
      string propertyValuesCulture = this.PropertyEditor.PropertyValuesCulture;
      this.PageSelectorControl.UICulture = propertyValuesCulture;
      string specificCultureFilter = CommonMethods.GenerateSpecificCultureFilter(propertyValuesCulture, "Title");
      if (string.IsNullOrEmpty(specificCultureFilter))
        return;
      this.PageSelectorControl.ConstantFilter = specificCultureFilter;
      this.PageSelectorControl.AppendConstantFilter = true;
    }

    /// <summary>Get the tag key of the Breadcrumb's designer.</summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets script descriptors of the Breadcrumb's designer.</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("pageSelector", this.PageSelectorControl.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (FieldControl fieldControl in this.Container.GetControls<FieldControl>().Values)
      {
        if (!string.IsNullOrEmpty(fieldControl.DataFieldName))
          dictionary.Add(fieldControl.DataFieldName, fieldControl.ClientID);
      }
      controlDescriptor.AddProperty("dataFieldNameControlIdMap", (object) dictionary);
      controlDescriptor.AddComponentProperty("radWindowManager", this.RadWindowManager.ClientID);
      controlDescriptor.AddProperty("_widgetEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}"));
      controlDescriptor.AddProperty("_editTemplateViewName", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName);
      controlDescriptor.AddProperty("_createTemplateViewName", (object) ControlTemplatesDefinitions.BackendInsertDetailsViewName);
      controlDescriptor.AddProperty("_defaultLayoutTemplateName", (object) "Telerik.Sitefinity.Resources.Templates.PublicControls.Breadcrumb.ascx");
      controlDescriptor.AddElementProperty("editTemplateLink", this.EditTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createTemplateLink", this.CreateTemplateLink.ClientID);
      controlDescriptor.AddProperty("_embeddedTemplateMap", (object) this.embeddedTemplateMap);
      controlDescriptor.AddComponentProperty("viewsList", this.MasterViewNameList.ClientID);
      controlDescriptor.AddProperty("_modifyWidgetTemplatePermission", (object) this.ModifyWidgetTemplatePermission);
      controlDescriptor.AddElementProperty("templateTitleElement", this.TemplateTitle.ClientID);
      controlDescriptor.AddElementProperty("selectorTag", this.SelectorTag.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) source.ToArray();
    }

    /// <summary>Gets script references for the Breadcrumb's designer.</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      if (!(base.GetScriptReferences() is List<ScriptReference> scriptReferences))
        return base.GetScriptReferences();
      string assembly = typeof (BreadcrumbDesigner).Assembly.ToString();
      scriptReferences.Add(new ScriptReference(this.scriptReference, assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources"));
      return (IEnumerable<ScriptReference>) scriptReferences.ToArray();
    }
  }
}
