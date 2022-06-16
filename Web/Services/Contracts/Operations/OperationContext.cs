// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.OperationContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations
{
  /// <summary>
  /// Operation context that contains additional parameters to it.
  /// </summary>
  public class OperationContext
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.Contracts.Operations.OperationContext" /> class.
    /// </summary>
    /// <param name="source">The source of the additional parameters which will be copied.</param>
    public OperationContext(IDictionary<string, object> source) => this.Data = (IReadOnlyDictionary<string, object>) new ReadOnlyDictionary<string, object>(source);

    /// <summary>Gets additional parameters to the operation.</summary>
    public IReadOnlyDictionary<string, object> Data { get; private set; }
  }
}
