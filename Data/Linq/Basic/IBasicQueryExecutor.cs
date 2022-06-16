// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Basic.IBasicQueryExecutor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.Linq.Basic
{
  /// <summary>An interface that enables LINQ query transformations.</summary>
  public interface IBasicQueryExecutor
  {
    /// <summary>
    /// This method acts as the execution of the LINQ expression.
    /// </summary>
    /// <param name="args">The gathered arguments from the LINQ expression.</param>
    /// <returns>The result of the executed query.</returns>
    object Execute(QueryArgs args);
  }
}
