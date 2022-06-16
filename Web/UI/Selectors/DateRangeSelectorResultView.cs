// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DateRangeSelectorResultView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// A <see cref="T:Telerik.Sitefinity.Web.UI.SelectorResultView" /> control that works with date ranges.
  /// </summary>
  public class DateRangeSelectorResultView : SelectorResultView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.DateRangeSelectorResultView.ascx");
    private const string controlScript = "Telerik.Sitefinity.Web.Scripts.DateRangeSelectorResultView.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DateRangeSelectorResultView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Web.UI.DateRangeSelector" /> DateRangesTitle property.
    /// </summary>
    public string SelectorDateRangesTitle { get; set; }

    /// <summary>
    /// Gets or sets the title of the <see cref="T:Telerik.Sitefinity.Web.UI.DateRangeSelector" /> control.
    /// </summary>
    public string SelectorTitle { get; set; }

    protected virtual DateRangeSelector Selector => this.Container.GetControl<DateRangeSelector>("selector", true, TraverseMethod.DepthFirst);

    /// <summary>Gets the choose button control.</summary>
    protected virtual Label ChooseButtonTextControl => this.Container.GetControl<Label>("chooseButtonTextControl", true, TraverseMethod.DepthFirst);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("selector", this.Selector.ClientID);
      controlDescriptor.AddProperty("_chooseButtonTextControlId", (object) this.ChooseButtonTextControl.ClientID);
      controlDescriptor.AddProperty("_selectLabel", (object) Res.Get<Labels>().Select);
      controlDescriptor.AddProperty("_changeLabel", (object) Res.Get<Labels>().Change);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = this.GetType().Assembly.FullName;
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.Scripts.DateRangeSelectorResultView.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      if (this.Selector == null)
        return;
      this.Selector.TabIndex = this.TabIndex;
      this.Selector.Title = this.SelectorTitle;
      this.Selector.DateRangesTitle = this.SelectorDateRangesTitle;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
