// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.DynamicModuleEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  internal class DynamicModuleEventArgs : EventArgs
  {
    public DynamicModuleEventArgs(string moduleName, List<string> moduleTypes)
    {
      this.ModuleName = moduleName;
      this.ModuleTypes = moduleTypes;
    }

    public string ModuleName { get; set; }

    public List<string> ModuleTypes { get; set; }
  }
}
