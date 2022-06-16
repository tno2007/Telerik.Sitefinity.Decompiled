// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common properties for all field controls.
  /// </summary>
  public interface IField
  {
    /// <summary>Gets or sets the description of the field control.</summary>
    /// <value>The description of the field control.</value>
    string Description { get; set; }

    /// <summary>
    /// Gets or sets the example text associated with the field control.
    /// </summary>
    /// <value>The example text associated with the field control.</value>
    string Example { get; set; }

    /// <summary>Gets or sets the title of the field control.</summary>
    /// <value>The title of the field control.</value>
    string Title { get; set; }

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
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    void Configure(IFieldDefinition definition);
  }
}
