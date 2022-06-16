// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.RoleWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security
{
  public class RoleWrapper : Role
  {
    private bool _isDirty;
    private bool _isNew;
    private bool _isDeleted;

    public RoleWrapper(string roleName)
    {
    }

    public RoleWrapper(Guid id, string roleName)
    {
      this.Id = id;
      this.Name = roleName;
      this._isNew = true;
      this._isDirty = false;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this user record has been modified since any last provider commit.
    /// </summary>
    public bool IsDirty
    {
      get => this._isDirty;
      set => this._isDirty = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user record is newly added and not yet committed to the provider.
    /// </summary>
    public bool IsNew
    {
      get => this._isNew;
      set => this._isNew = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user record was marked for deletion but not yet committed to the provider.
    /// </summary>
    public bool IsDeleted
    {
      get => this._isDeleted;
      set => this._isDeleted = value;
    }

    /// <summary>Gets or sets the id of the role.</summary>
    public override Guid Id
    {
      get => base.Id;
      set
      {
        base.Id = value;
        this._isDirty = true;
      }
    }

    /// <summary>Gets or sets the name of the role.</summary>
    public override string Name
    {
      get => base.Name;
      set
      {
        base.Name = value;
        this._isDirty = true;
      }
    }
  }
}
