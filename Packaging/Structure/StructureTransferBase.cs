// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Structure.StructureTransferBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Hosting;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Packaging.Events;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Packaging.Serialization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning;

namespace Telerik.Sitefinity.Packaging.Structure
{
  /// <summary>Imports and exports dynamic module packages</summary>
  internal abstract class StructureTransferBase : IStructureTransfer, IDataTransfer
  {
    protected const string WidgetTemplatesFileName = "widgetTemplates.sf";
    protected const string TaxonomiesFileName = "taxonomies.sf";
    protected const string ModuleConfigsFileName = "configs.sf";
    protected const string VersionFileName = "version.sf";
    protected readonly char Separator = Path.DirectorySeparatorChar;
    private const string StructureFolderName = "Structure";
    private static LocalDataStoreSlot addonNameSlot;
    private static LocalDataStoreSlot addonModulePathSlot;
    private static LocalDataStoreSlot packageTypeSlot;
    private IList<string> moduleNamesToActivate;
    private ISerializer serializer;

    /// <summary>Gets or sets the name of the current add on.</summary>
    /// <value>The name of the current add on.</value>
    public static string CurrentAddOnName
    {
      get => StructureTransferBase.GetPropertyValue("sf-addon-name", StructureTransferBase.AddonNameSlot)?.ToString();
      set => StructureTransferBase.SetPropertyValue((object) value, "sf-addon-name", StructureTransferBase.AddonNameSlot);
    }

    /// <summary>Gets or sets the current add-on module path.</summary>
    /// <value>The current add-on module path.</value>
    public static string CurrentAddOnModulePath
    {
      get => StructureTransferBase.GetPropertyValue("sf-addon-module-path", StructureTransferBase.AddonModulePathSlot)?.ToString();
      set => StructureTransferBase.SetPropertyValue((object) value, "sf-addon-module-path", StructureTransferBase.AddonModulePathSlot);
    }

    /// <summary>
    /// Gets or sets a value indicating whether current import is deployment import.
    /// </summary>
    /// <value>
    /// <c>true</c> if current import is deployment import; otherwise, <c>false</c>.
    /// </value>
    public static PackageType PackageType
    {
      get
      {
        object propertyValue = StructureTransferBase.GetPropertyValue("sf-package-type", StructureTransferBase.PackageTypeSlot);
        return propertyValue == null ? PackageType.None : (PackageType) propertyValue;
      }
      set => StructureTransferBase.SetPropertyValue((object) value, "sf-package-type", StructureTransferBase.PackageTypeSlot);
    }

    /// <summary>Gets the current origin.</summary>
    /// <value>The current origin.</value>
    public static string CurrentOrigin => StructureTransferBase.PackageType != PackageType.Addon || string.IsNullOrWhiteSpace(StructureTransferBase.CurrentAddOnName) ? (string) null : new AddonOrigin(StructureTransferBase.CurrentAddOnName, StructureTransferBase.CurrentAddOnModulePath).ToString();

    /// <inheritdoc />
    public abstract string Area { get; }

    /// <inheritdoc />
    public abstract string GroupName { get; }

    /// <inheritdoc />
    public abstract IEnumerable<string> Dependencies { get; }

    /// <inheritdoc />
    public abstract IEnumerable<ExportType> SupportedTypes { get; }

    /// <inheritdoc />
    public abstract IEnumerable<IPackageTransferObject> Export(
      IDictionary<string, string> configuration);

    /// <inheritdoc />
    public abstract void Import(
      IEnumerable<IPackageTransferObject> packageTransferObjects);

    /// <inheritdoc />
    public abstract void ImportCompleted();

    /// <inheritdoc />
    public abstract bool AllowToDelete(string directory);

    /// <inheritdoc />
    public virtual void Delete(string moduleName, out bool moduleDeleted) => moduleDeleted = false;

    /// <inheritdoc />
    public virtual void Uninstall(string sourceName)
    {
    }

    /// <inheritdoc />
    public event EventHandler<ItemImportedEventArgs> ItemImported;

    /// <inheritdoc />
    public virtual bool IsAvailableForCurrentSite() => true;

    /// <inheritdoc />
    public ExportMode ExportMode { get; set; }

