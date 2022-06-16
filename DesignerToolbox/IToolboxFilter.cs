// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DesignerToolbox.IToolboxFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.DesignerToolbox
{
  /// <summary>
  /// Determines which toolbox sections/tools are visible in the <see cref="T:Telerik.Sitefinity.Web.UI.ZoneEditor" />.
  /// Several filters can be registered via the <see cref="!:ObjectFactory" /> and an item will be visible if all registered filters return true for it.
  /// </summary>
  public interface IToolboxFilter
  {
    /// <summary>
    /// Determines whether a toolbox section should be visible in the <see cref="T:Telerik.Sitefinity.Web.UI.ZoneEditor" />.
    /// </summary>
    /// <param name="section">The section in question.</param>
    /// <returns><c>true</c> if it should be visible.</returns>
    bool IsSectionVisible(IToolboxSection section, IToolboxFilterContext context);

    /// <summary>
    /// Determines whether a tool (a.k.a toolbox item) should be visible in the <see cref="T:Telerik.Sitefinity.Web.UI.ZoneEditor" />.
    /// </summary>
    /// <param name="tool">The tool in question.</param>
    /// <returns><c>true</c> if it should be visible.</returns>
    bool IsToolVisible(IToolboxItem tool);
  }
}
