// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.MoreOptionsExpandableSection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>View for selecting pages</summary>
  public class MoreOptionsExpandableSection : SimpleScriptView
  {
    private const string viewScript = "Telerik.Sitefinity.Web.Scripts.MoreOptionsExpandableSection.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.MoreOptionsExpandableSection.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.MoreOptionsExpandableSection" /> class.
    /// </summary>
    public MoreOptionsExpandableSection() => this.LayoutTemplatePath = MoreOptionsExpandableSection.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets a value indicating whether openInNewWindow checkbox will be displayed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if openInNewWindow checkbox is displayed; otherwise, <c>false</c>.
    /// </value>
    public bool ShowOpenInNewWindow { get; set; }

    /// <summary>
    /// Gets the reference to the field displaying link tooltip.
    /// </summary>
    protected virtual TextField LinkTooltip => this.Container.GetControl<TextField>("linkTooltip", true);

    /// <summary>
    /// Gets the reference to the field displaying link CSS class.
    /// </summary>
    protected virtual TextField LinkClass => this.Container.GetControl<TextField>("linkClass", true);

    /// <summary>Gets the reference to openInNewWindow field.</summary>
    protected virtual ChoiceField OpenInNewWindow => this.Container.GetControl<ChoiceField>("openInNewWindow", true);

    /// <summary>Represents the container for more options elements</summary>
    protected virtual HtmlGenericControl MoreOptionsSection => this.Container.GetControl<HtmlGenericControl>("moreOptionsSection", true);

    /// <summary>
    /// Represents the button that expands/collapses the more options group
    /// </summary>
    protected virtual HtmlAnchor MoreOptionsExpander => this.Container.GetControl<HtmlAnchor>("moreOptionsExpander", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.ShowOpenInNewWindow)
        return;
      this.OpenInNewWindow.Visible = false;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (MoreOptionsExpandableSection).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("linkTooltip", this.LinkTooltip.ClientID);
      controlDescriptor.AddComponentProperty("linkClass", this.LinkClass.ClientID);
      controlDescriptor.AddComponentProperty("openInNewWindow", this.OpenInNewWindow.ClientID);
      controlDescriptor.AddElementProperty("moreOptionsSection", this.MoreOptionsSection.ClientID);
      controlDescriptor.AddElementProperty("moreOptionsExpander", this.MoreOptionsExpander.ClientID);
      controlDescriptor.AddProperty("_showOpenInNewWindow", (object) this.ShowOpenInNewWindow);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.MoreOptionsExpandableSection.js", typeof (MoreOptionsExpandableSection).Assembly.FullName)
    };
  }
}
