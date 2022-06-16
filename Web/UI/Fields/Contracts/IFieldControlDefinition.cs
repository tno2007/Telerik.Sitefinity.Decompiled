// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControlDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// Defines the mandated members that need to be implemented by every type that
  /// represents a field definition for the controls that implements <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" /> interface.
  /// </summary>
  public interface IFieldControlDefinition : IFieldDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the programmatic identifier assigned to the field control.
    /// </summary>
    /// <returns>
    /// The programmatic identifier assigned to the field control.
    /// </returns>
    string ID { get; set; }

    /// <summary>
    /// Gets or sets the name of the data item property the control will be bound to.
    /// </summary>
    /// <value>The name of the data field.</value>
    string DataFieldName { get; set; }

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The value.</value>
    object Value { get; set; }

    /// <summary>Gets or sets the display mode of the control.</summary>
    /// <value>The display mode.</value>
    FieldDisplayMode? DisplayMode { get; set; }

    /// <summary>Gets or sets a validation mechanism for the control.</summary>
    /// <value>The validation.</value>
    IValidatorDefinition Validation { get; }

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    HtmlTextWriterTag WrapperTag { get; set; }
  }
}
