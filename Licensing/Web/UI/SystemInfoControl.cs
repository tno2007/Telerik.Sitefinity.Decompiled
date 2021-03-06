// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.UI.SystemInfoControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Licensing.Web.UI
{
  /// <summary>Displays Sitefinity system information.</summary>
  public class SystemInfoControl : SimpleView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Licensing.SystemInfo.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SystemInfoControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => container.GetControl<RadListView>("DetailsView", true).DataSource = (object) new SystemInfo[1]
    {
      new SystemInfo()
    };
  }
}
