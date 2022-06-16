// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlManager`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Compilation;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.PropertyLoaders;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Pages.Model.PropertyLoaders;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Compilation;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>Provides API for managing Sitefinity controls.</summary>
  /// <typeparam name="TProvider">The type of the provider.</typeparam>
  public abstract class ControlManager<TProvider> : 
    ContentManagerBase<TProvider>,
    IControlManager,
    IContentManager,
    IManager,
    IDisposable,
    IProviderResolver,
    IControlPropertyProvider
    where TProvider : ContentDataProviderBase
  {
    private ObjectDataUtility objectDataUtility;

    /// <summary>
    /// Initializes a new instance of ManagerBase class with the default provider.
    /// </summary>
    protected ControlManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of ManagerBase class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">The name of the provider. If empty string or null the default provider is set</param>
    protected ControlManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.ContentManagerBase`1" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    protected ControlManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>
    /// Gets an instance of <see cref="P:Telerik.Sitefinity.Modules.ControlManager`1.ObjectDataUtility" /> class used to gather
    /// information about reading and populating the <see cref="T:Telerik.Sitefinity.Pages.Model.ObjectData" /> instances.
    /// </summary>
    protected internal virtual ObjectDataUtility ObjectDataUtility
    {
      get
      {
        if (this.objectDataUtility == null)
          this.objectDataUtility = ObjectFactory.Resolve<ObjectDataUtility>();
        return this.objectDataUtility;
      }
    }

    /// <inheritdoc />
    public override bool CanCreateProvider => false;

    /// <summary>
    /// Creates a new persistent class for storing serialized control data and serializes the provided web control in to the created instance.
    /// </summary>
    /// <param name="control">The control instance</param>
    /// <param name="placeHolder">The place holder to which the control will be added.</param>
    public virtual T CreateControl<T>(Control control, string placeHolder, bool isBackendObject = false) where T : ControlData
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      if (string.IsNullOrEmpty(placeHolder))
        throw new ArgumentNullException(nameof (placeHolder));
      T control1 = this.CreateControl<T>(isBackendObject);
      control1.ObjectType = control.GetType().AssemblyQualifiedName;
      control1.PlaceHolder = placeHolder;
      ++control1.Version;
      this.ReadProperties((object) control, (ObjectData) control1);
      return control1;
    }

    /// <summary>
    /// Creates a new persistent class for storing serialized control data and serializes the provided user control in to the created instance.
    /// </summary>
    /// <param name="controlPath">The path to the user control</param>
    /// <param name="placeHolder">The place holder to which the control will be added.</param>
    public virtual T CreateControl<T>(string controlPath, string placeHolder, bool isBackendObject = false) where T : ControlData
    {
      if (string.IsNullOrEmpty(controlPath))
        throw new ArgumentNullException("control");
      if (string.IsNullOrEmpty(placeHolder))
        throw new ArgumentNullException(nameof (placeHolder));
      UserControl component = CompilationHelpers.LoadControl<UserControl>(controlPath);
      T control = this.CreateControl<T>(isBackendObject);
      control.ObjectType = controlPath;
      control.PlaceHolder = placeHolder;
      this.ReadProperties((object) component, (ObjectData) control);
      return control;
    }

    /// <summary>
    /// Reads the properties recursively until the whole object graph has been read.
    /// </summary>
    /// <param name="component">The component on which the properties to be read are declared.</param>
    /// <param name="parentPropertyData">The persistent data of the parent property.</param>
    public virtual void ReadProperties(object component, ControlProperty parentPropertyData) => this.ReadProperties(component, parentPropertyData, (CultureInfo) null);

    /// <summary>
    /// Reads the properties recursively until the whole object graph has been read.
    /// </summary>
    /// <param name="component">The component on which the properties to be read are declared.</param>
    /// <param name="parentPropertyData">The persistent data of the parent property.</param>
    public virtual void ReadProperties(
      object component,
      ControlProperty parentPropertyData,
      CultureInfo language,
      object defaultComponentInstance = null)
    {
      if (component == null)
        throw new ArgumentNullException(nameof (component));
      if (parentPropertyData == null)
        throw new ArgumentNullException("objectData");
      this.ReadPropertiesInternal(component, parentPropertyData.Control, parentPropertyData.ChildProperties, (CultureInfo) null, defaultComponentInstance);
    }

    /// <summary>
    /// Reads all properties from the component and creates persistent elements for them.
    /// </summary>
    /// <param name="component">The component from which the properties should be read.</param>
    /// <param name="objectData">The persistent object that represents the object data.</param>
    public virtual void ReadProperties(object component, ObjectData objectData) => this.ReadProperties(component, objectData, (CultureInfo) null, (object) null);

    /// <summary>
    /// Reads all properties from the component and creates persistent elements for them.
    /// </summary>
    /// <param name="component">The component from which the properties should be read.</param>
    /// <param name="objectData">The persistent object that represents the object data.</param>
    /// <param name="language">The language.</param>
    /// <param name="defaultComponentInstance">The default component instance.</param>
    public virtual void ReadProperties(
      object component,
      ObjectData objectData,
      CultureInfo language,
      object defaultComponentInstance = null)
    {
      if (component == null)
        throw new ArgumentNullException(nameof (component));
      if (objectData == null)
        throw new ArgumentNullException(nameof (objectData));
      if (string.IsNullOrEmpty(objectData.ObjectType) || string.IsNullOrEmpty(objectData.ObjectType) || !objectData.ObjectType.StartsWith("~", StringComparison.InvariantCulture))
        objectData.ObjectType = component.GetType().FullName;
      language = this.ResolvePersistenceLanguage(objectData, language);
      foreach (ControlProperty clearProperty in PropertyLoader.GetLoader(objectData).ClearProperties(component, language))
        this.Delete(clearProperty);
      this.TryToInitializeControl(component, objectData);
      this.ReadPropertiesInternal(component, objectData, objectData.Properties, language, defaultComponentInstance);
    }

    /// <summary>
    /// Reads the specified property from the component and persist it in the specified ObjectData instance.
    /// </summary>
    /// <param name="component">The component from which the properties should be read.</param>
    /// <param name="objectData">The persistent object that represents the object data.</param>
    public virtual void ReadProperty(
      object component,
      ObjectData objectData,
      string propertyName,
      CultureInfo language)
    {
      if (component == null)
        throw new ArgumentNullException(nameof (component));
      if (objectData == null)
        throw new ArgumentNullException(nameof (objectData));
      if (string.IsNullOrEmpty(propertyName))
        throw new ArgumentNullException(nameof (propertyName));
      language = this.ResolvePersistenceLanguage(objectData, language);
      if (((IEnumerable<string>) ObjectData.NonMultilingualStandardPropNames).Contains<string>(propertyName))
        language = (CultureInfo) null;
      PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component).Find(propertyName, false);
      if (propertyDescriptor != null && propertyDescriptor.Attributes[typeof (NonMultilingualAttribute)] != null)
        language = (CultureInfo) null;
      string languageName = language == null ? (string) null : language.Name;
      ControlProperty controlProperty = objectData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (cp => cp.Language == languageName && cp.Name == propertyName)).SingleOrDefault<ControlProperty>();
      if (controlProperty != null)
      {
        objectData.Properties.Remove(controlProperty);
        this.Delete(controlProperty);
      }
      PropertyPersistenceAttribute persistenceAttribute = propertyDescriptor != null ? (PropertyPersistenceAttribute) propertyDescriptor.Attributes[typeof (PropertyPersistenceAttribute)] : throw new ArgumentException("The property " + propertyName + " was not found!");
      if (persistenceAttribute != null && persistenceAttribute.PersistInPage)
        return;
      PropertyPersister propertyPersister = this.ObjectDataUtility.ResolvePersister(propertyDescriptor, objectData, component, language);
      if (propertyPersister == null || !propertyPersister.DoPersist)
        return;
      propertyPersister.Read<TProvider>(this, objectData.Properties);
    }

    private void ReadPropertiesInternal(
      object component,
      ObjectData objectData,
      IList<ControlProperty> controlProperties,
      CultureInfo language,
      object defaultComponentInstance)
    {
      List<PropertyDescriptor> source = new List<PropertyDescriptor>();
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(component))
      {
        PropertyPersistenceAttribute attribute = (PropertyPersistenceAttribute) property.Attributes[typeof (PropertyPersistenceAttribute)];
        if (attribute != null)
        {
          if (!attribute.PersistInPage)
          {
            if (attribute.IsKey)
            {
              source.Add(property);
              continue;
            }
          }
          else
            continue;
        }
        propertyDescriptorList.Add(property);
      }
      if (defaultComponentInstance == null)
      {
        defaultComponentInstance = this.ObjectDataUtility.CreateDefaultInstance(component.GetType());
        foreach (PropertyDescriptor propertyDescriptor in source)
          propertyDescriptor.SetValue(defaultComponentInstance, propertyDescriptor.GetValue(component));
        if (objectData.Strategy == PropertyPersistenceStrategy.Translatable)
        {
          IEnumerable<string> keyPropertyNames = source.Select<PropertyDescriptor, string>((Func<PropertyDescriptor, string>) (x => x.Name));
          List<ControlProperty> list = objectData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => !keyPropertyNames.Contains<string>(x.Name) && x.Language == null && x.Value != null)).ToList<ControlProperty>();
          this.PopulateProperties(defaultComponentInstance, (IList<ControlProperty>) list, objectData: objectData);
        }
      }
      foreach (PropertyDescriptor propertyDescriptor in source)
        this.ObjectDataUtility.ResolvePersister(propertyDescriptor, objectData, component, defaultInstance: defaultComponentInstance)?.Read<TProvider>(this, controlProperties);
      foreach (PropertyDescriptor propertyDescriptor in propertyDescriptorList)
      {
        bool flag = this.IsNonMultilingualProperty(propertyDescriptor);
        PropertyPersister propertyPersister = ((language == null ? 0 : (component is Control ? 1 : 0)) & (flag ? 1 : 0)) == 0 ? this.ObjectDataUtility.ResolvePersister(propertyDescriptor, objectData, component, language, defaultComponentInstance) : this.ObjectDataUtility.ResolvePersister(propertyDescriptor, objectData, component, defaultInstance: defaultComponentInstance);
        if (propertyPersister != null && propertyPersister.DoPersist)
          propertyPersister.Read<TProvider>(this, controlProperties);
      }
      if (objectData != null && objectData.Strategy == PropertyPersistenceStrategy.Translatable)
        this.RemoveDuplicateProperties(controlProperties, language);
      if (objectData == null || objectData.Properties.Count <= 0 || objectData.Properties.Count != source.Count || objectData.ParentProperty == null)
        return;
      List<ControlProperty> controlPropertyList = new List<ControlProperty>((IEnumerable<ControlProperty>) controlProperties);
      controlProperties.Clear();
      foreach (ControlProperty controlProperty in controlPropertyList)
        this.Delete(controlProperty);
    }

    private bool IsNonMultilingualProperty(PropertyDescriptor propertyDescriptor) => ((IEnumerable<string>) ObjectData.NonMultilingualStandardPropNames).Contains<string>(propertyDescriptor.Name) || propertyDescriptor.Attributes[typeof (NonMultilingualAttribute)] != null || propertyDescriptor.Attributes[typeof (PropertyPersistenceAttribute)] is PropertyPersistenceAttribute attribute && attribute.IsKey;

    /// <summary>
    /// When the strategy is Translatable, all properties different from the default ones are preserved.
    /// This is not valid for complex properties. There is no need to persist properties which have the same value
    /// for the requested language and for all-translations
    /// </summary>
    /// <param name="controlProperties"></param>
    private void RemoveDuplicateProperties(
      IList<ControlProperty> controlProperties,
      CultureInfo language)
    {
      IEnumerable<IGrouping<\u003C\u003Ef__AnonymousType57<string, string>, ControlProperty>> groupings = controlProperties.GroupBy(x => new
      {
        Name = x.Name,
        Value = x.Value
      }).Where<IGrouping<\u003C\u003Ef__AnonymousType57<string, string>, ControlProperty>>(x => x.Key.Value != null);
      List<ControlProperty> controlPropertyList = new List<ControlProperty>();
      foreach (IGrouping<\u003C\u003Ef__AnonymousType57<string, string>, ControlProperty> source in groupings)
      {
        if (source.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == null)) != null)
        {
          IEnumerable<ControlProperty> collection = source.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language != null));
          controlPropertyList.AddRange(collection);
        }
      }
      foreach (ControlProperty controlProperty in controlPropertyList)
        controlProperties.Remove(controlProperty);
    }

    internal CopyDirection ResolveDraftsCopyDirection(
      DraftData sourceDraft,
      DraftData targetDraft)
    {
      CopyDirection copyDirection = CopyDirection.Unspecified;
      if (sourceDraft.IsTempDraft && !targetDraft.IsTempDraft)
        copyDirection = CopyDirection.CopyToOriginal;
      else if (!sourceDraft.IsTempDraft && targetDraft.IsTempDraft)
        copyDirection = CopyDirection.OriginalToCopy;
      return copyDirection;
    }

    internal void ClearPropertiesForLiveSyncedContainer<T>(
      IContentWithDrafts<T> container,
      CultureInfo language,
      CultureInfo lastLanguageLeft = null)
      where T : DraftData
    {
      List<IControlsContainer> controlsContainerList = new List<IControlsContainer>();
      controlsContainerList.Add((IControlsContainer) container);
      foreach (T draft in (IEnumerable<T>) container.Drafts)
        controlsContainerList.Add((IControlsContainer) draft);
      foreach (IControlsContainer controlsContainer in controlsContainerList)
      {
        foreach (ControlData control in controlsContainer.Controls)
        {
          ControlHelper.ClearProperties((ObjectData) control, language);
          if (lastLanguageLeft != null && control.Strategy == PropertyPersistenceStrategy.Translatable)
          {
            foreach (IGrouping<string, ControlProperty> source in control.Properties.GroupBy<ControlProperty, string>((Func<ControlProperty, string>) (x => x.Name)).ToList<IGrouping<string, ControlProperty>>())
            {
              ControlProperty controlProperty1 = source.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == lastLanguageLeft.Name));
              if (controlProperty1 != null)
              {
                ControlProperty controlProperty2 = source.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == null));
                if (controlProperty2 != null)
                  this.Delete(controlProperty2);
                controlProperty1.Language = (string) null;
              }
            }
            control.SetPersistanceStrategy();
          }
        }
      }
    }

    /// <summary>
    /// Clears the properties from the Property collections and deletes every cleared
    /// property from the persistent layer.
    /// </summary>
    /// <param name="objectData">
    /// An instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ObjectData" /> type from which the properties ought to be cleared.
    /// </param>
    protected internal virtual void ClearProperties(ObjectData objectData)
    {
      if (objectData == null)
        throw new ArgumentNullException(nameof (objectData));
      List<ControlProperty> controlPropertyList = objectData.Properties != null ? new List<ControlProperty>((IEnumerable<ControlProperty>) objectData.Properties) : throw new ArgumentException("Properties collection of the objectData argument must be initialized before passing the object to ClearProperties method.");
      objectData.Properties.Clear();
      foreach (ControlProperty controlProperty in controlPropertyList)
        this.Delete(controlProperty);
    }

    /// <summary>Deletes all properties for the specified language.</summary>
    /// <param name="language">The language.</param>
    public void ClearProperties(ObjectData obj, CultureInfo language)
    {
      if (obj.Strategy == PropertyPersistenceStrategy.NotTranslatable)
        this.ClearProperties(obj);
      else
        ControlHelper.ClearProperties(obj, language);
    }

    /// <summary>
    /// Deletes all properties for languages different than the specified language.
    /// </summary>
    /// <param name="language">The language.</param>
    public void ClearPropertiesExcept(
      ObjectData obj,
      CultureInfo language,
      bool clearInvariantProperties)
    {
      ControlHelper.ClearPropertiesExcept(obj, language, clearInvariantProperties);
    }

    /// <summary>
    /// Copies properties between different languages in a single object.
    /// </summary>
    /// <param name="obj">The object to work with.</param>
    /// <param name="sourceLanguage">The source language.</param>
    /// <param name="targetLanguage">The target language.</param>
    /// <param name="cleanOldTargetLanguageProperties">Whether to clean existing properties in the target language.</param>
    public void CopyProperties(
      ObjectData obj,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool cleanOldTargetLanguageProperties = true)
    {
      ControlHelper.CopyLanguageProperties((IControlPropertyProvider) this, obj, sourceLanguage, targetLanguage, cleanOldTargetLanguageProperties);
    }

    /// <summary>
    /// Clones the properties of the given object in the given language to the specified target languages. All properties previously existing in the
    /// target languages are removed. As a result, the object will have the property values of the source language in all specified languages.
    /// </summary>
    /// <param name="obj">The object to work on.</param>
    /// <param name="sourceLanguage">The language to clone.</param>
    /// <param name="targetLanguages">The target languages.</param>
    public void CloneLanguageProperties(
      ObjectData obj,
      CultureInfo sourceLanguage,
      IEnumerable<CultureInfo> targetLanguages)
    {
      ControlHelper.CloneLanguageProperties((IControlPropertyProvider) this, obj, sourceLanguage, targetLanguages);
    }

    /// <summary>
    /// Checks if the instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ObjectData" /> object is actually an instance
    /// of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> and if so initializes the specific properties of control
    /// data object.
    /// </summary>
    /// <param name="objectData">
    /// An instance of object data that may be actually <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" />.
    /// </param>
    protected internal virtual void TryToInitializeControl(object component, ObjectData objectData)
    {
      if (component == null)
        throw new ArgumentNullException(nameof (component));
      if (objectData == null)
        throw new ArgumentNullException(nameof (objectData));
      if (!(objectData is ControlData controlData))
        return;
      controlData.IsLayoutControl = component is LayoutControl;
      controlData.IsDataSource = component is IDataSource;
    }

    /// <summary>
    /// Sets a unique control ID for the provided page or template.
    /// </summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="controlData">The control data.</param>
    public virtual void SetControlId(
      IControlsContainer pageData,
      ObjectData controlData,
      CultureInfo language = null)
    {
      if (pageData == null)
        throw new ArgumentNullException(nameof (pageData));
      if (controlData == null)
        throw new ArgumentNullException(nameof (controlData));
      List<PageNode> pageNodeList;
      switch (pageData)
      {
        case PageData _:
          pageNodeList = new List<PageNode>()
          {
            ((PageData) pageData).NavigationNode
          };
          break;
        case PageDraft _:
          pageNodeList = new List<PageNode>()
          {
            ((PageDraft) pageData).ParentPage.NavigationNode
          };
          break;
        default:
          pageNodeList = new List<PageNode>();
          break;
      }
      foreach (PageNode pageNode in pageNodeList)
      {
        if (pageNode != null)
        {
          if (!pageNode.IsGranted("Pages", "CreateChildControls"))
            throw new UnauthorizedAccessException(string.Format(Res.Get<SecurityResources>().NotAuthorizedToDoSetAction, (object) "CreateChildControls", (object) "Pages"));
        }
      }
      ControlProperty controlProperty = (ControlProperty) null;
      foreach (ControlProperty property in (IEnumerable<ControlProperty>) controlData.Properties)
      {
        if (property.Name.Equals("ID", StringComparison.OrdinalIgnoreCase))
        {
          if (!string.IsNullOrEmpty(property.Value))
            return;
          controlProperty = property;
          break;
        }
      }
      bool suppressSecurityChecks = this.Provider.SuppressSecurityChecks;
      this.Provider.SuppressSecurityChecks = true;
      if (controlProperty == null)
      {
        controlProperty = this.CreateProperty();
        controlProperty.Name = "ID";
        controlProperty.Language = (string) null;
        controlData.Properties.Add(controlProperty);
      }
      this.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      int num = Math.Max(pageData.LastControlId + 1, pageData.Controls.Count<ControlData>() + 1);
      pageData.LastControlId = num;
      string str = !(pageData is IPageTemplate) ? "C" : ((IPageTemplate) pageData).Key;
      controlProperty.Value = str + num.ToString("#000");
    }

    /// <summary>
    /// Deserializes the provided data and creates a new instance of a custom or user control.
    /// </summary>
    /// <param name="controlData">The serialized data.</param>
    /// <param name="culture">
    /// Culture to be used for populating the properies of the control
    /// Note that only the properties marked with <typeparamref name="Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MultilingualPropertyAttribute.Sitefinity.Modules.Pages.PropertyPersisters.MultilingualPropertyAttribute" />
    /// will be processed with the specified culture
    /// </param>
    /// <returns>An instance of the control.</returns>
    public virtual Control LoadControl(ObjectData controlData, CultureInfo culture = null) => this.LoadControlOrObject(controlData, culture) as Control;

    /// <summary>
    /// Deserializes the provided data and creates a new instance of the specified object.
    /// </summary>
    /// <param name="controlData">The serialized data.</param>
    /// <returns>An instance of the object.</returns>
    public virtual object LoadObject(ObjectData objectData) => this.LoadControlOrObject(objectData);

    protected virtual object LoadControlOrObject(ObjectData objectData, CultureInfo culture = null)
    {
      if (objectData == null)
        throw new ArgumentNullException("controlData");
      object component = !objectData.ObjectType.StartsWith("~/") ? Activator.CreateInstance(TypeResolutionService.ResolveType(objectData.ObjectType, true)) : (object) CompilationHelpers.LoadControl<UserControl>(objectData.ObjectType);
      this.PopulateProperties(component, (IList<ControlProperty>) objectData.GetProperties(culture, true).ToList<ControlProperty>(), culture, objectData);
      return component;
    }

    /// <summary>Gets the type of the control.</summary>
    /// <param name="controlData">The control data.</param>
    /// <returns></returns>
    public static Type GetControlType(ObjectData controlData) => ControlManager<TProvider>.GetControlType(controlData, true);

    internal static Type GetControlType(ObjectData controlData, bool throwOnError)
    {
      if (controlData == null)
        throw new ArgumentNullException(nameof (controlData));
      return !controlData.ObjectType.StartsWith("~/") ? TypeResolutionService.ResolveType(controlData.ObjectType, throwOnError) : BuildManager.GetCompiledType(controlData.ObjectType);
    }

    /// <summary>
    /// Populates the properties of the specified control <paramref name="component" /> with the data from the properties <paramref name="propDataList" />
    /// </summary>
    /// <param name="component">The component/control to be populated</param>
    /// <param name="propDataList">The properties used to populate the control</param>
    /// <param name="culture">Culture for populating the multiulingual properties <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MultilingualPropertyAttribute" /></param>
    protected internal virtual void PopulateProperties(
      object component,
      IList<ControlProperty> propDataList,
      CultureInfo culture = null,
      ObjectData objectData = null)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
      foreach (ControlProperty orderedProperty in propDataList.GetOrderedProperties<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ControllerName" && p.Value != null)))
        this.PopulateProperty(component, properties, orderedProperty, culture, objectData);
    }

    /// <summary>
    /// Populates the property of the specified control <paramref name="component" /> with the data from the control property <paramref name="propertyData" />
    /// </summary>
    /// <param name="component">The component/control to be populated</param>
    /// <param name="propertyDescriptors">The property descriptors of the component type.</param>
    /// <param name="propertyData">The property used to populate the control.</param>
    /// <param name="culture">Culture for populating the multiulingual properties <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MultilingualPropertyAttribute" /></param>
    protected virtual void PopulateProperty(
      object component,
      PropertyDescriptorCollection propertyDescriptors,
      ControlProperty propertyData,
      CultureInfo culture = null,
      ObjectData objectData = null)
    {
      PropertyDescriptor propertyDescriptor = propertyDescriptors.Find(propertyData.Name, true);
      if (propertyDescriptor == null)
        return;
      if (objectData == null)
        objectData = propertyData.Control;
      this.ObjectDataUtility.ResolvePersister(propertyDescriptor, objectData, component, culture)?.Populate<TProvider>(this, propertyData);
    }

    /// <summary>
    /// Makes a deep copy of controls from the source collection to the target list.
    /// </summary>
    /// <typeparam name="SrcT">The source generic type.</typeparam>
    /// <typeparam name="TrgT">The target generic type.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public virtual void CopyControls<SrcT, TrgT>(IEnumerable<SrcT> source, IList<TrgT> target)
      where SrcT : ControlData
      where TrgT : ControlData
    {
      this.CopyControls<SrcT, TrgT>(source, target, (CultureInfo) null, (CultureInfo) null);
    }

    /// <summary>
    /// Makes a deep copy of controls from the source collection to the target list.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="sourceLanguage">The source language.</param>
    /// <param name="targetLanguage">The target language.</param>
    /// <param name="useOriginalLiveIds">Whether to use the original controls (if present) instead of creating new.</param>
    /// <param name="originalControls">The original controls of the target.</param>
    [Obsolete]
    public virtual void CopyControls<SrcT, TrgT>(
      IEnumerable<SrcT> source,
      IList<TrgT> target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool useOriginalLiveIds = false,
      IList<TrgT> originalControls = null)
      where SrcT : ControlData
      where TrgT : ControlData
    {
      this.CopyControls<SrcT, TrgT>(source, target, sourceLanguage, targetLanguage, useOriginalLiveIds ? CopyDirection.CopyToOriginal : CopyDirection.OriginalToCopy);
    }

    /// <summary>
    /// Makes a deep copy of controls from the source collection to the target list. Note, that this method handles links between controls.
    /// When a control is copied OriginalToCopy, the copies remember their original control. When copying back CopyToOriginal,
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="sourceLanguage">The source language.</param>
    /// <param name="targetLanguage">The target language.</param>
    /// <param name="useOriginalLiveIds">Whether to use the original controls (if present) instead of creating new.</param>
    /// <param name="originalControls">The original controls of the target.</param>
    public virtual void CopyControls<SrcT, TrgT>(
      IEnumerable<SrcT> source,
      IList<TrgT> target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      CopyDirection copyDirection,
      bool optimized = false)
      where SrcT : ControlData
      where TrgT : ControlData
    {
      this.CopyControls<SrcT, TrgT>(source, target, sourceLanguage, targetLanguage, copyDirection, optimized, false);
    }

    /// <summary>
    /// Makes a deep copy of controls from the source collection to the target list. Note, that this method handles links between controls.
    /// When a control is copied OriginalToCopy, the copies remember their original control. When copying back CopyToOriginal,
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="sourceLanguage">The source language.</param>
    /// <param name="targetLanguage">The target language.</param>
    /// <param name="useOriginalLiveIds">Whether to use the original controls (if present) instead of creating new.</param>
    /// <param name="originalControls">The original controls of the target.</param>
    /// <param name="ignorePersonalization">If set true controls' personalized versions will not be copied</param>
    public virtual void CopyControls<SrcT, TrgT>(
      IEnumerable<SrcT> source,
      IList<TrgT> target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      CopyDirection copyDirection,
      bool optimized,
      bool ignorePersonalization)
      where SrcT : ControlData
      where TrgT : ControlData
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      List<TrgT> source1 = (List<TrgT>) null;
      if (copyDirection == CopyDirection.Unspecified)
      {
        bool flag = false;
        try
        {
          flag = this.Provider.SuppressSecurityChecks;
          this.Provider.SuppressSecurityChecks = true;
          for (int index = target.Count - 1; index >= 0; --index)
            this.DeleteItem((object) target[index]);
        }
        finally
        {
          this.Provider.SuppressSecurityChecks = flag;
        }
      }
      else
        source1 = new List<TrgT>((IEnumerable<TrgT>) target);
      Dictionary<Guid, Guid> sourceTargetMapping = new Dictionary<Guid, Guid>();
      foreach (SrcT srcT in source)
      {
        SrcT src = srcT;
        TrgT trg = default (TrgT);
        if (source1 != null && source1.Count > 0)
        {
          if (copyDirection == CopyDirection.CopyToOriginal && ((SrcT) src).OriginalControlId != Guid.Empty)
            trg = source1.Where<TrgT>((Func<TrgT, bool>) (ctrl => ctrl.Id == src.OriginalControlId)).SingleOrDefault<TrgT>();
          else if (copyDirection == CopyDirection.OriginalToCopy)
            trg = source1.Where<TrgT>((Func<TrgT, bool>) (ctrl => ctrl.OriginalControlId == src.Id)).SingleOrDefault<TrgT>();
        }
        bool flag = false;
        if ((object) (TrgT) trg == null)
        {
          trg = this.CreateControl<TrgT>(((SrcT) src).IsBackendObject);
          flag = true;
        }
        if (!ignorePersonalization && (((SrcT) src).IsPersonalized || ((TrgT) trg).IsPersonalized))
        {
          IEnumerable<SrcT> source2 = ((SrcT) src).PersonalizedControls.Cast<SrcT>();
          List<TrgT> list = ((TrgT) trg).PersonalizedControls.Cast<TrgT>().ToList<TrgT>();
          this.CopyControls<SrcT, TrgT>(source2, (IList<TrgT>) list, sourceLanguage, targetLanguage, copyDirection, optimized);
          list.ForEach((Action<TrgT>) (t => t.PersonalizationMasterId = trg.Id));
          ((TrgT) trg).PersonalizedControls.Clear();
          foreach (TrgT rgT in list)
            ((TrgT) trg).PersonalizedControls.Add((ControlData) rgT);
        }
        if (!optimized || ((SrcT) src).Version == 0 || ((SrcT) src).Version != ((TrgT) trg).Version || ((SrcT) src).GetMultilingualVersion(sourceLanguage) != ((TrgT) trg).GetMultilingualVersion(sourceLanguage))
        {
          if (((SrcT) src).Version == 0)
            ++src.Version;
          this.CopyControl((ControlData) src, (ControlData) trg, sourceLanguage, targetLanguage, ignorePersonalization);
        }
        switch (copyDirection)
        {
          case CopyDirection.OriginalToCopy:
            if (((TrgT) trg).OriginalControlId != ((SrcT) src).Id)
            {
              ((TrgT) trg).OriginalControlId = ((SrcT) src).Id;
              break;
            }
            break;
          case CopyDirection.CopyToOriginal:
            if (flag)
            {
              ((SrcT) src).OriginalControlId = ((TrgT) trg).Id;
              break;
            }
            break;
        }
        if (flag)
          target.Add(trg);
        else
          source1.Remove(trg);
        sourceTargetMapping.Add(((SrcT) src).Id, ((TrgT) trg).Id);
      }
      if (source1 != null && source1.Count > 0)
      {
        bool flag = false;
        try
        {
          flag = this.Provider.SuppressSecurityChecks;
          this.Provider.SuppressSecurityChecks = true;
          for (int index = source1.Count - 1; index >= 0; --index)
          {
            TrgT rgT = source1[index];
            target.Remove(rgT);
            this.DeleteItem((object) rgT);
          }
        }
        finally
        {
          this.Provider.SuppressSecurityChecks = flag;
        }
      }
      foreach (SrcT srcT in source)
      {
        SrcT src = srcT;
        TrgT rgT1 = target.Single<TrgT>((Func<TrgT, bool>) (c => c.Id == sourceTargetMapping[src.Id]));
        SrcT srcParent = source.FirstOrDefault<SrcT>((Func<SrcT, bool>) (c => c.Id == src.ParentId));
        if ((object) (SrcT) srcParent != null)
        {
          TrgT rgT2 = target.Single<TrgT>((Func<TrgT, bool>) (c => c.Id == sourceTargetMapping[srcParent.Id]));
          if (rgT1.ParentId != rgT2.Id)
            rgT1.ParentId = rgT2.Id;
        }
        else if (rgT1.ParentId != ((SrcT) src).ParentId)
          rgT1.ParentId = ((SrcT) src).ParentId;
      }
      foreach (SrcT srcT in source)
      {
        SrcT src = srcT;
        TrgT rgT3 = target.Single<TrgT>((Func<TrgT, bool>) (c => c.Id == sourceTargetMapping[src.Id]));
        SrcT srcSibling = source.FirstOrDefault<SrcT>((Func<SrcT, bool>) (c => c.Id == src.SiblingId));
        if ((object) (SrcT) srcSibling != null)
        {
          TrgT rgT4 = target.Single<TrgT>((Func<TrgT, bool>) (c => c.Id == sourceTargetMapping[srcSibling.Id]));
          if (rgT3.SiblingId != rgT4.Id)
            rgT3.SiblingId = rgT4.Id;
        }
        else if (rgT3.SiblingId != ((SrcT) src).SiblingId)
          rgT3.SiblingId = ((SrcT) src).SiblingId;
      }
    }

    /// <summary>
    /// Copies all properties of the source control to the target control.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public virtual void CopyControl(ControlData source, ControlData target) => this.CopyControl(source, target, (CultureInfo) null, (CultureInfo) null);

    /// <summary>
    /// Copies properties of the source control to the target control. If source and target languages are specified,
    /// only properties in source language will be copied and they will be set in target language in the target object.
    /// If languages are not specified, all properties will be copied from source to target.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="sourceLanguage">The language to filter properties from the source object. Can be null.</param>
    /// <param name="targetLanguage">The language to copy the properties to. Can be null.</param>
    public virtual void CopyControl(
      ControlData source,
      ControlData target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage)
    {
      this.CopyControl(source, target, sourceLanguage, targetLanguage, false);
    }

    /// <summary>
    /// Copies properties of the source control to the target control. If source and target languages are specified,
    /// only properties in source language will be copied and they will be set in target language in the target object.
    /// If languages are not specified, all properties will be copied from source to target.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="sourceLanguage">The language to filter properties from the source object. Can be null.</param>
    /// <param name="targetLanguage">The language to copy the properties to. Can be null.</param>
    /// <param name="ignorePersonalization">If set true control's personalized versions will not be copied</param>
    public virtual void CopyControl(
      ControlData source,
      ControlData target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool ignorePersonalization)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      if (source.Caption != target.Caption)
        target.Caption = source.Caption;
      if (source.Description != target.Description)
        target.Description = source.Description;
      if (source.Owner != target.Owner)
        target.Owner = source.Owner;
      if (source.ObjectType != target.ObjectType)
        target.ObjectType = source.ObjectType;
      if (source.InheritsPermissions != target.InheritsPermissions)
        target.InheritsPermissions = source.InheritsPermissions;
      if (source.PlaceHolder != target.PlaceHolder)
        target.PlaceHolder = source.PlaceHolder;
      if (source.Shared != target.Shared)
        target.Shared = source.Shared;
      if (source.IsDataSource != target.IsDataSource)
        target.IsDataSource = source.IsDataSource;
      if (source.IsLayoutControl != target.IsLayoutControl)
        target.IsLayoutControl = source.IsLayoutControl;
      if (source.AllowSecurityTrimming != target.AllowSecurityTrimming)
        target.AllowSecurityTrimming = source.AllowSecurityTrimming;
      if (!this.AreEqual(source.SupportedPermissionSets, target.SupportedPermissionSets))
        target.SupportedPermissionSets = source.SupportedPermissionSets;
      if (source.IsOverridedControl != target.IsOverridedControl)
        target.IsOverridedControl = source.IsOverridedControl;
      if (source.BaseControlId != target.BaseControlId)
        target.BaseControlId = source.BaseControlId;
      if (source.Editable != target.Editable)
        target.Editable = source.Editable;
      if (source.Version != target.Version)
        target.Version = source.Version;
      if (source.Strategy != target.Strategy)
        target.Strategy = source.Strategy;
      target.CopyMultilingualVersionFrom((ObjectData) source, sourceLanguage);
      if (!ignorePersonalization)
        this.CopyPersonalizationProperties(source, target);
      this.Provider.CopyPermissions((IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) source.Permissions, (IList) target.Permissions, source.Id, target.Id, true);
      this.CopyPresentation<ControlPresentation, ControlPresentation>((IEnumerable<ControlPresentation>) source.Presentation, target.Presentation);
      if ((ControlHelper.NormalizeLanguage(sourceLanguage) == null || ControlHelper.NormalizeLanguage(targetLanguage) == null ? 1 : (sourceLanguage == targetLanguage ? 1 : 0)) != 0)
      {
        ControlHelper.CopyAllProperties((IControlPropertyProvider) this, (IEnumerable<ControlProperty>) source.Properties, target.Properties, sourceCulture: sourceLanguage);
      }
      else
      {
        this.ClearProperties((ObjectData) target, targetLanguage);
        ControlHelper.CopyLanguageProperties((IControlPropertyProvider) this, (IEnumerable<ControlProperty>) source.Properties, target.Properties, sourceLanguage, targetLanguage);
        ControlHelper.CopyNonMultilingualProperties((IControlPropertyProvider) this, (IEnumerable<ControlProperty>) source.Properties, target.Properties, false);
      }
    }

    /// <summary>
    /// Copies personalization properties from the source control to the target control.
    /// </summary>
    /// <param name="source">The source</param>
    /// <param name="atargetrr2">The target</param>
    /// <returns></returns>
    private void CopyPersonalizationProperties(ControlData source, ControlData target)
    {
      if (source.IsPersonalized != target.IsPersonalized)
        target.IsPersonalized = source.IsPersonalized;
      if (!(source.PersonalizationSegmentId != target.PersonalizationSegmentId))
        return;
      target.PersonalizationSegmentId = source.PersonalizationSegmentId;
    }

    /// <summary>
    /// Compares the specified arrays by comapring their items on each position.
    /// </summary>
    /// <param name="arr1">The arr1.</param>
    /// <param name="arr2">The arr2.</param>
    /// <returns></returns>
    private bool AreEqual(string[] arr1, string[] arr2)
    {
      if (arr1 == null && arr2 == null)
        return true;
      if (arr1 == null && arr2 != null || arr1 != null && arr2 == null || arr1.Length != arr2.Length)
        return false;
      bool flag = true;
      for (int index = arr1.Length - 1; index >= 0; --index)
      {
        flag &= arr1[index] == arr2[index];
        if (!flag)
          break;
      }
      return flag;
    }

    /// <summary>
    /// Makes a deep copy of the objects from the source collection to the target list.
    /// </summary>
    /// <typeparam name="SrcT">The source generic type.</typeparam>
    /// <typeparam name="TrgT">The target generic type.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public virtual void CopyObjects<SrcT, TrgT>(IEnumerable<SrcT> source, IList<TrgT> target)
      where SrcT : ObjectData
      where TrgT : ObjectData
    {
      ControlHelper.CopyObjects<SrcT, TrgT>(source, target, (IControlPropertyProvider) this);
    }

    /// <summary>
    /// Copies all properties of the source object to the target object.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public virtual void CopyObject(ObjectData source, ObjectData target) => ControlHelper.CopyObject(source, target, (IControlPropertyProvider) this);

    /// <summary>
    /// Creates new persistent class for storing object serialized data.
    /// </summary>
    /// <returns></returns>
    public virtual ObjectData CreateObjectData(bool isBackendObject = false) => this.CreateControl<ObjectData>(isBackendObject);

    /// <summary>Creates new control.</summary>
    /// <returns>The new control.</returns>
    public abstract T CreateControl<T>(bool isBackendObject = false) where T : ObjectData;

    /// <summary>Creates new control with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new control.</param>
    /// <returns>The new control.</returns>
    public abstract T CreateControl<T>(Guid id, bool isBackendObject = false) where T : ObjectData;

    /// <summary>Gets the control with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public abstract T GetControl<T>(Guid id) where T : ObjectData;

    /// <summary>Gets a query for controls.</summary>
    /// <returns>The query for controls.</returns>
    public abstract IQueryable<T> GetControls<T>() where T : ObjectData;

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public abstract void Delete(ControlData item);

    /// <summary>
    /// Set default permissions for controls (they do not inherit, but have hard-coded preset ones)
    /// </summary>
    /// <param name="ctrlData">The CTRL data.</param>
    /// <param name="manager">The manager.</param>
    public virtual void SetControlDefaultPermissions(ControlData ctrlData)
    {
      bool isLayoutControl = ctrlData.IsLayoutControl;
      string permissionSet = isLayoutControl ? "LayoutElement" : "Controls";
      string[] strArray1;
      if (!isLayoutControl)
        strArray1 = new string[1]{ "ViewControl" };
      else
        strArray1 = new string[1]{ "ViewLayout" };
      string[] strArray2 = strArray1;
      Telerik.Sitefinity.Security.Model.Permission permission1 = this.GetPermission(permissionSet, ctrlData.Id, SecurityManager.EveryoneRole.Id);
      if (permission1 == null)
      {
        permission1 = this.CreatePermission(permissionSet, ctrlData.Id, SecurityManager.EveryoneRole.Id);
        ctrlData.Permissions.Add(permission1);
      }
      permission1.GrantActions(true, strArray2);
      string[] strArray3;
      if (!isLayoutControl)
        strArray3 = new string[3]
        {
          "MoveControl",
          "EditControlProperties",
          "DeleteControl"
        };
      else
        strArray3 = new string[4]
        {
          "MoveLayout",
          "EditLayoutProperties",
          "DeleteLayout",
          "DropOnLayout"
        };
      string[] strArray4 = strArray3;
      Telerik.Sitefinity.Security.Model.Permission permission2 = this.GetPermission(permissionSet, ctrlData.Id, SecurityManager.BackEndUsersRole.Id);
      if (permission2 == null)
      {
        permission2 = this.CreatePermission(permissionSet, ctrlData.Id, SecurityManager.BackEndUsersRole.Id);
        ctrlData.Permissions.Add(permission2);
      }
      permission2.GrantActions(true, strArray4);
      string[] strArray5;
      if (!isLayoutControl)
        strArray5 = new string[1]
        {
          "ChangeControlPermissions"
        };
      else
        strArray5 = new string[1]
        {
          "ChangeLayoutPermissions"
        };
      string[] strArray6 = strArray5;
      Telerik.Sitefinity.Security.Model.Permission permission3 = this.GetPermission(permissionSet, ctrlData.Id, SecurityManager.OwnerRole.Id);
      if (permission3 == null)
      {
        permission3 = this.CreatePermission(permissionSet, ctrlData.Id, SecurityManager.OwnerRole.Id);
        ctrlData.Permissions.Add(permission3);
      }
      permission3.GrantActions(true, strArray6);
    }

    private CultureInfo ResolvePersistenceLanguage(ObjectData data, CultureInfo language) => data.Strategy == PropertyPersistenceStrategy.NotTranslatable ? (CultureInfo) null : ControlHelper.NormalizeLanguage(language);

    /// <summary>
    /// Makes a deep copy of properties from the source collection to the target list.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public virtual void CopyProperties(
      IEnumerable<ControlProperty> source,
      IList<ControlProperty> target)
    {
      this.CopyProperties(source, target, (CultureInfo) null, (CultureInfo) null);
    }

    /// <summary>
    /// Makes a deep copy of properties from the source collection to the target list.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public virtual void CopyProperties(
      IEnumerable<ControlProperty> source,
      IList<ControlProperty> target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool cleanOldTargetLanguageProperties = true)
    {
      ControlHelper.CopyLanguageProperties((IControlPropertyProvider) this, source, target, sourceLanguage, targetLanguage, cleanOldTargetLanguageProperties);
    }

    /// <summary>Copies the property.</summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public virtual void CopyProperty(ControlProperty source, ControlProperty target) => ControlHelper.CopyProperty((IControlPropertyProvider) this, source, target);

    /// <summary>Copies the property.</summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="sourceLanguage">The source language.</param>
    /// <param name="targetLanguage">The target language.</param>
    /// <param name="copySourceLstringLanguage">If true, the source language value of source Lstring properties is copied to the target Lstring properties.
    /// If false, the target language value is copied.
    /// </param>
    [Obsolete]
    public virtual void CopyProperty(
      ControlProperty source,
      ControlProperty target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool copySourceLstringLanguage)
    {
      this.CopyProperty(source, target);
    }

    /// <summary>Creates new page template.</summary>
    /// <returns>The new page template.</returns>
    public abstract ControlProperty CreateProperty();

    /// <summary>Creates new page template with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new page template.</param>
    /// <returns>The new page template.</returns>
    public abstract ControlProperty CreateProperty(Guid id);

    /// <summary>Gets the page template with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>A page template.</returns>
    public abstract ControlProperty GetProperty(Guid id);

    /// <summary>Gets a query for page templates.</summary>
    /// <returns>The query for page templates.</returns>
    public abstract IQueryable<ControlProperty> GetProperties();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The page template to delete.</param>
    public abstract void Delete(ControlProperty item);

    void IControlPropertyProvider.DeleteProperty(ControlProperty item) => this.Delete(item);

    void IControlPropertyProvider.DeleteObject(ObjectData item) => this.DeleteItem((object) item);

    void IControlPropertyProvider.DeletePresentation(PresentationData item) => this.Delete(item);

    internal static IList<string> GetPropertyCategories(Type controlType)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(controlType);
      IList<string> propertyCategories = (IList<string>) new List<string>();
      propertyCategories.Add(Res.Get<PageResources>().MiscCategory);
      foreach (PropertyDescriptor propertyDescriptor in properties)
      {
        if (!propertyCategories.Contains(propertyDescriptor.Category))
          propertyCategories.Add(propertyDescriptor.Category);
      }
      return propertyCategories;
    }

    /// <summary>
    /// Makes a deep copy of the presentation data from the source collection to the target list.
    /// </summary>
    /// <typeparam name="SrcT">The source generic type.</typeparam>
    /// <typeparam name="TrgT">The target generic type.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public virtual void CopyPresentation<SrcT, TrgT>(IEnumerable<SrcT> source, IList<TrgT> target)
      where SrcT : PresentationData
      where TrgT : PresentationData
    {
      ControlHelper.CopyPresentation<SrcT, TrgT>(source, target, (IControlPropertyProvider) this);
    }

    /// <summary>
    /// Copies the information from the source presentation item to the target.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public virtual void CopyPresentationItem(PresentationData source, PresentationData target) => ControlHelper.CopyPresentationItem(source, target);

    /// <summary>
    /// Creates new object for storing presentation information.
    /// </summary>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public abstract T CreatePresentationItem<T>() where T : PresentationData;

    /// <summary>
    /// Creates new object for storing presentation information with the specified ID.
    /// </summary>
    /// <param name="pageId">The pageId of the new item.</param>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public abstract T CreatePresentationItem<T>(Guid id) where T : PresentationData;

    /// <summary>Links the presentation item to site.</summary>
    /// <param name="presentationData">The presentation item.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal abstract SiteItemLink LinkPresentationItemToSite(
      PresentationData presentationData,
      Guid siteId);

    /// <summary>Gets the item with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public abstract T GetPresentationItem<T>(Guid id) where T : PresentationData;

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items.
    /// </summary>
    /// <returns>The query for page templates.</returns>
    public abstract IQueryable<T> GetPresentationItems<T>() where T : PresentationData;

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items in a specified site.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>The query for page templates.</returns>
    internal abstract IQueryable<T> GetPresentationItems<T>(Guid siteId) where T : PresentationData;

    /// <summary>Gets the links for all presentation items.</summary>
    /// <returns>The query for SiteItemLink.</returns>
    internal abstract IQueryable<SiteItemLink> GetSitePresentationItemLinks<T>() where T : PresentationData;

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public abstract void Delete(PresentationData item);
  }
}
