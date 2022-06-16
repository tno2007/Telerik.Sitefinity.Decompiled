// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.Designers.SiteSelectorControlDesigner
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
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Multisite.Web.UI.Designers
{
  /// <summary>
  /// Represents a designer for the <typeparamref name="SitefinityWebApp.Widgets.SiteSelector.Widgets.SiteSelector" /> widget
  /// </summary>
  public class SiteSelectorControlDesigner : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Multisite.SiteSelectorControlDesigner.ascx");
    private const string designerScriptName = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.SiteSelectorControlDesigner.js";
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";
    private IDictionary<string, string> embeddedTemplateMap;

    /// <summary>Gets the layout template path</summary>
    public override string LayoutTemplatePath => SiteSelectorControlDesigner.layoutTemplatePath;

    /// <summary>Gets the layout template name</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>
    /// Get the tag key of the SiteSelectorControl's designer.
    /// </summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");

    /// <summary>Gets a reference to the views choice field.</summary>
    protected virtual ChoiceField SiteSelectorViewChoiceField => this.Container.GetControl<ChoiceField>("siteSelectorViewChoiceField", true);

    /// <summary>Gets the URL type choice field.</summary>
    protected virtual ChoiceField UrlTypeChoiceField => this.Container.GetControl<ChoiceField>("urlTypeChoiceField", true);

    /// <summary>Gets the site selector label text box</summary>
    protected virtual TextBox SiteSelectorLabelTextBox => this.Container.GetControl<TextBox>("siteSelectorLabelTextBox", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected control template
    /// </summary>
    protected virtual HyperLink EditTemplateLink => this.Container.GetControl<HyperLink>("editTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for creating a new control template
    /// </summary>
    protected virtual HyperLink CreateTemplateLink => this.Container.GetControl<HyperLink>("createNewTemplateLink", true);

    /// <summary>
    /// Gets the choice field listing all possible layout templates
    /// </summary>
    protected virtual ChoiceField MasterViewNameList => this.Container.GetControl<ChoiceField>("masterViewName", true);

    /// <summary>
    /// Gets the Label control representing the title of the selected control template.
    /// </summary>
    protected virtual Label TemplateTitle => this.Container.GetControl<Label>("templateTitle", true);

    /// <summary>Gets the correct instance of RadWindowManager</summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    protected override void InitializeControls(GenericContainer container)
    {
      this.DesignerMode = ControlDesignerModes.Simple;
      this.AdvancedModeIsDefault = false;
      PageManager manager = PageManager.GetManager();
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      IQueryable<ControlPresentation> queryable = (multisiteContext == null ? manager.GetPresentationItems<ControlPresentation>() : manager.GetPresentationItems<ControlPresentation>(multisiteContext.CurrentSite.Id)).Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE" && tmpl.ControlType == typeof (SiteSelectorControl).FullName));
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
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
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
      controlDescriptor.AddProperty("_defaultLayoutTemplateName", (object) "Telerik.Sitefinity.Resources.Templates.Frontend.Multisite.SiteSelectorControl.ascx");
      controlDescriptor.AddElementProperty("editTemplateLink", this.EditTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createTemplateLink", this.CreateTemplateLink.ClientID);
      controlDescriptor.AddProperty("_embeddedTemplateMap", (object) this.embeddedTemplateMap);
      controlDescriptor.AddComponentProperty("viewsList", this.MasterViewNameList.ClientID);
      controlDescriptor.AddElementProperty("templateTitleElement", this.TemplateTitle.ClientID);
      controlDescriptor.AddProperty("_modifyWidgetTemplatePermission", (object) this.ModifyWidgetTemplatePermission);
      controlDescriptor.AddComponentProperty("siteSelectorViewChoiceField", this.SiteSelectorViewChoiceField.ClientID);
      controlDescriptor.AddElementProperty("siteSelectorLabelTextBoxId", this.SiteSelectorLabelTextBox.ClientID);
      controlDescriptor.AddComponentProperty("urlTypeChoiceField", this.UrlTypeChoiceField.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of ScriptReference objects that define script resources that the control requires.
    /// </summary>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Multisite.Web.UI.Scripts.SiteSelectorControlDesigner.js", typeof (SiteSelectorControlDesigner).Assembly.FullName)
    };
  }
}
