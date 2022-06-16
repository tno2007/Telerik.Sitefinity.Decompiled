// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage.EmptyBlobSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage
{
  public class EmptyBlobSettingsView : AjaxDialogBase, IBlobSettingsView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.EmptyBlobSettingsView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorage.DatabaseBlobSettingsView" /> class.
    /// </summary>
    public EmptyBlobSettingsView() => this.LayoutTemplatePath = EmptyBlobSettingsView.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    public NameValueCollection Settings
    {
      get => new NameValueCollection();
      set
      {
      }
    }
  }
}
