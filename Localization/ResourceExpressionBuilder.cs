// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.ResourceExpressionBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.CodeDom;
using System.Web.Compilation;
using System.Web.UI;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// 
  /// </summary>
  public class ResourceExpressionBuilder : ExpressionBuilder
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entry"></param>
    /// <param name="parsedData"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override CodeExpression GetCodeExpression(
      BoundPropertyEntry entry,
      object parsedData,
      ExpressionBuilderContext context)
    {
      return (CodeExpression) new CodeMethodInvokeExpression(new CodeMethodReferenceExpression((CodeExpression) new CodeTypeReferenceExpression(typeof (ResourceExpressionBuilder)), "GetValue"), new CodeExpression[1]
      {
        (CodeExpression) new CodePrimitiveExpression((object) entry.Expression)
      });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static object GetValue(string expression)
    {
      string[] strArray = expression.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries);
      return strArray.Length == 2 ? (object) Res.Get(strArray[0].Trim(), strArray[1].Trim()) : (object) null;
    }
  }
}
