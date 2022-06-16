// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DesignerToolbox.IToolboxFilterContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.DesignerToolbox
{
  /// <summary>
  /// Determines which toolbox sections/tools are visible in the <see cref="T:Telerik.Sitefinity.Web.UI.ZoneEditor" />.
  /// Several filters can be registered via the <see cref="!:ObjectFactory" /> and an item will be visible if all registered filters return true for it.
  /// </summary>
  public interface IToolboxFilterContext
  {
    /// <summary>Gets a value the media type of the context.</summary>
    DesignMediaType MediaType { get; }

    /// <summary>
    /// Gets the container id.
    /// Possible values: "LayoutToolboxContainer", "ControlToolboxContainer".
    /// </summary>
    string ContainerId { get; }
  }
}
