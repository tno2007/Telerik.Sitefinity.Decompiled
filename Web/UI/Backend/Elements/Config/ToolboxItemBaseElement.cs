// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ToolboxItemBaseElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Diagnostics;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Represents a configuration element for Sitefinity ToolboxItemBase element.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemBaseDescription", Title = "ToolboxItemBaseTitle")]
  [DebuggerDisplay("ToolboxItemBaseElement")]
  public class ToolboxItemBaseElement : DefinitionConfigElement, IToolboxItemBaseDefinition
  {
    private string controlDefinitionName;
    private string viewName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ToolboxItemBaseElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ToolboxItemBaseElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ToolboxItemBaseDefinition((ConfigElement) this);

    public string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set => this.controlDefinitionName = value;
    }

    public string ViewName
    {
      get => this.viewName;
      set => this.viewName = value;
    }

    [ConfigurationProperty("ContainerId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemBaseContainerIdDescription", Title = "ToolboxItemBaseContainerIdCaption")]
    public string ContainerId
    {
      get => this[nameof (ContainerId)] as string;
      set => this[nameof (ContainerId)] = (object) value;
    }

    [ConfigurationProperty("CssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemBaseCssClassDescription", Title = "ToolboxItemBaseCssClassCaption")]
    public string CssClass
    {
      get => this[nameof (CssClass)] as string;
      set => this[nameof (CssClass)] = (object) value;
    }

    [ConfigurationProperty("ItemTemplatePath")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemBaseItemTemplatePathDescription", Title = "ToolboxItemBaseItemTemplatePathCaption")]
    public string ItemTemplatePath
    {
      get => this[nameof (ItemTemplatePath)] as string;
      set => this[nameof (ItemTemplatePath)] = (object) value;
    }

    [ConfigurationProperty("Visible", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemBaseVisibleDescription", Title = "ToolboxItemBaseVisibleCaption")]
    public bool Visible
    {
      get => (bool) this[nameof (Visible)];
      set => this[nameof (Visible)] = (object) value;
    }

    [ConfigurationProperty("WrapperTagCssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemBaseWrapperTagCssClassDescription", Title = "ToolboxItemBaseWrapperTagCssClassCaption")]
    public string WrapperTagCssClass
    {
      get => this[nameof (WrapperTagCssClass)] as string;
      set => this[nameof (WrapperTagCssClass)] = (object) value;
    }

    [ConfigurationProperty("WrapperTagId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemBaseWrapperTagIdDescription", Title = "ToolboxItemBaseWrapperTagIdCaption")]
    public string WrapperTagId
    {
      get => this[nameof (WrapperTagId)] as string;
      set => this[nameof (WrapperTagId)] = (object) value;
    }

    [ConfigurationProperty("WrapperTagName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxItemBaseWrapperTagNameDescription", Title = "ToolboxItemBaseWrapperTagNameCaption")]
    public string WrapperTagName
    {
      get => this[nameof (WrapperTagName)] as string;
      set => this[nameof (WrapperTagName)] = (object) value;
    }
  }
}
