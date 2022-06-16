﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.Services.Operations.RuleAction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Forms.Model;

namespace Telerik.Sitefinity.Modules.Forms.Web.Services.Operations
{
  /// <summary>Contract for form rule action object</summary>
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class RuleAction
  {
    /// <summary>Gets or sets the target.</summary>
    /// <value>The target.</value>
    [DataMember]
    public string Target { get; set; }

    /// <summary>Gets or sets the action.</summary>
    /// <value>The action.</value>
    [DataMember]
    public FormRuleAction Action { get; set; }
  }
}
