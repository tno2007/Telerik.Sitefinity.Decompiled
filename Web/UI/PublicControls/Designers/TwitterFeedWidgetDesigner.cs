// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedWidgetDesigner
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
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.PublicControls.Designers
{
  /// <summary>Twitter Feed Widget designer class</summary>
  public class TwitterFeedWidgetDesigner : ControlDesignerBase
  {
    /// <summary>Control layout template path</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Social.TwitterFeedWidgetDesigner.ascx");
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    private const string serviceUrl = "~/Sitefinity/Services/Publishing/PublishingService.svc/indboundpipes/";
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";

    /// <summary>Initialize controls and set default settings</summary>
    /// <param name="container"></param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.DesignerMode = ControlDesignerModes.Simple;
      this.PropertyEditor.HideAdvancedMode = false;
      this.PipeSelectorBinder.ServiceUrl = "~/Sitefinity/Services/Publishing/PublishingService.svc/indboundpipes/";
      this.BindTemplates();
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TwitterFeedWidgetDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Get script Descriptors</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("pipeSelector", this.PipeSelector.ClientID);
      controlDescriptor.AddComponentProperty("pipeSelectorBinder", this.PipeSelectorBinder.ClientID);
      controlDescriptor.AddComponentProperty("MaxItems", this.MaxItems.ClientID);
      controlDescriptor.AddProperty("_publishingPointProvider", (object) "OAPublishingProvider");
      controlDescriptor.AddProperty("_", (object) "OAPublishingProvider");
      controlDescriptor.AddProperty("_pipeName", (object) "TwitterInboundPipe");
      controlDescriptor.AddComponentProperty("templateSelector", this.TemplateSelector.ClientID);
      controlDescriptor.AddElementProperty("editTemplateLink", this.EditTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createTemplateLink", this.CreateTemplateLink.ClientID);
      controlDescriptor.AddProperty("_widgetEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}"));
      controlDescriptor.AddComponentProperty("widgetEditorDialog", this.WidgetEditorDialog.ClientID);
      controlDescriptor.AddProperty("_editTemplateViewName", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName);
      controlDescriptor.AddProperty("_createTemplateViewName", (object) ControlTemplatesDefinitions.BackendInsertDetailsViewName);
      controlDescriptor.AddProperty("_modifyWidgetTemplatePermission", (object) this.ModifyWidgetTemplatePermission);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    private void BindTemplates()
    {
      PageManager manager = PageManager.GetManager();
      string str = string.Format("ControlType == \"{0}\"", (object) typeof (TwitterFeedWidget).FullName);
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      IQueryable<ControlPresentation> query = (multisiteContext == null ? manager.GetPresentationItems<ControlPresentation>() : manager.GetPresentationItems<ControlPresentation>(multisiteContext.CurrentSite.Id)).Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE"));
      int? nullable = new int?();
      string filterExpression = str;
      string empty = string.Empty;
      int? skip = new int?(0);
      int? take = new int?(0);
      ref int? local = ref nullable;
      IQueryable<ControlPresentation> source = DataProviderBase.SetExpressions<ControlPresentation>(query, filterExpression, empty, skip, take, ref local);
      Expression<Func<ControlPresentation, string>> keySelector = (Expression<Func<ControlPresentation, string>>) (t => t.Name);
      foreach (ControlPresentation controlPresentation in (IEnumerable<ControlPresentation>) source.OrderBy<ControlPresentation, string>(keySelector))
        this.TemplateSelector.Choices.Add(new ChoiceItem()
        {
          Text = controlPresentation.Name,
          Value = controlPresentation.Id.ToString()
        });
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.TwitterFeedWidgetDesigner.js", this.GetType().Assembly.GetName().ToString())
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected virtual TextField MaxItems => this.Container.GetControl<TextField>("maxItems", true);

    /// <summary>Gets the widget editor dialog.</summary>
    protected virtual RadWindow WidgetEditorDialog => this.Container.GetControl<RadWindow>("widgetEditorDialog", true);

    /// <summary>Pipe selector</summary>
    protected virtual RadComboBox PipeSelector => this.Container.GetControl<RadComboBox>("twitterFeedSelector", true);

    /// <summary>Pipe selector binder</summary>
    protected virtual RadComboBinder PipeSelectorBinder => this.Container.GetControl<RadComboBinder>("twitterFeedSelectorBinder", true);

    /// <summary>
    /// Gets the choice field responsible for displaying templates for the widget.
    /// </summary>
    protected virtual ChoiceField TemplateSelector => this.Container.GetControl<ChoiceField>("templateSelector", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected control template.
    /// </summary>
    protected virtual HyperLink EditTemplateLink => this.Container.GetControl<HyperLink>("editTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected control template.
    /// </summary>
    protected virtual HyperLink CreateTemplateLink => this.Container.GetControl<HyperLink>("createTemplateLink", true);

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");
  }
}
