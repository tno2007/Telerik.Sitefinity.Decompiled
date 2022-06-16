// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestDetailView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards
{
  /// <summary>Dialog for creating, editing and sending A/B tests.</summary>
  public class AbTestDetailView : AjaxDialogBase
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.AbTestDetailView.js";
    private const string kendoScriptRef = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.AbTestDetailView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? AbTestDetailView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; set; }

    /// <summary>Gets a reference to the middle step.</summary>
    protected virtual AbTestMiddleStep MiddleStep => this.Container.GetControl<AbTestMiddleStep>("middleStep", true);

    /// <summary>Gets a reference the issue properties view.</summary>
    protected virtual AbTestIssuePropertiesView IssuePropertiesView => this.Container.GetControl<AbTestIssuePropertiesView>("issuePropertiesView", true);

    /// <summary>Gets the message view.</summary>
    protected virtual AbTestMessageView MessageView => this.Container.GetControl<AbTestMessageView>("messageView", true);

    /// <summary>Gets a reference the ab test send view.</summary>
    protected virtual AbTestSendView SendView => this.Container.GetControl<AbTestSendView>("sendView", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string str = SystemManager.CurrentHttpContext.Request.Params["providerName"];
      if (str == null)
        return;
      this.ProviderName = str;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("abTestMiddleStep", this.MiddleStep.ClientID);
      controlDescriptor.AddComponentProperty("issuePropertiesView", this.IssuePropertiesView.ClientID);
      controlDescriptor.AddComponentProperty("messageView", this.MessageView.ClientID);
      controlDescriptor.AddComponentProperty("sendView", this.SendView.ClientID);
      controlDescriptor.AddProperty("_rootUrl", (object) this.Page.ResolveUrl("~/"));
      controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Scripts.AbTestDetailView.js", typeof (AbTestDetailView).Assembly.FullName)
    };
  }
}
