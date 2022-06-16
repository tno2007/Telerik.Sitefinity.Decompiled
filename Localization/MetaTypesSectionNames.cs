// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.MetaTypesSectionNames
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  [ObjectInfo("MetaTypesSectionNames", ResourceClassId = "MetaTypesSectionNames")]
  public class MetaTypesSectionNames : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.ErrorMessages" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public MetaTypesSectionNames()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.ErrorMessages" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public MetaTypesSectionNames(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Attributes section</summary>
    /// <value>The content_ attributes.</value>
    [ResourceEntry("FieldType_General", Description = "label", LastModified = "2009/11/03", Value = "General")]
    public string FieldType_General => this[nameof (FieldType_General)];

    [ResourceEntry("MetaTypesSectionNamesTitle", Description = "label", LastModified = "2009/11/03", Value = "Validation")]
    public string MetaTypesSectionNamesTitle => this[nameof (MetaTypesSectionNamesTitle)];

    [ResourceEntry("MetaTypesSectionNamesTitlePlural", Description = "label", LastModified = "2009/11/03", Value = "Validation")]
    public string MetaTypesSectionNamesTitlePlural => this[nameof (MetaTypesSectionNamesTitlePlural)];

    [ResourceEntry("MetaTypesSectionNamesDescription", Description = "label", LastModified = "2009/11/03", Value = "Validation")]
    public string MetaTypesSectionNamesDescription => this[nameof (MetaTypesSectionNamesDescription)];

    [ResourceEntry("FieldType_Validation", Description = "label", LastModified = "2009/11/03", Value = "Validation")]
    public string FieldType_Validation => this[nameof (FieldType_Validation)];

    [ResourceEntry("FieldType_Initialization", Description = "label", LastModified = "2009/11/03", Value = "Initialization")]
    public string FieldType_Initialization => this[nameof (FieldType_Initialization)];

    [ResourceEntry("FieldType_UI", Description = "label", LastModified = "2009/11/03", Value = "User Interface")]
    public string FieldType_UI => this[nameof (FieldType_UI)];
  }
}
