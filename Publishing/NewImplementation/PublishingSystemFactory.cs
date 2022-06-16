// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingSystemFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Publishing.PublishingPoints;
using Telerik.Sitefinity.Publishing.Translators;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// The factory used by publishing system to operate with pipes and publishing points.
  /// </summary>
  public static class PublishingSystemFactory
  {
    private static readonly object synch = new object();
    private static readonly object templatesSync = new object();
    private static readonly object mappingsSync = new object();
    private static readonly object definitionsSync = new object();
    private static readonly object pipeSettingsSync = new object();
    private static readonly object publishingPointMetaFieldsSync = new object();
    private static readonly object pipeDescriptionProvidersSync = new object();
    private static readonly object pipeDesignersSync = new object();
    private static IDictionary<string, Type> pipes = SystemManager.CreateStaticCache<string, Type>();
    private static IDictionary<string, Type> publishingPoints = SystemManager.CreateStaticCache<string, Type>();
    private static IDictionary<string, TranslatorBase> translators = SystemManager.CreateStaticCache<string, TranslatorBase>();
    private static IDictionary<string, List<PipeSettings>> templates = SystemManager.CreateStaticCache<string, List<PipeSettings>>();
    private static IDictionary<string, IList<Mapping>> mappings = SystemManager.CreateStaticCache<string, IList<Mapping>>();
    private static IDictionary<string, IList<IDefinitionField>> definitions = SystemManager.CreateStaticCache<string, IList<IDefinitionField>>();
    private static IDictionary<string, PipeSettings> pipeSettings = SystemManager.CreateStaticCache<string, PipeSettings>();
    private static IDictionary<string, IDefinitionField> publishingPointMetaFields = SystemManager.CreateStaticCache<string, IDefinitionField>();
    private static IDictionary<string, PipeDescriptionCompositeProvider> pipeDescriptionProviders = SystemManager.CreateStaticCache<string, PipeDescriptionCompositeProvider>();
    private static IDictionary<string, Type> pipeDesigners = SystemManager.CreateStaticCache<string, Type>();
    private static IDictionary<string, PipeSettings> defaultPipeSettings = SystemManager.CreateStaticCache<string, PipeSettings>();

    /// <summary>Gets the registered pipes.</summary>
    /// <returns></returns>
    public static IDictionary<string, Type> GetRegisteredPipes()
    {
      lock (PublishingSystemFactory.synch)
        return PublishingSystemFactory.pipes;
    }

    /// <summary>Unregisters all pipes</summary>
    public static void UnregisterAllPipes() => PublishingSystemFactory.pipes = (IDictionary<string, Type>) new Dictionary<string, Type>();

    public static IPipe GetPipe(string pipeName)
    {
      object pipe = PublishingSystemFactory.pipes.ContainsKey(pipeName) ? Activator.CreateInstance(PublishingSystemFactory.pipes[pipeName]) : throw new ArgumentException(string.Format("No pipe registered with name '{0}'!", (object) pipeName));
      if (pipe is IDynamicPipe dynamicPipe)
        dynamicPipe.SetPipeName(pipeName);
      return (IPipe) pipe;
    }

    /// <summary>Get already registered pipe</summary>
    /// <param name="pipe">The pipe settings to search for</param>
    /// <returns>The pipe with requested settings</returns>
    /// <exception cref="T:System.ArgumentException">If pipe with required settings does not exist.</exception>
    public static IPipe GetPipe(PipeSettings pipe)
    {
      IPipe pipe1 = PublishingSystemFactory.GetPipe(pipe.PipeName);
      pipe1.Initialize(pipe);
      return pipe1;
    }

    /// <summary>Gets the publishing pipes</summary>
    /// <returns>The pipes</returns>
    public static List<IPipe> GetPublishingPipes()
    {
      List<IPipe> publishingPipes = new List<IPipe>();
      foreach (string key in (IEnumerable<string>) PublishingSystemFactory.GetRegisteredPipes().Keys)
      {
        IPipe pipe = PublishingSystemFactory.GetPipe(key);
        publishingPipes.Add(pipe);
      }
      return publishingPipes;
    }

    /// <summary>Registers pipe by name and type</summary>
    /// <param name="pipeName">The name of the pipe</param>
    /// <param name="pipeType">The type of the pipe</param>
    /// <exception cref="T:System.ArgumentException">If <paramref name="pipeType" /> does not implement <see cref="T:Telerik.Sitefinity.Publishing.IPipe" /> or pipe with the same name already exists.</exception>
    public static void RegisterPipe(string pipeName, Type pipeType)
    {
      if (!pipeType.ImplementsInterface(typeof (IPipe)))
        throw new ArgumentException(string.Format("The type of the pipe must implement '{0}' interface!", (object) "IPipe"));
      if (PublishingSystemFactory.pipes.ContainsKey(pipeName))
        PublishingSystemFactory.pipes.Remove(pipeName);
      PublishingSystemFactory.pipes.Add(pipeName, pipeType);
    }

    /// <summary>Registers type, used by content pipe</summary>
    /// <param name="type">The type to be registered</param>
    public static void RegisterType(Type type, PublishingConfig config = null)
    {
      if (!typeof (IContent).IsAssignableFrom(type))
        throw new ArgumentException("The registered type should extend 'Telerik.Sitefinity.Model.IContent'.");
      if (config == null)
        config = Config.Get<PublishingConfig>();
      config.ContentPipeTypes.Add(new TypeConfigElement()
      {
        AssemblyQualifiedName = type.AssemblyQualifiedName,
        FullName = type.FullName
      });
    }

    /// <summary>
    /// Checks if a type is registered to be used by the content pipe.
    /// </summary>
    /// <param name="type">The type to be registered</param>
    public static bool TypeRegistered(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      return Config.Get<PublishingConfig>().ContentPipeTypes.Contains(type.FullName);
    }

    /// <summary>Unregisters pipe by it's name</summary>
    /// <param name="pipeName">The name of the pipe</param>
    public static void UnregisterPipe(string pipeName)
    {
      if (!PublishingSystemFactory.pipes.ContainsKey(pipeName))
        return;
      PublishingSystemFactory.pipes.Remove(pipeName);
    }

    /// <summary>Checks if a pipe is registered by its name</summary>
    /// <param name="pipeName">The name of the pipe</param>
    public static bool IsPipeRegistered(string pipeName) => PublishingSystemFactory.pipes.ContainsKey(pipeName);

    /// <summary>Unregisters all publishing points</summary>
    public static void UnregisterAllPublishingPoints() => PublishingSystemFactory.publishingPoints = (IDictionary<string, Type>) new Dictionary<string, Type>();

    /// <summary>Get already registered publishing point</summary>
    /// <param name="pp">The settings of the publishing point.</param>
    /// <returns>The publishing point with requested settings</returns>
    /// <exception cref="T:System.IndexOutOfRangeException">If publishing point with required settings does not exist.</exception>
    public static IPublishingPointBusinessObject GetPublishingPoint(
      PublishingPoint pp)
    {
      if (pp == null)
        return (IPublishingPointBusinessObject) null;
      IPublishingPointBusinessObject publishingPoint = PublishingSystemFactory.GetPublishingPoint(pp.PublishingPointBusinessObjectName);
      publishingPoint.Initialize(pp);
      return publishingPoint;
    }

    public static ICollection<string> GetRegisteredPublishingPointNames() => PublishingSystemFactory.publishingPoints.Keys;

    /// <summary>Get already registered publishing point</summary>
    /// <param name="name">The name of the publishing point</param>
    /// <returns>The publishing point with requested name</returns>
    /// <exception cref="T:System.ArgumentException">If publishing point with required name does not exist.</exception>
    public static IPublishingPointBusinessObject GetPublishingPoint(
      string name)
    {
      if (string.IsNullOrEmpty(name))
        return (IPublishingPointBusinessObject) Activator.CreateInstance(typeof (PassThroughPublishingPoint));
      return PublishingSystemFactory.publishingPoints.ContainsKey(name) ? (IPublishingPointBusinessObject) Activator.CreateInstance(PublishingSystemFactory.publishingPoints[name]) : throw new ArgumentException(string.Format("No publishing point registered with name '{0}'!", (object) name));
    }

    /// <summary>Registers publishing point by name and type</summary>
    /// <param name="publishingPointName">The name of the publishing point</param>
    /// <param name="publishingPointType">The type of the publishing point</param>
    /// <exception cref="T:System.ArgumentException">If <paramref name="publishingPointType" /> does not implement <see cref="T:Telerik.Sitefinity.Publishing.Model.IPublishingPoint" /> or publishing point with the same name already exists.</exception>
    public static void RegisterPublishingPoint(string publishingPointName, Type publishingPointType)
    {
      if (PublishingSystemFactory.publishingPoints.ContainsKey(publishingPointName))
        PublishingSystemFactory.publishingPoints.Remove(publishingPointName);
      if (PublishingSystemFactory.publishingPoints.ContainsKey(publishingPointName))
        throw new ArgumentException(string.Format("The publishing point with name '{0}' already exists. Please use different one!", (object) publishingPointName));
      if (!publishingPointType.ImplementsInterface(typeof (IPublishingPointBusinessObject)))
        throw new ArgumentException(string.Format("The type of the publishing point must implements '{0}' interface!", (object) "IPublishingPoint"));
      PublishingSystemFactory.publishingPoints.Add(publishingPointName, publishingPointType);
    }

    /// <summary>Unregisters publishing point by it's name</summary>
    /// <param name="publishingPointName">The name of the publishing point</param>
    public static void UnregisterPublishingPoint(string publishingPointName)
    {
      if (!PublishingSystemFactory.publishingPoints.ContainsKey(publishingPointName))
        return;
      PublishingSystemFactory.publishingPoints.Remove(publishingPointName);
    }

    /// <summary>Register a translator by its name</summary>
    /// <param name="translator">Translator to register</param>
    /// <exception cref="T:System.ArgumentNullException">if <paramref name="translator" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">If <paramref name="translator" />'s name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">IF there is another translator register with the same name.</exception>
    public static void RegisterTranslator(TranslatorBase translator)
    {
      if (translator == null)
        throw new ArgumentNullException(nameof (translator));
      if (translator.Name.IsNullOrEmpty())
        throw new ArgumentException("translator.Name is null or empty.");
      lock (PublishingSystemFactory.synch)
      {
        if (!PublishingSystemFactory.translators.ContainsKey(translator.Name))
          PublishingSystemFactory.translators.Add(translator.Name, translator);
        else
          PublishingSystemFactory.translators[translator.Name] = translator;
      }
    }

    /// <summary>Look up a translator by its name</summary>
    /// <param name="name">Name of the translator</param>
    /// <returns>Translator, if it was registered by this name, or null if it weren't</returns>
    public static TranslatorBase ResolveTranslator(string name)
    {
      if (name.IsNullOrEmpty())
        throw new ArgumentException("name is null or empty");
      lock (PublishingSystemFactory.synch)
      {
        TranslatorBase translatorBase;
        if (!PublishingSystemFactory.translators.TryGetValue(name, out translatorBase))
          translatorBase = (TranslatorBase) null;
        return translatorBase;
      }
    }

    /// <summary>Unregister a translator by its name</summary>
    /// <param name="name">Name of the translator</param>
    public static void UnregisterTranslator(string name)
    {
      if (name.IsNullOrEmpty())
        throw new ArgumentException("name is null or empty");
      lock (PublishingSystemFactory.synch)
      {
        if (!PublishingSystemFactory.translators.ContainsKey(name))
          return;
        PublishingSystemFactory.translators.Remove(name);
      }
    }

    /// <summary>
    /// Registers a pipe to a template by its settings.
    /// Accepts a predicate to search for an already registered pipe to replace it.
    /// If the predicate is null no search is performed and the pipe settings is appended.
    /// </summary>
    /// <param name="templateName">Name of the template.</param>
    /// <param name="pipeSettings">The pipe settings.</param>
    /// <param name="predicate">The predicate to search for an already registered pipe.</param>
    public static void RegisterTemplatePipe(
      string templateName,
      PipeSettings pipeSettings,
      Predicate<PipeSettings> predicate = null)
    {
      if (string.IsNullOrEmpty(templateName))
        throw new ArgumentException("The templateName parameter cannot be empty.");
      if (pipeSettings == null)
        throw new ArgumentNullException(nameof (pipeSettings));
      lock (PublishingSystemFactory.templatesSync)
      {
        if (!PublishingSystemFactory.templates.ContainsKey(templateName))
        {
          PublishingSystemFactory.templates.Add(templateName, new List<PipeSettings>()
          {
            pipeSettings
          });
        }
        else
        {
          List<PipeSettings> template = PublishingSystemFactory.templates[templateName];
          if (predicate != null)
          {
            int index = template.FindIndex(predicate);
            if (index >= 0)
              template[index] = pipeSettings;
            else
              template.Add(pipeSettings);
          }
          else
            template.Add(pipeSettings);
        }
      }
    }

    /// <summary>
    /// Determines whether a template with the given name is registered.
    /// </summary>
    /// <param name="templateName">Name of the template.</param>
    /// <returns>
    /// 	<c>true</c> if the template is registered; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsPipeTemplateRegistered(string templateName) => !string.IsNullOrEmpty(templateName) ? PublishingSystemFactory.templates.ContainsKey(templateName) : throw new ArgumentException("The templateName parameter cannot be empty.");

    /// <summary>
    /// Gets the settings of the pipes registered to a given template.
    /// </summary>
    /// <param name="templateName">Name of the template.</param>
    /// <returns></returns>
    public static List<PipeSettings> GetTemplatePipes(string templateName)
    {
      if (string.IsNullOrEmpty(templateName))
        throw new ArgumentException("The templateName parameter cannot be empty.");
      lock (PublishingSystemFactory.templatesSync)
        return PublishingSystemFactory.templates.ContainsKey(templateName) ? PublishingSystemFactory.templates[templateName] : throw new ArgumentException("There is no registered template with this name.");
    }

    /// <summary>
    /// Unregisters a pipe settings from a given template by pipe name.
    /// </summary>
    /// <param name="templateName">Name of the template.</param>
    /// <param name="pipeName">Name of the pipe.</param>
    public static void UnregisterTemplatePipe(string templateName, string pipeName)
    {
      if (string.IsNullOrEmpty(templateName))
        throw new ArgumentException("The templateName parameter cannot be empty.");
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      lock (PublishingSystemFactory.templatesSync)
      {
        if (!PublishingSystemFactory.templates.ContainsKey(templateName))
          return;
        List<PipeSettings> template = PublishingSystemFactory.templates[templateName];
        int index = template.FindIndex((Predicate<PipeSettings>) (settings => settings.PipeName == pipeName));
        if (index >= 0)
          template.RemoveAt(index);
        if (template.Count != 0)
          return;
        PublishingSystemFactory.templates.Remove(templateName);
      }
    }

    /// <summary>Unregisters the template with the given name.</summary>
    /// <param name="templateName">Name of the template.</param>
    public static void UnregisterTemplate(string templateName)
    {
      if (string.IsNullOrEmpty(templateName))
        throw new ArgumentException("The templateName parameter cannot be empty.");
      lock (PublishingSystemFactory.templatesSync)
      {
        if (!PublishingSystemFactory.templates.ContainsKey(templateName))
          return;
        PublishingSystemFactory.templates.Remove(templateName);
      }
    }

    /// <summary>Unregisters all templates.</summary>
    public static void UnregisterAllTemplates()
    {
      lock (PublishingSystemFactory.templatesSync)
        PublishingSystemFactory.templates.Clear();
    }

    public static void UpdatePublishingPointsBasedOnTemplate()
    {
      foreach (DataProviderSettings providerSetting in (ConfigElementCollection) Config.Get<PublishingConfig>().ProviderSettings)
      {
        string transactionName = "UpdatePublishingPointsFromTemplate_" + (object) Guid.NewGuid();
        try
        {
          PublishingManager manager = PublishingManager.GetManager(providerSetting.Name, transactionName);
          bool flag = false;
          IQueryable<PublishingPoint> publishingPoints = manager.GetPublishingPoints();
          Expression<Func<PublishingPoint, bool>> predicate = (Expression<Func<PublishingPoint, bool>>) (p => p.InboundPipesTemplate != default (string));
          foreach (PublishingPoint publishingPoint in (IEnumerable<PublishingPoint>) publishingPoints.Where<PublishingPoint>(predicate))
          {
            if (PublishingSystemFactory.TryUpdatePublishingPointBasedOnTemplateInternal(manager, publishingPoint))
              flag = true;
          }
          if (flag)
            TransactionManager.CommitTransaction(transactionName);
        }
        catch (Exception ex)
        {
          TransactionManager.RollbackTransaction(transactionName);
          Log.Error("Failed to update publishing points based on template. Exception: {0}", (object) ex);
        }
      }
    }

    public static void UpdatePublishingPointBasedOnTemplate(
      Guid publishingPointId,
      string providerName)
    {
      string transactionName = "UpdatePublishingPointsFromTemplate_" + (object) Guid.NewGuid();
      try
      {
        PublishingManager manager = PublishingManager.GetManager(providerName, transactionName);
        bool flag = false;
        PublishingPoint publishingPoint = manager.GetPublishingPoints().FirstOrDefault<PublishingPoint>((Expression<Func<PublishingPoint, bool>>) (p => p.Id == publishingPointId && p.InboundPipesTemplate != default (string)));
        if (publishingPoint != null)
          flag = PublishingSystemFactory.TryUpdatePublishingPointBasedOnTemplateInternal(manager, publishingPoint);
        if (!flag)
          return;
        TransactionManager.CommitTransaction(transactionName);
      }
      catch (Exception ex)
      {
        TransactionManager.RollbackTransaction(transactionName);
        Log.Error("Failed to update publishing points based on template. Exception: {0}", (object) ex);
      }
    }

    private static bool TryUpdatePublishingPointBasedOnTemplateInternal(
      PublishingManager manager,
      PublishingPoint publishingPoint)
    {
      bool flag = false;
      List<PipeSettings> templatePipes = PublishingSystemFactory.GetTemplatePipes(publishingPoint.InboundPipesTemplate);
      if (templatePipes != null)
      {
        List<PipeSettings> list = publishingPoint.PipeSettings.ToList<PipeSettings>();
        foreach (PipeSettings pipeSettings in templatePipes)
        {
          PipeSettings inboundPipeSettings = pipeSettings;
          if (!list.Any<PipeSettings>((Func<PipeSettings, bool>) (p => p.PipeName == inboundPipeSettings.PipeName)))
          {
            PublishingPointInitializer.CreatePipeSettings(publishingPoint, inboundPipeSettings, manager);
            flag = true;
          }
        }
      }
      return flag;
    }

    /// <summary>
    /// Registers a back end and front end pipe to a template by its settings.
    /// Accepts a predicate to search for an already registered pipe to replace it.
    /// If the predicate is null no search is performed and the pipe settings is appended.
    /// </summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    /// <param name="predicate">The predicate to search for an already registered pipe.</param>
    public static void RegisterPipeForAllContentTemplates(
      PipeSettings pipeSettings,
      Predicate<PipeSettings> predicate = null)
    {
      PublishingSystemFactory.RegisterTemplatePipe("SearchItemTemplate", pipeSettings, predicate);
      PublishingSystemFactory.RegisterTemplatePipe("BackendSearchItemTemplate", pipeSettings, predicate);
    }

    /// <summary>
    /// Registers a list of mappings to an inbound/outbound pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="isInbound">Specifies whether the pipe is inbound.</param>
    /// <param name="mappings">The mappings to be registered.</param>
    public static void RegisterPipeMappings(
      string pipeName,
      bool isInbound,
      IList<Mapping> mappings)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      if (mappings == null)
        throw new ArgumentNullException(nameof (mappings));
      string pipeKey = PublishingSystemFactory.GetPipeKey(pipeName, isInbound);
      lock (PublishingSystemFactory.mappingsSync)
        PublishingSystemFactory.mappings[pipeKey] = mappings;
    }

    /// <summary>
    /// Determines whether any mappings are registered to an inbound/outbound pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="isInbound">Specifies whether the pipe is inbound.</param>
    /// <returns></returns>
    public static bool PipeMappingsRegistered(string pipeName, bool isInbound)
    {
      string key = !string.IsNullOrEmpty(pipeName) ? PublishingSystemFactory.GetPipeKey(pipeName, isInbound) : throw new ArgumentException("The pipeName parameter cannot be empty.");
      return PublishingSystemFactory.mappings.ContainsKey(key);
    }

    /// <summary>
    /// Unregisters the mappings registered to the inbound/outbound pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="isInbound">Specifies whether the pipe is inbound.</param>
    public static void UnregisterPipeMappings(string pipeName, bool isInbound)
    {
      string key = !string.IsNullOrEmpty(pipeName) ? PublishingSystemFactory.GetPipeKey(pipeName, isInbound) : throw new ArgumentException("The pipeName parameter cannot be empty.");
      lock (PublishingSystemFactory.mappingsSync)
      {
        if (!PublishingSystemFactory.mappings.ContainsKey(key))
          return;
        PublishingSystemFactory.mappings.Remove(key);
      }
    }

    /// <summary>Unregisters all pipe mappings.</summary>
    public static void UnregisterAllPipeMappings()
    {
      lock (PublishingSystemFactory.mappingsSync)
        PublishingSystemFactory.mappings.Clear();
    }

    /// <summary>
    /// Gets the mappings registered to the inbound/outbound pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="isInbound">Specifies whether the pipe is inbound.</param>
    /// <returns></returns>
    public static IList<Mapping> GetPipeMappings(string pipeName, bool isInbound)
    {
      string key = !string.IsNullOrEmpty(pipeName) ? PublishingSystemFactory.GetPipeKey(pipeName, isInbound) : throw new ArgumentException("The pipeName parameter cannot be empty.");
      lock (PublishingSystemFactory.mappingsSync)
        return PublishingSystemFactory.mappings.ContainsKey(key) ? PublishingSystemFactory.mappings[key] : throw new ArgumentException("There are no mappings suitable for this pipe.");
    }

    /// <summary>
    /// Creates and returns mapping settings for the inbound/outbound pipe with the given name using the passed publishing manager.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="isInbound">Specifies whether the pipe is inbound.</param>
    /// <param name="manager">The publishing manager.</param>
    /// <returns></returns>
    public static MappingSettings CreateMappingSettings(
      string pipeName,
      bool isInbound,
      PublishingManager manager)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      if (PublishingSystemFactory.mappings == null)
        throw new ArgumentNullException("mappings");
      MappingSettings mappingSettings = manager != null ? manager.CreateMappingSettings() : throw new ArgumentNullException(nameof (manager));
      foreach (Mapping pipeMapping in (IEnumerable<Mapping>) PublishingSystemFactory.GetPipeMappings(pipeName, isInbound))
        mappingSettings.Mappings.Add(PublishingSystemFactory.CreateMappingHelper(manager, pipeMapping));
      return mappingSettings;
    }

    private static string GetPipeKey(string pipe, bool isInbound) => string.Format("{0}__{1}", (object) pipe, (object) isInbound);

    private static Mapping CreateMappingHelper(
      PublishingManager manager,
      Mapping sourceMapping)
    {
      Mapping mapping = manager.CreateMapping();
      sourceMapping.CopyTo(mapping);
      return mapping;
    }

    /// <summary>Creates a mapping from the given parameters.</summary>
    /// <param name="destinationProperty">The destination property name.</param>
    /// <param name="translatorName">Name of the translator.</param>
    /// <param name="isRequired">if set to <c>true</c> the mapping is required.</param>
    /// <param name="sourceProperties">The names of the source properties.</param>
    /// <returns></returns>
    public static Mapping CreateMapping(
      string destinationProperty,
      string translatorName,
      bool isRequired,
      params string[] sourceProperties)
    {
      Mapping mapping = new Mapping()
      {
        DestinationPropertyName = destinationProperty,
        IsRequired = isRequired,
        SourcePropertyNames = sourceProperties
      };
      if (!translatorName.IsNullOrEmpty())
        mapping.Translations.Add(new PipeMappingTranslation()
        {
          TranslatorName = translatorName
        });
      return mapping;
    }

    /// <summary>Creates a mapping from the given parameters.</summary>
    /// <param name="destinationProperty">The destination property name.</param>
    /// <param name="translatorNames">The translator names.</param>
    /// <param name="isRequired">if set to <c>true</c> the mapping is required.</param>
    /// <param name="sourceProperties">The names of the source properties.</param>
    /// <returns></returns>
    public static Mapping CreateMapping(
      string destinationProperty,
      IEnumerable<string> translatorNames,
      bool isRequired,
      params string[] sourceProperties)
    {
      Mapping mapping = new Mapping()
      {
        DestinationPropertyName = destinationProperty,
        IsRequired = isRequired,
        SourcePropertyNames = sourceProperties
      };
      if (translatorNames != null)
      {
        foreach (string translatorName in translatorNames)
          mapping.Translations.Add(new PipeMappingTranslation()
          {
            TranslatorName = translatorName
          });
      }
      return mapping;
    }

    /// <summary>
    /// Registers pipe definitions for a given pipe by its name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="definitions">The definitions.</param>
    public static void RegisterPipeDefinitions(string pipeName, IList<IDefinitionField> definitions)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      if (definitions == null)
        throw new ArgumentNullException(nameof (definitions));
      lock (PublishingSystemFactory.definitionsSync)
        PublishingSystemFactory.definitions[pipeName] = definitions;
    }

    /// <summary>
    /// Determines whether any definitions are registered to a pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    public static bool PipeDefinitionsRegistered(string pipeName) => !string.IsNullOrEmpty(pipeName) ? PublishingSystemFactory.definitions.ContainsKey(pipeName) : throw new ArgumentException("The pipeName parameter cannot be empty.");

    /// <summary>
    /// Unregisters the pipe definitions registered to the pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    public static void UnregisterPipeDefinitions(string pipeName)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      lock (PublishingSystemFactory.definitionsSync)
      {
        if (!PublishingSystemFactory.definitions.ContainsKey(pipeName))
          return;
        PublishingSystemFactory.definitions.Remove(pipeName);
      }
    }

    /// <summary>Unregisters all pipe definitions.</summary>
    public static void UnregisterAllPipeDefinitions()
    {
      lock (PublishingSystemFactory.definitionsSync)
        PublishingSystemFactory.definitions.Clear();
    }

    /// <summary>
    /// Gets the pipe definitions registered to the pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    public static IDefinitionField[] GetPipeDefinitions(string pipeName)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      lock (PublishingSystemFactory.definitionsSync)
      {
        IList<IDefinitionField> definitionFieldList = PublishingSystemFactory.definitions.ContainsKey(pipeName) ? PublishingSystemFactory.definitions[pipeName] : throw new ArgumentException("There are no definitions suitable for this pipe type.");
        IDefinitionField[] pipeDefinitions = new IDefinitionField[definitionFieldList.Count];
        for (int index = 0; index < definitionFieldList.Count; ++index)
        {
          SimpleDefinitionField simpleDefinitionField = new SimpleDefinitionField();
          simpleDefinitionField.CopyFrom(definitionFieldList[index]);
          pipeDefinitions[index] = (IDefinitionField) simpleDefinitionField;
        }
        return pipeDefinitions;
      }
    }

    /// <summary>
    /// Registers pipe definitions for a pipe that uses <see cref="T:SitefinityContentPipeSettings" />.
    /// The pipe is registered by its name and the type of the content.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="definitions">The definitions.</param>
    public static void RegisterContentPipeDefinitions(
      string pipeName,
      string contentType,
      IList<IDefinitionField> definitions)
    {
      PublishingSystemFactory.RegisterPipeDefinitions(PublishingSystemFactory.GetContentPipeKey(pipeName, contentType), definitions);
    }

    /// <summary>
    /// Determines whether any definitions are registered to a pipe that uses <see cref="T:SitefinityContentPipeSettings" />.
    /// The check is made by the given pipe name and the type of the content.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    public static bool ContentPipeDefinitionsRegistered(string pipeName, string contentType) => PublishingSystemFactory.PipeDefinitionsRegistered(PublishingSystemFactory.GetContentPipeKey(pipeName, contentType));

    /// <summary>
    /// Unregisters the pipe definitions registered to a pipe that uses <see cref="T:SitefinityContentPipeSettings" />.
    /// The method uses the given pipe name and the type of the content.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="contentType">Type of the content.</param>
    public static void UnregisterContentPipeDefinitions(string pipeName, string contentType) => PublishingSystemFactory.UnregisterPipeDefinitions(PublishingSystemFactory.GetContentPipeKey(pipeName, contentType));

    /// <summary>
    /// Gets the pipe definitions registered to a pipe that uses <see cref="T:SitefinityContentPipeSettings" />.
    /// The method uses the given pipe name and the type of the content.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    public static IDefinitionField[] GetContentPipeDefinitions(
      string pipeName,
      string contentType)
    {
      return PublishingSystemFactory.GetPipeDefinitions(PublishingSystemFactory.GetContentPipeKey(pipeName, contentType));
    }

    private static string GetContentPipeKey(string pipeName, string contentType) => string.Format("{0}_{1}", (object) pipeName, (object) contentType);

    /// <summary>Creates the default content pipe definitions.</summary>
    /// <returns></returns>
    public static IList<IDefinitionField> CreateDefaultContentPipeDefinitions() => (IList<IDefinitionField>) new List<IDefinitionField>()
    {
      (IDefinitionField) new SimpleDefinitionField("Id", Res.Get<PublishingMessages>().ContentId),
      (IDefinitionField) new SimpleDefinitionField("Title", Res.Get<PublishingMessages>().ContentTitle),
      (IDefinitionField) new SimpleDefinitionField("Content", Res.Get<PublishingMessages>().ContentContent),
      (IDefinitionField) new SimpleDefinitionField("Summary", Res.Get<PublishingMessages>().ContentSummary),
      (IDefinitionField) new SimpleDefinitionField("OwnerFirstName", Res.Get<PublishingMessages>().ContentOwnerFirstName),
      (IDefinitionField) new SimpleDefinitionField("OwnerLastName", Res.Get<PublishingMessages>().ContentOwnerLastName),
      (IDefinitionField) new SimpleDefinitionField("OwnerEmail", Res.Get<PublishingMessages>().ContentOwnerEmail),
      (IDefinitionField) new SimpleDefinitionField("Username", Res.Get<PublishingMessages>().ContentOwnerUsername),
      (IDefinitionField) new SimpleDefinitionField("PublicationDate", Res.Get<PublishingMessages>().ContentPublicationDate),
      (IDefinitionField) new SimpleDefinitionField("Categories", Res.Get<PublishingMessages>().ContentCategories),
      (IDefinitionField) new SimpleDefinitionField("ExpirationDate", Res.Get<PublishingMessages>().ContentExpirationDate),
      (IDefinitionField) new SimpleDefinitionField("OriginalParentId", Res.Get<PublishingMessages>().ContentParentItemId),
      (IDefinitionField) new SimpleDefinitionField("Link", Res.Get<PublishingMessages>().ContentLink),
      (IDefinitionField) new SimpleDefinitionField("PipeId", Res.Get<PublishingMessages>().PipeId),
      (IDefinitionField) new SimpleDefinitionField("ContentType", Res.Get<PublishingMessages>().ContentType),
      (IDefinitionField) new SimpleDefinitionField("ItemHash", Res.Get<PublishingMessages>().ItemHash),
      (IDefinitionField) new SimpleDefinitionField("OriginalContentId", Res.Get<PublishingMessages>().OriginalItemId),
      (IDefinitionField) new SimpleDefinitionField("LangId", Res.Expression("PublishingMessages", "ItemLangId")),
      (IDefinitionField) new SimpleDefinitionField("Description", Res.Get<PublishingMessages>().Description),
      (IDefinitionField) new SimpleDefinitionField("DateCreated", Res.Get<PublishingMessages>().ContentDateCreated),
      (IDefinitionField) new SimpleDefinitionField("Status", Res.Get<PublishingMessages>().ContentStatus)
    };

    /// <summary>Creates the default RSS pipe definitions.</summary>
    /// <returns></returns>
    public static IDefinitionField[] CreateDefaultRSSPipeDefinitions() => new IDefinitionField[14]
    {
      (IDefinitionField) new SimpleDefinitionField("Title", Res.Get<PublishingMessages>().RssTitle),
      (IDefinitionField) new SimpleDefinitionField("Content", Res.Get<PublishingMessages>().RssContent),
      (IDefinitionField) new SimpleDefinitionField("Link", Res.Get<PublishingMessages>().RssLink),
      (IDefinitionField) new SimpleDefinitionField("OwnerFirstName", Res.Get<PublishingMessages>().RssOwnerFirstName),
      (IDefinitionField) new SimpleDefinitionField("OwnerLastName", Res.Get<PublishingMessages>().RssOwnerLastName),
      (IDefinitionField) new SimpleDefinitionField("OwnerEmail", Res.Get<PublishingMessages>().RssOwnerEmail),
      (IDefinitionField) new SimpleDefinitionField("PublicationDate", Res.Get<PublishingMessages>().RssPublicationDate),
      (IDefinitionField) new SimpleDefinitionField("OriginalItemId", Res.Get<PublishingMessages>().RssItemId),
      (IDefinitionField) new SimpleDefinitionField("Categories", Res.Get<PublishingMessages>().RssCategories),
      (IDefinitionField) new SimpleDefinitionField("Summary", Res.Get<PublishingMessages>().RssSummary),
      (IDefinitionField) new SimpleDefinitionField("ItemHash", Res.Get<PublishingMessages>().RssHash),
      (IDefinitionField) new SimpleDefinitionField("ExpirationDate", "ExpirationDate"),
      (IDefinitionField) new SimpleDefinitionField("Username", "Username"),
      (IDefinitionField) new SimpleDefinitionField("LangId", Res.Expression("PublishingMessages", "ItemLangId"))
    };

    /// <summary>Creates the default search pipe definitions.</summary>
    /// <returns></returns>
    public static IDefinitionField[] CreateDefaultSearchPipeDefinitions() => new IDefinitionField[5]
    {
      (IDefinitionField) new SimpleDefinitionField("Title", Res.Get<PublishingMessages>().RssTitle),
      (IDefinitionField) new SimpleDefinitionField("Content", Res.Get<PublishingMessages>().Content),
      (IDefinitionField) new SimpleDefinitionField("Link", Res.Get<PublishingMessages>().ContentLink),
      (IDefinitionField) new SimpleDefinitionField("OriginalItemId", Res.Get<PublishingMessages>().ContentId),
      (IDefinitionField) new SimpleDefinitionField("LangId", Res.Expression("PublishingMessages", "ItemLangId"))
    };

    /// <summary>Creates the default page pipe definitions.</summary>
    /// <returns></returns>
    public static IDefinitionField[] CreateDefaultPagePipeDefinitions() => new IDefinitionField[7]
    {
      (IDefinitionField) new SimpleDefinitionField("Id", Res.Get<PublishingMessages>().ContentId),
      (IDefinitionField) new SimpleDefinitionField("Title", Res.Get<PublishingMessages>().ContentTitle),
      (IDefinitionField) new SimpleDefinitionField("Content", Res.Get<PublishingMessages>().ContentContent),
      (IDefinitionField) new SimpleDefinitionField("Description", Res.Get<PublishingMessages>().ContentSummary),
      (IDefinitionField) new SimpleDefinitionField("Keywords", Res.Get<PublishingMessages>().PageKeywords),
      (IDefinitionField) new SimpleDefinitionField("Link", Res.Get<PublishingMessages>().ContentLink),
      (IDefinitionField) new SimpleDefinitionField("PublicationDate", Res.Get<PublishingMessages>().ContentPublicationDate)
    };

    /// <summary>Creates the default twitter pipe definitions.</summary>
    /// <returns></returns>
    public static IDefinitionField[] CreateDefaultTwitterPipeDefinitions() => new IDefinitionField[2]
    {
      (IDefinitionField) new SimpleDefinitionField("Content", Res.Get<PublishingMessages>().RssContent),
      (IDefinitionField) new SimpleDefinitionField("Link", Res.Get<PublishingMessages>().RssLink)
    };

    /// <summary>Creates the default twitter pipe definitions.</summary>
    /// <returns></returns>
    public static IDefinitionField[] CreateDefaultTwitterInboundPipeDefinitions() => new IDefinitionField[8]
    {
      (IDefinitionField) new SimpleDefinitionField("Content", Res.Get<PublishingMessages>().RssContent),
      (IDefinitionField) new SimpleDefinitionField("Link", Res.Get<PublishingMessages>().RssLink),
      (IDefinitionField) new SimpleDefinitionField("PublicationDate", Res.Get<PublishingMessages>().ContentPublicationDate),
      (IDefinitionField) new SimpleDefinitionField("ItemHash", Res.Get<PublishingMessages>().ItemHash),
      (IDefinitionField) new SimpleDefinitionField("PipeId", Res.Get<PublishingMessages>().PipeId),
      (IDefinitionField) new SimpleDefinitionField("Username", Res.Get<PublishingMessages>().ContentOwnerUsername),
      (IDefinitionField) new SimpleDefinitionField("Owner", Res.Get<PublishingMessages>().ContentOwner),
      (IDefinitionField) new SimpleDefinitionField("UserAvatar", Res.Get<PublishingMessages>().UserAvatar)
    };

    /// <summary>Creates the default POP3 pipe definitions.</summary>
    /// <returns></returns>
    public static IDefinitionField[] CreateDefaultPop3PipeDefinitions() => new IDefinitionField[6]
    {
      (IDefinitionField) new SimpleDefinitionField("OwnerEmail", Res.Get<PublishingMessages>().Pop3SenderEmail),
      (IDefinitionField) new SimpleDefinitionField("OwnerFirstName", Res.Get<PublishingMessages>().Pop3SenderFirstName),
      (IDefinitionField) new SimpleDefinitionField("OwnerLastName", Res.Get<PublishingMessages>().Pop3SenderLastName),
      (IDefinitionField) new SimpleDefinitionField("PublicationDate", Res.Get<PublishingMessages>().Pop3ReceivedOn),
      (IDefinitionField) new SimpleDefinitionField("Title", Res.Get<PublishingMessages>().ContentTitle),
      (IDefinitionField) new SimpleDefinitionField("Content", Res.Get<PublishingMessages>().Content)
    };

    /// <summary>Creates the default content pipe definitions.</summary>
    /// <returns></returns>
    public static IList<IDefinitionField> CreateDefaultAnyContentPipeDefinitions() => (IList<IDefinitionField>) new List<IDefinitionField>()
    {
      (IDefinitionField) new SimpleDefinitionField("Id", Res.Get<PublishingMessages>().ContentId),
      (IDefinitionField) new SimpleDefinitionField("Title", Res.Get<PublishingMessages>().ContentTitle),
      (IDefinitionField) new SimpleDefinitionField("Content", Res.Get<PublishingMessages>().ContentContent)
    };

    /// <summary>
    /// Registers pipe settings to a pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="settings">The settings.</param>
    public static void RegisterPipeSettings(string pipeName, PipeSettings settings)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      if (settings == null)
        throw new ArgumentNullException(nameof (settings));
      lock (PublishingSystemFactory.pipeSettingsSync)
        PublishingSystemFactory.pipeSettings[pipeName] = settings;
    }

    /// <summary>
    /// Determines whether any pipe settings are registered to a pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    public static bool PipeSettingsRegistered(string pipeName) => !string.IsNullOrEmpty(pipeName) ? PublishingSystemFactory.pipeSettings.ContainsKey(pipeName) : throw new ArgumentException("The pipeName parameter cannot be empty.");

    /// <summary>
    /// Unregisters the pipe settings registered to the pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    public static void UnregisterPipeSettings(string pipeName)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      lock (PublishingSystemFactory.pipeSettingsSync)
      {
        if (!PublishingSystemFactory.pipeSettings.ContainsKey(pipeName))
          return;
        PublishingSystemFactory.pipeSettings.Remove(pipeName);
      }
    }

    /// <summary>Unregisters all pipe settings.</summary>
    public static void UnregisterAllPipeSettings()
    {
      lock (PublishingSystemFactory.pipeSettingsSync)
        PublishingSystemFactory.pipeSettings.Clear();
    }

    /// <summary>
    /// Gets the pipe settings registered to the pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="setMappings">if set to <c>true</c> the registered mappings for the given pipe are added to the result.</param>
    /// <returns></returns>
    public static PipeSettings GetPipeSettings(string pipeName, bool setMappings = true)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      PipeSettings pipeSettings = (PipeSettings) null;
      lock (PublishingSystemFactory.pipeSettingsSync)
        pipeSettings = PublishingSystemFactory.pipeSettings.ContainsKey(pipeName) ? PublishingSystemFactory.pipeSettings[pipeName] : throw new ArgumentException(string.Format("There are no settings suitable for pipe with name '{0}'.", (object) pipeName));
      PipeSettings instance = (PipeSettings) Activator.CreateInstance(pipeSettings.GetType());
      pipeSettings.CopyTo(instance);
      if (setMappings && PublishingSystemFactory.PipeMappingsRegistered(pipeSettings.PipeName, pipeSettings.IsInbound))
      {
        IList<Mapping> pipeMappings = PublishingSystemFactory.GetPipeMappings(pipeSettings.PipeName, pipeSettings.IsInbound);
        MappingSettings mappingSettings = new MappingSettings();
        foreach (Mapping source in (IEnumerable<Mapping>) pipeMappings)
        {
          Mapping mapping = new Mapping();
          mapping.CopyFrom(source);
          mappingSettings.Mappings.Add(mapping);
        }
        instance.Mappings = mappingSettings;
      }
      return instance;
    }

    /// <summary>
    /// Creates and returns pipe settings based on the pipe settings registered to the pipe with the given name using the passed publishing manager.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="manager">The publishing manager.</param>
    /// <returns></returns>
    public static PipeSettings CreatePipeSettings(
      string pipeName,
      PublishingManager manager)
    {
      PipeSettings pipeSettings1 = !string.IsNullOrEmpty(pipeName) ? PublishingSystemFactory.GetPipeSettings(pipeName, false) : throw new ArgumentException("The pipeName parameter cannot be empty.");
      PipeSettings pipeSettings2 = manager.CreatePipeSettings(pipeSettings1.GetType());
      pipeSettings1.CopyTo(pipeSettings2);
      pipeSettings2.Mappings = PublishingSystemFactory.CreateMappingSettings(pipeName, pipeSettings1.IsInbound, manager);
      return pipeSettings2;
    }

    /// <summary>Registers default pipe settings</summary>
    /// <param name="pipeName">The name of the pipe</param>
    /// <param name="settings">The settings</param>
    public static void RegisterTemplatePipeSettings(string pipeName, PipeSettings settings)
    {
      if (PublishingSystemFactory.defaultPipeSettings.ContainsKey(pipeName))
        PublishingSystemFactory.defaultPipeSettings[pipeName] = settings;
      else
        PublishingSystemFactory.defaultPipeSettings.Add(pipeName, settings);
    }

    /// <summary>Gets the default pipe settings</summary>
    /// <param name="pipeName">The name of the pipe</param>
    public static PipeSettings GetTemplatePipeSettings(string pipeName) => PublishingSystemFactory.defaultPipeSettings.ContainsKey(pipeName) ? PublishingSystemFactory.defaultPipeSettings[pipeName] : throw new ArgumentException("No default pipe settings registered for pipe with name '{0}'".Arrange((object) pipeName));

    /// <summary>Creates the default search pipe settings.</summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    [Obsolete("Use static method GetTemplatePipeSettings implemented in all pipes. Example: ContentInboundPipe.GetTemplatePipeSettings()")]
    public static PipeSettings CreateDefaultSearchPipeSettings(string pipeName) => PublishingSystemFactory.GetTemplateSettingsForPipe("SearchIndex");

    /// <summary>Creates the default page pipe settings.</summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    [Obsolete("Use static method GetTemplatePipeSettings implemented in all pipes. Example: ContentInboundPipe.GetTemplatePipeSettings()")]
    public static PipeSettings CreateDefaultPagePipeSettings(string pipeName) => PublishingSystemFactory.GetTemplateSettingsForPipe("PagePipe");

    /// <summary>Creates the default twitter pipe settings.</summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    [Obsolete("Use static method GetTemplatePipeSettings implemented in all pipes. Example: ContentInboundPipe.GetTemplatePipeSettings()")]
    public static PipeSettings CreateDefaultTwitterPipeSettings(string pipeName) => PublishingSystemFactory.GetTemplateSettingsForPipe("Twitter");

    /// <summary>Creates the default content inbound pipe settings.</summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    [Obsolete("Use static method GetTemplatePipeSettings implemented in all pipes. Example: ContentInboundPipe.GetTemplatePipeSettings()")]
    public static PipeSettings CreateDefaultContentInboundPipeSettings(string pipeName) => PublishingSystemFactory.GetTemplateSettingsForPipe("ContentInboundPipe");

    /// <summary>Creates the default content outbound pipe settings.</summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    [Obsolete("Use static method GetTemplatePipeSettings implemented in all pipes. Example: ContentInboundPipe.GetTemplatePipeSettings()")]
    public static PipeSettings CreateDefaultContentOutboundPipeSettings(string pipeName) => PublishingSystemFactory.GetTemplateSettingsForPipe("ContentOutboundPipe");

    /// <summary>Creates the default RSS inbound pipe settings.</summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    [Obsolete("Use static method GetTemplatePipeSettings implemented in all pipes. Example: ContentInboundPipe.GetTemplatePipeSettings()")]
    public static PipeSettings CreateDefaultRssInboundPipeSettings(string pipeName) => PublishingSystemFactory.GetTemplateSettingsForPipe("RSSInboundPipe");

    /// <summary>Creates the default RSS outbound pipe settings.</summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    [Obsolete("Use static method GetTemplatePipeSettings implemented in all pipes. Example: ContentInboundPipe.GetTemplatePipeSettings()")]
    public static PipeSettings CreateDefaultRssOutboundPipeSettings(string pipeName) => PublishingSystemFactory.GetTemplateSettingsForPipe("RSSOutboundPipe");

    /// <summary>Creates the default POP3 inbound pipe settings.</summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    [Obsolete("Use static method GetTemplatePipeSettings implemented in all pipes. Example: ContentInboundPipe.GetTemplatePipeSettings()")]
    public static PipeSettings CreateDefaultPop3InboundPipeSettings(string pipeName) => PublishingSystemFactory.GetTemplateSettingsForPipe("Pop3InboundPipe");

    /// <summary>Creates the default content outbound pipe settings.</summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    [Obsolete("Use static method GetTemplatePipeSettings implemented in all pipes. Example: ContentInboundPipe.GetTemplatePipeSettings()")]
    public static PipeSettings CreateDefaulPipeSettings(string pipeName, bool isInbound)
    {
      PipeSettings templateSettingsForPipe = PublishingSystemFactory.GetTemplateSettingsForPipe(pipeName, true);
      templateSettingsForPipe.IsInbound = isInbound;
      return templateSettingsForPipe;
    }

    private static PipeSettings GetTemplateSettingsForPipe(
      string pipeName,
      bool createNewInstance = false)
    {
      PipeSettings templatePipeSettings = PublishingSystemFactory.GetTemplatePipeSettings(pipeName);
      if (templatePipeSettings.PipeName == pipeName && !createNewInstance)
        return templatePipeSettings;
      PipeSettings instance = (PipeSettings) Activator.CreateInstance(templatePipeSettings.GetType());
      templatePipeSettings.CopyTo(instance);
      templatePipeSettings.PipeName = pipeName;
      return instance;
    }

    /// <summary>Gets the default content of the inbound mapping for.</summary>
    /// <returns></returns>
    [Obsolete("Use static method GetDefaultMappings implemented in all pipes. Example: ContentInboundPipe.GetDefaultMappings()")]
    public static List<Mapping> GetDefaultInboundMappingForContent() => ContentInboundPipe.GetDefaultMappings();

    [Obsolete("Use static method GetDefaultMappings implemented in all pipes. Example: ContentInboundPipe.GetDefaultMappings()")]
    public static List<Mapping> GetDefaultMappingForSiteSyncContentImportPipe() => new List<Mapping>();

    /// <summary>
    /// Gets the default content of the outbound mapping for any content.
    /// </summary>
    /// <returns></returns>
    [Obsolete("Use static method GetDefaultMappings implemented in all pipes. Example: ContentInboundPipe.GetDefaultMappings()")]
    public static List<Mapping> GetDefaultOutboundMappingForAnyContent()
    {
      List<Mapping> defaultMappings = ContentOutboundPipe.GetDefaultMappings();
      defaultMappings.Add(PublishingSystemFactory.CreateMapping("ItemAction", "concatenationtranslator", true, "ItemAction"));
      defaultMappings.Add(PublishingSystemFactory.CreateMapping("MimeType", "concatenationtranslator", true, "MimeType"));
      return defaultMappings;
    }

    /// <summary>Gets the default content of the outbound mapping for.</summary>
    /// <returns></returns>
    [Obsolete("Use static method GetDefaultMappings implemented in all pipes. Example: ContentInboundPipe.GetDefaultMappings()")]
    public static List<Mapping> GetDefaultOutboundMappingForContent() => ContentOutboundPipe.GetDefaultMappings();

    /// <summary>Gets the default inbound mapping for POP3.</summary>
    /// <param name="manager">Instance of PublishingManager.</param>
    /// <returns>The default inbound mapping for POP3.</returns>
    [Obsolete("Use static method GetDefaultMappings implemented in all pipes. Example: ContentInboundPipe.GetDefaultMappings()")]
    public static List<Mapping> GetDefaultInboundMappingForPop3() => Pop3InboundPipe.GetDefaultMappings();

    /// <summary>Gets the default inbound mapping for pages.</summary>
    /// <returns></returns>
    [Obsolete("Use static method GetDefaultMappings implemented in all pipes. Example: ContentInboundPipe.GetDefaultMappings()")]
    public static List<Mapping> GetDefaultInboundMappingForPages() => PageInboundPipe.GetDefaultMappings();

    /// <summary>Gets the default outbound mapping for RSS.</summary>
    /// <param name="manager">Instance of PublishingManager.</param>
    /// <returns>The default outbound mapping for RSS.</returns>
    [Obsolete("Use static method GetDefaultMappings implemented in all pipes. Example: ContentInboundPipe.GetDefaultMappings()")]
    public static List<Mapping> GetDefaultOutboundMappingForRss() => RSSOutboundPipe.GetDefaultMappings();

    /// <summary>
    /// Gets the default index of the outbound mapping for search.
    /// </summary>
    /// <returns></returns>
    [Obsolete("Use static method GetDefaultMappings implemented in all pipes. Example: ContentInboundPipe.GetDefaultMappings()")]
    public static List<Mapping> GetDefaultOutboundMappingForSearchIndex() => (List<Mapping>) PublishingSystemFactory.GetPipeMappings("SearchIndex", true);

    /// <summary>Gets the default outbound mapping for Twitter.</summary>
    /// <param name="manager">Instance of PublishingManager.</param>
    /// <returns>The default outbound mapping for Twitter.</returns>
    [Obsolete("Use static method GetDefaultMappings implemented in all pipes. Example: ContentInboundPipe.GetDefaultMappings()")]
    public static List<Mapping> GetDefaultOutboundMappingForTwitter() => TwitterFeedOutboundPipe.GetDefaultMappings();

    /// <summary>
    /// Registers a meta field to be used in the publishing point meta type.
    /// </summary>
    /// <param name="field">The field.</param>
    public static void RegisterPublishingPointMetaField(IDefinitionField field)
    {
      if (field == null)
        throw new ArgumentNullException(nameof (field));
      lock (PublishingSystemFactory.publishingPointMetaFieldsSync)
        PublishingSystemFactory.publishingPointMetaFields[field.Name] = field;
    }

    /// <summary>
    /// Registers meta fields to be used in the publishing point meta type.
    /// </summary>
    /// <param name="fields">The fields.</param>
    public static void RegisterPublishingPointMetaFields(IEnumerable<IDefinitionField> fields)
    {
      if (fields == null)
        throw new ArgumentNullException(nameof (fields));
      lock (PublishingSystemFactory.publishingPointMetaFieldsSync)
      {
        foreach (IDefinitionField field in fields)
          PublishingSystemFactory.publishingPointMetaFields[field.Name] = field;
      }
    }

    /// <summary>
    /// Determines whether a meta field with given name is registered to be used in the publishing point meta type.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public static bool PublishingPointMetaFieldRegistered(string fieldName) => !string.IsNullOrEmpty(fieldName) ? PublishingSystemFactory.publishingPointMetaFields.ContainsKey(fieldName) : throw new ArgumentException("fieldName is null or empty.");

    /// <summary>Unregisters the meta field with the given name.</summary>
    /// <param name="fieldName">Name of the field.</param>
    public static void UnregisterPublishingPointMetaField(string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentException("fieldName is null or empty.");
      lock (PublishingSystemFactory.publishingPointMetaFieldsSync)
      {
        if (!PublishingSystemFactory.publishingPointMetaFields.ContainsKey(fieldName))
          return;
        PublishingSystemFactory.publishingPointMetaFields.Remove(fieldName);
      }
    }

    /// <summary>Unregisters all meta fields.</summary>
    public static void UnregisterAllPublishingPointMetaFields()
    {
      lock (PublishingSystemFactory.publishingPointMetaFieldsSync)
        PublishingSystemFactory.publishingPointMetaFields.Clear();
    }

    /// <summary>
    /// Creates and returns a meta type for a publishing point based on the registered meta fields.
    /// </summary>
    /// <returns></returns>
    public static MetaType CreatePublishingPointDataType()
    {
      MetadataManager manager = MetadataManager.GetManager();
      MetaType metaType = manager.CreateMetaType("Telerik.Sitefinity.Publishing.Model", "Dynamic_" + Guid.NewGuid().ToString("N"));
      metaType.IsDynamic = true;
      metaType.DatabaseInheritance = DatabaseInheritanceType.vertical;
      metaType.BaseClassName = typeof (DynamicTypeBase).FullName;
      foreach (IDefinitionField srcField in (IEnumerable<IDefinitionField>) PublishingSystemFactory.publishingPointMetaFields.Values)
      {
        MetaField metafield = manager.CreateMetafield(srcField.Name);
        PublishingSystemFactory.PopulateMetaFieldProperties(srcField, metafield, metaType);
      }
      return metaType;
    }

    /// <summary>Gets the default publishing point meta fields.</summary>
    /// <returns></returns>
    public static List<IDefinitionField> GetDefaultPublishingPointMetaFields() => new List<IDefinitionField>()
    {
      (IDefinitionField) new SimpleDefinitionField("Title")
      {
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField("PublicationDate")
      {
        ClrType = typeof (DateTime).FullName
      },
      (IDefinitionField) new SimpleDefinitionField("Link")
      {
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField("Content")
      {
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR(MAX)",
        DBType = "LONGVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField("Summary")
      {
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR(MAX)",
        DBType = "LONGVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField("LangId")
      {
        ClrType = typeof (string).FullName
      },
      (IDefinitionField) new SimpleDefinitionField("Username")
      {
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField("OwnerFirstName")
      {
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField("OwnerLastName")
      {
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField("UserAvatar")
      {
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField("OwnerEmail")
      {
        ClrType = typeof (string).FullName,
        SQLDBType = "NVARCHAR"
      },
      (IDefinitionField) new SimpleDefinitionField("ItemHash")
      {
        ClrType = typeof (string).FullName
      },
      (IDefinitionField) new SimpleDefinitionField("ExpirationDate")
      {
        ClrType = typeof (DateTime).FullName
      },
      (IDefinitionField) new SimpleDefinitionField("PipeId")
      {
        ClrType = typeof (string).FullName
      },
      (IDefinitionField) new SimpleDefinitionField("ContentType")
      {
        ClrType = typeof (string).FullName
      }
    };

    /// <summary>
    /// Populates a meta field based on an IDefinitionField object.
    /// </summary>
    /// <param name="srcField">The source field.</param>
    /// <param name="fieldToPopulate">The meta field to populate.</param>
    /// <param name="parentType">The parent metatype of this metafield.</param>
    /// <returns>The updated metafield.</returns>
    private static MetaField PopulateMetaFieldProperties(
      IDefinitionField srcField,
      MetaField fieldToPopulate,
      MetaType parentType)
    {
      fieldToPopulate.ClrType = srcField.ClrType;
      fieldToPopulate.Title = srcField.Title;
      fieldToPopulate.DBSqlType = srcField.SQLDBType;
      fieldToPopulate.DBType = srcField.DBType;
      fieldToPopulate.DefaultValue = srcField.DefaultValue;
      fieldToPopulate.MaxLength = srcField.MaxLength;
      fieldToPopulate.TaxonomyId = string.IsNullOrWhiteSpace(srcField.TaxonomyId) ? Guid.Empty : new Guid(srcField.TaxonomyId);
      fieldToPopulate.TaxonomyProvider = srcField.TaxonomyProviderName;
      fieldToPopulate.IsSingleTaxon = !srcField.AllowMultipleTaxons;
      fieldToPopulate.Parent = parentType;
      return fieldToPopulate;
    }

    /// <summary>
    /// Registers a pipe description provider to a pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="provider">The pipe description provider.</param>
    internal static void RegisterPipeDescriptionProvider(
      string pipeName,
      IPipeDescriptionProvider provider)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      if (provider == null)
        throw new ArgumentNullException(nameof (provider));
      lock (PublishingSystemFactory.pipeDescriptionProvidersSync)
      {
        if (!PublishingSystemFactory.pipeDescriptionProviders.ContainsKey(pipeName))
          PublishingSystemFactory.pipeDescriptionProviders.Add(pipeName, new PipeDescriptionCompositeProvider());
        PublishingSystemFactory.pipeDescriptionProviders[pipeName].AddProvider(provider);
      }
    }

    /// <summary>
    /// Determines whether a pipe description provider is registered to a pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    internal static bool PipeDescriptionProviderRegistered(string pipeName) => !string.IsNullOrEmpty(pipeName) ? PublishingSystemFactory.pipeDescriptionProviders.ContainsKey(pipeName) : throw new ArgumentException("The pipeName parameter cannot be empty.");

    /// <summary>
    /// Unregisters the pipe description provider registered to the pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    internal static void UnregisterPipeDescriptionProvider(string pipeName)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      lock (PublishingSystemFactory.pipeDescriptionProvidersSync)
      {
        if (!PublishingSystemFactory.pipeDescriptionProviders.ContainsKey(pipeName))
          return;
        PublishingSystemFactory.pipeDescriptionProviders.Remove(pipeName);
      }
    }

    /// <summary>Unregisters all pipe description providers.</summary>
    internal static void UnregisterAllPipeDescriptionProviders()
    {
      lock (PublishingSystemFactory.pipeDescriptionProvidersSync)
        PublishingSystemFactory.pipeDescriptionProviders.Clear();
    }

    /// <summary>
    /// Gets the pipe description provider registered to the pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    internal static IPipeDescriptionProvider GetPipeDescriptionProvider(
      string pipeName)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      lock (PublishingSystemFactory.pipeDescriptionProvidersSync)
        return PublishingSystemFactory.pipeDescriptionProviders.ContainsKey(pipeName) ? (IPipeDescriptionProvider) PublishingSystemFactory.pipeDescriptionProviders[pipeName] : throw new ArgumentException("There is no description provider suitable for this pipe type.");
    }

    /// <summary>
    /// Registers a pipe designer to a pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <param name="provider">The pipe description provider.</param>
    internal static void RegisterPipeDesigner(string pipeName, bool isInbound, Type designerType)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      if (designerType == (Type) null)
        throw new ArgumentNullException(nameof (designerType));
      string pipeKey = PublishingSystemFactory.GetPipeKey(pipeName, isInbound);
      lock (PublishingSystemFactory.pipeDesignersSync)
        PublishingSystemFactory.pipeDesigners[pipeKey] = designerType;
    }

    /// <summary>
    /// Determines whether a pipe designer is registered to a pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    internal static bool PipeDesignerRegistered(string pipeName, bool isInbound)
    {
      string key = !string.IsNullOrEmpty(pipeName) ? PublishingSystemFactory.GetPipeKey(pipeName, isInbound) : throw new ArgumentException("The pipeName parameter cannot be empty.");
      return PublishingSystemFactory.pipeDesigners.ContainsKey(key);
    }

    /// <summary>
    /// Unregisters the pipe designer registered to the pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    internal static void UnregisterPipeDesigner(string pipeName, bool isInbound)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      lock (PublishingSystemFactory.pipeDesignersSync)
      {
        string pipeKey = PublishingSystemFactory.GetPipeKey(pipeName, isInbound);
        if (!PublishingSystemFactory.pipeDesigners.ContainsKey(pipeKey))
          return;
        PublishingSystemFactory.pipeDesigners.Remove(pipeKey);
      }
    }

    /// <summary>Unregisters all pipe designers.</summary>
    internal static void UnregisterAllPipeDesigners()
    {
      lock (PublishingSystemFactory.pipeDesignersSync)
        PublishingSystemFactory.pipeDesigners.Clear();
    }

    /// <summary>
    /// Gets the pipe designer registered to the pipe with the given name.
    /// </summary>
    /// <param name="pipeName">Name of the pipe.</param>
    /// <returns></returns>
    internal static Type GetPipeDesigner(string pipeName, bool isInbound)
    {
      if (string.IsNullOrEmpty(pipeName))
        throw new ArgumentException("The pipeName parameter cannot be empty.");
      lock (PublishingSystemFactory.pipeDesignersSync)
      {
        string pipeKey = PublishingSystemFactory.GetPipeKey(pipeName, isInbound);
        return PublishingSystemFactory.pipeDesigners.ContainsKey(pipeKey) ? PublishingSystemFactory.pipeDesigners[pipeKey] : throw new ArgumentException("There is no designer suitable for this pipe type.");
      }
    }

    internal static void RegisterType(Type from, Type to, string name) => ObjectFactory.Container.RegisterType(from, to, name, (LifetimeManager) new ContainerControlledLifetimeManager());
  }
}
