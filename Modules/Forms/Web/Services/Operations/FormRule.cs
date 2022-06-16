// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.Services.Operations.FormRule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Forms.Model;

namespace Telerik.Sitefinity.Modules.Forms.Web.Services.Operations
{
  /// <summary>Contract for form rule object</summary>
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class FormRule
  {
    private List<RuleCondition> conditions;
    private List<RuleAction> actions;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.Services.Operations.FormRule" /> is operator.
    /// </summary>
    /// <value>
    ///   <c>true</c> if AND; in case OR, <c>false</c>.
    /// </value>
    [DataMember]
    public LogicalOperator Operator { get; set; }

    /// <summary>Gets the conditions.</summary>
    /// <value>The conditions.</value>
    [DataMember]
    public IList<RuleCondition> Conditions
    {
      get
      {
        if (this.conditions == null)
          this.conditions = new List<RuleCondition>();
        return (IList<RuleCondition>) this.conditions;
      }
    }

    /// <summary>Gets the actions.</summary>
    /// <value>The actions.</value>
    [DataMember]
    public IList<RuleAction> Actions
    {
      get
      {
        if (this.actions == null)
          this.actions = new List<RuleAction>();
        return (IList<RuleAction>) this.actions;
      }
    }
  }
}
