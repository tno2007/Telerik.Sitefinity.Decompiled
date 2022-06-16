// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.OperationParameter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>A class describing the parameter of an operation</summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class OperationParameter
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.OperationParameter" /> class.
    /// </summary>
    public OperationParameter()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.OperationParameter" /> class.
    /// </summary>
    /// <param name="operationParameter">OperationParameter to copy from.</param>
    public OperationParameter(OperationParameter operationParameter)
    {
      this.Name = operationParameter.Name;
      this.Value = operationParameter.Value;
      this.Type = operationParameter.Type;
      this.Hint = operationParameter.Hint;
      this.Tooltip = operationParameter.Tooltip;
      this.Title = operationParameter.Title;
      this.FriendlyTitle = operationParameter.FriendlyTitle;
      this.Required = operationParameter.Required;
      this.Placeholder = operationParameter.Placeholder;
      if (operationParameter.Arguments == null)
        return;
      this.Arguments = ((IEnumerable<ParameterArgument>) operationParameter.Arguments).Select<ParameterArgument, ParameterArgument>((Func<ParameterArgument, ParameterArgument>) (a => new ParameterArgument(a))).ToArray<ParameterArgument>();
    }

    /// <summary>Gets or sets the name</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the value</summary>
    [DataMember]
    public string Value { get; set; }

    /// <summary>Gets or sets the type of the parameter - date, string</summary>
    [DataMember]
    public string Type { get; set; }

    /// <summary>Gets or sets the hint that describes the parameter</summary>
    [DataMember]
    public string Hint { get; set; }

    /// <summary>Gets or sets the tooltip that describes the parameter</summary>
    [DataMember]
    public string Tooltip { get; set; }

    /// <summary>Gets or sets the title</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the friendly title</summary>
    [DataMember]
    public string FriendlyTitle { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the parameter is required
    /// </summary>
    [DataMember]
    public bool Required { get; set; }

    /// <summary>Gets or sets the placeholder</summary>
    [DataMember]
    public string Placeholder { get; set; }

    /// <summary>Gets or sets the arguments.</summary>
    /// <value>The arguments.</value>
    [DataMember]
    public ParameterArgument[] Arguments { get; set; }

    internal static class ParameterValues
    {
      internal const string Delete = "Delete";
    }

    internal static class ParameterTypes
    {
      internal const string Date = "date";
      internal const string LongString = "long-string";
      internal const string DropDown = "choiceDropDown";
      internal const string SingleItemSync = "singleItemSync";
    }
  }
}