    public virtual void Count(
      DirectoryInfo directory,
      ScanOperation operation,
      AddOnEntryType type)
    {
      string name = directory.Name;
      DirectoryInfo directoryInfo = ((IEnumerable<DirectoryInfo>) directory.GetDirectories("Structure", SearchOption.TopDirectoryOnly)).FirstOrDefault<DirectoryInfo>();
      if (directoryInfo == null || ((IEnumerable<FileInfo>) directoryInfo.GetFiles()).Count<FileInfo>() <= 0)
        return;
      operation.AddOnInfo.Entries.Add(new AddOnEntry()
      {
        DisplayName = name,
        AddonEntryType = type
      });
    }

    internal virtual ISerializer Serializer
    {
      get
      {
        if (this.serializer == null)
          this.serializer = ObjectFactory.Resolve<ISerializer>();
        return this.serializer;
      }
    }

    internal virtual IList<string> ModuleNamesToActivate
    {
      get
      {
        if (this.moduleNamesToActivate == null)
          this.moduleNamesToActivate = (IList<string>) new List<string>();
        return this.moduleNamesToActivate;
      }
    }

    /// <summary>
    /// Checks if the given item can be deleted by current import
    /// </summary>
    /// <param name="item">The item to delete</param>
    /// <returns>True if delete is allowed; false otherwise.</returns>
    internal static bool IsDeleteAllowedForItem(IHasOrigin item)
    {
      string currentOrigin = StructureTransferBase.CurrentOrigin;
      int num = StructureTransferBase.PackageType == PackageType.None ? 1 : 0;
      bool flag = StructureTransferBase.PackageType == PackageType.Deployment;
      if (num != 0)
        return true;
      if (!(item.Origin == currentOrigin))
        return false;
      return flag || !currentOrigin.IsNullOrEmpty();
    }

