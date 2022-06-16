// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.UpgradeInfoAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Describes a method which is executed during the upgrade process of the system.
  /// </summary>
  public class UpgradeInfoAttribute : Attribute
  {
    /// <summary>Gets or sets the ID.</summary>
    /// <value>The ID.</value>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the version of the build related to the upgrade method.
    /// </summary>
    /// <value>The version of the build.</value>
    public int UpgradeTo { get; set; }

    /// <summary>
    /// Gets or sets the message to be displayed in case of failure.
    /// </summary>
    /// <value>The fail massage.</value>
    public string FailMassage { get; set; }

    /// <summary>Gets or sets the description of the upgrade method.</summary>
    /// <value>The description.</value>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the upgrade should be visible in the upgrade log.
    /// </summary>
    /// <value>
    /// <c>true</c> if the upgrade should be hidden from the log; otherwise, <c>false</c>.
    /// </value>
    public bool HideFromUpgradeLog { get; set; }
  }
}
