// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingPoints.PublishingPointBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Publishing.PublishingPoints
{
  /// <summary>
  /// A class responsible for creation of publishing point with specified pipe settings that determine the inbound and outbound pipes that belong to point and
  /// definition fields that determine the persistence of publishing point object.
  /// </summary>
  public class PublishingPointBuilder
  {
    private List<IDefinitionField> fields = new List<IDefinitionField>();
    private List<PipeSettings> pipeSettings;
    private PublishingManager publishingManager;
    private string publishingProviderName;
    private object[] dataItems;
    private string title;
    private string description;

    /// <summary>Gets or sets the name of the publishing provider.</summary>
    /// <value>The name of the publishing provider.</value>
    public string PublishingProviderName
    {
      get
      {
        if (string.IsNullOrEmpty(this.publishingProviderName))
          this.publishingProviderName = Config.Get<PublishingConfig>().DefaultProvider;
        return this.publishingProviderName;
      }
      set => this.publishingProviderName = value;
    }

    private List<IDefinitionField> DefinitionFields
    {
      get
      {
        if (this.fields == null)
          this.fields = new List<IDefinitionField>();
        return this.fields;
      }
    }

    private PublishingManager PublishingManager
    {
      get
      {
        if (this.publishingManager == null)
          this.publishingManager = PublishingManager.GetManager(this.PublishingProviderName);
        return this.publishingManager;
      }
    }

    /// <summary>Sets the title of publishing point.</summary>
    /// <param name="title">The title.</param>
    public void SetTitle(string title) => this.title = title;

    /// <summary>Sets the description of publishing point.</summary>
    /// <param name="description">The description.</param>
    public void SetDescription(string description) => this.description = description;

    /// <summary>Creates the publishing point.</summary>
    /// <param name="title">The title.</param>
    /// <returns></returns>
    public PublishingPoint CreatePublishingPoint()
    {
      if (string.IsNullOrEmpty(this.title))
        throw new ArgumentException("There is no assigned title to publishing point!");
      if (!this.DefinitionFields.Any<IDefinitionField>())
        throw new ArgumentException("There are no assigned definition fields to publishing point!");
      PublishingPoint point = this.PublishingManager.CreatePublishingPoint();
      point.PipeSettings.Clear();
      this.pipeSettings.ForEach((Action<PipeSettings>) (ps => point.PipeSettings.Add(ps)));
      PublishingPointFactory.CreatePublishingPointDataItem((IEnumerable<IDefinitionField>) this.DefinitionFields, (IPublishingPoint) point);
      if (string.IsNullOrEmpty(point.Name))
        point.Name = this.title;
      if (string.IsNullOrEmpty((string) point.Description))
        point.Description = (Lstring) this.description;
      point.IsActive = true;
      this.PublishingManager.SaveChanges();
      MetadataManager.GetManager().SaveChanges();
      return point;
    }

    /// <summary>
    /// Sets the pipe settings of publishing point that will be used for assigning a pipes to point when it is created.
    /// </summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    /// <param name="dataItems">The data items.</param>
    public void SetPipeSettings(IList<PipeSettings> pipeSettings, params object[] dataItems)
    {
      this.pipeSettings = pipeSettings.ToList<PipeSettings>();
      this.dataItems = dataItems;
    }

    /// <summary>
    /// Sets the publishing point definition fields that will define the structure of publishing point table.
    /// </summary>
    /// <param name="fields">The fields.</param>
    public void SetPublishingPointDefinitionFields(IEnumerable<IDefinitionField> fields) => this.DefinitionFields.AddRange(fields);

    /// <summary>
    /// Gets pipe settings for a specified pipe name if pipe settings exists otherwise adds default pipe settings.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="addedDefaultSettings">returns true if default settings are added, otherwise false.</param>
    /// <returns></returns>
    public PipeSettings GetOrAddDefaultPipeSettings(
      string pipeName,
      out bool addedDefaultSettings)
    {
      addedDefaultSettings = false;
      if (!this.pipeSettings.Any<PipeSettings>((Func<PipeSettings, bool>) (ps => ps.PipeName == pipeName)))
      {
        addedDefaultSettings = true;
        this.pipeSettings.Add(PublishingSystemFactory.GetPipe(pipeName).GetDefaultSettings());
      }
      IEnumerable<PipeSettings> source = this.pipeSettings.Where<PipeSettings>((Func<PipeSettings, bool>) (ps => ps.PipeName == pipeName));
      foreach (PipeSettings pipeSettings in source)
      {
        if (!pipeSettings.Mappings.Mappings.Any<Mapping>())
          pipeSettings.Mappings = PublishingSystemFactory.CreateMappingSettings(pipeName, pipeSettings.IsInbound, this.PublishingManager);
      }
      source.Where<PipeSettings>((Func<PipeSettings, bool>) (ps => ps.ApplicationName == null)).ToList<PipeSettings>().ForEach((Action<PipeSettings>) (ps => ps.ApplicationName = this.PublishingManager.Provider.ApplicationName));
      PipeSettings defaultPipeSettings = source.FirstOrDefault<PipeSettings>((Func<PipeSettings, bool>) (ps => ps.PipeName == pipeName));
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (appSettings.Multilingual)
        defaultPipeSettings.LanguageIds.Add(appSettings.DefaultFrontendLanguage.Name);
      return defaultPipeSettings;
    }

    internal object GetSingleDataItem() => this.dataItems != null && ((IEnumerable<object>) this.dataItems).Any<object>() ? this.dataItems[0] : (object) null;
  }
}
