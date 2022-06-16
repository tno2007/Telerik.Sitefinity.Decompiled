// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DesignerToolbox.DefaultToolboxFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.DesignerToolbox
{
  /// <summary>
  /// The default <see cref="T:Telerik.Sitefinity.DesignerToolbox.IToolboxFilter" /> implementation.
  /// It makes visible the enabled section/items.
  /// Sectin/items marked with the <see cref="F:Telerik.Sitefinity.Modules.Pages.Configuration.ToolboxTags.Backend" /> tag are made visible only when editing backend pages.
  /// </summary>
  internal class DefaultToolboxFilter : IToolboxFilter
  {
    /// <inheritdoc />
    public virtual bool IsSectionVisible(IToolboxSection section, IToolboxFilterContext context) => section != null && section.Enabled && this.ApplyBackendFilter(section.Tags);

    /// <inheritdoc />
    public virtual bool IsToolVisible(IToolboxItem tool) => tool.Enabled && this.ApplyBackendFilter(tool.Tags);

    private bool ApplyBackendFilter(ISet<string> tags)
    {
      if (tags == null || !tags.Contains("backend"))
        return true;
      PageSiteNode actualCurrentNode = SiteMapBase.GetActualCurrentNode();
      return actualCurrentNode != null && actualCurrentNode.IsBackend;
    }
  }
}
