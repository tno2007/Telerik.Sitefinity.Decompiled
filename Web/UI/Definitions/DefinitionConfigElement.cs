// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Definitions.DefinitionConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI.Definitions
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="TDefinition">The type of the definition.</typeparam>
  public abstract class DefinitionConfigElement : ConfigElement, IDefinition, IModuleDependentItem
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:FieldDefinitionElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public DefinitionConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:FieldControlDefinitionElement" /> class.
    /// </summary>
    internal DefinitionConfigElement()
      : base(false)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public abstract DefinitionBase GetDefinition();

    /// <summary>Gets the definition.</summary>
    /// <typeparam name="TDefinition">The type of the definition.</typeparam>
    /// <returns></returns>
    public TDefinition GetDefinition<TDefinition>() where TDefinition : DefinitionBase, new() => (TDefinition) this.GetDefinition();

    protected internal virtual IDefinition ToDefinition() => (IDefinition) this;

    /// <summary>Gets or sets the name of the module.</summary>
    [ConfigurationProperty("moduleName", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ModuleNameDescription", Title = "ModuleNameCaption")]
    public string ModuleName
    {
      get => (string) this["moduleName"];
      set => this["moduleName"] = (object) value;
    }
  }
}
