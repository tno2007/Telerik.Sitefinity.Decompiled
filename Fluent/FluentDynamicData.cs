// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.FluentDynamicData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Fluent.DynamicData;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// Aggregation class for all facades related to dynamic data in Sitefinity.
  /// </summary>
  public class FluentDynamicData
  {
    private AppSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.FluentDynamicData" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    public FluentDynamicData(AppSettings appSettings) => this.appSettings = appSettings;

    /// <summary>
    /// Provides the fluent API for working with a single dynamic field.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade Field() => new DynamicFieldFacade(this.appSettings, (DynamicTypeFacade) null);

    /// <summary>
    /// Provides the fluent API for working with a single dynamic field and loads the
    /// dynamic field from the provided dynamic field id.
    /// </summary>
    /// <param name="dynamicFieldId">
    /// Id of the dynamic field that ought to be loaded into the state of the facade.
    /// </param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade Field(Guid dynamicFieldId) => new DynamicFieldFacade(this.appSettings, (DynamicTypeFacade) null, dynamicFieldId);

    /// <summary>
    /// Provides the fluent API for working with a single dynamic field and loads the
    /// provided dynamic field into the state of the facade.
    /// </summary>
    /// <param name="dynamicField">
    /// Dynamic field that ought to be loaded into the state of the facade.
    /// </param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade Field(MetaField dynamicField) => new DynamicFieldFacade(this.appSettings, (DynamicTypeFacade) null, dynamicField);

    /// <summary>
    /// Provides the fluent API for working with the collection of dynamic fields.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicFieldsFacade Fields() => new DynamicFieldsFacade(this.appSettings, (DynamicTypeFacade) null);

    /// <summary>
    /// Provides the fluent API for working with a single dynamic type.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" />.</returns>
    public DynamicTypeFacade Type() => new DynamicTypeFacade(this.appSettings);

    /// <summary>
    /// Provides the fluent API for working with a single dynamic type and loads the
    /// dynamic type from the provided dynamic type id.
    /// </summary>
    /// <param name="dynamicTypeId">
    /// Id of the dynamic type that ought to be loaded into the sate of the facade.
    /// </param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" />.</returns>
    public DynamicTypeFacade Type(Guid dynamicTypeId) => new DynamicTypeFacade(this.appSettings, dynamicTypeId);

    /// <summary>
    /// Provides the fluent API for working with a single dynamic type and loads the provided
    /// dynamic type into the state of the facade.
    /// </summary>
    /// <param name="dynamicType">
    /// Dynamic type that ought to be loaded into the state of the facade.
    /// </param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicTypeFacade Type(MetaType dynamicType) => new DynamicTypeFacade(this.appSettings, dynamicType);

    /// <summary>
    /// Provides the fluent API for working with a single dynamic type and loads the
    /// dynamic type based on namespace and class name that was passed into the state of the
    /// facade.
    /// </summary>
    /// <param name="nameSpace">The namespace of the MetaType.</param>
    /// <param name="className">The class name for the MetaType.</param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.
    /// </returns>
    public DynamicTypeFacade Type(string nameSpace, string className) => new DynamicTypeFacade(this.appSettings, nameSpace, className);

    /// <summary>
    /// Provides the fluent API for working with a single dynamic type and loads the
    /// dynamic type based on the existing type that was passed into the state of the
    /// facade.
    /// </summary>
    /// <param name="existingType">
    /// Dynamic type that ought to be loaded into the state of the facade.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.
    /// </returns>
    public DynamicTypeFacade Type(System.Type existingType) => new DynamicTypeFacade(this.appSettings, existingType);

    /// <summary>
    /// Provides the fluent API for working with the collection of dynamic types.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicTypesFacade Types() => new DynamicTypesFacade(this.appSettings);

    /// <summary>
    /// Provides the fluent API for working with a single dynamic type description.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" />.</returns>
    public DynamicTypeDescriptionFacade TypeDescription() => new DynamicTypeDescriptionFacade(this.appSettings);

    /// <summary>
    /// Provides the fluent API for working with a single dynamic type description and loads the
    /// dynamic type based on the existing type that was passed into the state of the
    /// facade.
    /// </summary>
    /// <param name="existingType">
    /// Dynamic type that ought to be loaded into the state of the facade.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" />.
    /// </returns>
    public DynamicTypeDescriptionFacade TypeDescription(
      Guid dynamicTypeDescriptionId)
    {
      return new DynamicTypeDescriptionFacade(this.appSettings, dynamicTypeDescriptionId);
    }

    public DynamicTypeDescriptionFacade TypeDescription(System.Type dynamicType) => new DynamicTypeDescriptionFacade(this.appSettings, dynamicType);
  }
}
