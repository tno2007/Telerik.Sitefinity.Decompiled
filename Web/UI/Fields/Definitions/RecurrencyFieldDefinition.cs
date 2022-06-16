// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.RecurrencyFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  public class RecurrencyFieldDefinition : 
    FieldControlDefinition,
    IRecurrencyFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string rangeStartText;
    private string rangeEndText;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.RecurrencyFieldDefinition" /> class.
    /// </summary>
    public RecurrencyFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.RecurrencyFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public RecurrencyFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the range start text.</summary>
    public string RangeStartText
    {
      get => this.ResolveProperty<string>(nameof (RangeStartText), this.rangeStartText);
      set => this.rangeStartText = value;
    }

    /// <summary>Gets or sets the range end text.</summary>
    public string RangeEndText
    {
      get => this.ResolveProperty<string>(nameof (RangeEndText), this.rangeEndText);
      set => this.rangeEndText = value;
    }
  }
}
