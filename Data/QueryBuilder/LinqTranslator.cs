// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.QueryBuilder.LinqTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Data.QueryBuilder
{
  public class LinqTranslator
  {
    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <value>The translator.</value>
    internal static LinqTranslator TheTranslator => new LinqTranslator();

    /// <summary>Convert query data into dynamic query</summary>
    /// <param name="queryData">The query data.</param>
    /// <returns></returns>
    public static string ToDynamicLinq(QueryData queryData)
    {
      StringBuilder sb = new StringBuilder();
      LinqTranslator.TheTranslator.Visit(sb, queryData);
      return sb.ToString();
    }

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="sb">The sb.</param>
    /// <param name="queryData">The query data.</param>
    internal virtual void Visit(StringBuilder sb, QueryData queryData)
    {
      bool flag = true;
      if (this.FireVisitEvent(sb, queryData))
        return;
      foreach (QueryItem zeroLevelItem in queryData.GetZeroLevelItems())
      {
        if (flag)
        {
          flag = false;
        }
        else
        {
          sb.Append(" ");
          sb.Append(zeroLevelItem.Join);
          sb.Append(" ");
        }
        this.Visit(sb, zeroLevelItem, queryData);
      }
    }

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="sb">The sb.</param>
    /// <param name="item">The item.</param>
    /// <param name="queryData">The query data.</param>
    internal virtual void Visit(StringBuilder sb, QueryItem item, QueryData queryData)
    {
      if (this.FireVisitEvent(sb, item, queryData))
        return;
      if (item.IsGroup)
        this.VisitGroup(sb, item, queryData);
      else
        this.BuildOperation(sb, item);
    }

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="sb">The sb.</param>
    /// <param name="item">The item.</param>
    /// <param name="queryData">The query data.</param>
    internal virtual void VisitGroup(StringBuilder sb, QueryItem item, QueryData queryData)
    {
      bool flag = true;
      IEnumerable<QueryItem> immediateChildren = queryData.GetImmediateChildren(item);
      if (immediateChildren.Count<QueryItem>() > 1)
        sb.Append(" ( ");
      foreach (QueryItem queryItem in immediateChildren)
      {
        if (flag)
        {
          flag = false;
        }
        else
        {
          sb.Append(" ");
          sb.Append(queryItem.Join);
          sb.Append(" ");
        }
        this.Visit(sb, queryItem, queryData);
      }
      if (immediateChildren.Count<QueryItem>() <= 1)
        return;
      sb.Append(" ) ");
    }

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="condition">The condition.</param>
    /// <returns>
    /// 	<c>true</c> if [is binary operation] [the specified condition]; otherwise, <c>false</c>.
    /// </returns>
    internal virtual bool IsBinaryOperation(Condition condition) => condition.Operator == "==" || condition.Operator == "=" || condition.Operator == ">" || condition.Operator == ">=" || condition.Operator == "<" || condition.Operator == "<=" || condition.Operator == "<>";

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="condition">The condition.</param>
    /// <returns>
    /// 	<c>true</c> if [is method invoke operation] [the specified condition]; otherwise, <c>false</c>.
    /// </returns>
    internal virtual bool IsMethodInvokeOperation(Condition condition) => condition.Operator == "Contains" || condition.Operator == "StartsWith" || condition.Operator == "EndsWith";

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="condition">The condition.</param>
    /// <returns>
    /// 	<c>true</c> if [is unary operation] [the specified condition]; otherwise, <c>false</c>.
    /// </returns>
    internal virtual bool IsUnaryOperation(Condition condition) => false;

    internal virtual bool IsSpecialCase(Condition condition) => condition.Operator == "IN" && condition.FieldType == typeof (DateTime).FullName;

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="sb">The sb.</param>
    /// <param name="item">The item.</param>
    internal virtual void BuildOperation(StringBuilder sb, QueryItem item)
    {
      if (this.IsBinaryOperation(item.Condition))
        this.BuildBinaryOperation(sb, item);
      else if (this.IsMethodInvokeOperation(item.Condition))
        this.BuildMethodInvokeOperation(sb, item);
      else if (this.IsUnaryOperation(item.Condition))
        this.BuildUnaryOperation(sb, item);
      else if (this.IsSpecialCase(item.Condition))
        this.BuildSpecialCases(sb, item);
      else
        throw new NotSupportedException("Operation {0} is not supported.".Arrange((object) item.Condition.Operator));
    }

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="sb">The sb.</param>
    /// <param name="item">The item.</param>
    internal virtual void BuildMethodInvokeOperation(StringBuilder sb, QueryItem item)
    {
      sb.Append(" ");
      sb.Append(item.Condition.FieldName);
      sb.Append(".");
      sb.Append(item.Condition.Operator);
      sb.Append("(");
      sb.Append(this.GetFormatedValue(item));
      sb.Append(") ");
    }

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="sb">The sb.</param>
    /// <param name="item">The item.</param>
    internal virtual void BuildBinaryOperation(StringBuilder sb, QueryItem item)
    {
      sb.Append(" ");
      sb.Append(item.Condition.FieldName);
      this.TranslateBinaryOperation(sb, item.Condition);
      sb.Append(this.GetFormatedValue(item));
    }

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="sb">The sb.</param>
    /// <param name="item">The item.</param>
    internal virtual void BuildUnaryOperation(StringBuilder sb, QueryItem item) => throw new NotImplementedException("Build unary operation is not implemented");

    /// <summary>Build In clause</summary>
    /// <param name="sb">The sb.</param>
    /// <param name="item">The item.</param>
    internal virtual void BuildSpecialCases(StringBuilder sb, QueryItem item)
    {
      if (!(item.Condition.Operator == "IN") || !(item.Condition.FieldType == typeof (DateTime).FullName))
        return;
      sb.Append(" (");
      sb.Append(item.Condition.FieldName);
      sb.Append(" >= ");
      sb.Append(" DateTime.UtcNow.Date ");
      sb.Append(" AND ");
      sb.Append(item.Condition.FieldName);
      sb.Append(" <= ");
      sb.Append(this.ConvertInClauseValueToDateTime(item.Value));
    }

    internal virtual string ConvertInClauseValueToDateTime(string conditionValue)
    {
      StringBuilder stringBuilder = new StringBuilder(" DateTime.UtcNow");
      string[] source = conditionValue.Split(new string[1]
      {
        " "
      }, StringSplitOptions.RemoveEmptyEntries);
      if (((IEnumerable<string>) source).Count<string>() % 2 != 0)
        throw new ArgumentException("Incorrect values for IN clause : {0}".Arrange((object) conditionValue));
      int result = 0;
      string str = int.TryParse(source[0], out result) ? source[1].ToLower() : throw new ArgumentException("unable to cast first part of argument from IN Clause to int : {0}".Arrange((object) source[0]));
      if (!(str == "day"))
      {
        if (!(str == "month"))
        {
          if (!(str == "year"))
          {
            if (!(str == "hours"))
            {
              if (!(str == "minute"))
              {
                if (str == "second")
                  stringBuilder.Append(".AddSeconds(" + result.ToString() + ")");
                else
                  throw new NotSupportedException(" Datetime operator {0} is not supported".Arrange((object) source[1]));
              }
              else
                stringBuilder.Append(".AddMinutes(" + result.ToString() + ")");
            }
            else
              stringBuilder.Append(".AddHours(" + result.ToString() + ")");
          }
          else
            stringBuilder.Append(".AddYears(" + result.ToString() + ")");
        }
        else
          stringBuilder.Append(".AddMonths(" + result.ToString() + ")");
      }
      else
        stringBuilder.Append(".AddDays(" + result.ToString() + ")");
      stringBuilder.Append(" ");
      return stringBuilder.ToString();
    }

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    internal virtual string GetFormatedValue(QueryItem item)
    {
      Type fieldType = (Type) null;
      if (fieldType == (Type) null)
        fieldType = TypeResolutionService.ResolveType(item.Condition.FieldType);
      return fieldType.IsValueType || fieldType == typeof (string) || fieldType == typeof (Lstring) ? this.GetFormatedValueTypeValue(item, fieldType) : throw new NotSupportedException("Values of type {0} are not supported".Arrange((object) fieldType.FullName));
    }

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="item">The item.</param>
    /// <param name="fieldType">Type of the field.</param>
    /// <returns></returns>
    internal virtual string GetFormatedValueTypeValue(QueryItem item, Type fieldType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (fieldType == typeof (Guid) || fieldType == typeof (DateTime))
      {
        stringBuilder.Append("(");
        stringBuilder.Append(item.Value);
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (double))
      {
        stringBuilder.Append(item.Value);
        stringBuilder.Append("d");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (float))
      {
        stringBuilder.Append(item.Value);
        stringBuilder.Append("f");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (Decimal))
      {
        stringBuilder.Append(item.Value);
        stringBuilder.Append("m");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (uint))
      {
        stringBuilder.Append(item.Value);
        stringBuilder.Append("u");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (ulong))
      {
        stringBuilder.Append(item.Value);
        stringBuilder.Append("UL");
        return stringBuilder.ToString();
      }
      if (fieldType == typeof (string) || fieldType == typeof (Lstring))
      {
        stringBuilder.Append("\"");
        stringBuilder.Append(item.Value);
        stringBuilder.Append("\"");
        return stringBuilder.ToString();
      }
      stringBuilder.Append(item.Value);
      return stringBuilder.ToString();
    }

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="sb">The sb.</param>
    /// <param name="condition">The condition.</param>
    internal virtual void TranslateBinaryOperation(StringBuilder sb, Condition condition)
    {
      if (condition.Operator == "=")
        sb.Append("==");
      else if (condition.Operator == "<>")
        sb.Append("!=");
      else if (condition.Operator == "==" || condition.Operator == ">" || condition.Operator == ">=" || condition.Operator == "<" || condition.Operator == "<=")
        sb.Append(condition.Operator);
      else
        throw new NotSupportedException("Binary operation {0} is not supported".Arrange((object) condition.Operator));
    }

    /// <summary>Occurs when query data is visited.</summary>
    public static event LinqTranslator.VisitQueryDataHandler OnVisitQueryData;

    /// <summary>Occurs when query item is visited.</summary>
    public static event LinqTranslator.VisitQueryItemHandler OnVisitQueryItem;

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="sb">The sb.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    internal virtual bool FireVisitEvent(StringBuilder sb, QueryData item)
    {
      VisitEventArgs args = new VisitEventArgs(sb, (QueryItem) null, item);
      if (LinqTranslator.OnVisitQueryData != null)
        LinqTranslator.OnVisitQueryData(args);
      return args.IsHandled;
    }

    /// <summary>Only for UNIT TESTS !!!</summary>
    /// <param name="sb">The sb.</param>
    /// <param name="item">The item.</param>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    internal virtual bool FireVisitEvent(StringBuilder sb, QueryItem item, QueryData data)
    {
      VisitEventArgs args = new VisitEventArgs(sb, item, data);
      if (LinqTranslator.OnVisitQueryData != null)
        LinqTranslator.OnVisitQueryItem(args);
      return args.IsHandled;
    }

    /// <summary>Visit query data delegate</summary>
    public delegate void VisitQueryDataHandler(VisitEventArgs args);

    /// <summary>Visit query item delegate</summary>
    public delegate void VisitQueryItemHandler(VisitEventArgs args);
  }
}
