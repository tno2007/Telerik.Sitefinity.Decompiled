// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IDateFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of date field element.
  /// </summary>
  public interface IDateFieldDefinition : IFieldControlDefinition, IFieldDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the name of the field of the DataItem that contains the UtcOffset data.
    /// </summary>
    /// <value>The name of the field.</value>
    string UtcOffsetFiledName { get; set; }

    /// <summary>
    /// Gets or sets the UtcOffset mode of the control. When Client is selected the control returns the selected date and time using the client's utc offset.
    /// When Custom is selected the control returns the selected date and time regarding to the Utc offset specified by the DataItem. The Utc offset is stored into the DataItem's property which name is equal to the UtcOffsetFieldName property value.
    /// </summary>
    /// <value>The UtcOffset mode.</value>
    UtcOffsetMode UtcOffsetMode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable.
    /// </summary>
    bool IsLocalizable { get; set; }
  }
}
