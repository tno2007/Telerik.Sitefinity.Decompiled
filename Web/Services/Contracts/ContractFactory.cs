// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.ContractFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class ContractFactory
  {
    private static Lazy<ContractFactory> factory = new Lazy<ContractFactory>((Func<ContractFactory>) (() => new ContractFactory()));

    private ContractFactory()
    {
    }

    internal static ContractFactory Instance => ContractFactory.factory.Value;

    internal ITypeSettings Create(Type clrType, string urlKey)
    {
      if (!this.IsSupportedType(clrType))
        return (ITypeSettings) null;
      ITypeSettings contract;
      if (typeof (ILifecycleDataItem).IsAssignableFrom(clrType))
      {
        LifecycleTypeSettingsProxy typeSettingsProxy1 = new LifecycleTypeSettingsProxy();
        typeSettingsProxy1.ClrType = clrType.FullName;
        typeSettingsProxy1.UrlName = urlKey;
        LifecycleTypeSettingsProxy typeSettingsProxy2 = typeSettingsProxy1;
        typeSettingsProxy2.Status = !typeof (ILifecycleDataItemDraft).IsAssignableFrom(clrType) ? LifecycleStatus.Live : LifecycleStatus.Master;
        contract = (ITypeSettings) typeSettingsProxy2;
      }
      else
        contract = (ITypeSettings) new TypeSettingsProxy()
        {
          ClrType = clrType.FullName,
          UrlName = urlKey
        };
      this.AddDefaultFields(clrType, contract);
      ISet<string> ignoredProperties = this.AddCustomFields(clrType, contract);
      this.AddComments(clrType, contract);
      IEnumerable<ContractFactory.FieldsFilter> fieldFilters = this.GetFieldFilters(clrType);
      this.AddOwnFields(clrType, contract, ignoredProperties, fieldFilters);
      return contract;
    }

    private void AddComments(Type clrType, ITypeSettings contract)
    {
      if (!CommentsUtilities.CommentsEnabled(clrType.FullName))
        return;
      CalculatedPropertyMappingProxy propertyMappingProxy1 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy1.Name = "Comments";
      propertyMappingProxy1.ResolverType = "Telerik.Sitefinity.Services.Comments.CommentProperty";
      propertyMappingProxy1.SelectedByDefault = false;
      CalculatedPropertyMappingProxy propertyMappingProxy2 = propertyMappingProxy1;
      if (typeof (Content).IsAssignableFrom(clrType))
      {
        PersistentPropertyMappingProxy propertyMappingProxy3 = new PersistentPropertyMappingProxy();
        propertyMappingProxy3.Name = LinqHelper.MemberName<Content>((Expression<Func<Content, object>>) (x => (object) x.AllowComments));
        propertyMappingProxy3.AllowFilter = false;
        propertyMappingProxy3.AllowSort = false;
        PersistentPropertyMappingProxy propertyMappingProxy4 = propertyMappingProxy3;
        contract.Properties.Add((IPropertyMapping) propertyMappingProxy4);
      }
      if (contract.Properties.Contains((IPropertyMapping) propertyMappingProxy2))
        return;
      contract.Properties.Add((IPropertyMapping) propertyMappingProxy2);
    }

    private void AddOwnFields(
      Type clrType,
      ITypeSettings contract,
      ISet<string> ignoredProperties,
      IEnumerable<ContractFactory.FieldsFilter> filters)
    {
      IEnumerable<PropertyDescriptor> propertyDescriptors = TypeDescriptor.GetProperties(clrType).OfType<PropertyDescriptor>().Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => object.Equals((object) x.ComponentType, (object) clrType) && x.Attributes.OfType<DataMemberAttribute>().Any<DataMemberAttribute>() && !x.Attributes.OfType<IgnoreDataMemberAttribute>().Any<IgnoreDataMemberAttribute>() && !x.Attributes.OfType<ObsoleteAttribute>().Any<ObsoleteAttribute>() && !x.Attributes.OfType<NonSerializableProperty>().Any<NonSerializableProperty>()));
      foreach (ContractFactory.FieldsFilter filter in filters)
      {
        if (filter.ShouldFilter())
          propertyDescriptors = filter.Apply(propertyDescriptors);
      }
      foreach (PropertyDescriptor propertyDescriptor in propertyDescriptors)
      {
        PropertyDescriptor entry = propertyDescriptor;
        if (!contract.Properties.Any<IPropertyMapping>((Func<IPropertyMapping, bool>) (x => x.Name == entry.Name)) && !ignoredProperties.Contains(entry.Name))
        {
          IPropertyMapping propertyMapping = (IPropertyMapping) null;
          if (entry.Attributes.OfType<AssociationAttribute>().Any<AssociationAttribute>())
          {
            NavigationPropertyMappingProxy propertyMappingProxy = new NavigationPropertyMappingProxy();
            propertyMappingProxy.Name = entry.Name;
            propertyMappingProxy.ResolverType = typeof (PersistentNavigationResolver).FullName;
          }
          else
          {
            bool flag1 = false;
            ReadOnlyAttribute readOnlyAttribute = entry.Attributes.OfType<ReadOnlyAttribute>().FirstOrDefault<ReadOnlyAttribute>();
            if (readOnlyAttribute != null)
              flag1 = readOnlyAttribute.IsReadOnly;
            bool flag2 = typeof (IComparable).IsAssignableFrom(entry.PropertyType);
            PersistentPropertyMappingProxy propertyMappingProxy = new PersistentPropertyMappingProxy();
            propertyMappingProxy.Name = entry.Name;
            propertyMappingProxy.ReadOnly = flag1;
            propertyMappingProxy.AllowSort = flag2;
            propertyMapping = (IPropertyMapping) propertyMappingProxy;
          }
          contract.Properties.Add(propertyMapping);
        }
      }
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    private void AddDefaultFields(Type clrType, ITypeSettings contract)
    {
      List<IPropertyMapping> toAdd = new List<IPropertyMapping>();
      if (typeof (IDataItem).IsAssignableFrom(clrType))
      {
        List<IPropertyMapping> propertyMappingList1 = toAdd;
        PersistentPropertyMappingProxy propertyMappingProxy1 = new PersistentPropertyMappingProxy();
        propertyMappingProxy1.Name = LinqHelper.MemberName<IDataItem>((Expression<Func<IDataItem, object>>) (x => (object) x.Id));
        propertyMappingProxy1.ReadOnly = true;
        propertyMappingProxy1.IsKey = true;
        propertyMappingProxy1.AllowFilter = true;
        propertyMappingProxy1.AllowSort = true;
        PersistentPropertyMappingProxy propertyMappingProxy2 = propertyMappingProxy1;
        propertyMappingList1.Add((IPropertyMapping) propertyMappingProxy2);
        PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(clrType).Find("LastModified", false);
        if (propertyDescriptor != null && !propertyDescriptor.Attributes.OfType<IgnoreDataMemberAttribute>().Any<IgnoreDataMemberAttribute>())
        {
          List<IPropertyMapping> propertyMappingList2 = toAdd;
          PersistentPropertyMappingProxy propertyMappingProxy3 = new PersistentPropertyMappingProxy();
          propertyMappingProxy3.Name = LinqHelper.MemberName<IDataItem>((Expression<Func<IDataItem, object>>) (x => (object) x.LastModified));
          propertyMappingProxy3.ReadOnly = true;
          PersistentPropertyMappingProxy propertyMappingProxy4 = propertyMappingProxy3;
          propertyMappingList2.Add((IPropertyMapping) propertyMappingProxy4);
        }
        List<IPropertyMapping> propertyMappingList3 = toAdd;
        CalculatedPropertyMappingProxy propertyMappingProxy5 = new CalculatedPropertyMappingProxy();
        propertyMappingProxy5.Name = "Provider";
        propertyMappingProxy5.ResolverType = typeof (ProviderNameProperty).FullName;
        propertyMappingProxy5.SelectedByDefault = false;
        propertyMappingList3.Add((IPropertyMapping) propertyMappingProxy5);
      }
      if (typeof (IScheduleable).IsAssignableFrom(clrType))
      {
        List<IPropertyMapping> propertyMappingList = toAdd;
        PersistentPropertyMappingProxy[] collection = new PersistentPropertyMappingProxy[1];
        PersistentPropertyMappingProxy propertyMappingProxy = new PersistentPropertyMappingProxy();
        propertyMappingProxy.Name = LinqHelper.MemberName<IScheduleable>((Expression<Func<IScheduleable, object>>) (x => (object) x.PublicationDate));
        collection[0] = propertyMappingProxy;
        propertyMappingList.AddRange((IEnumerable<IPropertyMapping>) collection);
      }
      if (typeof (IContent).IsAssignableFrom(clrType))
      {
        List<IPropertyMapping> propertyMappingList = toAdd;
        PersistentPropertyMappingProxy[] collection = new PersistentPropertyMappingProxy[4];
        PersistentPropertyMappingProxy propertyMappingProxy6 = new PersistentPropertyMappingProxy();
        propertyMappingProxy6.Name = LinqHelper.MemberName<IContent>((Expression<Func<IContent, object>>) (x => x.Title));
        collection[0] = propertyMappingProxy6;
        PersistentPropertyMappingProxy propertyMappingProxy7 = new PersistentPropertyMappingProxy();
        propertyMappingProxy7.Name = LinqHelper.MemberName<IContent>((Expression<Func<IContent, object>>) (x => x.Description));
        collection[1] = propertyMappingProxy7;
        PersistentPropertyMappingProxy propertyMappingProxy8 = new PersistentPropertyMappingProxy();
        propertyMappingProxy8.Name = LinqHelper.MemberName<IContent>((Expression<Func<IContent, object>>) (x => (object) x.DateCreated));
        propertyMappingProxy8.ReadOnly = true;
        collection[2] = propertyMappingProxy8;
        PersistentPropertyMappingProxy propertyMappingProxy9 = new PersistentPropertyMappingProxy();
        propertyMappingProxy9.Name = LinqHelper.MemberName<Content>((Expression<Func<Content, object>>) (x => (object) x.IncludeInSitemap));
        propertyMappingProxy9.AllowFilter = false;
        propertyMappingProxy9.AllowSort = false;
        collection[3] = propertyMappingProxy9;
        propertyMappingList.AddRange((IEnumerable<IPropertyMapping>) collection);
      }
      if (typeof (DynamicContent).IsAssignableFrom(clrType))
      {
        List<IPropertyMapping> propertyMappingList = toAdd;
        PersistentPropertyMappingProxy[] collection = new PersistentPropertyMappingProxy[2];
        PersistentPropertyMappingProxy propertyMappingProxy10 = new PersistentPropertyMappingProxy();
        propertyMappingProxy10.Name = LinqHelper.MemberName<DynamicContent>((Expression<Func<DynamicContent, object>>) (x => (object) x.DateCreated));
        propertyMappingProxy10.ReadOnly = true;
        collection[0] = propertyMappingProxy10;
        PersistentPropertyMappingProxy propertyMappingProxy11 = new PersistentPropertyMappingProxy();
        propertyMappingProxy11.Name = LinqHelper.MemberName<DynamicContent>((Expression<Func<DynamicContent, object>>) (x => (object) x.IncludeInSitemap));
        propertyMappingProxy11.AllowFilter = false;
        propertyMappingProxy11.AllowSort = false;
        collection[1] = propertyMappingProxy11;
        propertyMappingList.AddRange((IEnumerable<IPropertyMapping>) collection);
      }
      if (typeof (ITaxon).IsAssignableFrom(clrType))
      {
        List<IPropertyMapping> propertyMappingList4 = toAdd;
        PersistentPropertyMappingProxy[] collection = new PersistentPropertyMappingProxy[6];
        PersistentPropertyMappingProxy propertyMappingProxy12 = new PersistentPropertyMappingProxy();
        propertyMappingProxy12.Name = LinqHelper.MemberName<ITaxon>((Expression<Func<ITaxon, object>>) (x => x.Title));
        collection[0] = propertyMappingProxy12;
        PersistentPropertyMappingProxy propertyMappingProxy13 = new PersistentPropertyMappingProxy();
        propertyMappingProxy13.Name = LinqHelper.MemberName<ITaxon>((Expression<Func<ITaxon, object>>) (x => x.Description));
        collection[1] = propertyMappingProxy13;
        PersistentPropertyMappingProxy propertyMappingProxy14 = new PersistentPropertyMappingProxy();
        propertyMappingProxy14.Name = LinqHelper.MemberName<ITaxon>((Expression<Func<ITaxon, object>>) (x => x.UrlName));
        collection[2] = propertyMappingProxy14;
        PersistentPropertyMappingProxy propertyMappingProxy15 = new PersistentPropertyMappingProxy();
        propertyMappingProxy15.Name = LinqHelper.MemberName<ITaxon>((Expression<Func<ITaxon, object>>) (x => x.Name));
        collection[3] = propertyMappingProxy15;
        PersistentPropertyMappingProxy propertyMappingProxy16 = new PersistentPropertyMappingProxy();
        propertyMappingProxy16.Name = LinqHelper.MemberName<Taxon>((Expression<Func<Taxon, object>>) (x => x.Synonyms));
        propertyMappingProxy16.AllowSort = false;
        propertyMappingProxy16.AllowFilter = false;
        collection[4] = propertyMappingProxy16;
        PersistentPropertyMappingProxy propertyMappingProxy17 = new PersistentPropertyMappingProxy();
        propertyMappingProxy17.Name = LinqHelper.MemberName<Taxon>((Expression<Func<Taxon, object>>) (x => (object) x.TaxonomyId));
        propertyMappingProxy17.AllowFilter = true;
        collection[5] = propertyMappingProxy17;
        propertyMappingList4.AddRange((IEnumerable<IPropertyMapping>) collection);
        List<IPropertyMapping> propertyMappingList5 = toAdd;
        CalculatedPropertyMappingProxy propertyMappingProxy18 = new CalculatedPropertyMappingProxy();
        propertyMappingProxy18.Name = "AppliedTo";
        propertyMappingProxy18.ResolverType = typeof (AppliedToProperty).FullName;
        propertyMappingProxy18.SelectedByDefault = false;
        propertyMappingList5.Add((IPropertyMapping) propertyMappingProxy18);
      }
      if (typeof (IOrderedItem).IsAssignableFrom(clrType))
      {
        List<IPropertyMapping> propertyMappingList = toAdd;
        PersistentPropertyMappingProxy[] collection = new PersistentPropertyMappingProxy[1];
        PersistentPropertyMappingProxy propertyMappingProxy = new PersistentPropertyMappingProxy();
        propertyMappingProxy.Name = LinqHelper.MemberName<IOrderedItem>((Expression<Func<IOrderedItem, object>>) (x => (object) x.Ordinal));
        collection[0] = propertyMappingProxy;
        propertyMappingList.AddRange((IEnumerable<IPropertyMapping>) collection);
      }
      if (typeof (IHasUrlName).IsAssignableFrom(clrType))
      {
        List<IPropertyMapping> propertyMappingList = toAdd;
        PersistentPropertyMappingProxy propertyMappingProxy19 = new PersistentPropertyMappingProxy();
        propertyMappingProxy19.Name = LinqHelper.MemberName<IHasUrlName>((Expression<Func<IHasUrlName, object>>) (x => x.UrlName));
        PersistentPropertyMappingProxy propertyMappingProxy20 = propertyMappingProxy19;
        propertyMappingList.Add((IPropertyMapping) propertyMappingProxy20);
      }
      if (typeof (MediaContent).IsAssignableFrom(clrType))
      {
        CalculatedPropertyMappingProxy propertyMappingProxy21 = new CalculatedPropertyMappingProxy();
        propertyMappingProxy21.Name = "Url";
        propertyMappingProxy21.ResolverType = typeof (MediaUrlProperty).FullName;
        CalculatedPropertyMappingProxy propertyMappingProxy22 = propertyMappingProxy21;
        toAdd.Add((IPropertyMapping) propertyMappingProxy22);
        if (typeof (Video).IsAssignableFrom(clrType))
        {
          List<IPropertyMapping> propertyMappingList = toAdd;
          PersistentPropertyMappingProxy propertyMappingProxy23 = new PersistentPropertyMappingProxy();
          propertyMappingProxy23.Name = "ThumbnailUrl";
          propertyMappingList.Add((IPropertyMapping) propertyMappingProxy23);
        }
        else
        {
          CalculatedPropertyMappingProxy propertyMappingProxy24 = new CalculatedPropertyMappingProxy();
          propertyMappingProxy24.Name = "ThumbnailUrl";
          propertyMappingProxy24.ResolverType = typeof (MediaUrlProperty).FullName;
          CalculatedPropertyMappingProxy propertyMappingProxy25 = propertyMappingProxy24;
          propertyMappingProxy25.Parameters.Add("thumbnail", "thumbnail");
          toAdd.Add((IPropertyMapping) propertyMappingProxy25);
        }
        List<IPropertyMapping> propertyMappingList6 = toAdd;
        PersistentPropertyMappingProxy propertyMappingProxy26 = new PersistentPropertyMappingProxy();
        propertyMappingProxy26.Name = LinqHelper.MemberName<MediaContent>((Expression<Func<MediaContent, object>>) (x => x.Author));
        PersistentPropertyMappingProxy propertyMappingProxy27 = propertyMappingProxy26;
        propertyMappingList6.Add((IPropertyMapping) propertyMappingProxy27);
        List<IPropertyMapping> propertyMappingList7 = toAdd;
        PersistentPropertyMappingProxy propertyMappingProxy28 = new PersistentPropertyMappingProxy();
        propertyMappingProxy28.Name = LinqHelper.MemberName<MediaContent>((Expression<Func<MediaContent, object>>) (x => x.Extension));
        PersistentPropertyMappingProxy propertyMappingProxy29 = propertyMappingProxy28;
        propertyMappingList7.Add((IPropertyMapping) propertyMappingProxy29);
        List<IPropertyMapping> propertyMappingList8 = toAdd;
        PersistentPropertyMappingProxy propertyMappingProxy30 = new PersistentPropertyMappingProxy();
        propertyMappingProxy30.Name = LinqHelper.MemberName<MediaContent>((Expression<Func<MediaContent, object>>) (x => x.MimeType));
        PersistentPropertyMappingProxy propertyMappingProxy31 = propertyMappingProxy30;
        propertyMappingList8.Add((IPropertyMapping) propertyMappingProxy31);
        List<IPropertyMapping> propertyMappingList9 = toAdd;
        PersistentPropertyMappingProxy propertyMappingProxy32 = new PersistentPropertyMappingProxy();
        propertyMappingProxy32.Name = LinqHelper.MemberName<MediaContent>((Expression<Func<MediaContent, object>>) (x => (object) x.TotalSize));
        PersistentPropertyMappingProxy propertyMappingProxy33 = propertyMappingProxy32;
        propertyMappingList9.Add((IPropertyMapping) propertyMappingProxy33);
      }
      if (typeof (ILibrary).IsAssignableFrom(clrType))
      {
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(typeof (ILibrary)))
        {
          PersistentPropertyMappingProxy propertyMappingProxy34 = new PersistentPropertyMappingProxy();
          propertyMappingProxy34.Name = property.Name;
          PersistentPropertyMappingProxy propertyMappingProxy35 = propertyMappingProxy34;
          toAdd.Add((IPropertyMapping) propertyMappingProxy35);
        }
        List<IPropertyMapping> propertyMappingList = toAdd;
        PersistentPropertyMappingProxy propertyMappingProxy36 = new PersistentPropertyMappingProxy();
        propertyMappingProxy36.Name = LinqHelper.MemberName<Library>((Expression<Func<Library, object>>) (x => x.ThumbnailProfiles));
        propertyMappingProxy36.AllowFilter = false;
        propertyMappingProxy36.AllowSort = false;
        propertyMappingProxy36.SelectedByDefault = false;
        PersistentPropertyMappingProxy propertyMappingProxy37 = propertyMappingProxy36;
        propertyMappingList.Add((IPropertyMapping) propertyMappingProxy37);
      }
      if (typeof (IFolder).IsAssignableFrom(clrType))
      {
        List<IPropertyMapping> propertyMappingList = toAdd;
        CalculatedPropertyMappingProxy propertyMappingProxy = new CalculatedPropertyMappingProxy();
        propertyMappingProxy.Name = "ChildrenCount";
        propertyMappingProxy.ResolverType = typeof (ChildrenCountProperty).FullName;
        propertyMappingList.Add((IPropertyMapping) propertyMappingProxy);
      }
      foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(clrType).OfType<PropertyDescriptor>().Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => !toAdd.Any<IPropertyMapping>((Func<IPropertyMapping, bool>) (y => y.Name == x.Name)))).Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.Attributes.OfType<KeyAttribute>().Any<KeyAttribute>())))
      {
        List<IPropertyMapping> propertyMappingList = toAdd;
        PersistentPropertyMappingProxy propertyMappingProxy = new PersistentPropertyMappingProxy();
        propertyMappingProxy.Name = propertyDescriptor.Name;
        propertyMappingProxy.ReadOnly = true;
        propertyMappingProxy.IsKey = true;
        propertyMappingProxy.AllowFilter = true;
        propertyMappingProxy.AllowSort = true;
        propertyMappingList.Add((IPropertyMapping) propertyMappingProxy);
      }
      foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(clrType).OfType<PropertyDescriptor>().Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => !toAdd.Any<IPropertyMapping>((Func<IPropertyMapping, bool>) (y => y.Name == x.Name)))).Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.Attributes.OfType<AssociationAttribute>().Any<AssociationAttribute>())))
      {
        NavigationPropertyMappingProxy propertyMappingProxy38 = new NavigationPropertyMappingProxy();
        propertyMappingProxy38.Name = propertyDescriptor.Name;
        propertyMappingProxy38.ResolverType = typeof (PersistentNavigationResolver).FullName;
        NavigationPropertyMappingProxy propertyMappingProxy39 = propertyMappingProxy38;
        if ((propertyDescriptor.Name == LinqHelper.MemberName<IHasParent>((Expression<Func<IHasParent, object>>) (y => y.Parent)) && (typeof (IHierarchicalItem).IsAssignableFrom(clrType) || typeof (IHasParent).IsAssignableFrom(clrType))) | (propertyDescriptor.Name == LinqHelper.MemberName<IHasIDataItemParent>((Expression<Func<IHasIDataItemParent, object>>) (y => y.ItemParent)) && typeof (IHasIDataItemParent).IsAssignableFrom(clrType)))
          propertyMappingProxy39.Parameters.Add("isParentReference", bool.TrueString);
        toAdd.Add((IPropertyMapping) propertyMappingProxy39);
      }
      foreach (IPropertyMapping propertyMapping in toAdd)
        contract.Properties.Add(propertyMapping);
    }

    private ISet<string> AddCustomFields(Type clrType, ITypeSettings contract)
    {
      HashSet<string> stringSet = new HashSet<string>();
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(clrType);
      List<IPropertyMapping> propertyMappingList1 = new List<IPropertyMapping>();
      foreach (PropertyDescriptor propertyDescriptor1 in properties)
      {
        bool flag1 = propertyDescriptor1 is DynamicLstringPropertyDescriptor;
        bool flag2 = propertyDescriptor1 is MetafieldPropertyDescriptor;
        bool flag3 = propertyDescriptor1 is TaxonomyPropertyDescriptor;
        if (flag2 && object.Equals((object) typeof (ContentLink[]), (object) propertyDescriptor1.PropertyType))
        {
          stringSet.Add(propertyDescriptor1.Name);
        }
        else
        {
          RelatedDataPropertyDescriptor propertyDescriptor2 = propertyDescriptor1 as RelatedDataPropertyDescriptor;
          if (flag1 | flag2 | flag3)
          {
            List<IPropertyMapping> propertyMappingList2 = propertyMappingList1;
            PersistentPropertyMappingProxy propertyMappingProxy = new PersistentPropertyMappingProxy();
            propertyMappingProxy.Name = propertyDescriptor1.Name;
            propertyMappingProxy.SelectedByDefault = !flag3;
            propertyMappingProxy.AllowSort = !flag3;
            propertyMappingList2.Add((IPropertyMapping) propertyMappingProxy);
          }
          else if (propertyDescriptor2 != null)
          {
            Type relatedType = propertyDescriptor2.RelatedType;
            if (!(relatedType == (Type) null) && this.IsSupportedType(relatedType))
            {
              NavigationPropertyMappingProxy propertyMappingProxy1 = new NavigationPropertyMappingProxy();
              propertyMappingProxy1.Name = propertyDescriptor1.Name;
              propertyMappingProxy1.ResolverType = typeof (RelatedDataPropertyResolver).FullName;
              propertyMappingProxy1.RelatedProviders = propertyDescriptor2.RelatedProviders;
              NavigationPropertyMappingProxy propertyMappingProxy2 = propertyMappingProxy1;
              propertyMappingList1.Add((IPropertyMapping) propertyMappingProxy2);
            }
          }
        }
      }
      foreach (IPropertyMapping propertyMapping in propertyMappingList1)
      {
        if (!contract.Properties.Contains(propertyMapping))
          contract.Properties.Add(propertyMapping);
      }
      return (ISet<string>) stringSet;
    }

    private bool IsSupportedType(Type clrType)
    {
      if (((IEnumerable<object>) clrType.GetCustomAttributes(typeof (ObsoleteAttribute), false)).Any<object>())
        return false;
      Type type = TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product", false);
      return !(type != (Type) null) || !type.IsAssignableFrom(clrType);
    }

    private IEnumerable<ContractFactory.FieldsFilter> GetFieldFilters(
      Type clrType)
    {
      return (IEnumerable<ContractFactory.FieldsFilter>) new List<ContractFactory.FieldsFilter>()
      {
        new ContractFactory.FieldsFilter()
        {
          ShouldFilter = (Func<bool>) (() => typeof (Content).IsAssignableFrom(clrType) && !CommentsUtilities.IsCommentableType(clrType.FullName)),
          Apply = (Func<IEnumerable<PropertyDescriptor>, IEnumerable<PropertyDescriptor>>) (props => props.Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.Name != LinqHelper.MemberName<Content>((Expression<Func<Content, object>>) (y => (object) y.AllowComments)))))
        },
        new ContractFactory.FieldsFilter()
        {
          ShouldFilter = (Func<bool>) (() => typeof (IApprovalWorkflowItem).IsAssignableFrom(clrType)),
          Apply = (Func<IEnumerable<PropertyDescriptor>, IEnumerable<PropertyDescriptor>>) (props => props.Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.Name != LinqHelper.MemberName<IApprovalWorkflowItem>((Expression<Func<IApprovalWorkflowItem, object>>) (y => y.ApprovalWorkflowState)))))
        },
        new ContractFactory.FieldsFilter()
        {
          ShouldFilter = (Func<bool>) (() => typeof (ILocatableExtended).IsAssignableFrom(clrType)),
          Apply = (Func<IEnumerable<PropertyDescriptor>, IEnumerable<PropertyDescriptor>>) (props => props.Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.Name != LinqHelper.MemberName<ILocatableExtended>((Expression<Func<ILocatableExtended, object>>) (y => y.ItemDefaultUrl)))))
        },
        new ContractFactory.FieldsFilter()
        {
          ShouldFilter = (Func<bool>) (() => typeof (IOwnership).IsAssignableFrom(clrType)),
          Apply = (Func<IEnumerable<PropertyDescriptor>, IEnumerable<PropertyDescriptor>>) (props => props.Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.Name != LinqHelper.MemberName<IOwnership>((Expression<Func<IOwnership, object>>) (y => (object) y.Owner)))))
        },
        new ContractFactory.FieldsFilter()
        {
          ShouldFilter = (Func<bool>) (() => typeof (ILocalizable).IsAssignableFrom(clrType)),
          Apply = (Func<IEnumerable<PropertyDescriptor>, IEnumerable<PropertyDescriptor>>) (props => props.Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.Name != LinqHelper.MemberName<ILocalizable>((Expression<Func<ILocalizable, object>>) (y => y.AvailableLanguages)))))
        }
      };
    }

    private class FieldsFilter
    {
      internal Func<bool> ShouldFilter { get; set; }

      internal Func<IEnumerable<PropertyDescriptor>, IEnumerable<PropertyDescriptor>> Apply { get; set; }
    }
  }
}
