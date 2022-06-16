// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.FolderContractProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class FolderContractProvider : ITypeSettingsProvider
  {
    private const string FolderDefaultUrl = "folders";

    public IDictionary<string, ITypeSettings> GetTypeSettings()
    {
      ITypeSettings typeSettings = ContractFactory.Instance.Create(typeof (Folder), "folders");
      CalculatedPropertyMappingProxy[] propertyMappingProxyArray = new CalculatedPropertyMappingProxy[1];
      CalculatedPropertyMappingProxy propertyMappingProxy1 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy1.Name = "Breadcrumb";
      propertyMappingProxy1.ResolverType = typeof (FolderBreadcrumbProperty).FullName;
      propertyMappingProxyArray[0] = propertyMappingProxy1;
      foreach (CalculatedPropertyMappingProxy propertyMappingProxy2 in propertyMappingProxyArray)
        typeSettings.Properties.Add((IPropertyMapping) propertyMappingProxy2);
      return (IDictionary<string, ITypeSettings>) new Dictionary<string, ITypeSettings>()
      {
        {
          typeSettings.ClrType,
          typeSettings
        }
      };
    }
  }
}
