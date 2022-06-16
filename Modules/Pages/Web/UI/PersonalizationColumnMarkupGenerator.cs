// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PersonalizationColumnMarkupGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Generates the markup for personalization column</summary>
  public class PersonalizationColumnMarkupGenerator : Control, IDynamicMarkupGenerator
  {
    /// <summary>
    /// Initialize properties of the markup generator implementing <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicMarkupGenerator" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public virtual void Configure(IDynamicMarkupGeneratorDefinition definition)
    {
    }

    /// <summary>Generates HTML markup for a dynamic column.</summary>
    /// <returns>The generated HTML markup.</returns>
    public virtual string GetMarkup() => "<div sys:class=\"{{ ($dataItem.IsPersonalized || $dataItem.HasPersonalizedWidgets) ? 'sfInlineBlock sfMRight5 sfPersonalizedLabel' : 'sfDisplayNone sfPersonalizedLabel'  }}\">Personalized</div>";
  }
}
