// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestAnalysis
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>
  /// Represents the control for displaying and editing Testing Note and Conclusion of an A/B test.
  /// </summary>
  public class AbTestAnalysis : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.AbTestAnalysis.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.AbTestAnalysis.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.AbTestAnalysis" /> class.
    /// </summary>
    public AbTestAnalysis() => this.LayoutTemplatePath = AbTestAnalysis.layoutTemplatePath;

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the ab test.</summary>
    public ABCampaign AbTest { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the what was tested read panel.</summary>
    protected virtual HtmlContainerControl WhatWasTestedReadPanel => this.Container.GetControl<HtmlContainerControl>("whatWasTestedReadPanel", true);

    /// <summary>Gets the what was tested label.</summary>
    protected virtual Label WhatWasTestedLabel => this.Container.GetControl<Label>("whatWasTestedLabel", true);

    /// <summary>Gets the what was tested edit button.</summary>
    protected virtual LinkButton WhatWasTestedEditButton => this.Container.GetControl<LinkButton>("whatWasTestedEditButton", true);

    /// <summary>Gets the what was tested write panel.</summary>
    protected virtual HtmlContainerControl WhatWasTestedWritePanel => this.Container.GetControl<HtmlContainerControl>("whatWasTestedWritePanel", true);

    /// <summary>Gets the what was tested text box.</summary>
    protected virtual TextBox WhatWasTestedTextBox => this.Container.GetControl<TextBox>("whatWasTestedTextBox", true);

    /// <summary>Gets the what was tested save button.</summary>
    protected virtual LinkButton WhatWasTestedSaveButton => this.Container.GetControl<LinkButton>("whatWasTestedSaveButton", true);

    /// <summary>Gets the conclusion read panel.</summary>
    protected virtual HtmlContainerControl ConclusionReadPanel => this.Container.GetControl<HtmlContainerControl>("conclusionReadPanel", true);

    /// <summary>Gets the conclusion label.</summary>
    protected virtual Label ConclusionLabel => this.Container.GetControl<Label>("conclusionLabel", true);

    /// <summary>Gets the conclusion edit button.</summary>
    protected virtual LinkButton ConclusionEditButton => this.Container.GetControl<LinkButton>("conclusionEditButton", true);

    /// <summary>Gets the conclusion write panel.</summary>
    protected virtual HtmlContainerControl ConclusionWritePanel => this.Container.GetControl<HtmlContainerControl>("conclusionWritePanel", true);

    /// <summary>Gets the conclusion text box.</summary>
    protected virtual TextBox ConclusionTextBox => this.Container.GetControl<TextBox>("conclusionTextBox", true);

    /// <summary>Gets the conclusion save button.</summary>
    protected virtual LinkButton ConclusionSaveButton => this.Container.GetControl<LinkButton>("conclusionSaveButton", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.WhatWasTestedLabel.Text = this.AbTest.TestingNote;
      this.ConclusionLabel.Text = this.AbTest.Conclusion;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("_rootUrl", (object) this.Page.ResolveUrl("~/"));
      controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      controlDescriptor.AddProperty("_abTestId", (object) this.AbTest.Id.ToString());
      controlDescriptor.AddElementProperty("whatWasTestedReadPanel", this.WhatWasTestedReadPanel.ClientID);
      controlDescriptor.AddElementProperty("whatWasTestedWritePanel", this.WhatWasTestedWritePanel.ClientID);
      controlDescriptor.AddElementProperty("whatWasTestedLabel", this.WhatWasTestedLabel.ClientID);
      controlDescriptor.AddElementProperty("whatWasTestedTextBox", this.WhatWasTestedTextBox.ClientID);
      controlDescriptor.AddElementProperty("whatWasTestedEditButton", this.WhatWasTestedEditButton.ClientID);
      controlDescriptor.AddElementProperty("whatWasTestedSaveButton", this.WhatWasTestedSaveButton.ClientID);
      controlDescriptor.AddElementProperty("conclusionReadPanel", this.ConclusionReadPanel.ClientID);
      controlDescriptor.AddElementProperty("conclusionWritePanel", this.ConclusionWritePanel.ClientID);
      controlDescriptor.AddElementProperty("conclusionLabel", this.ConclusionLabel.ClientID);
      controlDescriptor.AddElementProperty("conclusionTextBox", this.ConclusionTextBox.ClientID);
      controlDescriptor.AddElementProperty("conclusionEditButton", this.ConclusionEditButton.ClientID);
      controlDescriptor.AddElementProperty("conclusionSaveButton", this.ConclusionSaveButton.ClientID);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = typeof (AbTestAnalysis).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.Scripts.AbTestAnalysis.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.NewslettersClientManager.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
