// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.Services.ViewModel.DataSourceGridViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Data.DataSource;

namespace Telerik.Sitefinity.Multisite.Web.Services.ViewModel
{
  /// <summary>
  /// View model for Data Source without the Provider Infos.
  /// </summary>
  [DataContract]
  public class DataSourceGridViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.DataSourceGridViewModel" /> class.
    /// </summary>
    public DataSourceGridViewModel()
      : this((IDataSource) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.Services.ViewModel.DataSourceGridViewModel" /> class.
    /// </summary>
    /// <param name="dataSource">The data source.</param>
    internal DataSourceGridViewModel(IDataSource dataSource)
    {
      if (dataSource == null)
        return;
      this.Name = dataSource.Name;
      this.Title = dataSource.Title;
    }

    /// <summary>Gets or sets the name of the data source.</summary>
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Title { get; set; }
  }
}
