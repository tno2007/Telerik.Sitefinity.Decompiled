// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.ICustomFieldBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  /// <summary>
  /// Interface for creteing custom meta fields and creating their corresponding definitions and views
  /// </summary>
  public interface ICustomFieldBuilder
  {
    /// <summary>
    /// Implement custom logic for createion of a meta field and add it to the specified type.
    /// </summary>
    /// <param name="metaType">Type to which to add the metafield</param>
    /// <param name="field">The information comming from the client custom field creation UI.</param>
    /// <param name="manager">MetaData manger</param>
    /// <param name="contentType">Conetnt type</param>
    MetaField CreateCustomMetaField(
      MetaType metaType,
      WcfField field,
      MetadataManager manager,
      Type contentType);

    /// <summary>
    /// Implements the logic for creation of definitions and views based on the field.
    /// </summary>
    /// <param name="field">The information comming from the client custom field creation UI.</param>
    /// <param name="fieldControlType">The type of the field control (should implement <see cref="!:IField" />) that should be used to represent the custom field.</param>
    FieldDefinitionElement CreateOrUpdateDynamicDefinitionElement(
      WcfField field,
      Type fieldControlType,
      ConfigElement parent,
      ConfigElement instance);
  }
}
