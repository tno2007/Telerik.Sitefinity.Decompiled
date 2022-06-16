// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.ReportsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>
  /// The view for exploring the reports of newsletters campaigns.
  /// </summary>
  public class ReportsView : ViewModeControl<NewslettersControlPanel>
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.ReportsView.ascx");

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ReportsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }
  }
}
