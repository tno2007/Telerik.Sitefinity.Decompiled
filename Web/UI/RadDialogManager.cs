// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.RadDialogManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>RadDialogManager class</summary>
  public class RadDialogManager : ScriptControl
  {
    internal const string ScriptReference = "Telerik.Sitefinity.Web.Scripts.RadDialogManager.js";

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
    {
      new ScriptControlDescriptor(typeof (RadDialogManager).FullName, this.ClientID)
    };

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    protected override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>()
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.Scripts.RadDialogManager.js", typeof (RadDialogManager).Assembly.FullName)
    };
  }
}
