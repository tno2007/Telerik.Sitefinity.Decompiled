// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.OperationCategory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// Represents a class that describes the operation category interface
  /// </summary>
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal class OperationCategory
  {
    public OperationCategory(string name, string title = null)
    {
      this.Name = name;
      this.Title = title;
    }

    /// <summary>Gets or sets the name.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the title</summary>
    [DataMember]
    public string Title { get; set; }
  }
}
