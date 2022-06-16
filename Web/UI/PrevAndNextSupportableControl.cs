// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PrevAndNextSupportableControl`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents a class that sets the previous and next item of a specific data item.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class PrevAndNextSupportableControl<T> where T : Content
  {
    private string sortExpression;
    private string filterExpression;
    private Type contentType;
    private T dataItem;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PrevAndNextSupportableControl`1" /> class.
    /// </summary>
    public PrevAndNextSupportableControl()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PrevAndNextSupportableControl`1" /> class.
    /// </summary>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="dataItem">The data item.</param>
    public PrevAndNextSupportableControl(
      string sortExpression,
      string filterExpression,
      string providerName,
      T dataItem)
    {
      this.Initialize(Guid.Empty, sortExpression, filterExpression, providerName, dataItem);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PrevAndNextSupportableControl`1" /> class.
    /// </summary>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="dataItem">The data item.</param>
    public PrevAndNextSupportableControl(
      Guid parentId,
      string sortExpression,
      string filterExpression,
      string providerName,
      T dataItem)
    {
      this.Initialize(parentId, sortExpression, filterExpression, providerName, dataItem);
    }

    /// <summary>Gets the sort expression.</summary>
    /// <value>The sort expression.</value>
    public string SortExpression => this.sortExpression;

    /// <summary>Gets the filter expression.</summary>
    /// <value>The filter expression.</value>
    public string FilterExpression => this.filterExpression;

    /// <summary>Gets the type of the content.</summary>
    /// <value>The type of the content.</value>
    public Type ContentType => this.contentType;

    /// <summary>Gets the data item.</summary>
    /// <value>The data item.</value>
    public T DataItem => this.dataItem;

    /// <summary>Gets or sets the previous item.</summary>
    /// <value>The prev item.</value>
    public T PrevItem { get; set; }

    /// <summary>Gets or sets the next item.</summary>
    /// <value>The next item.</value>
    public T NextItem { get; set; }

    /// <summary>Gets the name of the field.</summary>
    /// <param name="exprSegments">The sort expression segments.</param>
    /// <returns></returns>
    private string GetFieldName(string[] exprSegments)
    {
      string fieldName = string.Empty;
      if (exprSegments.Length != 0)
        fieldName = exprSegments[0];
      return fieldName;
    }

    /// <summary>Gets the sort direction.</summary>
    /// <param name="exprSegments">The sort expression segments.</param>
    /// <returns></returns>
    private PrevAndNextSupportableControl<T>.SortDirection GetSortDirection(
      string[] exprSegments)
    {
      PrevAndNextSupportableControl<T>.SortDirection sortDirection = PrevAndNextSupportableControl<T>.SortDirection.ASC;
      if (exprSegments.Length > 1 && exprSegments[1].ToUpperInvariant() == "DESC")
        sortDirection = PrevAndNextSupportableControl<T>.SortDirection.DESC;
      return sortDirection;
    }

    /// <summary>Gets the property with the specified name.</summary>
    /// <param name="name">The name of the property.</param>
    /// <returns></returns>
    private PropertyInfo GetProperty(string name) => !(this.ContentType == (Type) null) ? this.ContentType.GetProperty(name) : throw new ArgumentNullException("ContentType");

    /// <summary>Gets the type of the property.</summary>
    /// <param name="property">The property.</param>
    /// <returns></returns>
    private Type GetPropertyType(PropertyInfo property)
    {
      Type propertyType = (Type) null;
      if (property != (PropertyInfo) null)
        propertyType = property.PropertyType;
      return propertyType;
    }

    /// <summary>Gets the property value.</summary>
    /// <param name="property">The property.</param>
    /// <param name="obj">The object whose value will be returned.</param>
    /// <returns></returns>
    private object GetPropertyValue(PropertyInfo property, object obj)
    {
      object propertyValue = (object) null;
      if (property != (PropertyInfo) null)
        propertyValue = property.GetValue(obj, (object[]) null);
      return propertyValue;
    }

    /// <summary>Gets the filter expression.</summary>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="expressionOperator">The condition operator.</param>
    /// <param name="obj">The object whose value will be returned.</param>
    /// <returns></returns>
    private string GetFilterExpression(
      string filterExpression,
      string fieldName,
      string expressionOperator,
      object obj)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (!filterExpression.IsNullOrWhitespace())
        stringBuilder.AppendFormat("({0}) AND ", (object) filterExpression);
      PropertyInfo property = this.GetProperty(fieldName);
      string formattedValue = this.GetFormattedValue(this.GetPropertyValue(property, obj), this.GetPropertyType(property));
      stringBuilder.AppendFormat("({0} {1} {2})", (object) fieldName, (object) expressionOperator, (object) formattedValue);
      return stringBuilder.ToString();
    }

    /// <summary>Gets the item.</summary>
    /// <param name="query">The query.</param>
    /// <param name="equalItems">The equal items.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="itemsToSkip">The items to skip.</param>
    /// <param name="totalCount">The total count.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="expressionOperator">The expression operator.</param>
    /// <param name="findPrevious">a value indicating if the previous item will be searched for.</param>
    /// <param name="dataItem">The current data item.</param>
    /// <returns></returns>
    private T GetItem(
      IQueryable<T> query,
      IQueryable<T> equalItems,
      string filterExpression,
      string sortExpression,
      int? itemsToSkip,
      int? totalCount,
      string fieldName,
      string expressionOperator,
      bool findPrevious,
      T dataItem)
    {
      T obj = default (T);
      int num1 = equalItems.Count<T>();
      if (num1 == 1)
      {
        obj = DataProviderBase.SetExpressions<T>(query, this.GetFilterExpression(filterExpression, fieldName, expressionOperator, (object) dataItem), sortExpression, itemsToSkip, new int?(1), ref totalCount).FirstOrDefault<T>();
      }
      else
      {
        List<T> list = equalItems.ToList<T>();
        int num2 = list.IndexOf(dataItem);
        if (num2 > -1)
        {
          if (findPrevious && num2 > 0)
            obj = list[num2 - 1];
          if (!findPrevious && num2 < num1 - 1)
            obj = list[num2 + 1];
          if ((object) obj == null && num2 != 0 && num2 != num1 - 1)
            obj = DataProviderBase.SetExpressions<T>(query, this.GetFilterExpression(filterExpression, fieldName, expressionOperator, (object) dataItem), sortExpression, itemsToSkip, new int?(1), ref totalCount).FirstOrDefault<T>();
        }
      }
      return obj;
    }

    /// <summary>Gets the formatted value depending on the field type.</summary>
    /// <param name="value">The value.</param>
    /// <param name="fieldType">Type of the field.</param>
    /// <returns></returns>
    private string GetFormattedValue(object value, Type fieldType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (fieldType == typeof (Guid) || fieldType == typeof (DateTime))
      {
        stringBuilder.Append("(");
        stringBuilder.Append(value);
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (double))
      {
        stringBuilder.Append(value);
        stringBuilder.Append("d");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (float))
      {
        stringBuilder.Append(value);
        stringBuilder.Append("f");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (Decimal))
      {
        stringBuilder.Append(value);
        stringBuilder.Append("m");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (uint))
      {
        stringBuilder.Append(value);
        stringBuilder.Append("u");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (ulong))
      {
        stringBuilder.Append(value);
        stringBuilder.Append("UL");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (string) || fieldType == typeof (Lstring))
      {
        stringBuilder.Append("\"");
        stringBuilder.Append(value);
        stringBuilder.Append("\"");
        return stringBuilder.ToString();
      }
      stringBuilder.Append(value);
      return stringBuilder.ToString();
    }

    private void Initialize(
      Guid parentId,
      string sortExpression,
      string filterExpression,
      string providerName,
      T dataItem)
    {
      this.dataItem = (object) dataItem != null ? dataItem : throw new ArgumentException("dataItem cannot be null.");
      this.contentType = dataItem.GetType();
      IManager mappedManager = ManagerBase.GetMappedManager(this.contentType, providerName);
      IQueryable<T> items = mappedManager.GetItems(this.contentType, string.Empty, string.Empty, 0, 0) as IQueryable<T>;
      this.sortExpression = sortExpression;
      this.filterExpression = filterExpression;
      if (!((object) dataItem is IOrderedItem))
        throw new ArgumentException("dataItem does not implement IOrderedItem.");
      int? skip = new int?(0);
      int? totalCount = new int?();
      string orderExpression1 = "Ordinal";
      string orderExpression2 = "Ordinal Desc";
      IQueryable<T> queryable1 = DataProviderBase.SetExpressions<T>(items, this.GetFilterExpression(filterExpression, "Ordinal", "<", (object) dataItem), orderExpression2, skip, new int?(1), ref totalCount);
      IQueryable<T> queryable2 = DataProviderBase.SetExpressions<T>(items, this.GetFilterExpression(filterExpression, "Ordinal", ">", (object) dataItem), orderExpression1, skip, new int?(1), ref totalCount);
      if ((object) dataItem is Image && parentId != Guid.Empty)
      {
        LibrariesManager librariesManager = (LibrariesManager) mappedManager;
        queryable1 = (IQueryable<T>) librariesManager.GetDescendantsFromQuery<Image>((IQueryable<Image>) queryable1, librariesManager.GetFolder(parentId));
        queryable2 = (IQueryable<T>) librariesManager.GetDescendantsFromQuery<Image>((IQueryable<Image>) queryable2, librariesManager.GetFolder(parentId));
      }
      this.PrevItem = queryable1.FirstOrDefault<T>();
      this.NextItem = queryable2.FirstOrDefault<T>();
    }

    internal enum SortDirection
    {
      ASC,
      DESC,
    }
  }
}
