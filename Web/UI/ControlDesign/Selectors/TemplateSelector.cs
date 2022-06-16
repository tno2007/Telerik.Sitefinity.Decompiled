// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.Selectors.TemplateSelector
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
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ControlDesign.Selectors
{
  /// <summary>
  /// represents the list view designer
  /// TODO: register script resources
  /// </summary>
  public class TemplateSelector : SimpleScriptView
  {
    private IDictionary<string, string> embeddedTemplateMap;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.Selectors.TemplateSelector.ascx");
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";
    internal const string templateSelectorScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.TemplateSelector.js";

    /// <summary>Gets or sets the master view name text.</summary>
    /// <value>The master view name text.</value>
    public string MasterViewNameText { get; set; }

    /// <summary>Gets or sets the create new template link text.</summary>
    /// <value>The create new template link text.</value>
    public string CreateNewTemplateLinkText
    {
      get => this.CreateNewTemplateLink.Text;
      set => this.CreateNewTemplateLink.Text = value;
    }

    /// <summary>
    /// The type name of the view used to display the designed control in master mode.
    /// </summary>
    public string DesignedControlType { get; set; }

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TemplateSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overriden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    protected override string ScriptDescriptorTypeName => typeof (TemplateSelector).FullName;

    /// <summary>
    /// Gets the choice field listing all possible layout templates
    /// </summary>
    protected virtual ChoiceField MasterViewNameList => this.Container.GetControl<ChoiceField>("masterViewName", true);

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

    /// <summary>Gets the master view name label.</summary>
    protected virtual Literal MasterViewNameLabel => this.Container.GetControl<Literal>("masterViewNameLabel", true);

    /// <summary>Gets the create new template link.</summary>
    protected virtual HyperLink CreateNewTemplateLink => this.Container.GetControl<HyperLink>("createNewTemplateLink", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.MasterViewNameLabel.Text = this.MasterViewNameText;
      PageManager manager = PageManager.GetManager();
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      IQueryable<ControlPresentation> query;
      if (multisiteContext != null)
        query = manager.GetPresentationItems<ControlPresentation>(multisiteContext.CurrentSite.Id).Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE"));
      else
        query = manager.GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE"));
      int? totalCount = new int?();
      string filterExpression = string.Format("ControlType == \"{0}\"", (object) this.DesignedControlType);
      IQueryable<ControlPresentation> queryable = DataProviderBase.SetExpressions<ControlPresentation>(query, filterExpression, "Name", new int?(0), new int?(0), ref totalCount);
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
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = typeof (TemplateSelector).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.TemplateSelector.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
