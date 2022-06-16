// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Configuration.MigrationConfigExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;

namespace Telerik.Sitefinity.Services.Configuration
{
  internal static class MigrationConfigExtensions
  {
    internal static string ToJson<T>(this T value)
    {
      if ((object) value == null)
        throw new ArgumentNullException(nameof (value));
      JsonSerializerSettings settings = new JsonSerializerSettings()
      {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        NullValueHandling = NullValueHandling.Ignore
      };
      return JsonConvert.SerializeObject((object) value, Formatting.None, settings);
    }

    internal static TResult FromJson<TResult>(this string value) => JsonConvert.DeserializeObject<TResult>(value);

    internal static AppModuleSettingsDto ToDto(this ModuleSettings value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return new AppModuleSettingsDto()
      {
        ErrorMessage = value.ErrorMessage,
        Name = value.Name,
        Version = value.Version == (Version) null ? (Version) null : (Version) value.Version.Clone()
      };
    }
  }
}
