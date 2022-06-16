// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Validation;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides all common properties to construct a field control.
  /// </summary>
  public interface IFieldControl : IField, IValidatable, IHasFieldDisplayMode
  {
    /// <summary>
    /// Gets or sets the name of the data item property the control will be bound to.
    /// </summary>
    /// <value>The name of the data field.</value>
    string DataFieldName { get; set; }

    /// <summary>
    /// Gets or sets the type of the data item to which the control will be bound to.
    /// </summary>
    /// <value>The type of the data item.</value>
    string DataItemType { get; set; }

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The value.</value>
    object Value { get; set; }

    /// <summary>Gets or sets the template of the field control.</summary>
    /// <value>The template.</value>
    ConditionalTemplateContainer ConditionalTemplates { get; set; }

    /// <summary>Gets or sets a validation mechanism for the control.</summary>
    /// <value>The validation.</value>
    ValidatorDefinition ValidatorDefinition { get; set; }

    /// <summary>Gets or sets a validation group for the control</summary>
    string ValidationGroup { get; set; }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl.Validator" /> configured with the field controls' <see cref="P:Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl.ValidatorDefinition" />.
    /// </summary>
    Validator Validator { get; }

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    HtmlTextWriterTag WrapperTag { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show message on error.
    /// </summary>
    /// <value><c>true</c> if to show message on error; otherwise, <c>false</c>.</value>
    bool ShowMessageOnError { get; set; }

    /// <summary>
    /// Gets or sets CSS Class that is added to the whole control upon error in validation.
    /// </summary>
    string ControlCssClassOnError { get; set; }
  }
}
