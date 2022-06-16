// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.StatusBehaviour
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services
{
  [Flags]
  internal enum StatusBehaviour
  {
    /// <summary>Workflow Status</summary>
    Workflow = 1,
    /// <summary>Additional status</summary>
    Additional = 2,
    All = Additional | Workflow, // 0x00000003
  }
}
