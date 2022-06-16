// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.CommandColumnElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>The configuration element for CommandColumnDefinition</summary>
  public class CommandColumnElement : 
    ColumnElement,
    ICommandColumnDefinition,
    IColumnDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.CommandColumnElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public CommandColumnElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new CommandColumnDefinition((ConfigElement) this);
  }
}
