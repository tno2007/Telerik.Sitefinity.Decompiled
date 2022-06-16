// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.OrganizerFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Organizers;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>
  /// Factory class for creating data item organizer objects.
  /// </summary>
  public class OrganizerFactory : IOrganizerFactory
  {
    /// <summary>Resolves the organizer.</summary>
    /// <param name="dataItem">The data item for which the organizer ought to be resolved.</param>
    /// <returns>Instance of the organizer.</returns>
    public OrganizerBase ResolveOrganizer(IDataItem dataItem)
    {
      object obj = dataItem != null ? ((IEnumerable<object>) dataItem.GetType().GetCustomAttributes(typeof (OrganizerTypeAttribute), true)).FirstOrDefault<object>() : throw new ArgumentNullException(nameof (dataItem));
      if (obj == null)
        return (OrganizerBase) new DefaultOrganizer(dataItem);
      return (OrganizerBase) Activator.CreateInstance(((OrganizerTypeAttribute) obj).OrganizerType, (object) dataItem, (object) this.GetMetaDataManager(string.Empty));
    }

    /// <summary>
    /// Gets the metadata manager to be used by the organizer factory.
    /// </summary>
    /// <param name="providerName">Name of the provider with which the manager should be instantiated.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.IMetadataManager" />.</returns>
    public virtual IMetadataManager GetMetaDataManager(string providerName) => (IMetadataManager) MetadataManager.GetManager(providerName);
  }
}
