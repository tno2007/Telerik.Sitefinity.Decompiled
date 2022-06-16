// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.TypeSettingsProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.Services.Contracts.Properties;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class TypeSettingsProxy : ITypeSettings
  {
    private IList<IPropertyMapping> properties;
    private IList<ITypeMethod> methods;

    public TypeSettingsProxy()
    {
      this.PageSize = 50;
      this.Enabled = true;
    }

    public TypeSettingsProxy(ITypeSettings source, Permissions? access)
    {
      this.Enabled = source.Enabled;
      this.ClrType = source.ClrType;
      this.UrlName = source.UrlName;
      this.PageSize = source.PageSize;
      this.Access = source.Access ?? access;
      this.AutogenerateProperties = source.AutogenerateProperties;
      foreach (ITypeMethod method in (IEnumerable<ITypeMethod>) source.Methods)
        this.Methods.Add((ITypeMethod) new TypeMethodProxy(method));
      foreach (IPropertyMapping property in (IEnumerable<IPropertyMapping>) source.Properties)
        this.Properties.Add(TypeSettingsProxy.ConvertProperty(property));
    }

    public bool Enabled { get; set; }

    public string ClrType { get; set; }

    public string UrlName { get; set; }

    public int PageSize { get; set; }

    public bool AutogenerateProperties { get; set; }

    public Permissions? Access { get; set; }

    public IList<IPropertyMapping> Properties
    {
      get
      {
        if (this.properties == null)
          this.properties = (IList<IPropertyMapping>) new List<IPropertyMapping>();
        return this.properties;
      }
      set => this.properties = value;
    }

    public IList<ITypeMethod> Methods
    {
      get
      {
        if (this.methods == null)
          this.methods = (IList<ITypeMethod>) new List<ITypeMethod>();
        return this.methods;
      }
    }

    internal static IPropertyMapping ConvertProperty(IPropertyMapping mapping)
    {
      switch (mapping)
      {
        case IComplexPropertyContract source1:
          return (IPropertyMapping) new ComplexPropertyContract(source1);
        case IPersistentPropertyMapping source2:
          return (IPropertyMapping) new PersistentPropertyMappingProxy(source2);
        case IClassPropertyMapping source3:
          return (IPropertyMapping) new ClassPropertyMappingProxy(source3);
        case ICalculatedPropertyMapping source4:
          return (IPropertyMapping) new CalculatedPropertyMappingProxy(source4);
        case INavigationPropertyMapping source5:
          return (IPropertyMapping) new NavigationPropertyMappingProxy(source5);
        default:
          return (IPropertyMapping) null;
      }
    }
  }
}
