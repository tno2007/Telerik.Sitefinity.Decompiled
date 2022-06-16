// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.LiteralWidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  ///  Represents a configuration element for Sitefinity literal widget elemet.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "LiteralWidgetDescription", Title = "LiteralWidgetTitle")]
  public class LiteralWidgetElement : 
    WidgetElement,
    ILiteralWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> class.
    /// </summary>
    /// <param name="element">The parent element.</param>
    public LiteralWidgetElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new LiteralWidgetDefinition((ConfigElement) this);
  }
}
