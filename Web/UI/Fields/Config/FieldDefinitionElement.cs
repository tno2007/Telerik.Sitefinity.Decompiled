// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.FieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// Represents a configuration element within a configuration file for each field definition.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionDescription", Title = "FieldDefinitionTitle")]
  public abstract class FieldDefinitionElement : 
    DefinitionConfigElement,
    IFieldDefinition,
    IDefinition
  {
    private string controlDefinitionName;
    private string viewName;
    private string sectionName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.FieldDefinitionElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public FieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.FieldControlDefinitionElement" /> class.
    /// </summary>
    internal FieldDefinitionElement()
    {
    }

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set => this.controlDefinitionName = value;
    }

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public string ViewName
    {
      get => this.viewName;
      set => this.viewName = value;
    }

    /// <summary>Gets or sets the name of the section.</summary>
    /// <value>The name of the section.</value>
    public string SectionName
    {
      get => this.sectionName;
      set => this.sectionName = value;
    }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the text field.
    /// </summary>
    [ConfigurationProperty("expandableDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ExpandableControlElementDescription", Title = "ExpandableControlElementCaption")]
    public ExpandableControlElement ExpandableDefinitionConfig
    {
      get => (ExpandableControlElement) this["expandableDefinition"];
      set => this["expandableDefinition"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the confinguration key that stores this config element within its parent config
    /// </summary>
    /// <value>Configuration key for the config element</value>
    [ConfigurationProperty("fieldName", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionNameDescription", Title = "FieldDefinitionNameCaption")]
    public virtual string FieldName
    {
      get => (string) this["fieldName"];
      set => this["fieldName"] = (object) value;
    }

    /// <summary>Gets or sets the title of the field element</summary>
    /// <value>The title.</value>
    [ConfigurationProperty("title")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionTitleDescription", Title = "FieldDefinitionTitleCaption")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Gets or sets the description of the field element.</summary>
    /// <value>The description.</value>
    [ConfigurationProperty("description")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionDescriptionDescription", Title = "FieldDefinitionDescriptionCaption")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>Gets or sets the example of the field element</summary>
    /// <value>The example.</value>
    [ConfigurationProperty("example")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionExampleDescription", Title = "FieldDefinitionExampleCaption")]
    public string Example
    {
      get => (string) this["example"];
      set => this["example"] = (object) value;
    }

    /// <summary>Gets or sets the type of the field control.</summary>
    /// <value>The type of the field.</value>
    [ConfigurationProperty("fieldType")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionFieldTypeDescription", Title = "FieldDefinitionFieldTypeCaption")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type FieldType
    {
      get => (Type) this["fieldType"];
      set => this["fieldType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the virtual path of the of the field as a user control.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("fieldVirtualPath")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionFieldVirtualPathDescription", Title = "FieldDefinitionFieldVirtualPathCaption")]
    public string FieldVirtualPath
    {
      get => (string) this["fieldVirtualPath"];
      set => this["fieldVirtualPath"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the resource class used to localize the labels of the field.
    /// </summary>
    /// <value>The name of the resource class.</value>
    /// <remarks>
    /// If this property is left empty, string values such as Title, Description and Example will
    /// be used directly; otherwise the values of these properties will be used as keys for the resources
    /// and localized resources will be loaded instead.
    /// </remarks>
    [ConfigurationProperty("resourceClassId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>Gets or sets the css class of the field control.</summary>
    /// <value>The css class of the field control.</value>
    [ConfigurationProperty("cssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CssClassDescription", Title = "CssClassCaption")]
    public string CssClass
    {
      get => (string) this["cssClass"];
      set => this["cssClass"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value indication if a field should be hidden.
    /// </summary>
    /// <value>The hidden.</value>
    [ConfigurationProperty("hidden")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HiddenDescription", Title = "HiddenCaption")]
    public bool? Hidden
    {
      get => (bool?) this["hidden"];
      set => this["hidden"] = (object) value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public virtual Type DefaultFieldType => (Type) null;

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public ConfigElement ConfigDefinition => throw new NotImplementedException();

    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.getDefaultValueHandler = new GetDefaultValue(this.GetDefaultValueHandler);
    }

    private object GetDefaultValueHandler(string propertyName) => propertyName == "fieldType" ? (object) this.DefaultFieldType : (object) null;

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct FieldProps
    {
      public const string FieldName = "fieldName";
      public const string Title = "title";
      public const string Description = "description";
      public const string Example = "example";
      public const string FieldType = "fieldType";
      public const string FieldVirtualPath = "fieldVirtualPath";
      public const string ResourceClassId = "resourceClassId";
      public const string CssClass = "cssClass";
      public const string Hidden = "hidden";
    }
  }
}
