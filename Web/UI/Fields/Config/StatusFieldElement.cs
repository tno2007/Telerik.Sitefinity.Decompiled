// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.StatusFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
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
  /// Providers the configuration element for StatusFieldControl
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "StatusFieldElementDescription", Title = "StatusFieldElementTitle")]
  public class StatusFieldElement : 
    CompositeFieldElement,
    IStatusFieldDefinition,
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.StatusFieldElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public StatusFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal StatusFieldElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new StatusFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the definition for the child DateField control representing an item's publication date
    /// </summary>
    /// <value>The publication date field definition.</value>
    [ConfigurationProperty("publicationDateFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "StatusFieldElementPublicationDateFieldDefinitionDescription", Title = "StatusFieldElementPublicationDateFieldDefinitionCaption")]
    public DateFieldElement PublicationDateFieldDefinition
    {
      get => (DateFieldElement) this["publicationDateFieldDefinition"];
      set => this["publicationDateFieldDefinition"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the definition for the child DateField control representing an item's expiration date
    /// </summary>
    /// <value>The expiration date field definition.</value>
    [ConfigurationProperty("expirationDateFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "StatusFieldElementExpirationDateFieldDefinitionDescription", Title = "StatusFieldElementExpirationDateFieldDefinitionCaption")]
    public DateFieldElement ExpirationDateFieldDefinition
    {
      get => (DateFieldElement) this["expirationDateFieldDefinition"];
      set => this["expirationDateFieldDefinition"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control representing an item's status
    /// </summary>
    /// <value>The status field control definition.</value>
    [ConfigurationProperty("statusFieldControlDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "StatusFieldElementStatusFieldControlDefinitionDescription", Title = "StatusFieldElementStatusFieldControlDefinitionCaption")]
    public ChoiceFieldElement StatusFieldControlDefinition
    {
      get => (ChoiceFieldElement) this["statusFieldControlDefinition"];
      set => this["statusFieldControlDefinition"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the definition for the child DateField control representing an item's publication date
    /// </summary>
    /// <value>The publication date field definition.</value>
    IDateFieldDefinition IStatusFieldDefinition.PublicationDateFieldDefinition => (IDateFieldDefinition) this.PublicationDateFieldDefinition;

    /// <summary>
    /// Gets or sets the definition for the child DateField control representing an item's expiration date
    /// </summary>
    /// <value>The expiration date field definition.</value>
    IDateFieldDefinition IStatusFieldDefinition.ExpirationDateFieldDefinition => (IDateFieldDefinition) this.ExpirationDateFieldDefinition;

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control representing an item's status
    /// </summary>
    /// <value>The status field control definition.</value>
    IChoiceFieldDefinition IStatusFieldDefinition.StatusFieldControlDefinition => (IChoiceFieldDefinition) this.StatusFieldControlDefinition;

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (StatusField);

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public new ConfigElement ConfigDefinition => throw new NotImplementedException();
  }
}
