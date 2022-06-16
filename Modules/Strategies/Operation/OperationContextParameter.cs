// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.OperationContextParameter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// A class describing the context parameter of an operation
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class OperationContextParameter
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.OperationContextParameter" /> class.
    /// </summary>
    public OperationContextParameter()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.OperationContextParameter" /> class.
    /// </summary>
    /// <param name="operationContextParameter">OperationContextParameter to copy from.</param>
    public OperationContextParameter(
      OperationContextParameter operationContextParameter)
    {
      this.Name = operationContextParameter.Name;
      this.Value = operationContextParameter.Value;
    }

    /// <summary>Gets or sets the name</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the value</summary>
    [DataMember]
    public string Value { get; set; }
  }
}
