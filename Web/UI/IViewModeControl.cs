// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IViewModeControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Interface for multi view controls.</summary>
  public interface IViewModeControl : IViewHostControl
  {
    /// <summary>The name of the view.</summary>
    string Name { get; set; }

    /// <summary>The title of the view.</summary>
    string Title { get; set; }

    /// <summary>Description of the view.</summary>
    string Description { get; set; }

    /// <summary>Gets or sets the current View Mode.</summary>
    string ViewMode { get; set; }

    /// <summary>
    /// Gets a collection of available Views for this control.
    /// </summary>
    ViewCollection Views { get; }

    /// <summary>Gets the default View Mode name.</summary>
    string DefaultViewMode { get; }

    /// <summary>Gets the key for passing view mode name in the URL.</summary>
    string ViewModeKey { get; }

    /// <summary>Gets the key for passing parameter in the URL.</summary>
    string ParameterKey { get; }

    /// <summary>Gets the parent pageId key.</summary>
    /// <value>The parent pageId key.</value>
    string ParentIdKey { get; }

    /// <summary>
    /// Gets the type from the assembly containing the embedded resources.
    /// Cannot be null reference.
    /// </summary>
    Type AssemblyInfo { get; set; }
  }
}
