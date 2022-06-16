// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Model.Proxy.PermissionIneritanceMapProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security.Model.Proxy
{
  /// <summary>
  /// Holds permission inheritance map info in memory without any tracking or persistence
  /// </summary>
  public class PermissionIneritanceMapProxy : PermissionsInheritanceMap
  {
    /// <summary>Create a copy of an existing map</summary>
    /// <param name="original">Existing map</param>
    public PermissionIneritanceMapProxy(PermissionsInheritanceMap original)
    {
      this.ChildObjectId = original.ChildObjectId;
      this.ChildObjectTypeName = original.ChildObjectTypeName;
      this.ObjectId = original.ObjectId;
    }

    /// <summary>Gets or sets the permissions child object id.</summary>
    /// <value>The child object id.</value>
    public override Guid ChildObjectId { get; set; }

    /// <summary>
    /// Gets or sets the name of the permissions child object type.
    /// </summary>
    /// <value></value>
    public override string ChildObjectTypeName { get; set; }

    /// <summary>Gets or sets the permissions parent object id.</summary>
    /// <value></value>
    public override Guid ObjectId { get; set; }
  }
}
