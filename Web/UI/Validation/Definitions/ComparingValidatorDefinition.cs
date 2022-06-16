// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Validation.Definitions.ComparingValidatorDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Validation.Config;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;

namespace Telerik.Sitefinity.Web.UI.Validation.Definitions
{
  [DataContract]
  [ParseChildren(true)]
  public class ComparingValidatorDefinition : 
    DefinitionBase,
    IComparingValidatorDefinition,
    IDefinition
  {
    private string controlToCompare;
    private ValidationCompareOperator operatorField;
    private string validationViolationMessage;
    private string validateAsType;

    public ComparingValidatorDefinition()
    {
    }

    public ComparingValidatorDefinition(ComparingValidatorElement configDefinition)
      : base((ConfigElement) configDefinition)
    {
    }

    /// <summary>
    /// Specifies the pageId of the control which value is going to be used as range limitation
    /// </summary>
    /// <value>The control to compare.</value>
    [DataMember]
    public string ControlToCompare
    {
      get => this.ResolveProperty<string>(nameof (ControlToCompare), this.controlToCompare);
      set => this.controlToCompare = value;
    }

    /// <summary>Gets or sets the comparison operation to perform.</summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" />
    /// values. The default value is Equal.
    /// </returns>
    [DataMember]
    public ValidationCompareOperator Operator
    {
      get => this.ResolveProperty<ValidationCompareOperator>(nameof (Operator), this.operatorField);
      set => this.operatorField = value;
    }

    /// <summary>
    /// Gets or sets the message to show if the validation failed.
    /// </summary>
    /// <value>The validation violation message.</value>
    [DataMember]
    public string ValidationViolationMessage
    {
      get => this.ResolveProperty<string>(nameof (ValidationViolationMessage), this.validationViolationMessage);
      set => this.validationViolationMessage = value;
    }

    /// <summary>Gets or sets the type to validate as.</summary>
    [DataMember]
    public string ValidationDataType
    {
      get => this.ResolveProperty<string>(nameof (ValidationDataType), this.validateAsType);
      set => this.validateAsType = value;
    }
  }
}
