// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Provides extension methods to configuration related classes and interfaces.
  /// </summary>
  public static class ConfigExtensions
  {
    /// <summary>
    /// The effective user friendly name, which should be used in the UI consistently. Its value is
    /// either the resource with <see cref="!:Title" /> as a key, when <see cref="!:ResourceClassId" /> is set,
    /// or <see cref="!:Title" /> itself, if set,
    /// or <see cref="!:Name" /> if <see cref="!:Title" /> is not set.
    /// </summary>
    public static string GetEffectiveTitle(this ITitledConfigElement configElement) => string.IsNullOrWhiteSpace(configElement.Title) ? configElement.Name : Res.GetLocalizable(configElement.Title, configElement.ResourceClassId);

    /// <summary>
    /// Determines whether configuration element is of simple type.
    /// </summary>
    /// <param name="configElement">The configuration element.</param>
    /// <returns></returns>
    internal static bool IsSimpleElement(this ConfigElement configElement) => configElement.Parent != null && !(configElement.Parent is ConfigElementCollection);
  }
}
