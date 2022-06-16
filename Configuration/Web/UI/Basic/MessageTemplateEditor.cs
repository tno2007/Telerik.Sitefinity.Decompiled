// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.MessageTemplateEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>A control for editing email message templates</summary>
  public class MessageTemplateEditor : SimpleScriptView
  {
    internal const string JavaScriptReference = "Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.MessageTemplateEditor.js";
    private static readonly string LayoutTemplateVirtualPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.MessageTemplateEditor.ascx");

    /// <summary>Gets the window body.</summary>
    protected HtmlGenericControl WindowBody => this.Container.GetControl<HtmlGenericControl>("windowBody", true);

    /// <summary>Gets the subject textbox.</summary>
    protected TextField SubjectTextBox => this.Container.GetControl<TextField>("subjectTextBox", true);

    /// <summary>Gets the sender textbox.</summary>
    protected TextField SenderTextBox => this.Container.GetControl<TextField>("senderTextBox", true);

    /// <summary>Gets the sender textbox.</summary>
    protected TextField SenderNameTextBox => this.Container.GetControl<TextField>("senderNameTextBox", true);

    /// <summary>Gets the html editor.</summary>
    protected virtual HtmlField TemplateEditor => this.Container.GetControl<HtmlField>("templateEditor", true);

    /// <summary>Gets insert dropdown list.</summary>
    protected virtual DropDownList DropDownListInserts => this.Container.GetControl<DropDownList>("dropDownListInserts", true);

    /// <summary>Gets the top command bar.</summary>
    private CommandBar TopCommandBar => this.Container.GetControl<CommandBar>("topCommandBar", true);

    /// <summary>Gets the bottom command bar.</summary>
    private CommandBar BottomCommandBar => this.Container.GetControl<CommandBar>("bottomCommandBar", true);

    /// <summary>Gets the insert dynamic data button.</summary>
    protected virtual LinkButton InsertDynamicDataButton => this.Container.GetControl<LinkButton>("insertDynamicDataButton", true);

    /// <summary>Gets the back button.</summary>
    protected virtual LinkButton BackButton => this.Container.GetControl<LinkButton>("backButton", true);

    /// <summary>Gets the title label.</summary>
    protected virtual SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("titleLabel", true);

    /// <summary>Gets the used by label.</summary>
    protected virtual SitefinityLabel UsedInLabel => this.Container.GetControl<SitefinityLabel>("usedInLabel", true);

    /// <summary>Gets the last modified label.</summary>
    protected virtual SitefinityLabel LastModifiedLabel => this.Container.GetControl<SitefinityLabel>("lastModifiedLabel", true);

    /// <summary>Gets the last modified by label.</summary>
    protected virtual SitefinityLabel LastModifiedByLabel => this.Container.GetControl<SitefinityLabel>("lastModifiedByLabel", true);

    /// <summary>Gets the restore to original prompt dialog.</summary>
    protected virtual PromptDialog RestoreToOriginalPromptDialog => this.Container.GetControl<PromptDialog>("restoreToOriginalPromptDialog", false);

    /// <summary>Gets the send test email dialog.</summary>
    protected virtual EmailSettingsSendTestEmailDialog SendTestEmailDialog => this.Container.GetControl<EmailSettingsSendTestEmailDialog>("sendTestEmailDialog", false);

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MessageTemplateEditor.LayoutTemplateVirtualPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (MessageTemplateEditor).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("windowBody", this.WindowBody.ClientID);
      controlDescriptor.AddComponentProperty("subjectTextBox", this.SubjectTextBox.ClientID);
      controlDescriptor.AddComponentProperty("senderTextBox", this.SenderTextBox.ClientID);
      controlDescriptor.AddComponentProperty("senderNameTextBox", this.SenderNameTextBox.ClientID);
      controlDescriptor.AddComponentProperty("templateEditor", this.TemplateEditor.ClientID);
      controlDescriptor.AddElementProperty("dropDownListInserts", this.DropDownListInserts.ClientID);
      controlDescriptor.AddElementProperty("insertDynamicDataButton", this.InsertDynamicDataButton.ClientID);
      controlDescriptor.AddElementProperty("backButton", this.BackButton.ClientID);
      controlDescriptor.AddElementProperty("titleLabel", this.TitleLabel.ClientID);
      controlDescriptor.AddElementProperty("usedInLabel", this.UsedInLabel.ClientID);
      controlDescriptor.AddElementProperty("lastModifiedLabel", this.LastModifiedLabel.ClientID);
      controlDescriptor.AddElementProperty("lastModifiedByLabel", this.LastModifiedByLabel.ClientID);
      controlDescriptor.AddComponentProperty("topCommandBar", this.TopCommandBar.ClientID);
      controlDescriptor.AddComponentProperty("bottomCommandBar", this.BottomCommandBar.ClientID);
      controlDescriptor.AddComponentProperty("restoreToOriginalPromptDialog", this.RestoreToOriginalPromptDialog.ClientID);
      controlDescriptor.AddComponentProperty("sendTestEmailDialog", this.SendTestEmailDialog.ClientID);
      controlDescriptor.AddProperty("serviceUrl", (object) this.ResolveClientUrl("~/Sitefinity/Services/SystemEmails/Settings.svc"));
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.MessageTemplateEditor.js", typeof (MessageTemplateEditor).Assembly.FullName)
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The view container.</param>
    protected override void InitializeControls(GenericContainer container) => this.TemplateEditor.EditorContentFilters = new EditorFilters?(EditorFilters.RemoveScripts | EditorFilters.FixUlBoldItalic | EditorFilters.IECleanAnchors | EditorFilters.MozEmStrong | EditorFilters.ConvertFontToSpan | EditorFilters.ConvertToXhtml | EditorFilters.IndentHTMLContent | EditorFilters.EncodeScripts | EditorFilters.OptimizeSpans | EditorFilters.ConvertTags | EditorFilters.StripCssExpressions | EditorFilters.RemoveExtraBreaks);
  }
}
