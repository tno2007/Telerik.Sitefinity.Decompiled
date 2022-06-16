// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.Services.Data.PublishingPointDetailViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Publishing.Web.Services.Data
{
  /// <summary>
  /// This class is used in the publishing system administration REST service
  /// The class represents a view model of the publishing point that includes full information about the inbound and outbound pipes, presented in a UI friendly structure
  /// </summary>
  [DataContract(Name = "PublishingPointDetailViewModel", Namespace = "Telerik.Sitefinity.Publishing.Web.Services.Data")]
  public class PublishingPointDetailViewModel
  {
    private List<WcfPipeSettings> inBoundSettings;
    private List<WcfPipeSettings> outBoundSettings;

    public PublishingPointDetailViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.Services.Data.PublishingPointDetailViewModel" /> class.
    /// </summary>
    /// <param name="modelPoint">The model point.</param>
    /// <param name="providerName">The provider name.</param>
    /// <param name="itemTemplate">The item template.</param>
    public PublishingPointDetailViewModel(
      PublishingPoint modelPoint,
      string providerName,
      string itemTemplate = null)
    {
      this.Id = modelPoint.Id;
      this.Title = modelPoint.Name;
      this.DateCreated = modelPoint.DateCreated;
      this.LastPublicationDate = modelPoint.LastPublicationDate;
      this.Description = (string) modelPoint.Description;
      this.PublishingPointBusinessObjectName = modelPoint.PublishingPointBusinessObjectName;
      this.Owner = UserProfilesHelper.GetUserDisplayName(modelPoint.Owner);
      this.IsActive = modelPoint.IsActive;
      this.IsBackend = modelPoint.Settings.ItemFilterStrategy == PublishingItemFilter.All;
      this.IsSharedWithAllSites = modelPoint.IsSharedWithAllSites;
      foreach (PipeSettings pipeSetting in (IEnumerable<PipeSettings>) modelPoint.PipeSettings)
      {
        if (PublishingSystemFactory.IsPipeRegistered(pipeSetting.PipeName))
        {
          WcfPipeSettings wcfPipeSettings = new WcfPipeSettings(pipeSetting, providerName);
          wcfPipeSettings.AdditionalFilter = pipeSetting.FilterExpression;
          wcfPipeSettings.LanguageIds.Clear();
          wcfPipeSettings.LanguageIds.AddRange((IEnumerable<string>) pipeSetting.LanguageIds.ToArray<string>());
          wcfPipeSettings.PublishingPointName = this.Title;
          wcfPipeSettings.MappingSettings = MappingSettingsViewModel.FromModel(pipeSetting.Mappings);
          if (pipeSetting.IsInbound)
            this.InboundSettings.Add(wcfPipeSettings);
          else
            this.OutboundSettings.Add(wcfPipeSettings);
        }
      }
      if (!string.IsNullOrEmpty(itemTemplate))
        this.IncludeTemplatePipes(itemTemplate, providerName, true);
      PublishingPointDynamicTypeManager.GetManager(modelPoint.StorageItemsProvider).Provider.EnsureDynamicTypesResolution();
      Type type = TypeResolutionService.ResolveType(modelPoint.StorageTypeName);
      this.SetPublishingPointDefinition(MetadataManager.GetManager().GetMetaType(type), type);
      this.SetRegisteredPipeSettings(providerName);
      this.ProcessAdditionalFields(modelPoint, this);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Web.Services.Data.PublishingPointDetailViewModel" /> class.
    /// </summary>
    /// <param name="templateName">The template name</param>
    /// <param name="providerName">The provider name</param>
    public PublishingPointDetailViewModel(string templateName, string providerName)
    {
      this.Title = "";
      this.Owner = "";
      this.IsActive = true;
      this.DateCreated = DateTime.UtcNow;
      this.IncludeTemplatePipes(templateName, providerName);
      MetaType publishingPointDataType = PublishingSystemFactory.CreatePublishingPointDataType();
      Type pointStorageType = TypeResolutionService.ResolveType(publishingPointDataType.BaseClassName);
      this.SetPublishingPointDefinition(publishingPointDataType, pointStorageType);
      this.SetRegisteredPipeSettings(providerName);
    }

    private void IncludeTemplatePipes(
      string templateName,
      string providerName,
      bool disabledByDefault = false)
    {
      foreach (PipeSettings templatePipe in PublishingSystemFactory.GetTemplatePipes(templateName))
      {
        WcfPipeSettings viewModel = new WcfPipeSettings(templatePipe, providerName);
        if (disabledByDefault)
          viewModel.IsActive = false;
        List<WcfPipeSettings> source = templatePipe.IsInbound ? this.InboundSettings : this.OutboundSettings;
        if (!source.Any<WcfPipeSettings>((Func<WcfPipeSettings, bool>) (s => s.PipeName == viewModel.PipeName && s.ContentName == viewModel.ContentName)))
          source.Add(viewModel);
      }
    }

    private void SetPublishingPointDefinition(MetaType metaType, Type pointStorageType)
    {
      List<SimpleDefinitionField> source = new List<SimpleDefinitionField>();
      foreach (MetaField field in (IEnumerable<MetaField>) metaType.Fields)
      {
        string title = string.IsNullOrWhiteSpace(field.Title) ? field.FieldName : field.Title;
        source.Add(new SimpleDefinitionField(field.FieldName, title)
        {
          ClrType = field.ClrType,
          SQLDBType = field.DBSqlType,
          DBType = field.DBType,
          DefaultValue = field.DefaultValue,
          MaxLength = field.MaxLength,
          TaxonomyId = field.TaxonomyId.ToString(),
          TaxonomyProviderName = field.TaxonomyProvider,
          AllowMultipleTaxons = !field.IsSingleTaxon,
          IsMetaField = true
        });
      }
      foreach (PropertyDescriptor property1 in TypeDescriptor.GetProperties(pointStorageType))
      {
        PropertyDescriptor property = property1;
        if (!metaType.Fields.Any<MetaField>((Func<MetaField, bool>) (f => f.FieldName == property.Name)))
        {
          string title = string.IsNullOrWhiteSpace(property.DisplayName) ? property.DisplayName : property.Name;
          source.Add(new SimpleDefinitionField(property.Name, title)
          {
            ClrType = property.PropertyType.FullName,
            IsMetaField = property is MetafieldPropertyDescriptor
          });
        }
      }
      string[] fieldNamesToHide = new string[3]
      {
        "Id",
        "ApplicationName",
        "LastModified"
      };
      foreach (SimpleDefinitionField simpleDefinitionField in source.Where<SimpleDefinitionField>((Func<SimpleDefinitionField, bool>) (f => ((IEnumerable<string>) fieldNamesToHide).Contains<string>(f.Name))))
        simpleDefinitionField.HideInUI = true;
      source.Sort((IComparer<SimpleDefinitionField>) DefinitionFieldComparer.TitleComparer);
      this.PublishingPointDefinition = source;
    }

    private void SetRegisteredPipeSettings(string providerName)
    {
      List<WcfPipeSettings> wcfPipeSettingsList1 = new List<WcfPipeSettings>();
      List<WcfPipeSettings> wcfPipeSettingsList2 = new List<WcfPipeSettings>();
      foreach (IPipe publishingPipe in PublishingSystemFactory.GetPublishingPipes())
      {
        if (publishingPipe is IInboundPipe)
          wcfPipeSettingsList1.Add(new WcfPipeSettings(publishingPipe.GetDefaultSettings(), providerName));
        else if (publishingPipe is IOutboundPipe)
          wcfPipeSettingsList2.Add(new WcfPipeSettings(publishingPipe.GetDefaultSettings(), providerName));
      }
      this.RegisteredInboundPipeSettings = wcfPipeSettingsList1.ToArray();
      this.RegisteredOutboundPipeSettings = wcfPipeSettingsList2.ToArray();
    }

    private void ProcessAdditionalFields(
      PublishingPoint dataItem,
      PublishingPointDetailViewModel viewModel)
    {
      PipeSettings pipeSettings = dataItem.PipeSettings.SingleOrDefault<PipeSettings>((Func<PipeSettings, bool>) (ps => ps is SearchIndexPipeSettings));
      if (pipeSettings == null || !pipeSettings.AdditionalData.ContainsKey("SearchIndexAdditionalFields"))
        return;
      viewModel.AdditionalFields = pipeSettings.AdditionalData["SearchIndexAdditionalFields"];
    }

    [DataMember]
    public List<SimpleDefinitionField> PublishingPointDefinition { get; set; }

    [DataMember]
    public string PublishingPointBusinessObjectName { get; set; }

    /// <summary>Gets or sets the id.</summary>
    /// <value>The id.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the description.</summary>
    /// <value>The description.</value>
    [DataMember]
    public string Description { get; set; }

    /// <summary>Gets or sets the Owner user Full Name.</summary>
    /// <value>The owner.</value>
    [DataMember]
    public string Owner { get; set; }

    /// <summary>Gets or sets the date created.</summary>
    /// <value>The date created.</value>
    [DataMember]
    public DateTime DateCreated { get; set; }

    [DataMember]
    public DateTime? LastPublicationDate { get; set; }

    /// <summary>Gets or sets the inbound pipe settings.</summary>
    /// <value>The inbound settings.</value>
    [DataMember]
    public List<WcfPipeSettings> InboundSettings
    {
      get
      {
        if (this.inBoundSettings == null)
          this.inBoundSettings = new List<WcfPipeSettings>();
        return this.inBoundSettings;
      }
      set => this.inBoundSettings = value;
    }

    /// <summary>Gets or sets the outbound  pipe settings.</summary>
    /// <value>The outbound settings.</value>
    [DataMember]
    public List<WcfPipeSettings> OutboundSettings
    {
      get
      {
        if (this.outBoundSettings == null)
          this.outBoundSettings = new List<WcfPipeSettings>();
        return this.outBoundSettings;
      }
      set => this.outBoundSettings = value;
    }

    [DataMember]
    public bool IsActive { get; set; }

    [DataMember]
    public bool IsBackend { get; set; }

    [DataMember]
    public bool IsSharedWithAllSites { get; set; }

    [DataMember]
    public WcfPipeSettings[] RegisteredInboundPipeSettings { get; set; }

    [DataMember]
    public WcfPipeSettings[] RegisteredOutboundPipeSettings { get; set; }

    [DataMember]
    public string AdditionalFields { get; set; }
  }
}
