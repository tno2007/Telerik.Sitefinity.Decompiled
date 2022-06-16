// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ParameterArgument
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>A class describing the argument of an operation</summary>
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class ParameterArgument
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ParameterArgument" /> class.
    /// </summary>
    public ParameterArgument()
    {
    }

    public ParameterArgument(ParameterArgument argument)
    {
      this.Label = argument.Label;
      this.Value = argument.Value;
      this.Warning = argument.Warning;
      this.AdditionalValue = argument.AdditionalValue;
      this.AdditionalValueLabel = argument.AdditionalValueLabel;
    }

    /// <summary>Gets or sets the label.</summary>
    /// <value>The label.</value>
    [DataMember]
    public string Label { get; set; }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The value.</value>
    [DataMember]
    public string Value { get; set; }

    /// <summary>Gets or sets the warning.</summary>
    /// <value>The warning.</value>
    [DataMember]
    public string Warning { get; set; }

    /// <summary>Gets or sets the additional value.</summary>
    /// <value>The additional value.</value>
    [DataMember]
    public string AdditionalValue { get; set; }

    /// <summary>Gets or sets the additional value label.</summary>
    /// <value>The additional value label.</value>
    [DataMember]
    public string AdditionalValueLabel { get; set; }
  }
}
