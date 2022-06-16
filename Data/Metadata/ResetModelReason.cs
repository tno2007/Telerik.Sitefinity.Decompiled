// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.ResetModelReason
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data.Metadata
{
  /// <summary>
  /// Define a class for structuring a reset model specific reason object
  /// </summary>
  internal class ResetModelReason : OperationReason
  {
    private bool dataTransfered;
    internal const string InvalidatedModulesOperationKey = "InvalidatedModules";
    internal const string HasDeletedTypesKey = "HasDeletedTypes";
    internal const string HasDeletedFieldsKey = "HasDeletedFields";

    /// <summary>
    /// Creates an instance of <see cref="T:Telerik.Sitefinity.Data.Metadata.ResetModelReason" /> from from the base class <see cref="T:Telerik.Sitefinity.Services.OperationReason" />.
    /// </summary>
    /// <param name="reason">The reason.</param>
    /// <returns>a new instance of <see cref="T:Telerik.Sitefinity.Data.Metadata.ResetModelReason" /></returns>
    public static ResetModelReason CreateFrom(OperationReason reason)
    {
      if (!(reason is ResetModelReason from))
      {
        from = new ResetModelReason();
        string str;
        if (reason.AdditionalInfo.TryGetValue("InvalidatedModules", out str))
          from.UpdatedModules = new List<string>((IEnumerable<string>) str.Split(';'));
        from.HasDeletedTypes = reason.AdditionalInfo.ContainsKey("HasDeletedTypes");
        from.HasDeletedFields = reason.AdditionalInfo.ContainsKey("HasDeletedFields");
      }
      return from;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.ResetModelReason" /> class.
    /// </summary>
    public ResetModelReason()
      : base("MetaDataChange")
    {
    }

    /// <summary>Adds the module to the list of updated modules.</summary>
    /// <param name="module">The module.</param>
    public void AddModule(string module)
    {
      if (this.UpdatedModules == null)
        this.UpdatedModules = new List<string>();
      if (this.UpdatedModules.Contains(module))
        return;
      this.UpdatedModules.Add(module);
    }

    /// <summary>
    /// Gets a value indicating whether there are any updated modules.
    /// </summary>
    /// <value>The has changes.</value>
    public bool HasChanges => this.UpdatedModules != null;

    /// <summary>Gets the updated modules.</summary>
    /// <value>The updated modules.</value>
    public List<string> UpdatedModules { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether there are any deleted types or fields.
    /// </summary>
    /// <value>The has deleted types or fields.</value>
    public bool HasDeletedTypes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether there are any deleted fields.
    /// </summary>
    /// <value>The has deleted types or fields.</value>
    public bool HasDeletedFields { get; set; }

    /// <summary>
    /// Merges the specified reset mode reason object to the current one.
    /// </summary>
    /// <param name="reason">The reason.</param>
    public void Merge(ResetModelReason reason)
    {
      this.HasDeletedTypes |= reason.HasDeletedTypes;
      this.HasDeletedFields |= reason.HasDeletedFields;
      if (reason.UpdatedModules == null)
        return;
      if (this.UpdatedModules != null)
        this.UpdatedModules.AddRange((IEnumerable<string>) reason.UpdatedModules);
      else
        this.UpdatedModules = reason.UpdatedModules;
    }

    /// <summary>
    /// Gets or sets the updated the name of the updated connection.
    /// </summary>
    /// <value>The updated connection.</value>
    internal string UpdatedConnection { get; set; }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents the current
    /// <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </returns>
    public override string ToString()
    {
      this.TransferBaseData();
      return base.ToString();
    }

    private void TransferBaseData()
    {
      if (this.dataTransfered)
        return;
      if (this.UpdatedModules != null)
        this.AddInfo("InvalidatedModules", string.Join(";", this.UpdatedModules.ToArray()));
      if (this.HasDeletedTypes)
        this.AddInfo("HasDeletedTypes");
      if (this.HasDeletedFields)
        this.AddInfo("HasDeletedFields");
      this.dataTransfered = true;
    }
  }
}
