// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.IOpenAccessMetadataProviderExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data
{
  internal static class IOpenAccessMetadataProviderExtensions
  {
    internal static Type GetStaticType(this IOpenAccessMetadataProvider provider)
    {
      Type staticType = provider.GetType();
      if (staticType.Assembly.IsDynamic)
        staticType = staticType.BaseType;
      return staticType;
    }

    internal static string GetModuleName(this IOpenAccessMetadataProvider provider)
    {
      string moduleName = provider.ModuleName;
      return string.IsNullOrEmpty(moduleName) ? provider.GetType().FullName : moduleName;
    }
  }
}
