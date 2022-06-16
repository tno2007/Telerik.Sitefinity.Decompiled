// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.RoleInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Represents information about user role.</summary>
  [Serializable]
  public class RoleInfo
  {
    /// <summary>Gets or sets the name of the role.</summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>Gets or sets the data provider name for this role.</summary>
    /// <value>The provider.</value>
    public string Provider { get; set; }

    /// <summary>Gets or sets the pageId of the role.</summary>
    /// <value>The pageId.</value>
    public Guid Id { get; set; }
  }
}
