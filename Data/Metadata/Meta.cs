// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.Meta
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Data.Metadata
{
  /// <summary>Helper class for reading Metadata</summary>
  public static class Meta
  {
    private static MetaDataProvider provider;

    /// <summary>Gets the default metadata provider.</summary>
    /// <value>The provider.</value>
    public static MetaDataProvider Provider
    {
      get
      {
        if (Meta.provider == null)
        {
          Meta.provider = MetadataManager.GetManager().Provider;
          Meta.provider.Disposing += new EventHandler<EventArgs>(Meta.Provider_Disposing);
        }
        return Meta.provider;
      }
    }

    private static void Provider_Disposing(object sender, EventArgs e) => Meta.provider = (MetaDataProvider) null;

    /// <summary>
    /// Gets an array of type descriptions for the specified type and its inherited types.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public static MetaType[] GetMetaTypes(Type type) => Meta.Provider.GetMetaTypes(type);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" />.
    /// </summary>
    public static IQueryable<MetaType> GetMetaTypes() => Meta.Provider.GetMetaTypes();

    /// <summary>
    /// Gets an array of type descriptions for the specified object instance and its inherited types.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public static MetaType[] GetMetaTypes(object instance) => Meta.Provider.GetMetaTypes(instance);
  }
}
