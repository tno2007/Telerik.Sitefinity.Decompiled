﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.EffectivePermission
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>Defines permission settings</summary>
  internal class EffectivePermission : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Configuration.EffectivePermission" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public EffectivePermission(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the permission set.</summary>
    [ConfigurationProperty("permissionName", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["permissionName"];
      set => this["permissionName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the bit mask value presenting the effective grant permissions for the specified permission set.
    /// </summary>
    [ConfigurationProperty("grant", DefaultValue = 0)]
    public int Grant
    {
      get => (int) this["grant"];
      set => this["grant"] = (object) value;
    }

    private class PropNames
    {
      internal const string Name = "permissionName";
      internal const string Grant = "grant";
    }
  }
}
