// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.CreateEditTemplateControl
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

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  public class CreateEditTemplateControl : SimpleScriptView
  {
    private IDictionary<string, string> embeddedTemplateMap;
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";
    internal const string controlScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.CreateEditTemplateControl.js";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.CreateEditTemplateControl.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CreateEditTemplateControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }

    /// <summary>
    /// The type name of the view used to display the designed control in master mode.
    /// </summary>
    public string DesignedMasterViewType { get; set; }

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");

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

    /// <summary>Gets or sets the window manager.</summary>
    /// <value>The window manager.</value>
    public virtual RadWindowManager WindowManager { get; set; }

    /// <summary>Gets the control title.</summary>
    /// <value>The control title.</value>
    protected virtual ITextControl ControlTitle => this.Container.GetControl<ITextControl>("controlTitle", false);

    protected override void InitializeControls(GenericContainer container)
    {
      PageManager manager = PageManager.GetManager();
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      IQueryable<ControlPresentation> query = (multisiteContext == null ? manager.GetPresentationItems<ControlPresentation>() : manager.GetPresentationItems<ControlPresentation>(multisiteContext.CurrentSite.Id)).Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE"));
      int? nullable = new int?();
      string filterExpression = string.Format("ControlType == \"{0}\"", (object) this.DesignedMasterViewType);
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
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.ControlTitle == null || string.IsNullOrEmpty(this.Title))
        return;
      this.ControlTitle.Text = this.Title;
    }

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
      controlDescriptor.AddElementProperty("templateTitleElement", this.TemplateTitle.ClientID);
      controlDescriptor.AddElementProperty("editTemplateLink", this.EditTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createTemplateLink", this.CreateTemplateLink.ClientID);
      controlDescriptor.AddProperty("_widgetEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}"));
      controlDescriptor.AddProperty("_editTemplateViewName", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName);
      controlDescriptor.AddProperty("_createTemplateViewName", (object) ControlTemplatesDefinitions.BackendInsertDetailsViewName);
      controlDescriptor.AddComponentProperty("radWindowManager", this.WindowManager.ClientID);
      controlDescriptor.AddComponentProperty("viewsList", this.MasterViewNameList.ClientID);
      controlDescriptor.AddProperty("_embeddedTemplateMap", (object) this.embeddedTemplateMap);
      controlDescriptor.AddProperty("_modifyWidgetTemplatePermission", (object) this.ModifyWidgetTemplatePermission);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.CreateEditTemplateControl.js", this.GetType().Assembly.GetName().ToString()));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
