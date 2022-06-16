// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.WcfFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI.Extenders.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  /// <summary>
  /// Defines the properties of the object for managing the data structure type and the backend user interface of the modules.
  /// The type is used for transferring the data through WCF when changes at the client are being saved.
  /// </summary>
  [DataContract]
  public class WcfFieldDefinition
  {
    private ITaxonomy taxonomy;
    private ChoiceDefinition[] choiceDefinitions;

    /// <summary>Gets or sets the name of the section.</summary>
    /// <value>The name of the section.</value>
    [DataMember]
    public string SectionName { get; set; }

    /// <summary>
    /// Gets or sets the name of the field. This value is used to identify the field.
    /// </summary>
    /// <value></value>
    [DataMember]
    public string FieldName { get; set; }

    /// <summary>Gets or sets the title of the field element</summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the description of the field element.</summary>
    /// <value>The description.</value>
    [DataMember]
    public string Description { get; set; }

    /// <summary>Gets or sets the example of the field element</summary>
    /// <value>The example.</value>
    [DataMember]
    public string Example { get; set; }

    /// <summary>
    /// Field control type (implementing <see cref="!:IField" />) used to represent custom field's value in the UI.
    /// </summary>
    [DataMember]
    public string FieldType { get; set; }

    /// <summary>
    /// Frontend field control type (implementing <see cref="!:IField" />) used to represent custom field's type.
    /// </summary>
    [DataMember]
    public string FrontendWidgetTypeName { get; set; }

    /// <summary>
    /// Gets or sets the front end widget label of the field element
    /// </summary>
    [DataMember]
    public string FrontendWidgetLabel { get; set; }

    /// <summary>Allowed file extensions</summary>
    [DataMember]
    public string FileExtensions { get; set; }

    /// <summary>Allow multiple images</summary>
    [DataMember]
    public bool AllowMultipleImages { get; set; }

    /// <summary>Allow multiple videos</summary>
    [DataMember]
    public bool AllowMultipleVideos { get; set; }

    /// <summary>Allow multiple documents and files</summary>
    [DataMember]
    public bool AllowMultipleFiles { get; set; }

    /// <summary>The type of the related media filed</summary>
    [DataMember]
    public string MediaType { get; set; }

    /// <summary>
    /// Gets or sets the virtual path of the of the field as a user control.
    /// </summary>
    /// <value></value>
    [DataMember]
    public string FieldVirtualPath { get; set; }

    /// <summary>
    /// Gets or sets the name of the resource class used to localize the labels of the field.
    /// </summary>
    /// <value>The name of the resource class.</value>
    /// <remarks>
    /// If this property is left empty, string values such as Title, Description and Example will
    /// be used directly; otherwise the values of these properties will be used as keys for the resources
    /// and localized resources will be loaded instead.
    /// </remarks>
    [DataMember]
    public string ResourceClassId { get; set; }

    /// <summary>Gets or sets the css class of the field control.</summary>
    /// <value>The css class of the field control.</value>
    [DataMember]
    public string CssClass { get; set; }

    /// <summary>Gets or sets the default value.</summary>
    /// <value>The default value.</value>
    [DataMember]
    public string DefaultValue { get; set; }

    /// <summary>Gets or sets the default string value.</summary>
    /// <value>The default string value.</value>
    [DataMember]
    public string DefaultStringValue { get; set; }

    /// <summary>Gets or sets the validation definition.</summary>
    /// <value>The validation.</value>
    [DataMember]
    public ValidatorDefinition ValidatorDefinition { get; set; }

    /// <summary>
    /// Gets a collection of <see cref="!:IChoiceDefinition" /> objects, representing the choices
    /// that the control ought to render.
    /// </summary>
    /// <value></value>
    [DataMember]
    public string Choices { get; set; }

    [DataMember]
    public string ChoiceItemsTitles { get; set; }

    /// <summary>Gets or sets the choice definitions.</summary>
    /// <value>The choice definitions.</value>
    public ChoiceDefinition[] ChoiceDefinitions
    {
      get
      {
        if (!string.IsNullOrEmpty(this.Choices))
          this.choiceDefinitions = this.Deserialize<ChoiceDefinition[]>(this.Choices);
        return this.choiceDefinitions;
      }
      set
      {
        this.choiceDefinitions = value;
        this.ChoiceItemsTitles = string.Join(";", ((IEnumerable<ChoiceDefinition>) this.choiceDefinitions).Select<ChoiceDefinition, string>((Func<ChoiceDefinition, string>) (d => d.Text)));
        this.Choices = JsonConvert.SerializeObject((object) ((IEnumerable<ChoiceDefinition>) this.choiceDefinitions).Select(d => new
        {
          Text = d.Text,
          Value = d.Value
        })).ToString();
      }
    }

    /// <summary>
    /// Gets or sets the value indicating which choice(s) is currently selected.
    /// </summary>
    /// <value></value>
    [DataMember]
    public int[] SelectedChoicesIndex { get; set; }

    /// <summary>
    /// Gets or set different types in which choices of the <see cref="!:ChoiceField" /> can be rendered.
    /// </summary>
    /// <value>The render choice as.</value>
    [DataMember]
    public RenderChoicesAs RenderChoiceAs { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [mutually exclusive].
    /// </summary>
    /// <value><c>true</c> if [mutually exclusive]; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool MutuallyExclusive { get; set; }

    /// <summary>
    /// <see cref="!:ChoiceField" />'s HideTitle memeber.
    /// </summary>
    [DataMember]
    public bool HideTitle { get; set; }

    /// <summary>
    /// Gets or sets a value inidicating whether to sort the choices alphabetically.
    /// </summary>
    [DataMember]
    public bool SortAlphabetically { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [allow multiple selection].
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [allow multiple selection]; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool AllowMultipleSelection { get; set; }

    /// <summary>Gets or sets the validation definition.</summary>
    /// <value>The validation.</value>
    [DataMember]
    public ExpandableControlDefinition ExpandableDefinitionConfig { get; set; }

    /// <summary>Gets or sets the name of the taxonomy.</summary>
    /// <value>The name of the taxonomy.</value>
    [DataMember]
    public string TaxonomyId { get; set; }

    public ITaxonomy Taxonomy
    {
      get
      {
        if (this.taxonomy == null && this.TaxonomyId.IsGuid())
          this.taxonomy = TaxonomyManager.GetManager().GetTaxonomy(new Guid(this.TaxonomyId));
        return this.taxonomy;
      }
    }

    /// <summary>
    /// Contains information about the visibility of the field in all relevant detail views
    /// </summary>
    [DataMember]
    public string[] VisibleViews { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.ModuleEditor.Web.Services.Model.WcfFieldDefinition" /> is hidden in all views
    /// </summary>
    [DataMember]
    public bool Hidden { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable
    /// </summary>
    [DataMember]
    public bool IsLocalizable { get; set; }

    /// <summary>Gets or sets the type of the related content</summary>
    [DataMember]
    public string RelatedDataType { get; set; }

    /// <summary>Gets or sets the provider of the related content</summary>
    [DataMember]
    public string RelatedDataProvider { get; set; }

    /// <summary>
    /// Represents the id of the default item id for the field - for ex. image id
    /// </summary>
    [DataMember]
    public Guid DefaultImageId { get; set; }

    /// <summary>The provider name to be used for the default item</summary>
    [DataMember]
    public string ProviderNameForDefaultImage { get; set; }

    /// <summary>The type of the default item, used in content links</summary>
    [DataMember]
    public string DefaultItemTypeName { get; set; }

    /// <summary>
    /// Max width of the image/video
    /// used by images
    /// </summary>
    [DataMember]
    public int? MaxWidth { get; set; }

    /// <summary>
    /// Max height of the image/video
    /// used by images
    /// </summary>
    [DataMember]
    public int? MaxHeight { get; set; }

    /// <summary>
    /// Max file size for the image/video
    /// used by images
    /// </summary>
    [DataMember]
    public int? MaxFileSize { get; set; }

    /// <summary>
    /// Represents the url of the default image
    /// used by images
    /// </summary>
    [DataMember]
    public string DefaultSrc { get; set; }

    internal bool ResourcesUpdated { get; set; }

    /// <summary>Deserializes the specified json.</summary>
    /// <param name="json">The json.</param>
    /// <returns></returns>
    public T Deserialize<T>(string json)
    {
      using (MemoryStream memoryStream = new MemoryStream(Encoding.GetEncoding("utf-8").GetBytes(json)))
        return (T) new DataContractJsonSerializer(typeof (T)).ReadObject((Stream) memoryStream);
    }
  }
}
