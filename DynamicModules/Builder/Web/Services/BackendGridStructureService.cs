// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.Services.BackendGridStructureService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.Services
{
  /// <summary>
  /// Web service for modifying the structure of the backend grid.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  internal class BackendGridStructureService : IBackendGridStructureService
  {
    /// <summary>
    /// Modifies the structure of the backend grid of the dynamic module.
    /// </summary>
    public void ModifyBackendGrid(GridColumnWrapper[] gridColumns, string parentTypeId)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      Guid guid = new Guid(parentTypeId);
      manager.ModifyBackendGrid(gridColumns, guid);
      manager.SaveChanges();
      DynamicModuleType dynamicModuleType = manager.GetDynamicModuleType(guid);
      DynamicModule dynamicModule = manager.GetDynamicModule(dynamicModuleType.ParentModuleId);
      if (dynamicModule.Status != DynamicModuleStatus.Active)
        return;
      manager.LoadDynamicModuleGraph(dynamicModule);
      new BackendDefinitionInstaller(manager).ReinstallBackendGridDefinitions(dynamicModule, dynamicModuleType);
    }

    /// <summary>
    /// Gets the structure of the backend grid of the dynamic module. The data is returned in JSON format.
    /// </summary>
    /// <param name="parentTypeId">The content type id.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data.GridColumnWrapper" /> objects.
    /// </returns>
    public CollectionContext<GridColumnWrapper> GetBackendGrid(
      string parentTypeId)
    {
      return this.GetBackendGridInternal(parentTypeId);
    }

    /// <summary>
    /// Gets the structure of the backend grid of the dynamic module. The data is returned in XML format.
    /// </summary>
    /// <param name="parentTypeId">The content type id.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data.GridColumnWrapper" /> objects.
    /// </returns>
    public CollectionContext<GridColumnWrapper> GetBackendGridInXml(
      string parentTypeId)
    {
      return this.GetBackendGridInternal(parentTypeId);
    }

    private CollectionContext<GridColumnWrapper> GetBackendGridInternal(
      string parentTypeId)
    {
      Guid contentTypeGuid = parentTypeId.IsGuid() ? new Guid(parentTypeId) : throw new ArgumentException("ParentTypeId must be a valid GUID.");
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      DynamicModuleType dynamicModuleType = manager.GetDynamicModuleType(contentTypeGuid);
      IQueryable<DynamicModuleField> source = manager.GetDynamicModuleFields().Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => f.ParentTypeId == contentTypeGuid && f.IsHiddenField == false && (f.MediaType != "file" || string.IsNullOrEmpty(f.MediaType)) && (int) f.FieldType != 8)).Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => (int) f.FieldType != 14 || f.MediaType == "image" && f.Name != "OpenGraphImage"));
      if (!SystemManager.CurrentContext.AppSettings.Multilingual)
        source = source.Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => (int) f.SpecialType != 6));
      IQueryable<DynamicModuleField> fields = (IQueryable<DynamicModuleField>) source.OrderBy<DynamicModuleField, int>((Expression<Func<DynamicModuleField, int>>) (f => f.GridColumnOrdinal));
      ServiceUtility.DisableCache();
      return this.GetGridColumnWrappers(fields, dynamicModuleType.MainShortTextFieldName);
    }

    private CollectionContext<GridColumnWrapper> GetGridColumnWrappers(
      IQueryable<DynamicModuleField> fields,
      string mainShortTextFieldName)
    {
      List<GridColumnWrapper> items = new List<GridColumnWrapper>();
      foreach (DynamicModuleField field in (IEnumerable<DynamicModuleField>) fields)
      {
        if (field.SpecialType != FieldSpecialType.ParentId)
        {
          GridColumnWrapper gridColumnWrapper = new GridColumnWrapper()
          {
            Id = field.Id,
            Name = field.Name,
            Title = field.Title,
            ShowInGrid = field.ShowInGrid,
            GridEditorCssClass = BackendUIHelper.GetCssClassByField(field, mainShortTextFieldName)
          };
          items.Add(gridColumnWrapper);
        }
      }
      if (!fields.Any<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => (int) f.SpecialType == 8)))
      {
        DynamicModuleField field = new DynamicModuleField();
        field.FieldType = FieldType.DateTime;
        GridColumnWrapper gridColumnWrapper1 = new GridColumnWrapper();
        gridColumnWrapper1.Id = Guid.NewGuid();
        gridColumnWrapper1.Name = LinqHelper.MemberName<DynamicContent>((Expression<Func<DynamicContent, object>>) (x => (object) x.LastModified));
        gridColumnWrapper1.Title = "Last Modified";
        gridColumnWrapper1.ShowInGrid = false;
        gridColumnWrapper1.GridEditorCssClass = BackendUIHelper.GetCssClassByField(field, mainShortTextFieldName);
        GridColumnWrapper gridColumnWrapper2 = gridColumnWrapper1;
        items.Add(gridColumnWrapper2);
      }
      if (!fields.Any<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => (int) f.SpecialType == 9)))
      {
        DynamicModuleField field = new DynamicModuleField();
        field.FieldType = FieldType.DateTime;
        GridColumnWrapper gridColumnWrapper3 = new GridColumnWrapper();
        gridColumnWrapper3.Id = Guid.NewGuid();
        gridColumnWrapper3.Name = LinqHelper.MemberName<DynamicContent>((Expression<Func<DynamicContent, object>>) (x => (object) x.DateCreated));
        gridColumnWrapper3.Title = "Date Created";
        gridColumnWrapper3.ShowInGrid = false;
        gridColumnWrapper3.GridEditorCssClass = BackendUIHelper.GetCssClassByField(field, mainShortTextFieldName);
        GridColumnWrapper gridColumnWrapper4 = gridColumnWrapper3;
        items.Add(gridColumnWrapper4);
      }
      return new CollectionContext<GridColumnWrapper>((IEnumerable<GridColumnWrapper>) items)
      {
        TotalCount = items.Count
      };
    }
  }
}
