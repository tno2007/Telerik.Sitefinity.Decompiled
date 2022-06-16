// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.OpenAccessUpgradableProviderExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data.OA
{
  /// <summary>
  /// Provides extension methods for IOpenAccessUpgradableProvider
  /// </summary>
  public static class OpenAccessUpgradableProviderExtensions
  {
    public static int GetAssemblyBuildNumber(this IOpenAccessUpgradableProvider upgradable)
    {
      Type type = upgradable.GetType();
      if (type.Assembly.IsDynamic)
        type = type.BaseType;
      return type.Assembly.GetName().Version.Build;
    }
  }
}
