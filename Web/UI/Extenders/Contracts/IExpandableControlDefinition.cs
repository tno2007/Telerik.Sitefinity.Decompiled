// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Extenders.Contracts.IExpandableControlDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Extenders.Contracts
{
  /// <summary>
  /// Interface that defines the members of the expandable control definition types.
  /// </summary>
  public interface IExpandableControlDefinition
  {
    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    string ControlDefinitionName { get; set; }

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    string ViewName { get; set; }

    /// <summary>Gets or sets the name of the section.</summary>
    /// <value>The name of the section.</value>
    string SectionName { get; set; }

    /// <summary>Gets or sets the name of the field.</summary>
    /// <value>The name of the field.</value>
    string FieldName { get; set; }

    /// <summary>
    /// Gets or sets the text to be displayed on the element that expands the control.
    /// </summary>
    string ExpandText { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the control is expanded by default. If control
    /// is to be expanded by default true; otherwise false.
    /// </summary>
    bool? Expanded { get; set; }

    /// <summary>
    /// Gets or sets the resource class used to localize messages.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// If this property is set; text properties will be treated as
    /// keys of localized entry in the localization class; otherwise they will be displayed
    /// as is.
    /// </remarks>
    string ResourceClassId { get; set; }
  }
}
