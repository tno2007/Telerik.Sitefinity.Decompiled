// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.NavigationPropertyMappingProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class NavigationPropertyMappingProxy : 
    PropertyMappingProxy,
    INavigationPropertyMapping,
    IPropertyMapping
  {
    private NameValueCollection parameters = new NameValueCollection();

    public NavigationPropertyMappingProxy()
    {
    }

    public NavigationPropertyMappingProxy(INavigationPropertyMapping source)
      : base((IPropertyMapping) source)
    {
      this.ResolverType = source.ResolverType;
      if (source.Parameters == null)
        return;
      this.parameters = new NameValueCollection(source.Parameters);
    }

    public string ResolverType { get; set; }

    public string RelatedProviders { get; set; }

    public NameValueCollection Parameters
    {
      get => this.parameters;
      set => this.parameters = value;
    }
  }
}
