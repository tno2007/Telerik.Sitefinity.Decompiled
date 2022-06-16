// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.LinqQuery`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;
using System.Linq.Expressions;

namespace Telerik.Sitefinity.Data.Linq
{
  /// <summary>
  /// Linq query classes, which instantiates Query providers and translators. This class inherits from IQueryable interfaces
  /// and is returned to the query instantiator through the mediator <see cref="T:Telerik.Sitefinity.Data.Linq.SitefinityQuery" /> class.
  /// </summary>
  /// <typeparam name="TBaseItem">The type of the base item.</typeparam>
  public class LinqQuery<TBaseItem> : LinqQuery<TBaseItem, TBaseItem>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Linq.LinqQuery`1" /> class.
    /// </summary>
    /// <param name="dataProvider">The instance of data provider that instantiated this Linq query.</param>
    public LinqQuery(DataProviderBase dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Linq.LinqQuery`1" /> class.
    /// </summary>
    /// <param name="dataProvider">The data provider.</param>
    /// <param name="expression">The expression.</param>
    public LinqQuery(DataProviderBase dataProvider, Expression expression)
      : base(dataProvider, expression)
    {
    }

    public LinqQuery(DataProviderBase dataProvider, Expression expression, IQueryable result)
      : base(dataProvider, expression, result)
    {
    }
  }
}
