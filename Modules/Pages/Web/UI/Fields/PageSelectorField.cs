// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageSelectorField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI.Fields
{
  public class PageSelectorField : GenericHierarchicalField
  {
    internal const string clientScript = "Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Scripts.PageSelectorField.js";

    protected override string ScriptDescriptorType => typeof (PageSelectorField).FullName;

    protected override string ScriptDescriptorTypeName => this.ScriptDescriptorType;

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Scripts.PageSelectorField.js", typeof (PageSelectorField).Assembly.FullName)
    };

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      if (!(scriptDescriptors.Last<ScriptDescriptor>() is ScriptControlDescriptor controlDescriptor))
        return scriptDescriptors;
      controlDescriptor.AddProperty("isBackend", (object) (this.RootNodeID == SiteInitializer.SitefinityNodeId));
      return scriptDescriptors;
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (!(this.RootNodeID == Guid.Empty))
        return;
      this.RootNodeID = SiteInitializer.CurrentFrontendRootNodeId;
    }
  }
}
