// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.StaticPermission
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>This class is used to cache permission information.</summary>
  public class StaticPermission : Permission
  {
    private string appName;
    private string setName;
    private Guid objectId;
    private Guid principalId;
    private int grant;
    private int deny;
    private DateTime lastModified;
    private Guid id;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Data.StaticPermission" /> class.
    /// </summary>
    /// <param name="permission">The permission.</param>
    public StaticPermission(Permission permission)
    {
      this.appName = permission.ApplicationName;
      this.setName = permission.SetName;
      this.objectId = permission.ObjectId;
      this.principalId = permission.PrincipalId;
      this.grant = permission.Grant;
      this.deny = permission.Deny;
      this.lastModified = permission.LastModified;
      this.id = permission.Id;
    }

    /// <summary>
    /// Gets or sets the unique identifier for this permission.
    /// </summary>
    /// <value>The pageId.</value>
    public override Guid Id
    {
      get => this.id;
      set => this.id = value;
    }

    /// <summary>Gets the name of the application.</summary>
    /// <value>The name of the application.</value>
    public override string ApplicationName => this.appName;

    /// <summary>Gets or sets the name of the permission set.</summary>
    /// <value>The name of the set.</value>
    public override string SetName
    {
      get => this.setName;
      set => throw new NotSupportedException();
    }

    /// <summary>
    /// Gets or sets the ID of the secured object this permission.
    /// </summary>
    /// <value>The object pageId.</value>
    public override Guid ObjectId
    {
      get => this.objectId;
      set => throw new NotSupportedException();
    }

    /// <summary>
    /// Gets or sets the ID of the principal for this permission.
    /// </summary>
    /// <value>The principal pageId.</value>
    public override Guid PrincipalId
    {
      get => this.principalId;
      set => throw new NotSupportedException();
    }

    /// <summary>Gets or sets the grant value.</summary>
    /// <value>The grant.</value>
    public override int Grant
    {
      get => this.grant;
      set => throw new NotSupportedException();
    }

    /// <summary>Gets or sets the deny value.</summary>
    /// <value>The deny.</value>
    public override int Deny
    {
      get => this.deny;
      set => throw new NotSupportedException();
    }

    /// <summary>Gets or sets the time this item was last modified.</summary>
    /// <value>The last modified time.</value>
    public override DateTime LastModified
    {
      get => this.lastModified;
      set => throw new NotSupportedException();
    }
  }
}
