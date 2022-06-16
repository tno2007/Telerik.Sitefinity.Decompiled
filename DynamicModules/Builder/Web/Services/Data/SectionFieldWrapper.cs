// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data.SectionFieldWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data
{
  /// <summary>
  /// This is an object used for drag and drop operations of the backend screen tweaks.
  /// It represents a wrapper either around a field or a section.
  /// </summary>
  [DataContract]
  internal class SectionFieldWrapper
  {
    public SectionFieldWrapper()
    {
    }

    public SectionFieldWrapper(DynamicModuleField moduleField, int aggregateOrdinal)
    {
      this.Id = moduleField.Id;
      this.Title = moduleField.Title;
      this.IsSection = false;
      this.AggregateOrdinal = aggregateOrdinal;
      this.Name = moduleField.Name;
    }

    public SectionFieldWrapper(FieldsBackendSection section, int aggregateOrdinal)
    {
      this.Id = section.Id;
      this.Title = section.Title;
      this.IsSection = true;
      this.AggregateOrdinal = aggregateOrdinal;
      this.IsExpandedByDefault = section.IsExpandedByDefault;
      this.IsTitleDisplayed = section.IsExpandable;
      this.Name = section.Name;
    }

    /// <summary>Gets or sets the id of the section or field.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the title of the section or field.</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the name of the section or field.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the aggregate ordinal (both sections and fields are counted) of the item.
    /// </summary>
    [DataMember]
    public int AggregateOrdinal { get; set; }

    /// <summary>
    /// Gets the value that indicates weather the object
    /// represents section or field; if true it's a section,
    /// otherwise it's a field.
    /// </summary>
    [DataMember]
    public bool IsSection { get; set; }

    /// <summary>
    /// Gets the value that indicates wether the
    /// will be expanded by default or it will be collapsed
    /// </summary>
    [DataMember]
    public bool IsExpandedByDefault { get; set; }

    /// <summary>
    /// Gets the value that indicates wether the
    /// title for the section will be visible or not.
    /// If the title is not visible, the user won't be able
    /// to expand/collapse the section
    /// </summary>
    [DataMember]
    public bool IsTitleDisplayed { get; set; }
  }
}
