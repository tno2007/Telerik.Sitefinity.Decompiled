// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.IOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations
{
  /// <summary>
  /// Service that provides custom operations which will be registered as endpoints.
  /// </summary>
  public interface IOperationProvider
  {
    /// <summary>
    /// Returns the operations which will be registered as endpoints.
    /// </summary>
    /// <param name="clrType">The CLR type for which operations can be returned.</param>
    /// <returns>The custom operations.</returns>
    IEnumerable<OperationData> GetOperations(Type clrType);
  }
}
