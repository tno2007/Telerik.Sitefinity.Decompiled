// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.DateFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// Contains all properties needed to construct a DateField control
  /// </summary>
  public class DateFieldDefinition : 
    FieldControlDefinition,
    IDateFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string utcOffsetFiledName;
    private UtcOffsetMode utcOffsetMode;
    private bool isLocalizable;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ColumnDefinition" /> class.
    /// </summary>
    public DateFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DateFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (DateField);

    /// <summary>
    /// Gets or sets the name of the field of the DataItem that contains the UtcOffset data.
    /// </summary>
    /// <value>The name of the field.</value>
    public string UtcOffsetFiledName
    {
      get => this.ResolveProperty<string>(nameof (UtcOffsetFiledName), this.utcOffsetFiledName);
      set => this.utcOffsetFiledName = value;
    }

    /// <summary>
    /// Gets or sets the UtcOffset mode of the control. When Client is selected the control returns the selected date and time using the client's utc offset.
    /// When Custom is selected the control returns the selected date and time regarding to the Utc offset specified by the DataItem. The Utc offset is stored into the DataItem's property which name is equal to the UtcOffsetFieldName property value.
    /// </summary>
    /// <value>The UtcOffset mode.</value>
    public UtcOffsetMode UtcOffsetMode
    {
      get => this.ResolveProperty<UtcOffsetMode>(nameof (UtcOffsetMode), this.utcOffsetMode);
      set => this.utcOffsetMode = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable.
    /// </summary>
    public bool IsLocalizable
    {
      get => this.ResolveProperty<bool>(nameof (IsLocalizable), this.isLocalizable);
      set => this.isLocalizable = value;
    }
  }
}
