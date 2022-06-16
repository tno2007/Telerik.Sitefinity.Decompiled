// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.ChoiceFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// A configuration element that describes a choice field.
  /// </summary>
  public class ChoiceFieldElement : 
    FieldControlDefinitionElement,
    IChoiceFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private List<IChoiceDefinition> choices;

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ChoiceFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.FieldControlDefinitionElement" /> class.
    /// </summary>
    internal ChoiceFieldElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ChoiceFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.ChoiceElement" /> objects.
    /// </summary>
    /// <value>The collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.ChoiceElement" /> objects.</value>
    [ConfigurationProperty("choicesConfig")]
    [ConfigurationCollection(typeof (ChoiceElement), AddItemName = "element")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ChoicesConfigDescription", Title = "ChoicesConfigTitle")]
    public ConfigElementList<ChoiceElement> ChoicesConfig => (ConfigElementList<ChoiceElement>) this["choicesConfig"];

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IChoiceDefinition" /> objects, representing the choices
    /// that the control ought to render.
    /// </summary>
    /// <value></value>
    public List<IChoiceDefinition> Choices
    {
      get
      {
        if (this.choices == null)
          this.choices = this.ChoicesConfig.Elements.Select<ChoiceElement, IChoiceDefinition>((Func<ChoiceElement, IChoiceDefinition>) (ch => (IChoiceDefinition) ch.ToDefinition())).ToList<IChoiceDefinition>();
        return this.choices;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether more than one choice can be made. If only one choice
    /// can be made true; otherwise false.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("mutuallyExclusive", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MutuallyExclusiveDescription", Title = "MutuallyExclusiveTitle")]
    public bool MutuallyExclusive
    {
      get => (bool) this["mutuallyExclusive"];
      set => this["mutuallyExclusive"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value indicating how should the choices be rendered.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("renderChoiceAs", DefaultValue = RenderChoicesAs.CheckBoxes)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RenderChoiceAsDescription", Title = "RenderChoiceAsTitle")]
    public RenderChoicesAs RenderChoiceAs
    {
      get => (RenderChoicesAs) this["renderChoiceAs"];
      set => this["renderChoiceAs"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value indicating which choice(s) is currently selected.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("selectedChoicesIndex")]
    [TypeConverter(typeof (StringListConverter<int>))]
    [ConfigurationCollection(typeof (int), AddItemName = "index")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SelectedChoicesIndexDescription", Title = "SelectedChoicesIndexTitle")]
    public ICollection<int> SelectedChoicesIndex
    {
      get => (ICollection<int>) this["selectedChoicesIndex"];
      set => this["selectedChoicesIndex"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("hideTitle", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HideTitleDescription", Title = "HideTitleCaption")]
    public bool HideTitle
    {
      get => (bool) this["hideTitle"];
      set => this["hideTitle"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value which indicates that the values returned from the
    /// field control (client side) should always be returned in an array of strings,
    /// regardless if one or more choices have been selected.
    /// </summary>
    [ConfigurationProperty("returnValuesAlwaysInArray", DefaultValue = false)]
    public bool ReturnValuesAlwaysInArray
    {
      get => (bool) this["returnValuesAlwaysInArray"];
      set => this["returnValuesAlwaysInArray"] = (object) value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (ChoiceField);

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public new ConfigElement ConfigDefinition => throw new NotImplementedException();
  }
}
