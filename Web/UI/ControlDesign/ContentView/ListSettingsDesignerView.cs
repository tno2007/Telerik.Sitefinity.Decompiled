// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ListSettingsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// represents the list view designer
  /// TODO: register script resources
  /// </summary>
  public class ListSettingsDesignerView : ContentViewDesignerView
  {
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";
    private IDictionary<string, string> embeddedTemplateMap;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.ListSettingsDesignerView.ascx");

    /// <summary>Gets or sets the paging text.</summary>
    /// <value>The paging text.</value>
    public string PagingText
    {
      get => this.PagingLabel.Text;
      set => this.PagingLabel.Text = value;
    }

    /// <summary>Gets or sets the divide pages text.</summary>
    /// <value>The divide pages text.</value>
    public string DividePagesText
    {
      get => this.DividePagesLabel.Text;
      set => this.DividePagesLabel.Text = value;
    }

    /// <summary>Gets or sets the total limit text.</summary>
    /// <value>The total limit text.</value>
    public string TotalLimitText
    {
      get => this.TotalLimitLabel.Text;
      set => this.TotalLimitLabel.Text = value;
    }

    /// <summary>Gets or sets the list limit text.</summary>
    /// <value>The list limit text.</value>
    public string ListLimitText
    {
      get => this.ListLimitLabel.Text;
      set => this.ListLimitLabel.Text = value;
    }

    /// <summary>Gets or sets the sort items text.</summary>
    /// <value>The sort items text.</value>
    public string SortItemsText { get; set; }

    /// <summary>Gets or sets the master view name text.</summary>
    /// <value>The master view name text.</value>
    public string MasterViewNameText
    {
      get => this.MasterViewNameLabel.Text;
      set => this.MasterViewNameLabel.Text = value;
    }

    /// <summary>Gets or sets the create new template link text.</summary>
    /// <value>The create new template link text.</value>
    public string CreateNewTemplateLinkText
    {
      get => this.CreateNewTemplateLink.Text;
      set => this.CreateNewTemplateLink.Text = value;
    }

    public ChoiceField SortExpressionControl => this.Container.GetControl<ChoiceField>("sortExpression", true);

    /// <summary>
    /// The type name of the view used to display the designed control in master mode.
    /// </summary>
    public string DesignedMasterViewType { get; set; }

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");

    protected Label PagingLabel => this.Container.GetControl<Label>("pagingLabel", true);

    protected Label DividePagesLabel => this.Container.GetControl<Label>("dividePagesLabel", true);

    protected Label TotalLimitLabel => this.Container.GetControl<Label>("totalLimitLabel", true);

    protected Label ListLimitLabel => this.Container.GetControl<Label>("listLimitLabel", true);

    protected Literal SortItemsLabel => this.Container.GetControl<Literal>("sortLabel", true);

    protected Literal MasterViewNameLabel => this.Container.GetControl<Literal>("masterViewNameLabel", true);

    protected HyperLink CreateNewTemplateLink => this.Container.GetControl<HyperLink>("createNewTemplateLink", true);

    public override string ViewName => "listViewSettings";

    public override string ViewTitle => "List Settings";

    protected TextField ListLimitControl => this.Container.GetControl<TextField>("listLimit", true);

    /// <summary>
    /// Gets the choice field listing all possible layout templates
    /// </summary>
    protected virtual ChoiceField MasterViewNameList => this.Container.GetControl<ChoiceField>("masterViewName", true);

    /// <summary>
    /// Gets the Label control representing the title of the selected control template
    /// </summary>
    protected virtual Label TemplateTitle => this.Container.GetControl<Label>("templateTitle", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected control template
    /// </summary>
    protected virtual HyperLink EditTemplateLink => this.Container.GetControl<HyperLink>("editTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for creating a new control template
    /// </summary>
    protected virtual HyperLink CreateTemplateLink => this.Container.GetControl<HyperLink>("createNewTemplateLink", true);

    /// <summary>Gets the correct instance of RadWindowManager</summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    protected virtual Panel ItemsPerPageCustomFieldsPanel => this.Container.GetControl<Panel>("itemsPerPageCustomFields", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SortItemsLabel.Text = this.SortItemsText;
      PageManager manager = PageManager.GetManager();
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      IQueryable<ControlPresentation> query = (multisiteContext == null ? manager.GetPresentationItems<ControlPresentation>() : manager.GetPresentationItems<ControlPresentation>(multisiteContext.CurrentSite.Id)).Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE"));
      int? nullable = new int?();
      string filterExpression = !(this.ParentDesigner is DynamicContentViewDesigner) ? string.Format("ControlType == \"{0}\"", (object) this.DesignedMasterViewType) : string.Format("ControlType == \"{0}\" AND Condition == \"{1}\"", (object) this.DesignedMasterViewType, (object) ((DynamicContentViewDesigner) this.ParentDesigner).DataItemTypeFullName);
      int? skip = new int?(0);
      int? take = new int?(0);
      ref int? local = ref nullable;
      IQueryable<ControlPresentation> queryable = DataProviderBase.SetExpressions<ControlPresentation>(query, filterExpression, "Name", skip, take, ref local);
      this.embeddedTemplateMap = (IDictionary<string, string>) new Dictionary<string, string>();
      foreach (ControlPresentation controlPresentation in (IEnumerable<ControlPresentation>) queryable)
      {
        this.MasterViewNameList.Choices.Add(new ChoiceItem()
        {
          Text = controlPresentation.Name,
          Value = controlPresentation.Id.ToString()
        });
        if (!string.IsNullOrEmpty(controlPresentation.EmbeddedTemplateName))
          this.embeddedTemplateMap.Add(controlPresentation.Id.ToString(), controlPresentation.EmbeddedTemplateName);
      }
      if (this.MasterViewNameList.Choices.Count >= 2)
        return;
      this.MasterViewNameList.CssClass += " sfDisplayNone";
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ListSettingsDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
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
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (FieldControl fieldControl in this.Container.GetControls<FieldControl>().Values)
      {
        if (!string.IsNullOrEmpty(fieldControl.DataFieldName))
          dictionary.Add(fieldControl.DataFieldName, fieldControl.ClientID);
      }
      controlDescriptor.AddProperty("dataFieldNameControlIdMap", (object) dictionary);
      controlDescriptor.AddElementProperty("templateTitleElement", this.TemplateTitle.ClientID);
      controlDescriptor.AddElementProperty("editTemplateLink", this.EditTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createTemplateLink", this.CreateTemplateLink.ClientID);
      controlDescriptor.AddProperty("_widgetEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}"));
      controlDescriptor.AddProperty("_editTemplateViewName", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName);
      controlDescriptor.AddProperty("_createTemplateViewName", (object) ControlTemplatesDefinitions.BackendInsertDetailsViewName);
      controlDescriptor.AddComponentProperty("radWindowManager", this.RadWindowManager.ClientID);
      controlDescriptor.AddComponentProperty("viewsList", this.MasterViewNameList.ClientID);
      controlDescriptor.AddProperty("_embeddedTemplateMap", (object) this.embeddedTemplateMap);
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
      string assembly = typeof (ListSettingsDesignerView).Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.ListSettingsDesignerView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
