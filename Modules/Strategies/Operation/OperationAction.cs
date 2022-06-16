// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Strategies.Operation.OperationAction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Strategies.Operation
{
  [DataContract]
  internal class OperationAction
  {
    public OperationAction()
    {
    }

    public OperationAction(OperationAction source)
      : this()
    {
      this.Title = source != null ? source.Title : throw new ArgumentNullException(nameof (source));
      this.Name = source.Name;
      this.Type = source.Type;
    }

    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public int Type { get; set; }

    internal class Types
    {
      internal const int None = 0;
      internal const int Positive = 1;
      internal const int Negative = 2;
    }
  }
}
