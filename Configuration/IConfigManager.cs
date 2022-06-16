// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.IConfigManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Interface that is to be implemented by Sitefinity configuration managers.
  /// </summary>
  public interface IConfigManager
  {
    /// <summary>
    /// Exports all settings that are different from the default values to external resource.
    /// </summary>
    /// <param name="section">The configuration section to be exported.</param>
    void SaveSection(ConfigSection section);

    /// <summary>Gets configuration section for the specified type.</summary>
    /// <typeparam name="TSection">The type of the configuration section.</typeparam>
    /// <returns>Configuration section.</returns>
    TSection GetSection<TSection>() where TSection : ConfigSection, new();
  }
}
