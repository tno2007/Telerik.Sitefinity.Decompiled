// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Data.DataProviderViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Data.DataSource;

namespace Telerik.Sitefinity.Services.Data
{
  /// <summary>Represents the DataProvider view model.</summary>
  [DataContract]
  public class DataProviderViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Data.DataProviderViewModel" /> class.
    /// </summary>
    public DataProviderViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Data.DataProviderViewModel" /> class.
    /// </summary>
    public DataProviderViewModel(DataProviderInfo dataProviderInfo)
    {
      this.Name = dataProviderInfo.ProviderName;
      this.Title = dataProviderInfo.ProviderTitle;
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the title of the provider.</summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title { get; set; }
  }
}
