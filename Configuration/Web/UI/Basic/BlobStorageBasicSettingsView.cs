// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.BlobStorageBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  public class BlobStorageBasicSettingsView : BasicSettingsView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.BlobStorageBasicSettingsView.ascx");

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => !string.IsNullOrEmpty(base.LayoutTemplatePath) ? base.LayoutTemplatePath : BlobStorageBasicSettingsView.layoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override void InitializeViews()
    {
      base.InitializeViews();
      ControlCollection controls = this.Container.GetControl<PlaceHolder>("placeHolder", true).Controls;
      BackendContentView child = new BackendContentView();
      child.ModuleName = "Libraries";
      child.ControlDefinitionName = "BlobStorageBackend";
      controls.Add((Control) child);
    }
  }
}
