// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.IMetadataManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Data.Metadata
{
  /// <summary>
  /// Defines the functionality of the types that act as business layer for managing
  /// meta data (dynamic data).
  /// </summary>
  public interface IMetadataManager : IManager, IDisposable, IProviderResolver
  {
    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing description of the specified type.
    /// </summary>
    /// <param name="type">The type that will be described.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    MetaType CreateMetaType(Type type);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing descriptions for specified type and sets the pageId of the type.
    /// </summary>
    /// <param name="namespaceParam">The name space for the described type.</param>
    /// <param name="className">The name of the described type.</param>
    /// <param name="pageId">The pageId for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> description.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    MetaType CreateMetaType(string namespaceParam, string className, Guid id);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing description of the specified type and sets the pageId of the type.
    /// </summary>
    /// <param name="type">The type that will be described.</param>
    /// <param name="pageId">The pageId for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> description.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    MetaType CreateMetaType(Type type, Guid id);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing descriptions for specified type.
    /// </summary>
    /// <param name="namespaceParam">The name space for the described type.</param>
    /// <param name="className">The name of the described type.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    MetaType CreateMetaType(string namespaceParam, string className);

    /// <summary>Gets a type description by ID.</summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    MetaType GetMetaType(Guid id);

    /// <summary>
    /// Gets an array of type descriptions for the specified type and its inherited types.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    MetaType[] GetMetaTypes(Type type);

    /// <summary>
    /// Gets an array of type descriptions for the specified object instance and its inherited types.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    MetaType[] GetMetaTypes(object instance);

    /// <summary>Gets a type description for the specified type.</summary>
    /// <param name="namespaceParm">The name space.</param>
    /// <param name="className">The class name.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    MetaType GetMetaType(string namespaceParm, string className);

    /// <summary>Gets a type description for the specified type.</summary>
    /// <param name="type">The CLR type.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    MetaType GetMetaType(Type type);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" />.
    /// </summary>
    /// <returns></returns>
    IQueryable<MetaType> GetMetaTypes();

    /// <summary>Deletes the specified meta type.</summary>
    /// <param name="metaType">Type of the meta.</param>
    void Delete(MetaType metaType);

    /// <summary>Gets a query for metafield.</summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> objects.</returns>
    IQueryable<MetaField> GetMetafields();

    /// <summary>
    /// Creates new metadata field for the specified type (class).
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    MetaField CreateMetafield(string fieldName);

    /// <summary>
    /// Creates new metadata field for the specified type (class) and set the ID for the field.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="pageId">The pageId for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> description.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    MetaField CreateMetafield(string fieldName, Guid id);

    /// <summary>Gets the metafield with the specified ID.</summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    MetaField GetMetafield(Guid id);

    /// <summary>Deletes the specified metafield.</summary>
    /// <param name="metafield">The metafield.</param>
    void Delete(MetaField metafield);

    /// <summary>
    /// Creates a new metatype description for the specified type (class).
    /// </summary>
    /// <param name="metaTypeId">Id of the meta type.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    MetaTypeDescription CreateMetaTypeDescription(Guid metaTypeId);

    /// <summary>
    /// Creates new metadata field for the specified type (class) and set the ID for the field.
    /// </summary>
    /// <param name="metaTypeId">Id of the meta type.</param>
    /// <param name="id">The id for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> description.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> object for storing data field description.</returns>
    MetaTypeDescription CreateMetaTypeDescription(Guid metaTypeId, Guid id);

    /// <summary>Gets the a meta type description by ID.</summary>
    /// <param name="id">The id of the type description.</param>
    /// <returns></returns>
    MetaTypeDescription GetMetaTypeDescription(Guid id);

    /// <summary>Gets a query for meta type descriptions.</summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> objects.</returns>
    IQueryable<MetaTypeDescription> GetMetaTypeDescriptions();

    /// <summary>Deletes the specified meta type description.</summary>
    /// <param name="descr">The type description to delete.</param>
    void Delete(MetaTypeDescription descr);
  }
}
