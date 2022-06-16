// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.IExportableModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// Provides API for import and export static module`s custom fields with their configuration.
  /// </summary>
  public interface IExportableModule
  {
    /// <summary>Gets the module name</summary>
    string ModuleName { get; }

    /// <summary>Gets the current module configuration</summary>
    ConfigSection ModuleConfig { get; }

    /// <summary>Gets the module meta types</summary>
    /// <returns>Collection of all meta types for this particular module</returns>
    IList<MetaType> GetModuleMetaTypes();
  }
}
