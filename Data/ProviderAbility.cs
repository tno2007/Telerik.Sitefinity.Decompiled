// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ProviderAbility
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Specifies an ability of the provider to execute an operation , e.g. if it is supported and allowed.
  /// </summary>
  public class ProviderAbility
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.ProviderAbility" /> class.
    /// </summary>
    /// <param name="operationName">Name of the operation.</param>
    /// <param name="supported">if set to <c>true</c> operation is supported</param>
    /// <param name="allowed">if set to <c>true</c>  operation is allowed</param>
    public ProviderAbility(string operationName, bool supported, bool allowed)
    {
      this.OperationName = operationName;
      this.Supported = supported;
      this.Allowed = allowed;
    }

    /// <summary>
    /// Gets or sets the name of the operation. example : UpdateUser, ChangePassword, DeleteRole
    /// </summary>
    /// <value>The name of the operation.</value>
    public string OperationName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Data.ProviderAbility" /> is supported.
    /// </summary>
    /// <value><c>true</c> if supported; otherwise, <c>false</c>.</value>
    public bool Supported { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Data.ProviderAbility" /> is allowed.
    /// </summary>
    /// <value><c>true</c> if allowed; otherwise, <c>false</c>.</value>
    public bool Allowed { get; set; }
  }
}
