// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLifecycleManagerExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.RelatedData;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Provides extension methods over IContentLifecycleManager
  /// </summary>
  public static class ContentLifecycleManagerExtensions
  {
    /// <summary>
    /// Copies dynamic fields fron <paramref name="source" /> to <paramref name="destination" />
    /// </summary>
    /// <param name="manager">Content lifecycle manager.</param>
    /// <param name="source">Source to copy dynamic fields from</param>
    /// <param name="destination">Destination where dynamic fields should be copied to</param>
    public static void CopyDynamicFields(
      this IContentLifecycleManager manager,
      IDynamicFieldsContainer source,
      IDynamicFieldsContainer destination)
    {
      ContentLifecycleManagerExtensions.CopyDynamicFieldsRecursively(source, destination);
    }

    /// <summary>
    /// Copies dynamic fields fron <paramref name="source" /> to <paramref name="destination" />
    /// </summary>
    /// <param name="source">Source to copy dynamic fields from</param>
    /// <param name="destination">Destination where dynamic fields should be copied to</param>
    public static void CopyDynamicFieldsRecursively(
      IDynamicFieldsContainer source,
      IDynamicFieldsContainer destination)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) source);
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties((object) destination))
      {
        if (!(property is RelatedDataPropertyDescriptor))
        {
          PropertyDescriptor sourceDescriptor = properties[property.Name];
          if (property is TaxonomyPropertyDescriptor)
          {
            if (destination is Content content)
              ((IOrganizable) destination).Organizer.StatisticType = content.Status;
            else
              ((IOrganizable) destination).Organizer.StatisticType = ((ILifecycleDataItem) destination).Status;
            Guid[] array1 = source.GetValue<IList<Guid>>(property.Name).ToArray<Guid>();
            Guid[] array2 = destination.GetValue<IList<Guid>>(property.Name).ToArray<Guid>();
            if (!((IEnumerable<Guid>) array1).SequenceEqual<Guid>((IEnumerable<Guid>) array2))
            {
              ((IOrganizable) destination).Organizer.Clear(property.Name);
              ((IOrganizable) destination).Organizer.AddTaxa(property.Name, array1);
            }
          }
          else if (Attribute.GetCustomAttribute((MemberInfo) sourceDescriptor.PropertyType, typeof (ArtificialAssociationAttribute)) != null)
            ReflectionHelper.CopyArtificialAssociation((object) source, (object) destination, sourceDescriptor, property);
          else if (property is MetafieldPropertyDescriptor)
          {
            object obj = sourceDescriptor.GetValue((object) source);
            property.SetValue((object) destination, obj);
          }
        }
      }
    }

    /// <summary>
    /// Copies item relations from <paramref name="source" /> to <paramref name="destination" />
    /// </summary>
    /// <param name="manager">Content lifecycle manager.</param>
    /// <param name="source">Source to copy relations from</param>
    /// <param name="destination">Destination where relations should be copied to</param>
    internal static void CopyItemRelations(
      this ILifecycleManager manager,
      ILifecycleDataItemGeneric source,
      ILifecycleDataItemGeneric destination)
    {
      IContentLinksManager mappedRelatedManager = manager.Provider.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
      RelatedDataHelper.CopyItemRelations(source, destination, mappedRelatedManager);
    }
  }
}
