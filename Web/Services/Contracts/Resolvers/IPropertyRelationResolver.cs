// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.PropertyRelationResolverBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal abstract class PropertyRelationResolverBase : IPropertyRelationResolver
  {
    private string componentType;
    private Type relatedType;
    private string relatedProviders;
    private string propName;
    private bool isParentReference;

    public virtual void Init(NameValueCollection parameters)
    {
      string parameter1 = parameters["componentType"];
      this.componentType = !string.IsNullOrEmpty(parameter1) ? parameter1 : throw new ArgumentOutOfRangeException("componentType", "No parent type was specified");
      string parameter2 = parameters["propName"];
      this.propName = !string.IsNullOrEmpty(parameter2) ? parameter2 : throw new ArgumentOutOfRangeException("propName", "No propertyName parameter was specified");
      bool.TryParse(parameters["isParentReference"], out this.isParentReference);
      this.relatedType = TypeResolutionService.ResolveType(parameters["relatedType"], false);
      this.relatedProviders = parameters["relatedProviders"];
      if (TypeDescriptor.GetProperties(TypeResolutionService.ResolveType(parameter1)).Find(this.propName, false) == null)
        throw new InvalidOperationException(string.Format("The navigation property with the name \"{0}\" does not exist.", (object) parameter2));
    }

    public abstract IQueryable GetRelatedItems(object item);

    public abstract object GetRelatedItem(object item, Guid relatedItemKey);

    public abstract void CreateRelation(
      object item,
      Guid relatedItemKey,
      string relatedItemProvider,
      object persistentItem);

    public abstract void DeleteRelation(object item, Guid relatedItemKey);

    public abstract void DeleteAllRelations(object item);

    public virtual Type RelatedType
    {
      get
      {
        if (this.relatedType == (Type) null && !this.Descriptor.PropertyType.TryGetEnumerableType(out this.relatedType))
          this.relatedType = this.Descriptor.PropertyType;
        return this.relatedType;
      }
    }

    public virtual bool IsMultipleRelation
    {
      get
      {
        AssociationAttribute associationAttribute = this.Descriptor.Attributes.OfType<AssociationAttribute>().FirstOrDefault<AssociationAttribute>();
        if (associationAttribute != null)
          return !associationAttribute.IsForeignKey;
        Type elementType = (Type) null;
        return this.Descriptor.PropertyType.TryGetEnumerableType(out elementType);
      }
    }

    public virtual PropertyDescriptor Descriptor => TypeDescriptor.GetProperties(TypeResolutionService.ResolveType(this.componentType)).Find(this.propName, false);

    public bool IsParentReference => this.isParentReference;

    public virtual string RelatedProviders => this.relatedProviders;

    internal class PropNames
    {
      internal const string RelatedType = "relatedType";
      internal const string RelatedProviders = "relatedProviders";
      internal const string ComponentType = "componentType";
      internal const string PropertyName = "propName";
      internal const string IsParentReference = "isParentReference";
    }
  }
}
