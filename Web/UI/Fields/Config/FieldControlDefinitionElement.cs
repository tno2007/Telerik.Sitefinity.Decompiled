// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.FieldControlDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Validation.Config;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// Represents a configuration element within a configuration file for each field definition.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldControlDefinitionDescription", Title = "FieldControlDefinitionTitle")]
  public abstract class FieldControlDefinitionElement : 
    FieldDefinitionElement,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.FieldControlDefinitionElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public FieldControlDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.FieldControlDefinitionElement" /> class.
    /// </summary>
    internal FieldControlDefinitionElement()
    {
    }

    /// <summary>
    /// Gets or sets the programmatic identifier assigned to the field control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The programmatic identifier assigned to the field control.
    /// </returns>
    [ConfigurationProperty("id")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldControlIDDescription", Title = "FieldControlIDCaption")]
    public string ID
    {
      get => (string) this["id"];
      set => this["id"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the data item property the control will be bound to.
    /// </summary>
    /// <value>The name of the data field.</value>
    [ConfigurationProperty("dataFieldName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionDataFieldNameDescription", Title = "FieldDefinitionDataFieldNameCaption")]
    public string DataFieldName
    {
      get => (string) this["dataFieldName"];
      set
      {
        this["dataFieldName"] = (object) value;
        if (!string.IsNullOrEmpty(this.FieldName))
          return;
        int length = value.IndexOf('.');
        if (length > 0)
          this.FieldName = value.Substring(0, length);
        else
          this.FieldName = value;
      }
    }

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("value")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionValueDescription", Title = "FieldDefinitionValueCaption")]
    public object Value
    {
      get => this["value"];
      set => this[nameof (value)] = value;
    }

    /// <summary>Gets or sets the display mode of the control.</summary>
    /// <value>The display mode.</value>
    [ConfigurationProperty("displayMode")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionDisplayModeDescription", Title = "FieldDefinitionDisplayModeCaption")]
    public FieldDisplayMode? DisplayMode
    {
      get => (FieldDisplayMode?) this["displayMode"];
      set => this["displayMode"] = (object) value;
    }

    /// <summary>Gets or sets a validation mechanism for the control.</summary>
    /// <value>The validation.</value>
    [ConfigurationProperty("validator")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldDefinitionValidationDescription", Title = "FieldDefinitionValidationCaption")]
    public ValidatorDefinitionElement ValidatorConfig
    {
      get => (ValidatorDefinitionElement) this["validator"];
      set => this["validator"] = (object) value;
    }

    /// <summary>Gets or sets a validation mechanism for the control.</summary>
    /// <value>The validation.</value>
    public IValidatorDefinition Validation => (IValidatorDefinition) this.ValidatorConfig;

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
    internal new struct FieldProps
    {
      public const string ID = "id";
      public const string DataFieldName = "dataFieldName";
      public const string Value = "value";
      public const string DisplayMode = "displayMode";
      public const string Validator = "validator";
      public const string WrapperTag = "wrapperTag";
    }
  }
}
