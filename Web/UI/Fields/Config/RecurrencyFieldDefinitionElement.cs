// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.RecurrencyFieldDefinitionElement
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
  /// <summary>Configuration element for recurrency field.</summary>
  public class RecurrencyFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IRecurrencyFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.RecurrencyFieldDefinitionElement" /> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    public RecurrencyFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new RecurrencyFieldDefinition((ConfigElement) this);

    /// <summary>Gets the default type of the field.</summary>
    public override Type DefaultFieldType => typeof (RecurrencyField);

    /// <summary>Gets or sets the range start text.</summary>
    [ConfigurationProperty("rangeStartText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RecurrencyFieldRangeStartTextDescription", Title = "RecurrencyFieldRangeStartTextTitle")]
    public string RangeStartText
    {
      get => (string) this["rangeStartText"];
      set => this["rangeStartText"] = (object) value;
    }

    /// <summary>Gets or sets the range end text.</summary>
    [ConfigurationProperty("rangeEndText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RecurrencyFieldRangeEndTextDescription", Title = "RecurrencyFieldRangeEndTextTitle")]
    public string RangeEndText
    {
      get => (string) this["rangeEndText"];
      set => this["rangeEndText"] = (object) value;
    }
  }
}
