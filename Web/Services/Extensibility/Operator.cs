// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.Operator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Forms.Model;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal class Operator
  {
    [DataMember]
    public ConditionOperator Key { get; set; }

    [DataMember]
    public string Value { get; set; }
  }
}