    /// <summary>
    /// Import content of the widget templates transfer object
    /// </summary>
    /// <param name="packageTransferObjects">The package transfer objects.</param>
    /// <param name="providerName">The provider name.</param>
    /// <param name="transactionName">The transaction name.</param>
    internal virtual void ImportControlPresentations(
      IEnumerable<IPackageTransferObject> packageTransferObjects,
      string providerName,
      string transactionName)
    {
      IPackageTransferObject packageTransferObject = packageTransferObjects.FirstOrDefault<IPackageTransferObject>((Func<IPackageTransferObject, bool>) (o => o.Name.EndsWith("widgetTemplates.sf")));
      if (packageTransferObject == null)
        return;
      using (Stream stream = packageTransferObject.GetStream())
      {
        if (!stream.CanRead || stream.Length <= 0L)
          return;
        stream.Position = 0L;
        IEnumerable<ControlPresentation> controlPresentations = this.Serializer.Deserialize<IEnumerable<ControlPresentation>>(stream);
        PageManager manager = PageManager.GetManager(providerName, transactionName);
        IQueryable<ControlPresentation> presentationItems = manager.GetPresentationItems<ControlPresentation>();
        foreach (ControlPresentation controlPresentation1 in controlPresentations)
        {
          ControlPresentation widgetTemplate = controlPresentation1;
          ControlPresentation controlPresentation2 = presentationItems.Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (p => p.Id == widgetTemplate.Id)).FirstOrDefault<ControlPresentation>();
          if (controlPresentation2 == null)
            controlPresentation2 = presentationItems.Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (p => p.Name == widgetTemplate.Name && p.AreaName == widgetTemplate.AreaName && p.ControlType == widgetTemplate.ControlType)).FirstOrDefault<ControlPresentation>() ?? manager.Provider.CreatePresentationItem<ControlPresentation>(widgetTemplate.Id);
          this.CopyControlPresentation(widgetTemplate, controlPresentation2);
          VersionManager.GetManager((string) null, transactionName).CreateVersion((IDataItem) controlPresentation2, true);
          this.OnItemImported(new ItemImportedEventArgs()
          {
            ItemId = controlPresentation2.Id,
            ItemProvider = providerName,
            ItemType = controlPresentation2.GetType().FullName,
            TransactionName = transactionName
          });
        }
        TransactionManager.CommitTransaction(transactionName);
      }
    }

    protected bool AllowToExport(IDictionary<string, string> configuration) => configuration == null || configuration.Keys.Any<string>((Func<string, bool>) (t => this.ContainsType(this.SupportedTypes, t)));

    protected string PreventServerScriptInjection(string data) => Regex.Replace(data, "<script runat=\"server\".*?</script>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

    /// <summary>Imports a configuration.</summary>
    /// <param name="config">The config.</param>
    /// <param name="configurationManager">The configuration manager.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    protected virtual void ImportConfiguration(
      KeyValuePair<Type, string> config,
      ConfigManager configurationManager,
      string transactionName)
    {
      bool overrideOrigin = !StructureTransferBase.CurrentOrigin.IsNullOrEmpty() || StructureTransferBase.PackageType == PackageType.Deployment && typeof (ContentModuleConfigBase).IsAssignableFrom(config.Key);
      configurationManager.Import(config.Key, config.Value, StructureTransferBase.CurrentOrigin, overrideOrigin: overrideOrigin);
      this.RaiseItemImportedForConfigSection(config.Key.FullName, transactionName);
    }

    protected void RaiseItemImportedForConfigSection(string section, string transactionName) => this.OnItemImported(new ItemImportedEventArgs()
    {
      ItemType = typeof (ConfigSection).FullName,
      AdditionalInfo = section,
      TransactionName = transactionName
    });

    protected void OnItemImported(ItemImportedEventArgs args)
    {
      EventHandler<ItemImportedEventArgs> itemImported = this.ItemImported;
      if (itemImported == null)
        return;
      itemImported((object) this, args);
    }

    private static void SetPropertyValue(object value, string key, LocalDataStoreSlot slot)
    {
      if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
        SystemManager.CurrentHttpContext.Items[(object) key] = value;
      else
        Thread.SetData(slot, value);
    }

    private static object GetPropertyValue(string key, LocalDataStoreSlot slot) => !HostingEnvironment.IsHosted || SystemManager.CurrentHttpContext == null ? Thread.GetData(slot) : SystemManager.CurrentHttpContext.Items[(object) key];

    private bool ContainsType(IEnumerable<ExportType> exportTypes, string typeName)
    {
      foreach (ExportType exportType in exportTypes)
      {
        if (exportType.TypeName == typeName || exportType.ChildTypes.Count > 0 && this.ContainsType((IEnumerable<ExportType>) exportType.ChildTypes, typeName))
          return true;
      }
      return false;
    }

    private static LocalDataStoreSlot AddonNameSlot
    {
      get
      {
        if (StructureTransferBase.addonNameSlot == null)
          StructureTransferBase.addonNameSlot = Thread.AllocateDataSlot();
        return StructureTransferBase.addonNameSlot;
      }
    }

    private static LocalDataStoreSlot AddonModulePathSlot
    {
      get
      {
        if (StructureTransferBase.addonModulePathSlot == null)
          StructureTransferBase.addonModulePathSlot = Thread.AllocateDataSlot();
        return StructureTransferBase.addonModulePathSlot;
      }
    }

    private static LocalDataStoreSlot PackageTypeSlot
    {
      get
      {
        if (StructureTransferBase.packageTypeSlot == null)
          StructureTransferBase.packageTypeSlot = Thread.AllocateDataSlot();
        return StructureTransferBase.packageTypeSlot;
      }
    }

    private void CopyControlPresentation(
      ControlPresentation source,
      ControlPresentation destination)
    {
      destination.ControlType = source.ControlType;
      destination.FriendlyControlName = source.FriendlyControlName;
      destination.IsDifferentFromEmbedded = source.IsDifferentFromEmbedded;
      destination.AreaName = source.AreaName;
      destination.ResourceAssemblyName = source.ResourceAssemblyName;
      string str = this.PreventServerScriptInjection(source.Data);
      destination.Data = str;
      destination.Condition = source.Condition;
      destination.NameForDevelopers = source.NameForDevelopers;
      destination.Name = source.Name;
      destination.DataType = source.DataType;
      destination.EmbeddedTemplateName = source.EmbeddedTemplateName;
      destination.Theme = source.Theme;
      destination.ResourceClassId = source.ResourceClassId;
    }
  }
}
