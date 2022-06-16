// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Maybe.RenderConditions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Maybe
{
  /// <summary>
  /// Conditions for the operands of <see cref="T:Telerik.Sitefinity.Web.UI.Maybe.ConditionalTemplate" />
  /// </summary>
  public enum RenderConditions
  {
    /// <summary>Always render the template</summary>
    Default,
    /// <summary>
    /// Render the template only if the left operand has a property of that name
    /// </summary>
    IsDefined,
    /// <summary>
    /// Render the tmplate only of the left operand doens't have a property of that name
    /// </summary>
    IsNotDefined,
    /// <summary>
    /// Render the template only if the value of the left operand is of default value
    /// </summary>
    IsDefaultValue,
    /// <summary>
    /// Render template if left operand is not of default value
    /// </summary>
    IsNotDefaultValue,
    /// <summary>
    /// Render template if its string representation is not null or empty or whitespace
    /// </summary>
    IsNotWhitespaceString,
    /// <summary>
    /// Rendet template if its string representation is null or empty or whitespace
    /// </summary>
    IsWhitespaceString,
  }
}
