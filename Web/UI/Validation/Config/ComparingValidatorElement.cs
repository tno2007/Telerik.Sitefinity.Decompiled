// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Validation.Config.ComparingValidatorElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Web.UI.Validation.Config
{
  public class ComparingValidatorElement : ConfigElement, IComparingValidatorDefinition, IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Config.ComparingValidatorElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ComparingValidatorElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Config.ComparingValidatorElement" /> class.
    /// </summary>
    internal ComparingValidatorElement()
      : base(false)
    {
    }

    /// <summary>
    /// Specifies the pageId of the control which value is going to be used as range limitation
    /// </summary>
    /// <value>The control to compare.</value>
    [ConfigurationProperty("controlToCompare")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ControlToCompareDescription", Title = "ControlToCompareCaption")]
    public string ControlToCompare
    {
      get => (string) this["controlToCompare"];
      set => this["controlToCompare"] = (object) value;
    }

    /// <summary>Gets or sets the comparison operation to perform.</summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" />
    /// values. The default value is Equal.
    /// </returns>
    [ConfigurationProperty("operator", DefaultValue = ValidationCompareOperator.Equal)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OperatorDescription", Title = "OperatorCaption")]
    public ValidationCompareOperator Operator
    {
      get => (ValidationCompareOperator) this["operator"];
      set => this["operator"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message to show if the validation failed.
    /// </summary>
    /// <value>The validation violation message.</value>
    [ConfigurationProperty("validationViolationMessage")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ValidationViolationMessageDescription", Title = "ValidationViolationMessageCaption")]
    public string ValidationViolationMessage
    {
      get => (string) this["validationViolationMessage"];
      set => this["validationViolationMessage"] = (object) value;
    }

    /// <summary>
    /// Treat comparisons as this type - currently only "Numeric" is supported. If not provided, the type of value is used.
    /// </summary>
    [ConfigurationProperty("validationDataType")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ValidationViolationMessageDescription", Title = "ValidationViolationMessageCaption")]
    public string ValidationDataType
    {
      get => (string) this["validationDataType"];
      set => this["validationDataType"] = (object) value;
    }

    public DefinitionBase GetDefinition() => (DefinitionBase) new ComparingValidatorDefinition(this);

    public TDefinition GetDefinition<TDefinition>() where TDefinition : DefinitionBase, new() => (TDefinition) this.GetDefinition();

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      public const string ControlToCompare = "controlToCompare";
      public const string Operator = "operator";
      public const string ValidationViolationMessage = "validationViolationMessage";
      public const string ValidationDataType = "validationDataType";
    }
  }
}
