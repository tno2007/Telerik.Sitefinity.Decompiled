// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeItemFieldContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.DynamicModules.Builder.Data
{
  /// <summary>
  /// This class represents the state of the content type item field created or edited through the
  /// wizard for creating or editing a content type.
  /// </summary>
  [DebuggerDisplay("Field name : {Name}")]
  [DataContract]
  public class ContentTypeItemFieldContext
  {
    /// <summary>Gets or sets the name of the field.</summary>
    /// <remarks>
    /// This is the programmatic name and will not be displayed in the
    /// user interface. This value must be unique inside of a type.
    /// </remarks>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the title of the field.</summary>
    /// <remarks>
    /// This value will be used to in the user interface to
    /// identify the field.
    /// </remarks>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the media type of the field.</summary>
    [DataMember]
    public string MediaType { get; set; }

    /// <summary>
    /// Gets or sets the name of the type of the content type item field.
    /// </summary>
    [DataMember]
    public string TypeName { get; set; }

    /// <summary>Gets or sets number unit text</summary>
    [DataMember]
    public string NumberUnit { get; set; }

    /// <summary>
    /// Gets or sets the value indicating wheter the field is transient or not
    /// </summary>
    [DataMember]
    public bool IsTransient { get; set; }

    /// <summary>
    /// Gets or sets the value indicating wheter
    /// the yes/no checkbox field will be checked by default
    /// </summary>
    [DataMember]
    public bool CheckedByDefault { get; set; }

    /// <summary>
    /// Gets or sets if the user can select/upload more than one image.
    /// </summary>
    [DataMember]
    public bool AllowMultipleImages { get; set; }

    /// <summary>Gets or sets if the user can attach image library</summary>
    [DataMember]
    public bool AllowImageLibrary { get; set; }

    /// <summary>
    /// Gets or sets if the user can select/upload more than one video
    /// </summary>
    [DataMember]
    public bool AllowMultipleVideos { get; set; }

    /// <summary>
    /// Gets or sets if the user can select/upload more than one file
    /// </summary>
    [DataMember]
    public bool AllowMultipleFiles { get; set; }

    /// <summary>
    /// Gets or sets the value of the checkbox for "ddl is required"
    /// </summary>
    [DataMember]
    public bool IsRequiredToSelectDdlValue { get; set; }

    /// <summary>
    /// Gets or sets the default value index of the drop down choice field
    /// </summary>
    [DataMember]
    public int DropDownListDefaulValueIndex { get; set; }

    /// <summary>
    /// Gets or sets the value of the checkbox for "at least one checkbox must be selected"
    /// </summary>
    /// Modu
    [DataMember]
    public bool IsRequiredToSelectCheckbox { get; set; }

    /// <summary>Gets or sets instructional choice</summary>
    [DataMember]
    public string InstructionalChoice { get; set; }

    /// <summary>
    /// Gets or sets the value of the regular expression for the field definitionModu
    /// </summary>
    [DataMember]
    public string RegularExpression { get; set; }

    /// <summary>Gets or sets the allowed image extensions</summary>
    [DataMember]
    public string ImageExtensions { get; set; }

    /// <summary>Gets or sets the allowed file extensions</summary>
    [DataMember]
    public string FileExtensions { get; set; }

    /// <summary>Gets or sets the allowed video extensions</summary>
    [DataMember]
    public string VideoExtensions { get; set; }

    /// <summary>Allow creating classification items while selecting</summary>
    [DataMember]
    public bool CanCreateItemsWhileSelecting { get; set; }

    /// <summary>
    /// Set to true if multiple classification items can be selected
    /// </summary>
    [DataMember]
    public bool CanSelectMultipleItems { get; set; }

    /// <summary>Contains the render as choice type</summary>
    [DataMember]
    public string ChoiceRenderType { get; set; }

    /// <summary>
    /// Gets or sets the visibility of the field in the user interface. If true
    /// field is hidden, otherwise false.
    /// </summary>
    [DataMember]
    public bool IsHiddenField { get; set; }

    /// <summary>
    /// Gets or sets the type of the widget to be used in the backend details view
    /// to represent this field.
    /// </summary>
    [DataMember]
    public string WidgetTypeName { get; set; }

    /// <summary>
    /// Gets or sets the type of the widget to be used in the frontend details view
    /// to represent this field.
    /// </summary>
    [DataMember]
    public string FrontendWidgetTypeName { get; set; }

    /// <summary>
    /// Gets or sets the label of the widget to be used in the frontend details view
    /// to represent this field.
    /// </summary>
    [DataMember]
    public string FrontendWidgetLabel { get; set; }

    /// <summary>Gets or sets the database type of the field.</summary>
    [DataMember]
    public string DBType { get; set; }

    /// <summary>Gets or sets the classification of the field.</summary>
    [DataMember]
    public Guid ClassificationId { get; set; }

    /// <summary>
    /// Gets or sets the number of decimal places allowed in case
    /// the field is a number.
    /// </summary>
    [DataMember]
    public int DecimalPlacesCount { get; set; }

    /// <summary>Gets or sets the max size of an image</summary>
    [DataMember]
    public int ImageMaxSize { get; set; }

    /// <summary>Gets or sets the max size of a file</summary>
    [DataMember]
    public int FileMaxSize { get; set; }

    /// <summary>Gets or sets the max size of a video</summary>
    [DataMember]
    public int VideoMaxSize { get; set; }

    /// <summary>Gets or sets the database length of the field.</summary>
    /// <remarks>Used where applies.</remarks>
    [DataMember]
    public string DBLength { get; set; }

    /// <summary>
    /// Gets or sets value indicating weather null values can be inserted in the database.
    /// </summary>
    [DataMember]
    public bool AllowNulls { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather the field should be indexed.
    /// </summary>
    [DataMember]
    public bool IncludeInIndexes { get; set; }

    /// <summary>
    /// Gets or sets the name of the column in database for this field.
    /// </summary>
    [DataMember]
    public string ColumnName { get; set; }

    /// <summary>Gets or sets the instructionalText text of the field.</summary>
    [DataMember]
    public string InstructionalText { get; set; }

    /// <summary>Gets or sets the default value of the field.</summary>
    [DataMember]
    public string DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets the value which indicates if the field is required. If true
    /// field is required; otherwise field is not required.
    /// </summary>
    [DataMember]
    public bool IsRequired { get; set; }

    /// <summary>
    /// Gets or sets the minimum length accepted for this field.
    /// </summary>
    [DataMember]
    public int? MinLength { get; set; }

    /// <summary>
    /// Gets or sets the maximum length accepted for this field.
    /// </summary>
    [DataMember]
    public int? MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the minimum length accepted for this field.
    /// </summary>
    [DataMember]
    public string MinNumberRange { get; set; }

    /// <summary>
    /// Gets or sets the maximum length accepted for this field.
    /// </summary>
    [DataMember]
    public string MaxNumberRange { get; set; }

    /// <summary>
    /// Gets or sets the message to be displayed if the entered value
    /// is not inside of valid length (min and max length values).
    /// </summary>
    [DataMember]
    public string LengthValidationMessage { get; set; }

    /// <summary>
    /// Gets or sets the comma separated list of choices if the field
    /// is a multiple choice field.
    /// </summary>
    [DataMember]
    public string Choices { get; set; }

    /// <summary>Gets or sets the friendly name of the field type.</summary>
    [DataMember]
    public string TypeUIName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable
    /// </summary>
    [DataMember]
    public bool IsLocalizable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to disable the link parser for this field.
    /// </summary>
    [DataMember]
    public bool DisableLinkParser { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the address field mode.
    /// </summary>
    [DataMember]
    public string AddressFieldMode { get; set; }

    /// <summary>Gets or sets the id of the field.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the ordinal of the field.</summary>
    [DataMember]
    public int Ordinal { get; set; }

    /// <summary>
    /// Gets or sets the id of the parent section to which this fields belong to.
    /// </summary>
    [DataMember]
    public Guid ParentSectionId { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather the item should be displayed in the
    /// backend grid.
    /// </summary>
    [DataMember]
    public bool ShowInGrid { get; set; }

    /// <summary>
    /// Gets or sets the ordinal of the column in the backend grid.
    /// </summary>
    /// <remarks>
    /// Applies only if the ShowInGrid property is set to true.
    /// </remarks>
    [DataMember]
    public int GridColumnOrdinal { get; set; }

    /// <summary>Gets or sets the type of the related content</summary>
    [DataMember]
    public string RelatedDataType { get; set; }

    /// <summary>Gets or sets the provider of the related content</summary>
    [DataMember]
    public string RelatedDataProvider { get; set; }

    /// <summary>Gets or sets the origin.</summary>
    /// <value>The origin.</value>
    [DataMember]
    public string Origin { get; set; }

    /// <summary>
    /// Gets or sets the recommended characters count for this field.
    /// </summary>
    [DataMember]
    public int? RecommendedCharactersCount { get; set; }
  }
}
