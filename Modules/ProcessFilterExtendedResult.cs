// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ProcessFilterExtendedResult`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// Result of ProcessFilterExtended method of ContentServiceBase
  /// </summary>
  public class ProcessFilterExtendedResult<TContent> where TContent : Content
  {
    /// <summary>
    /// If not null, this will override the result of ProcessFilter and pass it to ProcessResultQuery
    /// </summary>
    public object PostProcessingContext;
    /// <summary>
    /// If the custom filter can directly return a query, this is it
    /// </summary>
    public IQueryable<TContent> CustomQuery;
    /// <summary>
    /// If the filter has done everything, regular logic will be skipped
    /// </summary>
    public bool SkipRegularLogic;
  }
}
