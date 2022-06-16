// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.ExpandableFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for expandable field.</summary>
  public class ExpandableFieldElement : 
    FieldControlDefinitionElement,
    IExpandableFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.ExpandableFieldElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ExpandableFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.ExpandableFieldElement" /> class.
    /// </summary>
    internal ExpandableFieldElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ExpandableFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets the definition for the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    /// <value>The expand control.</value>
    [ConfigurationProperty("expandFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ExpandFieldDescription", Title = "ExpandFieldCaption")]
    public ChoiceFieldElement ExpandFieldDefinition
    {
      get => (ChoiceFieldElement) this["expandFieldDefinition"];
      set => this["expandFieldDefinition"] = (object) value;
    }

    /// <summary>
    /// Defines a collection of field definitions that belong to this field.
    /// </summary>
    /// <value>The expandable fields.</value>
    [ConfigurationProperty("expandableFields")]
    [ConfigurationCollection(typeof (FieldControlDefinitionElement), AddItemName = "expandableField")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ExpandableFieldsDescription", Title = "ExpandableFieldsCaption")]
    public ConfigElementDictionary<string, FieldDefinitionElement> ExpandableFields => (ConfigElementDictionary<string, FieldDefinitionElement>) this["expandableFields"];

    IChoiceFieldDefinition IExpandableFieldDefinition.ExpandFieldDefinition => (IChoiceFieldDefinition) this.ExpandFieldDefinition;

    IEnumerable<IFieldDefinition> IExpandableFieldDefinition.ExpandableFields => (IEnumerable<IFieldDefinition>) this.ExpandableFields.Elements.Select<FieldDefinitionElement, FieldDefinition>((Func<FieldDefinitionElement, FieldDefinition>) (configField =>
    {
      FieldDefinition definition = (FieldDefinition) configField.GetDefinition();
      definition.ControlDefinitionName = this.ControlDefinitionName;
      definition.ViewName = this.ViewName;
      definition.FieldName = configField.FieldName;
      definition.SectionName = this.SectionName;
      return definition;
    }));

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (ExpandableField);
  }
}
