// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Web.UI.JQueryCron
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Scheduling.Web.UI
{
  /// <summary>
  /// Enables the user to use predefined values (from drop-down menu) for generation of CRON.
  /// </summary>
  public class JQueryCron : SimpleView
  {
    internal static readonly string LayoutTemplPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Scheduling.JQueryCron.ascx");

    /// <summary>Gets the layout template path</summary>
    public override string LayoutTemplatePath => JQueryCron.LayoutTemplPath;

    /// <summary>Gets the layout template name</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }
  }
}
