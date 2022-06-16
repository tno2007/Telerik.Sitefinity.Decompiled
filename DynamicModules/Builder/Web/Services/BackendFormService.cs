// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.Services.BackendFormService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.Services
{
  /// <summary>
  /// Web service for modifying the fields and sections of the backend forms.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  internal class BackendFormService : IBackendFormService
  {
    /// <summary>
    /// Modifies the sections and fields of the backend forms for dynamic modules.
    /// </summary>
    public void ModifyBackendForms(SectionFieldWrapper[] sectionsAndFields, string parentTypeId)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      Guid guid = new Guid(parentTypeId);
      manager.ModifyBackendForms(sectionsAndFields, guid);
      manager.SaveChanges();
      DynamicModuleType dynamicModuleType = manager.GetDynamicModuleType(guid);
      DynamicModule dynamicModule = manager.GetDynamicModule(dynamicModuleType.ParentModuleId);
      if (dynamicModule.Status != DynamicModuleStatus.Active)
        return;
      manager.LoadDynamicModuleGraph(dynamicModule);
      new BackendDefinitionInstaller(manager).ReinstallBackendDetailDefinitions(dynamicModuleType);
    }

    /// <summary>
    /// Gets the sections and fields of the backend forms for dynamic modules. The data is returned in JSON format.
    /// </summary>
    /// <param name="parentTypeId">Id of the desired module type.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data.SectionFieldWrapper" /> objects.
    /// </returns>
    public CollectionContext<SectionFieldWrapper> GetBackendForms(
      string parentTypeId)
    {
      return this.GetBackendFormsInternal(parentTypeId);
    }

    /// <summary>
    /// Gets the sections and fields of the backend forms for dynamic modules. The data is returned in XML format.
    /// </summary>
    /// <param name="parentTypeId">Id of the desired module type.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data.SectionFieldWrapper" /> objects.
    /// </returns>
    public CollectionContext<SectionFieldWrapper> GetBackendFormsInXml(
      string parentTypeId)
    {
      return this.GetBackendFormsInternal(parentTypeId);
    }

    private CollectionContext<SectionFieldWrapper> GetBackendFormsInternal(
      string parentTypeId)
    {
      Guid contentTypeGuid = parentTypeId.IsGuid() ? new Guid(parentTypeId) : throw new ArgumentException("ParentTypeId must be a valid GUID.");
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      IOrderedQueryable<FieldsBackendSection> orderedQueryable = manager.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == contentTypeGuid)).OrderBy<FieldsBackendSection, int>((Expression<Func<FieldsBackendSection, int>>) (s => s.Ordinal));
      int aggregateOrdinal = 0;
      List<SectionFieldWrapper> items = new List<SectionFieldWrapper>();
      FieldSpecialType[] source1 = new FieldSpecialType[6]
      {
        FieldSpecialType.Author,
        FieldSpecialType.PublicationDate,
        FieldSpecialType.Actions,
        FieldSpecialType.Translations,
        FieldSpecialType.LastModified,
        FieldSpecialType.DateCreated
      };
      foreach (FieldsBackendSection fieldsBackendSection in (IEnumerable<FieldsBackendSection>) orderedQueryable)
      {
        FieldsBackendSection section = fieldsBackendSection;
        IOrderedQueryable<DynamicModuleField> source2 = manager.GetDynamicModuleFields().Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => f.ParentSectionId == section.Id && f.ParentTypeId == contentTypeGuid)).OrderBy<DynamicModuleField, int>((Expression<Func<DynamicModuleField, int>>) (f => f.Ordinal));
        if (source2.Count<DynamicModuleField>() != 0)
        {
          SectionFieldWrapper sectionFieldWrapper1 = new SectionFieldWrapper(section, aggregateOrdinal);
          items.Add(sectionFieldWrapper1);
          ++aggregateOrdinal;
          foreach (DynamicModuleField dynamicModuleField in (IEnumerable<DynamicModuleField>) source2)
          {
            DynamicModuleField sectionField = dynamicModuleField;
            if (!((IEnumerable<FieldSpecialType>) source1).Any<FieldSpecialType>((Func<FieldSpecialType, bool>) (x => x == sectionField.SpecialType)))
            {
              SectionFieldWrapper sectionFieldWrapper2 = new SectionFieldWrapper(sectionField, aggregateOrdinal);
              items.Add(sectionFieldWrapper2);
              ++aggregateOrdinal;
            }
          }
        }
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<SectionFieldWrapper>((IEnumerable<SectionFieldWrapper>) items)
      {
        TotalCount = items.Count
      };
    }
  }
}
