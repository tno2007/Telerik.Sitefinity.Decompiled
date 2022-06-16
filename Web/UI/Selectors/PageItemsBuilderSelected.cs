// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PageItemsBuilderSelected
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Class that provides the UI for listing pages. Uses Ajax binding.
  /// </summary>
  public class PageItemsBuilderSelected : PageItemsBuilder
  {
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.CustomSelectedPagesTemplate.ascx");
    private const string scriptJs = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.PageItemsBuilderSelected.js";

    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => !string.IsNullOrEmpty(PageItemsBuilderSelected.layoutTemplatePath) ? PageItemsBuilderSelected.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Selectors.Scripts.PageItemsBuilderSelected.js", this.GetType().Assembly.FullName)
    };
  }
}
