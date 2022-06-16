// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.DeleteModuleContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  internal class DeleteModuleContext
  {
    /// <summary>
    /// Gets or sets a value indicating whether the data for the module should be deleted when deleting it.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [delete module data]; otherwise, <c>false</c>.
    /// </value>
    public bool DeleteModuleData { get; set; }

    /// <summary>
    /// Gets or sets the delete context where various information regarding the delete process can be stored.
    /// List tables to be dropped when the module is deleted
    /// </summary>
    /// <value>The delete context.</value>
    public Dictionary<string, string> DeleteContext { get; set; }
  }
}
