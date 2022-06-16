// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.FieldSettingsOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Permissions;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor
{
  internal class FieldSettingsOperationProvider : IOperationProvider
  {
    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      if (!(clrType == (Type) null))
        return (IEnumerable<OperationData>) Array.Empty<OperationData>();
      OperationData operationData = OperationData.Create<string, FieldSettingsContext>(new Func<OperationContext, string, FieldSettingsContext>(this.FieldSettings));
      operationData.OperationType = OperationType.Unbound;
      return (IEnumerable<OperationData>) new OperationData[1]
      {
        operationData
      };
    }

    private FieldSettingsContext FieldSettings(
      OperationContext context,
      string types)
    {
      IEnumerable<string> strings = ((IEnumerable<string>) types.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (x => x.Trim()));
      string oldValue = "Taxonomy_";
      List<Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Permissions.FieldSettings> fieldSettingsList = new List<Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Permissions.FieldSettings>();
      foreach (string name in strings)
      {
        Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Permissions.FieldSettings fieldSettings;
        if (name.StartsWith(oldValue))
        {
          string taxonomyName = name.Replace(oldValue, string.Empty);
          ITaxonomyProxy taxonomyProxy = TaxonomyManager.GetTaxonomiesCache().FirstOrDefault<ITaxonomyProxy>((Func<ITaxonomyProxy, bool>) (x => x.Name == taxonomyName));
          fieldSettings = taxonomyProxy == null ? new Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Permissions.FieldSettings() : FieldSettingsResolver.GetPermissions(TaxonomyManager.GetManager(), taxonomyProxy.Id);
        }
        else
        {
          Type clrType = TypeResolutionService.ResolveType(name, false);
          fieldSettings = !(clrType == (Type) null) ? (!(clrType == typeof (PageNode)) ? FieldSettingsResolver.GetPermissions(clrType, "sf-any-site-provider") : FieldSettingsResolver.GetPermissions(SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId)) : new Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Permissions.FieldSettings();
        }
        fieldSettings.Key = name;
        fieldSettingsList.Add(fieldSettings);
      }
      return new FieldSettingsContext()
      {
        FieldSettings = (IEnumerable<Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Permissions.FieldSettings>) fieldSettingsList
      };
    }
  }
}
