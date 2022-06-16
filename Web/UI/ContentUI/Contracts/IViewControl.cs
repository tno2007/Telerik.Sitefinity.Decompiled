// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IViewControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.ContentUI.Contracts
{
  /// <summary>
  /// Declares the contract for control views that display all types inheriting from the abstract
  /// type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
  /// </summary>
  public interface IViewControl
  {
    /// <summary>
    /// Gets or sets the container for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <value>The host.</value>
    ContentView Host { get; set; }

    /// <summary>Gets or sets the content view definition.</summary>
    /// <value>The definition.</value>
    IContentViewDefinition Definition { get; set; }

    string LayoutTemplatePath { get; set; }

    /// <summary>
    /// Gets a value indicating whether this view is empty, e.g. renders no objects.
    /// This is used with public controls to determine whether to display an icon+link
    /// in design mode.
    /// </summary>
    bool IsEmptyView { get; }
  }
}
