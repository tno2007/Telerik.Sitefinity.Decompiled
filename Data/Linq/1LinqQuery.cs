// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.LinqQuery`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data.Linq.Ldap;
using Telerik.Sitefinity.Data.Linq.OpenAccess;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Ldap;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Data.Linq
{
  /// <summary>
  /// Linq query classes, which instantiates Query providers and translators. This class inherits from IQueryable interfaces
  /// and is returned to the query instantiator through the mediator <see cref="T:Telerik.Sitefinity.Data.Linq.SitefinityQuery" /> class.
  /// </summary>
  /// <typeparam name="TBaseItem">The type of the base item.</typeparam>
  /// <typeparam name="TDataItem">The type of the data item.</typeparam>
  public class LinqQuery<TBaseItem, TDataItem> : 
    IOrderedQueryable<TBaseItem>,
    IQueryable<TBaseItem>,
    IEnumerable<TBaseItem>,
    IEnumerable,
    IQueryable,
    IOrderedQueryable,
    IPermissionApplier,
    ILinqQuery
  {
    private IQueryable result;
    private DataProviderBase dataProvider;
    private IQueryProvider provider;
    private Expression expression;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Linq.LinqQuery`2" /> class.
    /// </summary>
    /// <param name="dataProvider">The instance of data provider that instantiated this Linq query.</param>
    public LinqQuery(DataProviderBase dataProvider)
    {
      if (dataProvider == null)
        throw new ArgumentNullException(nameof (dataProvider));
      if (this.dataProvider == null)
        this.dataProvider = dataProvider;
      if (this.provider != null)
        return;
      this.DetermineQueryProvider();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Linq.LinqQuery`2" /> class.
    /// </summary>
    /// <param name="dataProvider">The instance of data provider that instantiated this Linq query.</param>
    /// <param name="expression">The expression to be used by this Linq query.</param>
    public LinqQuery(DataProviderBase dataProvider, Expression expression)
      : this(dataProvider)
    {
      this.expression = expression != null ? expression : throw new ArgumentNullException(nameof (expression));
    }

    public LinqQuery(DataProviderBase dataProvider, Expression expression, IQueryable result)
      : this(dataProvider, expression)
    {
      this.result = result;
    }

    public LinqQuery(
      DataProviderBase dataProvider,
      Expression expression,
      IQueryable result,
      IQueryProvider openAccessQueryProvider)
    {
      this.expression = expression != null ? expression : throw new ArgumentNullException(nameof (expression));
      if (dataProvider == null)
        throw new ArgumentNullException(nameof (dataProvider));
      if (this.dataProvider == null)
        this.dataProvider = dataProvider;
      this.result = result;
      this.provider = openAccessQueryProvider;
    }

    /// <summary>Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.
    /// </summary>
    /// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is associated
    /// with this instance of <see cref="T:System.Linq.IQueryable" />.</returns>
    Expression IQueryable.Expression
    {
      get
      {
        if (this.expression == null)
          this.expression = ((ISitefinityQueryProvider) this.Provider).Expression ?? (Expression) Expression.Constant((object) this);
        return this.expression;
      }
    }

    /// <summary>Gets the type of the element(s) that are returned when the expression
    /// tree associated with this instance of <see cref="T:System.Linq.IQueryable" />
    /// is executed.</summary>
    /// <returns>A <see cref="T:System.Type" /> that represents the type of the element(s)
    /// that are returned when the expression tree associated with this object is executed.
    /// </returns>
    Type IQueryable.ElementType => typeof (TDataItem);

    /// <summary>Gets the query provider that is associated with this data source.</summary>
    /// <returns>The <see cref="T:System.Linq.IQueryProvider" /> that is associated with
    /// this data source.</returns>
    public IQueryProvider Provider => this.provider;

    IDataProviderBase ILinqQuery.DataProvider => (IDataProviderBase) this.dataProvider;

    private void DetermineQueryProvider()
    {
      if (this.dataProvider is ILdapProviderMarker && (typeof (TDataItem).IsAssignableFrom(typeof (User)) || typeof (TDataItem).IsAssignableFrom(typeof (Role)) || typeof (TDataItem).IsAssignableFrom(typeof (UserLink))))
        this.provider = (IQueryProvider) new LdapQueryProvider<TBaseItem, TDataItem>(this.dataProvider);
      else
        this.provider = this.dataProvider is IOpenAccessDataProvider ? (IQueryProvider) new OpenAccessQueryProvider<TDataItem, TDataItem>((IOpenAccessDataProvider) this.dataProvider) : throw new InvalidOperationException("This data provider is not supported.");
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<TBaseItem> GetEnumerator()
    {
      IEnumerator<TBaseItem> enumerator = this.result == null ? ((IEnumerable<TBaseItem>) this.provider.Execute(this.expression)).GetEnumerator() : ((IEnumerable<TBaseItem>) this.result).GetEnumerator();
      if (typeof (TBaseItem) == typeof (DynamicContent) || this.result != null && this.result.ElementType != (Type) null && typeof (DynamicContent).IsAssignableFrom(this.result.ElementType))
        enumerator = (IEnumerator<TBaseItem>) new FieldsPermissionsApplierEnumerator<TBaseItem>(enumerator, this.dataProvider);
      else if (typeof (TBaseItem).GetInterface(typeof (ISecuredObject).FullName) != (Type) null && this.PermissionsInfo != null)
        enumerator = (IEnumerator<TBaseItem>) new PermissionApplierEnumerator<TBaseItem>(enumerator, this.dataProvider, this.PermissionsInfo);
      return (IEnumerator<TBaseItem>) new DataItemEnumerator<TBaseItem>(enumerator, this.dataProvider);
    }

    IEnumerator IEnumerable.GetEnumerator() => this.dataProvider is DynamicModuleDataProvider ? (IEnumerator) new FieldsPermissionsApplierEnumerator<DynamicContent>(this.result == null ? ((IEnumerable) this.provider.Execute(this.expression)).Cast<DynamicContent>().GetEnumerator() : Enumerable.Cast<DynamicContent>(this.result).GetEnumerator(), this.dataProvider) : (IEnumerator) new DataItemEnumerator(this.result == null ? ((IEnumerable) this.provider.Execute(this.expression)).GetEnumerator() : this.result.GetEnumerator(), this.dataProvider);

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </returns>
    public override string ToString() => this.expression != null ? this.expression.ToString() : string.Empty;

    /// <summary>
    /// Gets or sets the information about the permission that should be applied.
    /// </summary>
    /// <value>The permission.</value>
    public PermissionAttribute[] PermissionsInfo { get; set; }

    IQueryable ILinqQuery.InnerQuery => this.result;
  }
}
