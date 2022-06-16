// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Package.AddOnEntry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Packaging.Package
{
  /// <summary>Add-on response entry</summary>
  internal class AddOnEntry
  {
    /// <summary>Gets or sets the Add-on display name</summary>
    public string DisplayName { get; set; }

    /// <summary>Gets or sets the count of Add-on entry</summary>
    public int Count { get; set; }

    /// <summary>Gets or sets the Add-on entry type</summary>
    public AddOnEntryType AddonEntryType { get; set; }
  }
}
