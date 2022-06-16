// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.DataResolving.IDataResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;

namespace Telerik.Sitefinity.Web.DataResolving
{
  /// <summary>Represents an interface for property data resolvers.</summary>
  public interface IDataResolver
  {
    /// <summary>
    /// Resolves and formats specific data form the specified item.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="format">The format.</param>
    /// <param name="args">Optional arguments.</param>
    /// <returns></returns>
    string Resolve(object dataItem, string format, string args);

    /// <summary>
    /// Initializes this instance with the provided configuration information.
    /// </summary>
    /// <param name="config"></param>
    void Initialize(NameValueCollection config);
  }
}
