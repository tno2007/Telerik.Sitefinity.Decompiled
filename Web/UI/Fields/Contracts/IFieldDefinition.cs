// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// Defines the contract for FieldElement definitions and configuration elements
  /// </summary>
  public interface IFieldDefinition : IDefinition
  {
    string ControlDefinitionName { get; set; }

    string ViewName { get; set; }

    string SectionName { get; set; }

    /// <summary>
    /// Gets or sets the name of the field. This value is used to identify the field.
    /// </summary>
    string FieldName { get; set; }

    /// <summary>Gets or sets the title of the field element</summary>
    /// <value>The title.</value>
    string Title { get; set; }

    /// <summary>Gets or sets the description of the field element.</summary>
    /// <value>The description.</value>
    string Description { get; set; }

    /// <summary>Gets or sets the example of the field element</summary>
    /// <value>The example.</value>
    string Example { get; set; }

    /// <summary>
    /// Gets or sets the type of the control that represents the field.
    /// </summary>
    /// <remarks>
    /// Used for fields that are represented by custom controls.
    /// </remarks>
    Type FieldType { get; set; }

    /// <summary>
    /// Gets or sets the virtual path of the user control that represents the field.
    /// </summary>
    /// <remarks>
    /// Used for fields that are represented by user controls.
    /// </remarks>
    string FieldVirtualPath { get; set; }

    /// <summary>
    /// Gets the default type of the control that represents the field. This property is to be
    /// implemented by specific definitions that have a corresponding control type built in.
    /// </summary>
    Type DefaultFieldType { get; }

    /// <summary>Gets or sets the css class of the field control.</summary>
    /// <value>The css class of the field control.</value>
    string CssClass { get; set; }

    /// <summary>
    /// Gets or sets the name of the resource class used to localize the labels of the field.
    /// </summary>
    /// <remarks>
    /// If this property is left empty, string values such as Title, Description and Example will
    /// be used directly; otherwise the values of these properties will be used as keys for the resources
    /// and localized resources will be loaded instead.
    /// </remarks>
    /// <value>The name of the resource class.</value>
    string ResourceClassId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the field element is hidden.
    /// </summary>
    /// <value>The title.</value>
    bool? Hidden { get; set; }
  }
}
