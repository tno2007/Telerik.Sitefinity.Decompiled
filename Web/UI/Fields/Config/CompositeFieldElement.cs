// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.CompositeFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// Providers the configuration element for CompositeFieldControl
  /// </summary>
  public class CompositeFieldElement : 
    FieldDefinitionElement,
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.CompositeFieldElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public CompositeFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.CompositeFieldElement" /> class.
    /// </summary>
    internal CompositeFieldElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new CompositeFieldDefinition((ConfigElement) this);

    /// <summary>
    /// A collection of IFieldControlDefinition objects containing the definitions of all child field controls
    /// of the composite control implementing this contract
    /// </summary>
    /// <value>The field definitions.</value>
    [ConfigurationProperty("fields")]
    [ConfigurationCollection(typeof (FieldControlDefinitionElement), AddItemName = "field")]
    public ConfigElementDictionary<string, FieldControlDefinitionElement> Fields => (ConfigElementDictionary<string, FieldControlDefinitionElement>) this["fields"];

    /// <summary>Gets or sets the title of the field element</summary>
    /// <value>The title.</value>
    [ConfigurationProperty("title")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CompositeFieldDefinitionTitleDescription", Title = "CompositeFieldDefinitionTitleCaption")]
    public new string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Gets or sets the description of the field element.</summary>
    /// <value>The description.</value>
    [ConfigurationProperty("description")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CompositeFieldDefinitionDescriptionDescription", Title = "CompositeFieldDefinitionDescriptionCaption")]
    public new string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>Gets or sets the example of the field element</summary>
    /// <value>The example.</value>
    [ConfigurationProperty("example")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CompositeFieldDefinitionExampleDescription", Title = "CompositeFieldDefinitionExampleCaption")]
    public new string Example
    {
      get => (string) this["example"];
      set => this["example"] = (object) value;
    }

    /// <summary>Gets or sets the display mode of the control.</summary>
    /// <value>The display mode.</value>
    [ConfigurationProperty("displayMode", DefaultValue = FieldDisplayMode.Read)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionDisplayModeDescription", Title = "FieldDefinitionDisplayModeCaption")]
    public FieldDisplayMode DisplayMode
    {
      get => (FieldDisplayMode) this["displayMode"];
      set => this["displayMode"] = (object) value;
    }

    /// <summary>
    /// A collection of IFieldControlDefinition objects containing the definitions of all child field controls
    /// of the composite control implementing this contract
    /// </summary>
    /// <value>The field definitions.</value>
    IEnumerable<IFieldControlDefinition> ICompositeFieldDefinition.Fields => this.Fields.Cast<IFieldControlDefinition>();

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    [ConfigurationProperty("wrapperTag", DefaultValue = HtmlTextWriterTag.Li)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WrapperTagDescription", Title = "WrapperTagCaption")]
    public HtmlTextWriterTag WrapperTag
    {
      get => (HtmlTextWriterTag) this["wrapperTag"];
      set => this["wrapperTag"] = (object) value;
    }

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public new ConfigElement ConfigDefinition => throw new NotImplementedException();

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct CompositeFieldProps
    {
      public const string Description = "description";
      public const string Example = "example";
      public const string Title = "title";
      public const string DisplayMode = "displayMode";
      public const string WrapperTag = "wrapperTag";
    }
  }
}
