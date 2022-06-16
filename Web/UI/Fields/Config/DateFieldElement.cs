// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.DateFieldElement
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
  /// <summary>A configuration element that describes a date field.</summary>
  public class DateFieldElement : 
    FieldControlDefinitionElement,
    IDateFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public DateFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new DateFieldDefinition((ConfigElement) this);

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (DateField);

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public new ConfigElement ConfigDefinition => throw new NotImplementedException();

    /// <summary>
    /// Gets or sets the name of the field of the DataItem that contains the UtcOffset data.
    /// </summary>
    /// <value>The name of the field.</value>
    [ConfigurationProperty("UtcOffsetFiledName")]
    public string UtcOffsetFiledName
    {
      get => (string) this[nameof (UtcOffsetFiledName)];
      set => this[nameof (UtcOffsetFiledName)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the UtcOffset mode of the control. When Client is selected the control returns the selected date and time using the client's utc offset.
    /// When Custom is selected the control returns the selected date and time regarding to the Utc offset specified by the DataItem. The Utc offset is stored into the DataItem's property which name is equal to the UtcOffsetFieldName property value.
    /// </summary>
    /// <value>The UtcOffset mode.</value>
    [ConfigurationProperty("UtcOffsetMode")]
    public UtcOffsetMode UtcOffsetMode
    {
      get => (UtcOffsetMode) this[nameof (UtcOffsetMode)];
      set => this[nameof (UtcOffsetMode)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable.
    /// </summary>
    [ConfigurationProperty("isLocalizable")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsLocalizableDescription", Title = "IsLocalizableCaption")]
    public bool IsLocalizable
    {
      get => (bool) this["isLocalizable"];
      set => this["isLocalizable"] = (object) value;
    }
  }
}
