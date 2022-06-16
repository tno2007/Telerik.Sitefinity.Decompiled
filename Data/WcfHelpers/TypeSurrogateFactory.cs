// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.TypeSurrogateFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.SPI.dataobjects;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Model.WcfHelpers;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>
  /// Creates dynamic type for objects that implements IDynamicFieldsContainer.
  /// The generated type will have all the properties of the original type plus its meta fields
  /// as regular properties.
  /// </summary>
  /// <remarks>
  /// How surrogate works:
  /// http://msdn.microsoft.com/en-us/library/ms733064.aspx
  /// </remarks>
  public class TypeSurrogateFactory : ITypeSurrogateFactory
  {
    private const string DynamicModuleName = "Telerik.Sitefinity.DynamicTypeSurrogates";
    private const TypeAttributes DynamicTypeAttributes = TypeAttributes.Public;
    private const MethodAttributes GetSetAttributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName;
    private static TypeSurrogateFactory instance;
    private IDictionary<string, Type> cacheOfGeneratedTypes;
    private IDictionary<Type, DataContractAttribute> cacheOfContracts;
    private ModuleBuilder moduleBuilder;
    private AssemblyBuilder assemblyBuilder;
    private PropertyFilterDelegate filterTypeProperties;

    /// <summary>Make this class a singleton</summary>
    private TypeSurrogateFactory()
    {
      this.cacheOfGeneratedTypes = SystemManager.CreateStaticCache<string, Type>();
      this.cacheOfContracts = SystemManager.CreateStaticCache<Type, DataContractAttribute>();
      this.filterTypeProperties = this.DefaultPropertyFilter;
      SystemManager.ModelReset += new EventHandler<EventArgs>(this.SystemManager_ModelReset);
      SystemManager.ShuttingDown += new EventHandler<CancelEventArgs>(this.SystemManager_ShuttingDown);
    }

    /// <summary>
    /// Returns the singleton instance of TypeSurrogateFactory
    /// </summary>
    public static TypeSurrogateFactory Instance
    {
      get
      {
        if (TypeSurrogateFactory.instance == null)
          TypeSurrogateFactory.instance = new TypeSurrogateFactory();
        return TypeSurrogateFactory.instance;
      }
    }

    /// <summary>
    /// Returns the MetadataManager used by the TypeSurrogateFactory
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public MetadataManager MetaManager => MetadataManager.GetManager();

    /// <summary>
    /// Determines whether surrogate types would inherit from source types
    /// </summary>
    public bool InheritFromSourceType => false;

    /// <summary>
    /// Get a collection of known types.
    /// Known are all surrogate types that have been created so far.
    /// </summary>
    /// <remarks>This will return the cached surrogate types.</remarks>
    public ICollection<Type> KnownTypes => this.cacheOfGeneratedTypes.Values;

    /// <summary>
    /// Get/set the delegate that determines which properties are to be added to the dynamically-generated surrogates
    /// </summary>
    /// <value>Property filter. Cannot be null.</value>
    public PropertyFilterDelegate PropertyFilter
    {
      get => this.filterTypeProperties;
      set => this.filterTypeProperties = value != null ? value : throw new ArgumentNullException(nameof (value));
    }

    /// <summary>
    /// Creates an instance of <paramref name="type" /> by loading it from the data layer,
    /// by using identity data from the <paramref name="surrogate" />.
    /// </summary>
    /// <param name="type">Persistent type to create an instance of</param>
    /// <param name="surrogate">Surrogate for the persistent <paramref name="type" /></param>
    /// <param name="propertyPath">Property path to the object in the surrogate. This object will be used to get identity.</param>
    /// <returns>Object, joined in transaction,</returns>
    /// <remarks>
    /// <para>
    /// Creates an instance of a type that provoked the creation of a surrogate.
    /// The requirement for creating a surrogate is to have meta fields. Therefore,
    /// this object must exist in database. This method tries to get the identity information
    /// from the surrogate, and use the identity information to get the an instance from
    /// database.
    /// </para>
    /// <para>
    /// For this to work, <paramref name="type" /> must have
    /// <see cref="T:Telerik.Sitefinity.Model.ManagerTypeAttribute" /> applied to it,
    /// or work otherwise with
    /// <see cref="M:Telerik.Sitefinity.Data.ManagerBase.GetMappedManager(System.Type, System.String)" />.
    /// </para>
    /// </remarks>
    public object CreateInstance(Type type, object surrogate, string propertyPath)
    {
      if (this.IsSurrogate(type))
        type = this.GetOriginalType(type);
      bool flag1 = this.IsPersistent(type);
      if (flag1 && string.IsNullOrEmpty(propertyPath))
      {
        surrogate = this.GetPropertyValueFromPath(surrogate, propertyPath);
        Type managerType = WcfContext.ManagerType;
        IManager manager = !(managerType == (Type) null) ? ManagerBase.GetMappedManagerInTransaction(type, managerType, WcfContext.ProviderName, WcfContext.CurrentTransactionName) : WcfContext.GetManager(type, WcfContext.CurrentTransactionName);
        Guid identity = this.GetIdentity(type, surrogate, manager);
        object instance = (object) null;
        if (identity != Guid.Empty)
        {
          Guid lastModifiedBy = Guid.Empty;
          bool flag2 = (typeof (Content).IsAssignableFrom(type) || typeof (DynamicContent).IsAssignableFrom(type)) && ((ContentLifecycleStatus) TypeDescriptor.GetProperties(surrogate)["Status"].GetValue(surrogate) == ContentLifecycleStatus.Temp || (ContentLifecycleStatus) TypeDescriptor.GetProperties(surrogate)["Status"].GetValue(surrogate) == ContentLifecycleStatus.PartialTemp);
          try
          {
            manager.Provider.FetchAllLanguagesData();
            instance = manager.GetItem(type, identity);
          }
          catch (ItemNotFoundException ex)
          {
            if (flag2)
            {
              Guid originalContentId = (Guid) TypeDescriptor.GetProperties(surrogate)["OriginalContentId"].GetValue(surrogate);
              instance = !typeof (Content).IsAssignableFrom(type) ? (object) this.GetDynamicContentItem(type, manager, originalContentId, out lastModifiedBy) : (object) this.GetTempContentItem(type, manager, originalContentId, out lastModifiedBy);
            }
            else
              throw;
          }
          finally
          {
            if (flag2)
            {
              if (!(instance is ILifecycleDataItem lifecycleDataItem) || lifecycleDataItem.Owner == Guid.Empty)
                throw new NotSupportedException(string.Format(Res.Get<ContentLifecycleMessages>().ItemUnlockedWhileEditing));
              if (lifecycleDataItem != null && lifecycleDataItem.Owner != ClaimsManager.GetCurrentUserId())
              {
                string formattedUserName = SecurityManager.GetFormattedUserName(lastModifiedBy);
                throw new NotSupportedException(string.Format(Res.Get<ContentLifecycleMessages>().ItemUnlockedWhileEditingBy, (object) formattedUserName));
              }
            }
          }
        }
        else
          instance = manager.CreateItem(type);
        return instance;
      }
      ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
      if (constructor != (ConstructorInfo) null && !flag1)
        return constructor.Invoke((object[]) null);
      surrogate = this.GetPropertyValueFromPath(surrogate, propertyPath);
      return surrogate;
    }

    private ILifecycleDataItem GetTempContentItem(
      Type type,
      IManager manager,
      Guid originalContentId,
      out Guid lastModifiedBy)
    {
      ILifecycleDataItem lifecycleDataItem;
      try
      {
        Content content = (Content) manager.GetItem(type, originalContentId);
        lastModifiedBy = content.LastModifiedBy;
        lifecycleDataItem = content as ILifecycleDataItem;
      }
      catch (ItemNotFoundException ex)
      {
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().ItemDeletedWhileEditing);
      }
      return manager.GetType().GetMethod("GetTemp", new Type[1]
      {
        type
      }).Invoke((object) manager, new object[1]
      {
        (object) lifecycleDataItem
      }) as ILifecycleDataItem;
    }

    private ILifecycleDataItem GetDynamicContentItem(
      Type type,
      IManager manager,
      Guid originalContentId,
      out Guid lastModifiedBy)
    {
      ILifecycleDataItem lifecycleDataItem;
      try
      {
        DynamicContent dynamicContent = (DynamicContent) manager.GetItem(type, originalContentId);
        lastModifiedBy = dynamicContent.LastModifiedBy;
        lifecycleDataItem = (ILifecycleDataItem) dynamicContent;
      }
      catch (ItemNotFoundException ex)
      {
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().ItemDeletedWhileEditing);
      }
      return ((DynamicModuleManager) manager).Lifecycle.GetType().GetMethod("GetTemp").Invoke((object) ((DynamicModuleManager) manager).Lifecycle, new object[2]
      {
        (object) lifecycleDataItem,
        null
      }) as ILifecycleDataItem;
    }

    private object GetPropertyValueFromPath(object source, string path)
    {
      if (string.IsNullOrEmpty(path) || source == null)
        return source;
      string name = path;
      int num = path.IndexOf('.');
      string path1;
      if (num != -1)
      {
        name = path.Substring(0, num);
        path1 = path.Substring(num);
      }
      else
        path1 = "";
      return this.GetPropertyValueFromPath(((!(source.GetType() == typeof (Type)) ? TypeDescriptor.GetProperties(source).Find(name, false) : TypeDescriptor.GetProperties(source.GetType()).Find(name, false)) ?? throw new InvalidOperationException(string.Format(Res.Get<ErrorMessages>().PropertyNotFound, (object) source.GetType(), (object) name))).GetValue(source), path1);
    }

    /// <summary>Determines if a type is persistent or not</summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private bool IsPersistent(Type type) => typeof (PersistenceCapable).IsAssignableFrom(type);

    /// <summary>
    /// Get identity information from a <paramref name="type" /> using information stored in
    /// <paramref name="surrogate" />
    /// </summary>
    /// <param name="type">Type to get identity from</param>
    /// <param name="surrogate">Data to extract information from</param>
    /// <returns>Identity</returns>
    public Guid GetIdentity(Type type, object surrogate, IManager manager) => typeof (IDataItem).IsAssignableFrom(type) ? (Guid) TypeDescriptor.GetProperties(surrogate).Find("Id", true).GetValue(surrogate) : this.GetOpenAccessIdentity(type, surrogate, manager);

    private Guid GetOpenAccessIdentity(Type type, object surrogate, IManager manager)
    {
      if (manager.Provider is IOpenAccessDataProvider provider)
      {
        MetaPersistentType metaPersistentType = provider.GetContext().Metadata.PersistentTypes.FirstOrDefault<MetaPersistentType>((Func<MetaPersistentType, bool>) (pt => pt.Name == type.Name));
        if (metaPersistentType != null)
        {
          MetaMember metaMember = metaPersistentType.Members.FirstOrDefault<MetaMember>((Func<MetaMember, bool>) (m => m is MetaPrimitiveMember && ((MetaPrimitiveMember) m).IsIdentity));
          if (metaMember != null)
            return (Guid) TypeDescriptor.GetProperties(surrogate).Find(metaMember.PropertyName, true).GetValue(surrogate);
        }
      }
      PersistentAttribute attribute1 = this.GetAttribute<PersistentAttribute>(type);
      string str = attribute1 != null ? attribute1.IdentityField : throw new NotSupportedException(string.Format("Persistent type {0} identity cannot be resolved.", (object) type.FullName));
      while (string.IsNullOrEmpty(str) && type != (Type) null)
      {
        type = type.BaseType;
        if (type != (Type) null)
          attribute1 = this.GetAttribute<PersistentAttribute>(type);
        if (attribute1 != null)
          str = attribute1.IdentityField;
      }
      if (!string.IsNullOrEmpty(attribute1.IdentityField))
      {
        PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties(type);
        PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties(surrogate);
        foreach (PropertyDescriptor propertyDescriptor1 in properties1)
        {
          if (propertyDescriptor1.Attributes[typeof (FieldAliasAttribute)] is FieldAliasAttribute attribute2 && attribute2.FieldName == str)
          {
            PropertyDescriptor propertyDescriptor2 = properties2[propertyDescriptor1.Name];
            if (propertyDescriptor2 != null)
            {
              object openAccessIdentity = propertyDescriptor2.GetValue(surrogate);
              if (openAccessIdentity != null && openAccessIdentity.GetType() == typeof (Guid))
                return (Guid) openAccessIdentity;
            }
          }
        }
        throw new NotSupportedException(string.Format(Res.Get<ErrorMessages>().CannotFindIdentityForType, (object) type));
      }
      throw new NotSupportedException(string.Format("Persistent type {0} identity cannot be resolved.", (object) type.FullName));
    }

    /// <summary>
    /// Get a surrogate for the requested <paramref name="dynamicFieldContainer" />.
    /// If <paramref name="dynamicFieldContainer" /> is not
    /// </summary>
    /// <param name="dynamicFieldContainer">Class instance that we want to generate a surrogate for</param>
    /// <returns>Surrogate</returns>
    public object GetSurrogate(IDynamicFieldsContainer dynamicFieldContainer)
    {
      Type type = dynamicFieldContainer != null ? this.GetSurrogateType(dynamicFieldContainer.GetType()) : throw new ArgumentNullException(nameof (dynamicFieldContainer));
      return !(type != dynamicFieldContainer.GetType()) ? (object) dynamicFieldContainer : Activator.CreateInstance(type);
    }

    /// <summary>
    /// Determines if the <paramref name="typeToCheck" /> is a surrogate or not
    /// </summary>
    /// <param name="typeToCheck">Type to check if it is surrogate (generated by this class) or not</param>
    /// <returns>Whether the <paramref name="typeToCheck" /> is a surrogate or not.</returns>
    /// <exception cref="T:System.ArgumentNullException">Checks <paramref name="typeToCheck" />.</exception>
    public bool IsSurrogate(Type typeToCheck)
    {
      if (typeToCheck == (Type) null)
        throw new ArgumentNullException(nameof (typeToCheck));
      Type attributeType = typeof (DynamicObjectSurrogateAttribute);
      return TypeDescriptor.GetAttributes(typeToCheck)[attributeType] != null;
    }

    /// <summary>
    /// Gets the type that caused <paramref name="surrogateType" />'s creation.
    /// </summary>
    /// <param name="surrogateType">type that was intended to replace the searched type</param>
    /// <returns>Type that is replaced ty <paramref name="surrogateType" /></returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="surrogateType" /> is null</exception>
    public Type GetOriginalType(Type surrogateType)
    {
      if (surrogateType == (Type) null)
        throw new ArgumentNullException(nameof (surrogateType));
      foreach (KeyValuePair<string, Type> keyValuePair in (IEnumerable<KeyValuePair<string, Type>>) this.GetCache())
      {
        if (keyValuePair.Value == surrogateType)
          return TypeResolutionService.ResolveType(keyValuePair.Key);
      }
      return (Type) null;
    }

    /// <summary>Try to get a type from the cache; otherwize</summary>
    /// <param name="source">Type of the dynamic fields container</param>
    /// <returns></returns>
    /// <remarks>Uses cache</remarks>
    public Type GetSurrogateType(Type sourceType)
    {
      if (sourceType == (Type) null)
        throw new ArgumentNullException("source");
      bool flag1 = typeof (IDynamicFieldsContainer).IsAssignableFrom(sourceType);
      bool flag2 = this.IsCustomSurrogate(sourceType);
      if (!(flag1 | flag2))
        throw new ArgumentException(!flag1 ? string.Format(Res.Get<ErrorMessages>().TypeDoesNotHaveAnAttribute, (object) sourceType, (object) typeof (SurrogateMetaDataAttribute)) : string.Format(Res.Get<ErrorMessages>().TypeDoesNotImplementAnotherType, (object) sourceType, (object) typeof (IDynamicFieldsContainer)));
      Type surrogateType;
      if (!this.cacheOfGeneratedTypes.TryGetValue(sourceType.FullName, out surrogateType))
      {
        lock (this.cacheOfGeneratedTypes)
        {
          if (!this.cacheOfGeneratedTypes.TryGetValue(sourceType.FullName, out surrogateType))
          {
            surrogateType = this.CreateSurrogateType(sourceType);
            this.cacheOfGeneratedTypes.Add(sourceType.FullName, surrogateType);
            this.CacheDataContract(sourceType);
          }
        }
      }
      return surrogateType;
    }

    /// <summary>Get the DAL-base type</summary>
    /// <param name="type">Type to get the base of</param>
    /// <returns>Type or null if not found</returns>
    public Type GetBaseType(Type type)
    {
      for (Type type1 = type; !(type1 == (Type) null) && !(type1 == typeof (object)); type1 = type1.BaseType)
      {
        if (this.IsBaseType(type1))
          return type1;
      }
      return (Type) null;
    }

    /// <summary>Checks if a type is the base type of a DAL-heirarchy</summary>
    /// <param name="type">type to be tested</param>
    /// <returns>Boolean indicating if the passed <paramref name="type" /> is base or not.</returns>
    public bool IsBaseType(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      return this.GetAttribute<PersistentAttribute>(type) != null;
    }

    /// <summary>Gets the default property filter</summary>
    public PropertyFilterDelegate DefaultPropertyFilter => this.InheritFromSourceType ? new PropertyFilterDelegate(this.DynamicFieldsPropertyFilter) : new PropertyFilterDelegate(this.DynamicFieldsAndDataMemberPropertiesPropertyFilter);

    /// <summary>
    /// Performs a check against a CLR type to see if it should be surrogated or not.
    /// </summary>
    /// <param name="typeToInspect">CLR type to perform a check against</param>
    /// <returns>True if <paramref name="typeToInspect" /> should be surrogated, false it should not be.</returns>
    public bool ShouldCreateSurrogateForType(Type typeToInspect) => typeof (IDynamicFieldsContainer).IsAssignableFrom(typeToInspect) && typeof (IDataItem).IsAssignableFrom(typeToInspect) && this.GetAttribute<DataContractAttribute>(typeToInspect) != null || this.IsCustomSurrogate(typeToInspect);

    /// <summary>Get an attribute from a type</summary>
    /// <typeparam name="TAttr">Type of the attribute to get</typeparam>
    /// <param name="type">type to extract attribute from</param>
    /// <returns>Attribute instance or if not found null.</returns>
    private TAttr GetAttribute<TAttr>(Type type) where TAttr : Attribute => TypeDescriptor.GetAttributes(type)[typeof (TAttr)] as TAttr;

    private void SystemManager_ShuttingDown(object sender, CancelEventArgs e) => this.InvalidateWholeCache();

    private void SystemManager_ModelReset(object sender, EventArgs e) => this.InvalidateWholeCache();

    private bool IsCustomSurrogate(Type sourceType) => sourceType.GetCustomAttributes(true).OfType<SurrogateMetaDataAttribute>().FirstOrDefault<SurrogateMetaDataAttribute>() != null;

    private ICustomSurrogateDescriptor GetSurrogateMetaData(
      Type sourceType)
    {
      SurrogateMetaDataAttribute metaDataAttr = sourceType.GetCustomAttributes(true).OfType<SurrogateMetaDataAttribute>().FirstOrDefault<SurrogateMetaDataAttribute>();
      if (metaDataAttr != null)
      {
        MethodInfo methodInfo = ((IEnumerable<MethodInfo>) metaDataAttr.MetaObjectContainer.GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>) (x => x.Name == metaDataAttr.MethodName));
        if (methodInfo != (MethodInfo) null)
          return methodInfo.Invoke((object) null, (object[]) null) as ICustomSurrogateDescriptor;
      }
      return (ICustomSurrogateDescriptor) null;
    }

    /// <summary>
    /// Generate the dynamic assembly and module that will host our dynamically-generated assemblies
    /// </summary>
    private void CreateDynamicAssembly()
    {
      this.ClearCache();
      if ((Assembly) this.assemblyBuilder != (Assembly) null)
      {
        this.assemblyBuilder = (AssemblyBuilder) null;
        this.moduleBuilder = (ModuleBuilder) null;
        GC.Collect();
      }
      this.assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName()
      {
        Name = "Telerik.Sitefinity.DynamicTypeSurrogates",
        Version = new Version(1, 0, 0, 0)
      }, AssemblyBuilderAccess.Run);
      this.moduleBuilder = this.assemblyBuilder.DefineDynamicModule("Telerik.Sitefinity.DynamicTypeSurrogates");
    }

    /// <summary>Create the surrogate type</summary>
    /// <param name="sourceType">Type to create a surrogate for</param>
    /// <returns>Surrogate type, if <paramref name="sourceType" />
    /// has dynamic fields, or <paramref name="sourceType" /> otherwise.</returns>
    private Type CreateSurrogateType(Type sourceType)
    {
      ICustomSurrogateDescriptor surrogateMetaData = this.GetSurrogateMetaData(sourceType);
      PropertyDescriptorCollection descriptorCollection = surrogateMetaData == null || surrogateMetaData.Properties == null ? this.PropertyFilter(sourceType) : surrogateMetaData.Properties;
      if (descriptorCollection.Count == 0)
        return sourceType;
      if ((Module) this.moduleBuilder == (Module) null)
        this.CreateDynamicAssembly();
      string name = sourceType.FullName + "Surrogate";
      TypeBuilder builder = this.InheritFromSourceType || surrogateMetaData != null && surrogateMetaData.InheritsFromSourceType ? this.moduleBuilder.DefineType(name, TypeAttributes.Public, sourceType) : this.moduleBuilder.DefineType(name, TypeAttributes.Public);
      this.AddDataContractAttribute(builder, sourceType);
      this.AddCustomAttribute(builder);
      this.AddTypeDescriptionProviderAttribute(builder);
      foreach (PropertyDescriptor sourceProperty in descriptorCollection)
        this.CreateProperty(builder, sourceProperty);
      return builder.CreateType();
    }

    /// <summary>
    /// Adds custom type description provider attribute, so that we are sure
    /// that reflection is used with surrogates.
    /// </summary>
    /// <param name="builder">Builder that is used to generate the surrogate typ</param>
    private void AddTypeDescriptionProviderAttribute(TypeBuilder builder)
    {
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(typeof (TypeDescriptionProviderAttribute).GetConstructor(new Type[1]
      {
        typeof (Type)
      }), (object[]) new Type[1]
      {
        typeof (SurrogateDescriptionProvider)
      });
      builder.SetCustomAttribute(customBuilder);
    }

    /// <summary>Adds a custom attribute to mark the generated classes</summary>
    /// <param name="builder">Builder that is used to generate the surrogate type</param>
    private void AddCustomAttribute(TypeBuilder builder)
    {
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(typeof (DynamicObjectSurrogateAttribute).GetConstructor(Type.EmptyTypes), new object[0]);
      builder.SetCustomAttribute(customBuilder);
    }

    /// <summary>Add the DataContractAttribute to the generated type</summary>
    /// <param name="builder">Builder that is used to generate the surrogate type</param>
    /// <param name="sourceType">Type that is being copied.</param>
    private void AddDataContractAttribute(TypeBuilder builder, Type sourceType)
    {
      Type type = typeof (DataContractAttribute);
      DataContractAttribute contractAttribute = this.IsBaseType(sourceType) || this.IsCustomSurrogate(sourceType) ? this.GetAttribute<DataContractAttribute>(sourceType) : this.GetAttribute<DataContractAttribute>(this.GetBaseType(sourceType));
      ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
      if (contractAttribute == null)
        throw new NotSupportedException(string.Format(Res.Get<ErrorMessages>().TypeDoesNotHaveAnAttribute, (object) sourceType, (object) type));
      PropertyInfo[] namedProperties = new PropertyInfo[3]
      {
        type.GetProperty("Name"),
        type.GetProperty("Namespace"),
        type.GetProperty("IsReference")
      };
      object[] propertyValues = new object[3]
      {
        !string.IsNullOrEmpty(contractAttribute.Name) ? (object) contractAttribute.Name : (object) sourceType.Name,
        !string.IsNullOrEmpty(contractAttribute.Namespace) ? (object) sourceType.Namespace : (object) sourceType.Namespace,
        (object) contractAttribute.IsReference
      };
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(constructor, new object[0], namedProperties, propertyValues);
      builder.SetCustomAttribute(customBuilder);
    }

    /// <summary>This property filter leaves only meta properties</summary>
    /// <param name="type">Type to apply the filter to</param>
    /// <returns>Filtered properties by meta property</returns>
    public PropertyDescriptorCollection DynamicFieldsPropertyFilter(
      Type type)
    {
      if (!this.ShouldCreateSurrogateForType(type))
        return new PropertyDescriptorCollection(new PropertyDescriptor[0], true);
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
      foreach (PropertyDescriptor propertyDescriptor in properties)
      {
        switch (propertyDescriptor)
        {
          case MetafieldPropertyDescriptor _:
          case TaxonomyPropertyDescriptor _:
          case LstringPropertyDescriptor _:
          case ContentLinksPropertyDescriptor _:
            propertyDescriptorList.Add(propertyDescriptor);
            continue;
          default:
            continue;
        }
      }
      return new PropertyDescriptorCollection(propertyDescriptorList.ToArray(), true);
    }

    /// <summary>
    /// This property filter returns only propertie that have the DataMember attribute applied to them,
    /// plus all dynamic properties
    /// </summary>
    /// <param name="type">Type to get the list of properties to filter from.</param>
    /// <returns>Dynamic fields + properties marked with DataMemberAttribute</returns>
    public PropertyDescriptorCollection DynamicFieldsAndDataMemberPropertiesPropertyFilter(
      Type type)
    {
      if (!this.ShouldCreateSurrogateForType(type))
        return new PropertyDescriptorCollection(new PropertyDescriptor[0], true);
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
      Type attributeType = typeof (DataMemberAttribute);
      foreach (PropertyDescriptor propertyDescriptor in properties)
      {
        int num;
        switch (propertyDescriptor)
        {
          case MetafieldPropertyDescriptor _:
          case TaxonomyPropertyDescriptor _:
          case LstringPropertyDescriptor _:
            num = 1;
            break;
          default:
            num = propertyDescriptor is ContentLinksPropertyDescriptor ? 1 : 0;
            break;
        }
        bool flag = num != 0;
        if (propertyDescriptor.Attributes[attributeType] != null | flag)
          propertyDescriptorList.Add(propertyDescriptor);
      }
      return new PropertyDescriptorCollection(propertyDescriptorList.ToArray(), true);
    }

    /// <summary>
    /// Create a property in the type being built with <paramref name="builder" />, a backing field
    /// to hold its value
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="sourceProperty"></param>
    /// <returns></returns>
    private PropertyBuilder CreateProperty(
      TypeBuilder builder,
      PropertyDescriptor sourceProperty)
    {
      Type type = sourceProperty.PropertyType;
      string name = sourceProperty.Name;
      if (type == typeof (IList<Guid>))
        type = typeof (List<Guid>);
      PropertyBuilder propertyBuilder = builder.DefineProperty(name, PropertyAttributes.HasDefault, type, (Type[]) null);
      FieldBuilder field = this.CreateField(builder, type, name);
      MethodBuilder propertyGetter = this.CreatePropertyGetter(builder, field, type, name);
      propertyBuilder.SetGetMethod(propertyGetter);
      MethodBuilder propertySetter = this.CreatePropertySetter(builder, field, type, name);
      propertyBuilder.SetSetMethod(propertySetter);
      this.AddDataMemberAttribute(propertyBuilder);
      return propertyBuilder;
    }

    /// <summary>Add a DataMemberAttribute to a property</summary>
    /// <param name="propertyBuilder">property builder to add the DataMemberAttribute to</param>
    private void AddDataMemberAttribute(PropertyBuilder propertyBuilder)
    {
      CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(typeof (DataMemberAttribute).GetConstructor(Type.EmptyTypes), new object[0]);
      propertyBuilder.SetCustomAttribute(customBuilder);
    }

    private FieldBuilder CreateField(
      TypeBuilder builder,
      Type propertyType,
      string propertyName)
    {
      string fieldName = "_field_" + propertyName;
      return builder.DefineField(fieldName, propertyType, FieldAttributes.Private);
    }

    /// <summary>Creates the property getter.</summary>
    /// <param name="builder">The builder.</param>
    /// <param name="fieldBuilder">The field builder.</param>
    /// <param name="propertyType">Type of the property.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    private MethodBuilder CreatePropertyGetter(
      TypeBuilder builder,
      FieldBuilder fieldBuilder,
      Type propertyType,
      string propertyName)
    {
      string name = "get_" + propertyName;
      MethodBuilder propertyGetter = builder.DefineMethod(name, MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, propertyType, Type.EmptyTypes);
      ILGenerator ilGenerator = propertyGetter.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fieldBuilder);
      ilGenerator.Emit(OpCodes.Ret);
      return propertyGetter;
    }

    /// <summary>Creates the property setter.</summary>
    /// <param name="builder">The builder.</param>
    /// <param name="fieldBuilder">The field builder.</param>
    /// <param name="propertyType">Type of the property.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    private MethodBuilder CreatePropertySetter(
      TypeBuilder builder,
      FieldBuilder fieldBuilder,
      Type propertyType,
      string propertyName)
    {
      string name = "set_" + propertyName;
      MethodBuilder propertySetter = builder.DefineMethod(name, MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, (Type) null, new Type[1]
      {
        propertyType
      });
      ILGenerator ilGenerator = propertySetter.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Ldarg_1);
      ilGenerator.Emit(OpCodes.Stfld, (FieldInfo) fieldBuilder);
      ilGenerator.Emit(OpCodes.Ret);
      return propertySetter;
    }

    /// <summary>Invalidates the whole cache</summary>
    private void InvalidateWholeCache()
    {
      lock (this.cacheOfGeneratedTypes)
      {
        this.cacheOfContracts.Clear();
        this.cacheOfGeneratedTypes.Clear();
        this.CreateDynamicAssembly();
      }
    }

    /// <summary>Tries to get a type from the cache of generated types</summary>
    /// <param name="source">Source type that triggered the generation of the surrogate type we
    /// are requesting</param>
    /// <returns>Cached surrogate type</returns>
    private Type GetCachedType(Type source)
    {
      Type type;
      return !this.cacheOfGeneratedTypes.TryGetValue(source.FullName, out type) ? (Type) null : type;
    }

    /// <summary>Clears the cache of generated types</summary>
    protected void ClearCache()
    {
      lock (this.cacheOfGeneratedTypes)
        this.cacheOfGeneratedTypes.Clear();
    }

    /// <summary>Gets the whole cache</summary>
    /// <returns>Cache</returns>
    protected virtual IDictionary<string, Type> GetCache() => this.cacheOfGeneratedTypes;

    /// <summary>Get the surrogate type by WCF name and namespace</summary>
    /// <param name="wcfName">Name of the class under WCF</param>
    /// <param name="wcfNamespace">Name of the namespace under WCF</param>
    /// <returns></returns>
    public virtual Type GetSurrogate(string wcfName, string wcfNamespace)
    {
      Type source = this.cacheOfContracts.Where<KeyValuePair<Type, DataContractAttribute>>((Func<KeyValuePair<Type, DataContractAttribute>, bool>) (w => w.Value.Name + "Surrogated" == wcfName && w.Value.Namespace == wcfNamespace)).Select<KeyValuePair<Type, DataContractAttribute>, Type>((Func<KeyValuePair<Type, DataContractAttribute>, Type>) (w => w.Key)).FirstOrDefault<Type>();
      return !(source != (Type) null) ? (Type) null : this.GetCachedType(source);
    }

    /// <summary>Get a custom attribute</summary>
    /// <typeparam name="TAttribute">Type of attribute inheriting Attribute</typeparam>
    /// <param name="type">Type from which to extract the attribute</param>
    /// <returns></returns>
    private TAttribute GetCustomAttribute<TAttribute>(Type type) where TAttribute : Attribute => TypeDescriptor.GetAttributes(type)[typeof (TAttribute)] as TAttribute;

    /// <summary>Cache an object that has DataContractAttribute</summary>
    /// <param name="sourceType">Cached type</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="sourceType" /> is null.</exception>
    /// <exception cref="T:System.NotSupportedException">When <paramref name="sourceType" /> is not marked with the DataContractAttribute</exception>
    protected virtual void CacheDataContract(Type sourceType)
    {
      if (sourceType == (Type) null)
        throw new ArgumentNullException(nameof (sourceType));
      if (this.cacheOfContracts.ContainsKey(sourceType))
        return;
      lock (this.cacheOfContracts)
      {
        if (this.cacheOfContracts.ContainsKey(sourceType))
          return;
        this.cacheOfContracts.Add(sourceType, this.GetCustomAttribute<DataContractAttribute>(sourceType) ?? throw new NotSupportedException(string.Format(Res.Get<ErrorMessages>().TypeDoesNotHaveAnAttribute, (object) sourceType, (object) typeof (DataContractAttribute))));
      }
    }
  }
}
