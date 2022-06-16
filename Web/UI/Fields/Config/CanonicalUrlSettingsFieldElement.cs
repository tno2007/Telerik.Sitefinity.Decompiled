// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.CanonicalUrlSettingsFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// A configuration element that describes a canonical url settings field.
  /// </summary>
  public class CanonicalUrlSettingsFieldElement : 
    CompositeFieldElement,
    ICanonicalUrlSettingsFieldDefinition,
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    private const string CanonicalUrlSettingsChoiceFieldDefinitionKey = "canonicalUrlSettingsChoiceFieldDefinition";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.CanonicalUrlSettingsFieldElement" /> class with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public CanonicalUrlSettingsFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.CanonicalUrlSettingsFieldElement" /> class.
    /// </summary>
    internal CanonicalUrlSettingsFieldElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new CanonicalUrlSettingsFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets the canonical url settings choice field definition.
    /// </summary>
    /// <value>The canonical url settings choice field definition.</value>
    [ConfigurationProperty("canonicalUrlSettingsChoiceFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CanonicalUrlSettingsChoiceFieldDefinitionDescription", Title = "CanonicalUrlSettingsChoiceFieldDefinitionCaption")]
    public ChoiceFieldElement CanonicalUrlSettingsChoiceFieldDefinition
    {
      get => (ChoiceFieldElement) this["canonicalUrlSettingsChoiceFieldDefinition"];
      set => this["canonicalUrlSettingsChoiceFieldDefinition"] = (object) value;
    }

    IChoiceFieldDefinition ICanonicalUrlSettingsFieldDefinition.CanonicalUrlSettingsChoiceFieldDefinition => (IChoiceFieldDefinition) this.CanonicalUrlSettingsChoiceFieldDefinition;
  }
}
