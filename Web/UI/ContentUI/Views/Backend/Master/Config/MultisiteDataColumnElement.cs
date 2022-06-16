// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.MultisiteDataColumnElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>Configuration element for DataColumnDefinitions</summary>
  public class MultisiteDataColumnElement : DataColumnElement
  {
    /// <summary>
    /// Initializes a new instance of the  <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.MultisiteDataColumnElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public MultisiteDataColumnElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns>The CommandWidgetDefinition</returns>
    public override DefinitionBase GetDefinition()
    {
      if (base.GetDefinition() is DataColumnDefinition definition && SystemManager.CurrentContext.IsOneSiteMode)
      {
        definition.ItemCssClass = "sfDisplayNone";
        definition.HeaderCssClass = "sfDisplayNone";
      }
      return (DefinitionBase) definition;
    }
  }
}
