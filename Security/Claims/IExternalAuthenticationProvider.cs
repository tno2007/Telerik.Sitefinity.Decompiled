// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.IExternalAuthenticationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>Represents an external authentication provider.</summary>
  public interface IExternalAuthenticationProvider
  {
    /// <summary>Gets or sets the display title of the provider</summary>
    string Title { get; set; }

    /// <summary>Gets or sets the developer name of the provider.</summary>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the provider is enabled.
    /// </summary>
    bool Enabled { get; set; }

    /// <summary>Gets or sets the link class.</summary>
    string LinkCssClass { get; set; }
  }
}
