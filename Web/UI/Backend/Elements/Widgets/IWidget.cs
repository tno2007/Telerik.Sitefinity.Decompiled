// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.IWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>Base interface for defining widgets.</summary>
  public interface IWidget
  {
    /// <summary>Gets or sets the definition.</summary>
    /// <value>The definition.</value>
    IWidgetDefinition Definition { get; set; }

    void Configure(IWidgetDefinition definition);
  }
}
