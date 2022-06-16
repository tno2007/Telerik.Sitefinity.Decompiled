// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.Specifics.IControlsContainerExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Renderer.Editor.Specifics
{
  internal static class IControlsContainerExtensions
  {
    public static T GetLastContainer<T>(this IControlsContainer startContainer) where T : class => (T) new ContainerIterator().GetContainers(startContainer).Last<IControlsContainer>();
  }
}
