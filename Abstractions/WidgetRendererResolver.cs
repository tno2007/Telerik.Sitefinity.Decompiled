// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.WidgetRendererResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Progress.Sitefinity.Renderer.Designers;
using System;

namespace Telerik.Sitefinity.Abstractions
{
  internal class WidgetRendererResolver
  {
    private static Lazy<IDefaultValueResolver> defaultValueResolver = new Lazy<IDefaultValueResolver>((Func<IDefaultValueResolver>) (() => (IDefaultValueResolver) new DefaultValueResolver()));
    private static Lazy<IPropertiesConfigurator> propertiesConfigurator = new Lazy<IPropertiesConfigurator>((Func<IPropertiesConfigurator>) (() => (IPropertiesConfigurator) new PropertiesConfigurator()));

    private WidgetRendererResolver()
    {
    }

    internal static IDefaultValueResolver GetDefaultValueResolver() => WidgetRendererResolver.defaultValueResolver.Value;

    internal static IPropertiesConfigurator GetPropertyConfigurator() => WidgetRendererResolver.propertiesConfigurator.Value;
  }
}
