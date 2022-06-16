// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ILayoutResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Class that implements this interface must be registered in the object factory
  /// and will be used for resolving the layout html
  /// </summary>
  public interface ILayoutResolver
  {
    /// <summary>Resolves the layout virtual path.</summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    string GetVirtualPath(IPageTemplate template);
  }
}
