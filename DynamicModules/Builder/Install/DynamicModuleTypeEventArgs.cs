// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.DynamicModuleTypeEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  internal class DynamicModuleTypeEventArgs : EventArgs
  {
    public DynamicModuleTypeEventArgs(string moduleName, string typeName, string transactionName = null)
    {
      this.ModuleName = moduleName;
      this.TypeName = typeName;
      this.TransactionName = transactionName;
    }

    public string ModuleName { get; set; }

    public string TypeName { get; set; }

    public string TransactionName { get; set; }
  }
}
