// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Model.Proxy.PermissionProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security.Model.Proxy
{
  /// <summary>
  /// Copies a permission into memory without any tracking or persistence
  /// </summary>
  public class PermissionProxy : Permission
  {
    private DateTime lastModified;

    /// <summary>
    /// Read information from an existing permission and copy its data
    /// </summary>
    /// <param name="original">Permission to get data from</param>
    public PermissionProxy(Permission original)
    {
      this.ApplicationName = original.ApplicationName;
      this.Deny = original.Deny;
      this.Grant = original.Grant;
      this.Id = original.Id;
      this.LastModified = original.LastModified;
      this.ObjectId = original.ObjectId;
      this.PrincipalId = original.PrincipalId;
      this.SetName = original.SetName;
    }

    /// <summary>Gets the name of the application.</summary>
    /// <value>The name of the application.</value>
    public override string ApplicationName { get; set; }

    /// <summary>Gets or sets the deny value.</summary>
    /// <value>The deny.</value>
    public override int Deny { get; set; }

    /// <summary>Gets or sets the grant value.</summary>
    /// <value>The grant.</value>
    public override int Grant { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for this permission.
    /// </summary>
    /// <value>The id.</value>
    public override Guid Id { get; set; }

    /// <summary>Gets or sets the time this item was last modified.</summary>
    /// <value>The last modified time.</value>
    public override DateTime LastModified
    {
      get => this.lastModified;
      set => this.lastModified = value.ToUniversalTime();
    }

    /// <summary>
    /// Gets or sets the ID of the secured object this permission.
    /// </summary>
    /// <value>The object id.</value>
    public override Guid ObjectId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the principal for this permission.
    /// </summary>
    /// <value>The principal id.</value>
    public override Guid PrincipalId { get; set; }

    /// <summary>Gets or sets the name of the permission set.</summary>
    /// <value>The name of the set.</value>
    public override string SetName { get; set; }
  }
}
