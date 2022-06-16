// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.ICompositeFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// Defines the contract for CompositeField definition classes and configuration elements
  /// </summary>
  public interface ICompositeFieldDefinition : IFieldDefinition, IDefinition
  {
    /// <summary>
    /// A collection of IFieldControlDefinition objects containing the definitions of all child field controls
    /// of the composite control implementing this contract
    /// </summary>
    /// <value>The field definitions.</value>
    IEnumerable<IFieldControlDefinition> Fields { get; }

    /// <summary>Gets or sets the display mode of the control.</summary>
    /// <value>The display mode.</value>
    FieldDisplayMode DisplayMode { get; set; }

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    HtmlTextWriterTag WrapperTag { get; set; }
  }
}
