// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ItemOperation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules.Strategies.Operation;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// Represents a class that describes the operation interface
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class ItemOperation
  {
    internal const string ItemIdParamName = "itemId";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ItemOperation" /> class.
    /// </summary>
    public ItemOperation()
    {
      this.Parameters = new OperationParameter[0];
      this.ContextParameters = new OperationContextParameter[0];
      this.ParentOperation = new ParentOperationInfo();
      this.Actions = new OperationAction[0];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ItemOperation" /> class.
    /// </summary>
    /// <param name="operation">The operation to copy from.</param>
    public ItemOperation(ItemOperation operation)
    {
      this.Name = operation.Name;
      this.Title = operation.Title;
      this.Ordinal = operation.Ordinal;
      this.Category = operation.Category;
      this.Link = operation.Link;
      this.Description = operation.Description;
      this.RequiresConfirmation = operation.RequiresConfirmation;
      this.Warning = operation.Warning;
      if (operation.ParentOperation != null)
        this.ParentOperation = new ParentOperationInfo()
        {
          Name = operation.ParentOperation.Name,
          Required = operation.ParentOperation.Required
        };
      if (operation.Parameters != null)
        this.Parameters = ((IEnumerable<OperationParameter>) operation.Parameters).Select<OperationParameter, OperationParameter>((Func<OperationParameter, OperationParameter>) (p => new OperationParameter(p))).ToArray<OperationParameter>();
      if (operation.ContextParameters != null)
        this.ContextParameters = ((IEnumerable<OperationContextParameter>) operation.ContextParameters).Select<OperationContextParameter, OperationContextParameter>((Func<OperationContextParameter, OperationContextParameter>) (p => new OperationContextParameter(p))).ToArray<OperationContextParameter>();
      if (operation.Actions == null)
        return;
      this.Actions = ((IEnumerable<OperationAction>) operation.Actions).Select<OperationAction, OperationAction>((Func<OperationAction, OperationAction>) (x => new OperationAction(x))).ToArray<OperationAction>();
    }

    /// <summary>Executes the operation.</summary>
    /// <param name="parameters">Parameters provided for the operation execution.</param>
    /// <returns>Returns <see cref="T:Telerik.Sitefinity.Modules.OperationResult" /> with Success property set according to the success of the execution.</returns>
    public virtual OperationResult Execute(Dictionary<string, string> parameters) => new OperationResult()
    {
      Message = string.Format("Operation: {0} is not available", (object) this.Name),
      Success = false
    };

    /// <summary>Gets or sets the name.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the title</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the detailed title</summary>
    [DataMember]
    public string DetailedTitle { get; set; }

    /// <summary>Gets or sets the ordinal</summary>
    [DataMember]
    public int Ordinal { get; set; }

    /// <summary>Gets or sets the category</summary>
    [DataMember]
    public OperationCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the link. The link is present if the operation requires navigation only and no server side actions
    /// </summary>
    [DataMember]
    public string Link { get; set; }

    /// <summary>Gets or sets the description.</summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>Gets or sets the warning.</summary>
    /// <value>The warning.</value>
    [DataMember]
    public string Warning { get; set; }

    /// <summary>Gets or sets the parameters for this operations</summary>
    [DataMember]
    public OperationParameter[] Parameters { get; set; }

    /// <summary>
    /// Gets or sets the context parameters for this operations
    /// </summary>
    [DataMember]
    public OperationContextParameter[] ContextParameters { get; set; }

    /// <summary>Gets or sets the parent operation for this operation.</summary>
    [DataMember]
    public ParentOperationInfo ParentOperation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether confirmation for this operation is required
    /// </summary>
    [DataMember]
    public bool RequiresConfirmation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether an item update is required when executing this operation
    /// </summary>
    [DataMember]
    public bool RequiresItemUpdate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this action should be executed on the server
    /// </summary>
    [DataMember]
    public bool ExecuteOnServer { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this action should produce a warning
    /// </summary>
    [DataMember]
    public bool PerformsDelete { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this action should produce a link
    /// </summary>
    [DataMember]
    public bool HasLinkResult { get; set; }

    [DataMember]
    public OperationAction[] Actions { get; set; }
  }
}
