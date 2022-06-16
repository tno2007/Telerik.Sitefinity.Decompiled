// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Package.AddOnInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Packaging.Model;

namespace Telerik.Sitefinity.Packaging.Package
{
  /// <summary>This class defines add-on info object</summary>
  internal class AddOnInfo
  {
    /// <summary>Gets the add-on information.</summary>
    /// <param name="addon">The add-on.</param>
    /// <returns>The add-on info</returns>
    public static AddOnInfo GetAddonInfo(Addon addon)
    {
      if (addon == null)
        return (AddOnInfo) null;
      return new AddOnInfo()
      {
        Id = addon.Id,
        Name = addon.Name,
        Entries = (ICollection<AddOnEntry>) new List<AddOnEntry>()
      };
    }

    /// <summary>Gets or sets the Add-on id</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the Add-on name</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the Add-on entries</summary>
    internal ICollection<AddOnEntry> Entries { get; set; }
  }
}
