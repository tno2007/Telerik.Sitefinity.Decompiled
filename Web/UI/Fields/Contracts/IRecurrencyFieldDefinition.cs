// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IRecurrencyFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>Field definition for the recurrency field.</summary>
  public interface IRecurrencyFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>Gets or sets the range start text.</summary>
    string RangeStartText { get; set; }

    /// <summary>Gets or sets the range end text.</summary>
    string RangeEndText { get; set; }
  }
}
