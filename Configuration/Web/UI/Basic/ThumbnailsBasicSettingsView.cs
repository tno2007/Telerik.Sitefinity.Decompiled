// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.ThumbnailsBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  public class ThumbnailsBasicSettingsView : BasicSettingsView
  {
    internal const string viewScript = "~/WebControls/ThumbnailsBasicSettingsView.js";
    private const string layoutTemplatePath = "Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.ThumbnailsBasicSettingsView.ascx";

    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.ThumbnailsBasicSettingsView.ascx") : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
    {
      base.GetScriptDescriptors().Last<ScriptDescriptor>()
    };

    protected override void InitializeControls(Control container)
    {
      this.InitializeViews();
      ControlCollection controls = this.Container.GetControl<PlaceHolder>("placeHolder", true).Controls;
      BackendContentView child = new BackendContentView();
      child.ModuleName = "Libraries";
      child.ControlDefinitionName = "ThumbnailsBackend";
      controls.Add((Control) child);
    }
  }
}
