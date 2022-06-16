// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.ConfigurationsInboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Basic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>Advanced Settings Inbound Pipe</summary>
  internal class ConfigurationsInboundPipe : 
    BasePipe<ConfigurationsPipeSettings>,
    IPushPipe,
    IInboundPipe
  {
    /// <summary>Pipe name</summary>
    public const string PipeName = "ConfigurationsInboundPipe";
    private IPublishingPointBusinessObject publishingPoint;
    private IDefinitionField[] definitionFields;
    private const string CommaSeparator = ",";
    private readonly string configSectionDelimiter = "|".Base64Encode();

    /// <inheritdoc />
    public override IDefinitionField[] Definition
    {
      get
      {
        if (this.definitionFields == null)
          this.definitionFields = PublishingSystemFactory.GetPipeDefinitions(this.Name);
        return this.definitionFields;
      }
    }

    /// <inheritdoc />
    public override string Name => nameof (ConfigurationsInboundPipe);

    /// <inheritdoc />
    public override bool CanProcessItem(object item)
    {
      switch (item)
      {
        case null:
          return false;
        case PublishingSystemEventInfo publishingSystemEventInfo:
          return this.CanProcessItem(publishingSystemEventInfo.Item);
        case ConfigSection _:
          return true;
        default:
          return false;
      }
    }

    /// <inheritdoc />
    public override void Initialize(PipeSettings pipeSettings)
    {
      this.PipeSettingsInternal = (ConfigurationsPipeSettings) pipeSettings;
      this.publishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettings.PublishingPoint);
    }

    /// <inheritdoc />
    public void PushData(IList<PublishingSystemEventInfo> items)
    {
      using (new CultureRegion(SystemManager.CurrentContext.AppSettings.DefaultBackendLanguage))
      {
        List<WrapperObject> items1 = new List<WrapperObject>();
        List<WrapperObject> items2 = new List<WrapperObject>();
        foreach (PublishingSystemEventInfo publishingSystemEventInfo in (IEnumerable<PublishingSystemEventInfo>) items)
        {
          foreach (SettingModel settingModel in (publishingSystemEventInfo.Item as ConfigSection).GetSettingsMetadataFromSection())
          {
            try
            {
              WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, (object) settingModel);
              this.SetProperties(wrapperObject, settingModel);
              items2.Add(wrapperObject);
              items1.Add(wrapperObject);
            }
            catch (Exception ex)
            {
              this.HandleError("Error when push data for item {0}.".Arrange((object) settingModel), ex);
            }
          }
        }
        if (items2.Count > 0)
          this.publishingPoint.RemoveItems((IList<WrapperObject>) items2);
        if (items1.Count <= 0)
          return;
        this.publishingPoint.AddItems((IList<WrapperObject>) items1);
      }
    }

    /// <inheritdoc />
    public void ToPublishingPoint()
    {
      foreach (ConfigSection allConfigSection in ConfigManager.GetManager().GetAllConfigSections())
      {
        List<PublishingSystemEventInfo> publishingSystemEventInfoList = new List<PublishingSystemEventInfo>();
        this.AddModifiedEventInfo(publishingSystemEventInfoList, allConfigSection);
        this.PushData((IList<PublishingSystemEventInfo>) publishingSystemEventInfoList);
      }
    }

    private void AddModifiedEventInfo(
      List<PublishingSystemEventInfo> eventInfo,
      ConfigSection setting)
    {
      PublishingSystemEventInfo publishingSystemEventInfo = new PublishingSystemEventInfo()
      {
        Item = (object) setting,
        ItemAction = "SystemObjectModified"
      };
      eventInfo.Add(publishingSystemEventInfo);
    }

    internal static ConfigurationsPipeSettings GetTemplatePipeSettings()
    {
      ConfigurationsPipeSettings templatePipeSettings = new ConfigurationsPipeSettings();
      templatePipeSettings.IsInbound = true;
      templatePipeSettings.PipeName = nameof (ConfigurationsInboundPipe);
      templatePipeSettings.IsActive = true;
      templatePipeSettings.MaxItems = 0;
      templatePipeSettings.InvocationMode = PipeInvokationMode.Push;
      return templatePipeSettings;
    }

    internal static List<Mapping> GetDefaultMappings() => new List<Mapping>()
    {
      PublishingSystemFactory.CreateMapping("PipeId", "TransparentTranslator", false, "PipeId"),
      PublishingSystemFactory.CreateMapping("OriginalItemId", "TransparentTranslator", true, "OriginalItemId"),
      PublishingSystemFactory.CreateMapping("Title", "concatenationtranslator", true, "Title"),
      PublishingSystemFactory.CreateMapping("Key", "TransparentTranslator", true, "Key"),
      PublishingSystemFactory.CreateMapping("Content", "TransparentTranslator", true, "Description"),
      PublishingSystemFactory.CreateMapping("SectionName", "TransparentTranslator", true, "SectionName"),
      PublishingSystemFactory.CreateMapping("Path", "TransparentTranslator", true, "Path"),
      PublishingSystemFactory.CreateMapping("FullSettingName", "TransparentTranslator", true, "FullSettingName")
    };

    internal static IList<IDefinitionField> CreateDefaultAdvancedSettingsPipeDefinitions() => (IList<IDefinitionField>) new List<IDefinitionField>()
    {
      (IDefinitionField) new SimpleDefinitionField("Id", Res.Get<PublishingMessages>().ContentId),
      (IDefinitionField) new SimpleDefinitionField("Title", Res.Get<PublishingMessages>().ContentTitle),
      (IDefinitionField) new SimpleDefinitionField("Key", Res.Get<PublishingMessages>().SettingKey),
      (IDefinitionField) new SimpleDefinitionField("Description", Res.Get<PublishingMessages>().Description),
      (IDefinitionField) new SimpleDefinitionField("Path", Res.Get<PublishingMessages>().SettingPath),
      (IDefinitionField) new SimpleDefinitionField("FullSettingName", Res.Get<PublishingMessages>().FullSettingName),
      (IDefinitionField) new SimpleDefinitionField("SectionName", Res.Get<PublishingMessages>().SectionName)
    };

    private void SetProperties(WrapperObject wrapperObject, SettingModel settingItem)
    {
      string path = string.Join(",", (IEnumerable<string>) this.BuildPath(settingItem));
      string str = string.Join(this.configSectionDelimiter, (IEnumerable<string>) this.BuildFullSettingName(settingItem));
      wrapperObject.MappingSettings = this.PipeSettings.Mappings;
      wrapperObject.SetOrAddProperty("PipeId", (object) this.PipeSettings.Id);
      wrapperObject.SetOrAddProperty("Path", (object) path);
      wrapperObject.SetOrAddProperty("FullSettingName", (object) str);
      wrapperObject.SetOrAddProperty("OriginalItemId", (object) this.ConstructIdentifier(path, settingItem.Key));
      wrapperObject.SetOrAddProperty("SectionName", (object) settingItem.SectionName);
    }

    private SettingModel GetSettingItem(PublishingSystemEventInfo item) => (SettingModel) item.Item;

    private ICollection<string> BuildPath(SettingModel settingItem)
    {
      if (settingItem.Parent == null)
        return (ICollection<string>) new List<string>()
        {
          settingItem.Title
        };
      ICollection<string> strings = this.BuildPath(settingItem.Parent);
      if (settingItem.NodeType == SettingNodeType.Navigation)
        strings.Add(settingItem.Title);
      return strings;
    }

    private Stack<string> BuildFullSettingName(SettingModel settingItem)
    {
      if (settingItem.Parent == null)
        return new Stack<string>((IEnumerable<string>) new string[1]
        {
          settingItem.SectionName
        });
      Stack<string> stringStack = this.BuildFullSettingName(settingItem.Parent);
      if (settingItem.NodeType == SettingNodeType.Navigation)
      {
        if (string.IsNullOrWhiteSpace(settingItem.CollectionKey))
          stringStack.Push(settingItem.Key ?? "");
        else
          stringStack.Push(settingItem.Key + "_" + settingItem.CollectionKey);
      }
      return stringStack;
    }

    private Guid ConstructIdentifier(string path, string name)
    {
      string s = string.Format("{0},{1}", (object) path, (object) name);
      Guid empty = Guid.Empty;
      using (SHA256 shA256 = SHA256.Create())
        return new Guid(((IEnumerable<byte>) shA256.ComputeHash(Encoding.Default.GetBytes(s))).Take<byte>(16).ToArray<byte>());
    }
  }
}
