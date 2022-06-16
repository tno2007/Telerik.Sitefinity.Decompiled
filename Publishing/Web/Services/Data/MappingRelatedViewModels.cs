﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.Services.Data.MappingSettingsViewModel
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
  [DataContract(Name = "MappingSettingsViewModel", Namespace = "Telerik.Sitefinity.Publishing.Web.Services.Data")]
  public class MappingSettingsViewModel
  {
    public MappingSettingsViewModel() => this.Mappings = new List<MappingViewModel>();

    public MappingSettings ToModel()
    {
      MappingSettings model = new MappingSettings();
      model.Id = Guid.NewGuid();
      model.ApplicationName = this.ApplicationName;
      foreach (MappingViewModel mapping in this.Mappings)
        model.Mappings.Add(mapping.ToModel());
      return model;
    }

    public void CopyToModel(PublishingManager manager, MappingSettings model)
    {
      foreach (Mapping toDelete in model.Mappings.ToArray<Mapping>())
        manager.DeleteMapping(toDelete);
      model.Mappings.Clear();
      foreach (MappingViewModel mapping1 in this.Mappings)
      {
        Mapping mapping2 = manager.CreateMapping();
        PublishingManager manager1 = manager;
        Mapping model1 = mapping2;
        mapping1.CopyToModel(manager1, model1);
        model.Mappings.Add(mapping2);
      }
    }

    public static MappingSettingsViewModel FromModel(MappingSettings model)
    {
      MappingSettingsViewModel settingsViewModel = new MappingSettingsViewModel()
      {
        ApplicationName = model.ApplicationName
      };
      foreach (Mapping mapping in (IEnumerable<Mapping>) model.Mappings)
        settingsViewModel.Mappings.Add(MappingViewModel.FromModel(mapping));
      return settingsViewModel;
    }

    [DataMember]
    public List<MappingViewModel> Mappings { get; set; }

    [DataMember]
    public Guid Id { get; set; }

    [DataMember]
    public string ApplicationName { get; set; }
  }
}
