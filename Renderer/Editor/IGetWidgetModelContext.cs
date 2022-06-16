// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.DefaultGetWidgetModelContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Renderer.Editor
{
  /// <summary>
  /// Class containing information for the currently requested widgets' model.
  /// </summary>
  public class DefaultGetWidgetModelContext : 
    DefaultEditorContext,
    IGetWidgetModelContext,
    IEditorContext
  {
    /// <summary>Gets or sets the id.</summary>
    public string Id { get; set; }
  }
}
