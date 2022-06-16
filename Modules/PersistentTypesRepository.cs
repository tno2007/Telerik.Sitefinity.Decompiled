// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.PersistentTypesRepository
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules
{
  public class PersistentTypesRepository
  {
    private static List<Type> persistentTypes = new List<Type>();

    /// <summary>Gets the persistent types.</summary>
    /// <value>The persistent types.</value>
    public static List<Type> PersistentTypes
    {
      get
      {
        if (PersistentTypesRepository.persistentTypes.Count == 0)
        {
          PersistentTypesRepository.persistentTypes = new List<Type>();
          IQueryable<MetaType> metaTypes = MetadataManager.GetManager(string.Empty).GetMetaTypes();
          Expression<Func<MetaType, bool>> predicate = (Expression<Func<MetaType, bool>>) (mt => mt.IsDynamic == false);
          foreach (MetaType metaType in (IEnumerable<MetaType>) metaTypes.Where<MetaType>(predicate))
          {
            Type type = TypeResolutionService.ResolveType(metaType.Namespace + "." + metaType.ClassName, false);
            if (type != (Type) null)
              PersistentTypesRepository.persistentTypes.Add(type);
          }
        }
        return PersistentTypesRepository.persistentTypes;
      }
    }

    /// <summary>Gets all types from specific namespace.</summary>
    /// <param name="nameSpace">The name space.</param>
    /// <returns></returns>
    public static IEnumerable<Type> GetAllTypesFromSpecificNamespace(string nameSpace) => PersistentTypesRepository.PersistentTypes.Where<Type>((Func<Type, bool>) (type => type.Namespace == nameSpace));

    /// <summary>Gets the type of all types based on specific.</summary>
    /// <param name="baseType">Type of the base.</param>
    /// <returns></returns>
    public static IEnumerable<Type> GetAllTypesBasedOnSpecificType(Type baseType) => PersistentTypesRepository.PersistentTypes.Where<Type>((Func<Type, bool>) (currentType => baseType.IsAssignableFrom(currentType)));
  }
}
