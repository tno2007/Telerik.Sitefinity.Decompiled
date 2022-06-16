// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Validation.Contracts.IComparingValidatorDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Validation.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the Comparing Validator definition
  /// </summary>
  public interface IComparingValidatorDefinition : IDefinition
  {
    /// <summary>
    /// Specifies the pageId of the control which value is going to be used as range limitation
    /// </summary>
    /// <value>The control to compare.</value>
    string ControlToCompare { get; set; }

    /// <summary>Gets or sets the comparison operation to perform.</summary>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" />
    /// values. The default value is Equal.
    /// </returns>
    ValidationCompareOperator Operator { get; set; }

    /// <summary>
    /// Gets or sets the message to show if the validation failed.
    /// </summary>
    /// <value>The validation violation message.</value>
    string ValidationViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the type to validate as, if it does not match the type of the provided values
    /// </summary>
    /// <value>The full name of the type to validate as.</value>
    string ValidationDataType { get; set; }
  }
}
