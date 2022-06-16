// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SitefinityToolTip
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Control which shows tooltip.</summary>
  public class SitefinityToolTip : SimpleScriptView
  {
    private readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.SitefinityToolTip.ascx");
    private readonly string script = "Telerik.Sitefinity.Web.UI.Scripts.SitefinityToolTip.js";
    private readonly string defaultTargetText = "(?)";

    /// <summary>Gets or sets the title of the tooltip.</summary>
    public string Title { get; set; }

    /// <summary>Gets or sets the Text of the tooltip link.</summary>
    public string Text { get; set; }

    /// <summary>Gets or sets the Content of the tooltip.</summary>
    public string Content { get; set; }

    /// <summary>Gets or sets the Css class of the tooltip.</summary>
    public new string CssClass { get; set; }

    /// <summary>Gets or sets the Css class of the target.</summary>
    public string TargetCssClass { get; set; }

    /// <summary>Gets a reference to the ToolTip target control.</summary>
    public virtual LinkButton ToolTipTarget => this.Container.GetControl<LinkButton>("btnToolTipTarget", true);

    /// <summary>Gets a reference to the ToolTip title control.</summary>
    public virtual Label TitleControl => this.Container.GetControl<Label>("lblTitle", true);

    /// <summary>Gets a reference to the ToolTip content control.</summary>
    public virtual Literal ContentLiteral => this.Container.GetControl<Literal>("ltrlContent", true);

    /// <summary>Gets a reference to the ToolTip wrapper control.</summary>
    public virtual HtmlGenericControl ToolTipWrapper => this.Container.GetControl<HtmlGenericControl>("wrpToolTip", true);

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? this.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template
    /// this property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddElementProperty("wrpToolTip", this.ToolTipWrapper.ClientID);
      controlDescriptor.AddElementProperty("btnToolTipTarget", this.ToolTipTarget.ClientID);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference()
      {
        Assembly = typeof (SitefinityToolTip).Assembly.FullName,
        Name = this.script
      }
    }.ToArray();

    /// <summary>
    /// Overridden. Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="container">The container that will host the template of this control.</param>
    protected override void InitializeControls(GenericContainer container)
    {
      if (string.IsNullOrEmpty(this.Text))
        this.Text = this.defaultTargetText;
      this.ToolTipTarget.Text = string.Format("<span class=\"sfTooltip\">{0}</span>", (object) this.Text);
      this.ContentLiteral.Text = this.Content;
      this.TitleControl.Text = this.Title;
      if (!string.IsNullOrEmpty(this.TargetCssClass))
      {
        if (string.IsNullOrEmpty(this.ToolTipTarget.CssClass))
          this.ToolTipTarget.CssClass = this.TargetCssClass;
        else
          this.ToolTipTarget.CssClass = string.Format("{0} {1}", (object) this.ToolTipTarget.CssClass, (object) this.TargetCssClass);
      }
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      if (string.IsNullOrEmpty(this.ToolTipWrapper.Attributes["class"]))
        this.ToolTipWrapper.Attributes["class"] = this.CssClass;
      else
        this.ToolTipWrapper.Attributes["class"] = string.Format("{0} {1}", (object) this.ToolTipWrapper.Attributes["class"], (object) this.CssClass);
    }
  }
}
