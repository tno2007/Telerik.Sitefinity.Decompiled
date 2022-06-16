// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.MultiFlatSelectorDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>
  /// Implementation of IMultiFlatSelectorDefinition that is usable in the template parser for declarative programming
  /// </summary>
  [ParseChildren(ChildrenAsProperties = true)]
  public class MultiFlatSelectorDefinition : 
    WidgetDefinition,
    IMultiFlatSelectorDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public MultiFlatSelectorDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public MultiFlatSelectorDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public MultiFlatSelectorDefinition GetDefinition() => this;
  }
}
