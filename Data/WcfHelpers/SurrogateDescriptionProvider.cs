// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.SurrogateDescriptionProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>Provides type description</summary>
  public class SurrogateDescriptionProvider : TypeDescriptionProvider
  {
    private static Dictionary<Type, ICustomTypeDescriptor> cache;
    private static string cacheKey = typeof (SurrogateDescriptionProvider).FullName + "Cache";

    /// <summary>
    /// Gets a custom type descriptor for the given type and object.
    /// </summary>
    /// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
    /// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
    /// <returns>
    /// An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.
    /// </returns>
    public override ICustomTypeDescriptor GetTypeDescriptor(
      Type objectType,
      object instance)
    {
      Type type = instance != null || !(objectType == (Type) null) ? objectType : throw new WebProtocolException(HttpStatusCode.InternalServerError, "NullReferenceException", (Exception) new NullReferenceException());
      if (type == (Type) null)
        type = instance.GetType();
      return SurrogateDescriptionProvider.GetDescriptor(type);
    }

    /// <summary>Get cached descriptor</summary>
    /// <param name="type">Type to get description of</param>
    /// <returns>Cached descriptor</returns>
    private static ICustomTypeDescriptor GetDescriptor(Type type)
    {
      if (SurrogateDescriptionProvider.cache == null)
        SurrogateDescriptionProvider.cache = new Dictionary<Type, ICustomTypeDescriptor>();
      ICustomTypeDescriptor descriptor;
      if (!SurrogateDescriptionProvider.cache.TryGetValue(type, out descriptor))
      {
        lock (SurrogateDescriptionProvider.cache)
        {
          if (!SurrogateDescriptionProvider.cache.TryGetValue(type, out descriptor))
          {
            descriptor = (ICustomTypeDescriptor) new SurrogateDescriptor(type);
            SurrogateDescriptionProvider.cache.Add(type, descriptor);
          }
        }
      }
      return descriptor;
    }
  }
}
