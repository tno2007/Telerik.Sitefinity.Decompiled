// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.PageBehaviourControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>
  /// Represents the UI related to the behavior when accessing a page of the offline site.
  /// </summary>
  public class PageBehaviourControl : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.PageBehaviourControl.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.PageBehaviourControl.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.PageBehaviourControl" /> class.
    /// </summary>
    public PageBehaviourControl() => this.LayoutTemplatePath = PageBehaviourControl.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (PageBehaviourControl).FullName;

    /// <summary>Gets a reference to the show message radio button.</summary>
    protected virtual HtmlInputRadioButton ShowMessageRadio => this.Container.GetControl<HtmlInputRadioButton>("showMessageRadio", true);

    /// <summary>Gets a reference to the message text field.</summary>
    protected virtual TextField MessageField => this.Container.GetControl<TextField>("messageField", true);

    /// <summary>
    /// Gets a reference to the redirect to page radio button.
    /// </summary>
    protected virtual HtmlInputRadioButton RedirectRadio => this.Container.GetControl<HtmlInputRadioButton>("redirectRadio", true);

    /// <summary>Gets a reference to the page selector field.</summary>
    protected virtual PageField PageField => this.Container.GetControl<PageField>("pageField", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddElementProperty("showMessageRadio", this.ShowMessageRadio.ClientID);
      controlDescriptor.AddComponentProperty("messageField", this.MessageField.ClientID);
      controlDescriptor.AddElementProperty("redirectRadio", this.RedirectRadio.ClientID);
      controlDescriptor.AddComponentProperty("pageField", this.PageField.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Multisite.Web.UI.Scripts.PageBehaviourControl.js", typeof (PageBehaviourControl).Assembly.FullName)
    };
  }
}
