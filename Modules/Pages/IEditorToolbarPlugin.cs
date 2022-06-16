// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.IEditorToolbarPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Modules.Pages.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Defines a common interface for pluggable components inside the editor tool bar for pages, forms, newsletters etc.
  /// </summary>
  public interface IEditorToolbarPlugin
  {
    /// <summary>
    /// This method is called when the tool bar is being initialized.
    /// </summary>
    /// <param name="toolBar">The tool bar that this plug-in should initialize for.</param>
    void Initialize(EditorToolBar toolBar);

    /// <summary>
    /// This method is called when the tool bar pre render event is being raised.
    /// </summary>
    /// <param name="toolBar">The tool bar.</param>
    void PreRender(EditorToolBar toolBar);
  }
}
