// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.Services.Data.PipeMappingTranslationViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing.Web.Services.Data
{
  [DataContract(Name = "PipeMappingTranslationViewModel", Namespace = "Telerik.Sitefinity.Publishing.Web.Services.Data")]
  public class PipeMappingTranslationViewModel
  {
    public PipeMappingTranslation ToModel() => new PipeMappingTranslation()
    {
      Id = Guid.NewGuid(),
      TranslatorName = this.TranslatorName,
      TranslatorSettings = this.TranslatorSettings
    };

    public void CopyToModel(PipeMappingTranslation model)
    {
      model.TranslatorName = this.TranslatorName;
      model.TranslatorSettings = this.TranslatorSettings;
    }

    public static PipeMappingTranslationViewModel FromModel(
      PipeMappingTranslation model)
    {
      return new PipeMappingTranslationViewModel()
      {
        Id = model.Id,
        TranslatorName = model.TranslatorName,
        TranslatorSettings = model.TranslatorSettings
      };
    }

    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the name of the translator.</summary>
    /// <value>The name of the translator.</value>
    [DataMember]
    public string TranslatorName { get; set; }

    /// <summary>Gets or sets the translator settings.</summary>
    /// <value>The translator settings.</value>
    [DataMember]
    public string TranslatorSettings { get; set; }
  }
}
