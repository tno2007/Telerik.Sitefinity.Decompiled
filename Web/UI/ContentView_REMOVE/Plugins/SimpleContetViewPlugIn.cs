// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SimpleContentViewPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Provides a base class for ContentView plugins that want to be templated controls themselves
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Web.UI.ContentView" />
  internal abstract class SimpleContentViewPlugin : SimpleView, IContentViewPlugIn
  {
    /// <summary>Current View (mode) of the ContentView control that hosts this plugin</summary>
    /// <remarks>Same value that was passed as an argument to the InstantiateIn method</remarks>
    private bool initialized;
    private Content content;

    /// <summary>Content item that the plugin works for.</summary>
    /// <remarks>Same value that was passed as an argument to the InstantiateIn method</remarks>
    public Content Content => this.content;
  }
}
