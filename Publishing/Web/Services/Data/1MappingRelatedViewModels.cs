// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.Services.Data.MappingViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing.Web.Services.Data
{
  [DataContract(Name = "MappingViewModel", Namespace = "Telerik.Sitefinity.Publishing.Web.Services.Data")]
  public class MappingViewModel
  {
    public MappingViewModel() => this.Translations = new List<PipeMappingTranslationViewModel>();

    public Mapping ToModel()
    {
      Mapping model = new Mapping()
      {
        Id = Guid.NewGuid(),
        ApplicationName = this.ApplicationName,
        DefaultValue = this.DefaultValue,
        IsRequired = this.IsRequired,
        DestinationPropertyName = this.DestinationPropertyName
      };
      model.SourcePropertyNames = new string[this.SourcePropertyNames.Length];
      Array.Copy((Array) this.SourcePropertyNames, (Array) model.SourcePropertyNames, this.SourcePropertyNames.Length);
      foreach (PipeMappingTranslationViewModel translation in this.Translations)
        model.Translations.Add(translation.ToModel());
      return model;
    }

    public void CopyToModel(PublishingManager manager, Mapping model)
    {
      model.DefaultValue = this.DefaultValue;
      model.IsRequired = this.IsRequired;
      model.DestinationPropertyName = this.DestinationPropertyName;
      model.SourcePropertyNames = new string[this.SourcePropertyNames.Length];
      Array.Copy((Array) this.SourcePropertyNames, (Array) model.SourcePropertyNames, this.SourcePropertyNames.Length);
      foreach (PipeMappingTranslation toDelete in model.Translations.ToArray<PipeMappingTranslation>())
        manager.DeletePipeMappingTranslation(toDelete);
      model.Translations.Clear();
      foreach (PipeMappingTranslationViewModel translation in this.Translations)
      {
        PipeMappingTranslation mappingTranslation = manager.CreatePipeMappingTranslation();
        PipeMappingTranslation model1 = mappingTranslation;
        translation.CopyToModel(model1);
        model.Translations.Add(mappingTranslation);
      }
    }

    public static MappingViewModel FromModel(Mapping model)
    {
      MappingViewModel mappingViewModel = new MappingViewModel()
      {
        Id = model.Id,
        ApplicationName = model.ApplicationName,
        DefaultValue = model.DefaultValue,
        IsRequired = model.IsRequired,
        DestinationPropertyName = model.DestinationPropertyName
      };
      mappingViewModel.SourcePropertyNames = new string[model.SourcePropertyNames.Length];
      Array.Copy((Array) model.SourcePropertyNames, (Array) mappingViewModel.SourcePropertyNames, model.SourcePropertyNames.Length);
      foreach (PipeMappingTranslation translation in (IEnumerable<PipeMappingTranslation>) model.Translations)
        mappingViewModel.Translations.Add(PipeMappingTranslationViewModel.FromModel(translation));
      return mappingViewModel;
    }

    [DataMember]
    public Guid Id { get; set; }

    [DataMember]
    public string ApplicationName { get; set; }

    /// <summary>Name of the destination property</summary>
    [DataMember]
    public string DestinationPropertyName { get; set; }

    /// <summary>Names of the source properties</summary>
    [DataMember]
    public string[] SourcePropertyNames { get; set; }

    /// <summary>
    /// Gets the list of translations(concatenation, formatting, trimmimg,converting etc.) to be performed during the mapping. The translations are performed in a chain, each translator receives the result of the pervious
    /// </summary>
    /// <value>The translations.</value>
    [DataMember]
    public List<PipeMappingTranslationViewModel> Translations { get; set; }

    /// <summary>Default field value.</summary>
    /// <value>The default value.</value>
    [DataMember]
    public string DefaultValue { get; set; }

    /// <summary>
    /// Indicate whether the destination field must have a value
    /// </summary>
    /// <value>
    /// 	<c>true</c> if it is required; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsRequired { get; set; }
  }
}
