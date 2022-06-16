// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PermissionInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Copies a permission into memory without any tracking or persistence and can be used in asp.net markup
  /// </summary>
  public class PermissionInfo : Telerik.Sitefinity.Security.Model.Permission
  {
    private DateTime lastModified;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PermissionInfo" /> class.
    /// </summary>
    public PermissionInfo()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PermissionInfo" /> class. Read information from an existing permission and copy its data.
    /// </summary>
    /// <param name="original">Permission to get data from</param>
    public PermissionInfo(Telerik.Sitefinity.Security.Model.Permission original)
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

    /// <summary>
    /// Gets or sets the unique identifier for this permission.
    /// </summary>
    /// <value>The id.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Guid Id
    {
      get => base.Id;
      set => base.Id = value;
    }

    /// <summary>
    /// Gets or sets the unique identifier for this permission.
    /// </summary>
    /// <value>The id.</value>
    public Guid PermissionId
    {
      get => this.Id;
      set => this.Id = value;
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

    /// <summary>Gets or sets the time this item was last modified.</summary>
    /// <value>The last modified time.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
