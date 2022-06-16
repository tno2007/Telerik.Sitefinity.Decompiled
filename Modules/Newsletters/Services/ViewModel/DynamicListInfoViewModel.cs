// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.DynamicListInfoViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>View model class for the DynamicListInfo type.</summary>
  [DataContract]
  public class DynamicListInfoViewModel
  {
    private IList<MergeTagViewModel> availableProperties;

    /// <summary>
    /// Gets or sets the unique key of the dynamic list. Every <see cref="!:IDynamicListProvider" /> needs to be able
    /// to resolve the dynamic list by the key.
    /// </summary>
    [DataMember]
    public string Key { get; set; }

    /// <summary>Gets or sets the title of the dynamic list.</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider from which this dynamic list is pulled.
    /// </summary>
    [DataMember]
    public string ProviderName { get; set; }

    /// <summary>Gets the collection of available properties.</summary>
    [DataMember]
    public IList<MergeTagViewModel> AvailableProperties
    {
      get
      {
        if (this.availableProperties == null)
          this.availableProperties = (IList<MergeTagViewModel>) new List<MergeTagViewModel>();
        return this.availableProperties;
      }
    }
  }
}
