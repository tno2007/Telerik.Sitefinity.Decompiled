// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowScopeSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.Workflow.UI
{
  /// <summary>A dialog used for selecting the scope of a workflow</summary>
  public class WorkflowScopeSelectorDialog : AjaxDialogBase
  {
    internal const string dialogScript = "Telerik.Sitefinity.Workflow.Scripts.WorkflowScopeSelectorDialog.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.WorkflowScopeSelector.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.UI.WorkflowScopeSelectorDialog" /> class.
    /// </summary>
    public WorkflowScopeSelectorDialog() => this.LayoutTemplatePath = WorkflowScopeSelectorDialog.layoutTemplatePath;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (WorkflowScopeSelectorDialog).FullName;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the text field holding the title of the workflow scope.
    /// </summary>
    protected virtual TextField WorkflowScopeTitle => this.Container.GetControl<TextField>("workflowScopeTitle", true);

    /// <summary>Gets the dropdown listing all available languages</summary>
    protected virtual DropDownList LanguageList => this.Container.GetControl<DropDownList>("languageList", true);

    /// <summary>
    /// Gets the dropdown listing all available content types (modules supporting workflow).
    /// </summary>
    protected virtual DropDownList ContentType => this.Container.GetControl<DropDownList>("contentType", true);

    /// <summary>
    /// Gets the button for saving the selected scope, and closing the dialog.
    /// </summary>
    protected virtual LinkButton DoneLink => this.Container.GetControl<LinkButton>("lnkDone", true);

    /// <summary>
    /// Gets the button for cancelling and closing the dialog.
    /// </summary>
    protected virtual LinkButton CancelLink => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>
    /// Gets the reference to the control wrapping content workflow scope view.
    /// </summary>
    protected virtual HtmlGenericControl ContentViewWrapper => this.Container.GetControl<HtmlGenericControl>("contentViewWrapper", true);

    /// <summary>
    /// Gets the reference to the control wrapping pages workflow scope view.
    /// </summary>
    protected virtual HtmlGenericControl PagesViewWrapper => this.Container.GetControl<HtmlGenericControl>("pagesViewWrapper", true);

    /// <summary>
    /// Gets the reference to the content workflow scope view.
    /// </summary>
    protected virtual ContentWorkflowScopeView ContentView => this.Container.GetControl<ContentWorkflowScopeView>("contentWorkflowScopeView", true);

    /// <summary>Gets the reference to the pages workflow scope view.</summary>
    protected virtual PagesWorkflowScopeView PagesView => this.Container.GetControl<PagesWorkflowScopeView>("pagesView", true);

    /// <summary>
    /// Gets the reference to the control wrapping pages workflow scope view.
    /// </summary>
    protected virtual HtmlGenericControl ContentTypeStepHeader => this.Container.GetControl<HtmlGenericControl>("contentTypeStepHeader", true);

    /// <summary>
    /// Gets the reference to the control wrapping pages workflow scope view.
    /// </summary>
    protected virtual HtmlGenericControl LanguageStepHeader => this.Container.GetControl<HtmlGenericControl>("langaugeStepHeader", true);

    /// <summary>
    /// Gets the reference to the control wrapping pages workflow scope view.
    /// </summary>
    protected virtual HtmlGenericControl LanguageStepWrapper => this.Container.GetControl<HtmlGenericControl>("languageStepWrapper", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.BindContentTypeList();
      this.BindLanguageList();
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        return;
      this.PagesView.PreRender += new EventHandler(this.PagesView_PreRender);
      this.ContentView.PreRender += new EventHandler(this.ContentView_PreRender);
    }

    private void ContentView_PreRender(object sender, EventArgs e) => this.ContentView.Header.Attributes.Add("class", "sfStep2");

    private void PagesView_PreRender(object sender, EventArgs e) => this.PagesView.Header.Attributes.Add("class", "sfStep2");

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("doneSelectingLink", this.DoneLink.ClientID);
      controlDescriptor.AddElementProperty("cancelSelectingLink", this.CancelLink.ClientID);
      controlDescriptor.AddElementProperty("languageList", this.LanguageList.ClientID);
      controlDescriptor.AddElementProperty("contentTypeList", this.ContentType.ClientID);
      controlDescriptor.AddElementProperty("contentViewWrapper", this.ContentViewWrapper.ClientID);
      controlDescriptor.AddElementProperty("pagesViewWrapper", this.PagesViewWrapper.ClientID);
      controlDescriptor.AddComponentProperty("workflowScopeTitleField", this.WorkflowScopeTitle.ClientID);
      controlDescriptor.AddComponentProperty("contentView", this.ContentView.ClientID);
      controlDescriptor.AddComponentProperty("pagesView", this.PagesView.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Workflow.Scripts.WorkflowScopeSelectorDialog.js", typeof (WorkflowScopeSelectorDialog).Assembly.FullName)
    };

    private void BindContentTypeList()
    {
      WorkflowConfig workflowConfig = Config.Get<WorkflowConfig>();
      this.ContentType.Items.Clear();
      this.ContentType.Items.Add(new ListItem(Res.Get<WorkflowResources>().ALL_CONTENT, "ALL_CONTENT"));
      foreach (WorkflowElement workflowElement in (IEnumerable<WorkflowElement>) workflowConfig.Workflows.Values)
      {
        string title = workflowElement.Title;
        if (!string.IsNullOrEmpty(workflowElement.ResourceClassId))
          title = Res.Get(workflowElement.ResourceClassId, title);
        this.ContentType.Items.Add(new ListItem(title, workflowElement.ContentType));
      }
    }

    private void BindLanguageList()
    {
      Dictionary<string, string> availableLanguages = new Dictionary<string, string>();
      availableLanguages.Add(Res.Get<Labels>().AllLanguages, "");
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (appSettings.Multilingual)
        ((IEnumerable<CultureInfo>) appSettings.DefinedFrontendLanguages).ToList<CultureInfo>().ForEach((Action<CultureInfo>) (l => availableLanguages.Add(l.EnglishName, l.Name)));
      this.LanguageList.DataSource = (object) availableLanguages;
      this.LanguageList.DataTextField = "Key";
      this.LanguageList.DataValueField = "Value";
      this.LanguageList.DataBind();
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        return;
      this.LanguageStepWrapper.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
      this.ContentTypeStepHeader.Attributes.Add("class", "sfStep1");
    }
  }
}
