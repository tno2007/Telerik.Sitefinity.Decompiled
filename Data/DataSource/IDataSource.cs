// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataSource.DataProviderInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;

namespace Telerik.Sitefinity.Data.DataSource
{
  /// <summary>
  /// Contains the basic information about a provider which could be used by a data source <see cref="T:Telerik.Sitefinity.Data.DataSource.IDataSource" />
  /// </summary>
  public class DataProviderInfo
  {
    private NameValueCollection providerParameters;

    public DataProviderInfo(string providerName, string providerTitle, Type providerType)
      : this(providerName, providerTitle, providerType, (NameValueCollection) null)
    {
    }

    public DataProviderInfo(
      string providerName,
      string providerTitle,
      Type providerType,
      NameValueCollection providerParameters)
    {
      this.ProviderName = providerName;
      this.ProviderTitle = providerTitle;
      this.ProviderType = providerType;
      this.providerParameters = providerParameters;
    }

    /// <summary>Gets the name of the provider</summary>
    public string ProviderName { get; private set; }

    /// <summary>Gets the title of the provider</summary>
    public string ProviderTitle { get; private set; }

    /// <summary>Gets the type of the provider</summary>
    public Type ProviderType { get; private set; }

    /// <summary>The settings of the provider.</summary>
    public NameValueCollection ProviderParameters
    {
      get
      {
        if (this.providerParameters == null)
          this.providerParameters = new NameValueCollection();
        return this.providerParameters;
      }
      private set => this.providerParameters = value;
    }
  }
}
