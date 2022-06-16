// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.PlainTextEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  internal class PlainTextEditor : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.PlainTextEditor.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.PlainTextEditor.ascx");

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath => PlainTextEditor.layoutTemplatePath;

    /// <summary>Gets a reference to the automatic plain text radio.</summary>
    protected virtual RadioButton AutomaticPlainTextRadio => this.Container.GetControl<RadioButton>("automaticPlainTextRadio", true);

    /// <summary>Gets a reference the manual plain text radio.</summary>
    protected virtual RadioButton ManualPlainTextRadio => this.Container.GetControl<RadioButton>("manualPlainTextRadio", true);

    /// <summary>Gets a reference to the plain text version panel.</summary>
    protected virtual HtmlContainerControl PlainTextVersionPanel => this.Container.GetControl<HtmlContainerControl>("plainTextVersionPanel", true);

    /// <summary>Gets a reference to the plain text version text area.</summary>
    protected virtual TextBox PlainTextVersion => this.Container.GetControl<TextBox>("plainTextVersion", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddElementProperty("automaticPlainTextRadio", this.AutomaticPlainTextRadio.ClientID);
      controlDescriptor.AddElementProperty("manualPlainTextRadio", this.ManualPlainTextRadio.ClientID);
      controlDescriptor.AddElementProperty("plainTextVersionPanel", this.PlainTextVersionPanel.ClientID);
      controlDescriptor.AddElementProperty("plainTextVersion", this.PlainTextVersion.ClientID);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.PlainTextEditor.js", typeof (PlainTextEditor).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;
  }
}
