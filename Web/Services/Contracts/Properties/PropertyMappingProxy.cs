// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.PropertyMappingProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal abstract class PropertyMappingProxy : IPropertyMapping
  {
    private string name;
    private string validateCondition;

    public PropertyMappingProxy()
    {
    }

    public PropertyMappingProxy(IPropertyMapping source)
    {
      this.name = source.Name;
      this.validateCondition = source.ValidateCondition;
    }

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    public string ValidateCondition
    {
      get => this.validateCondition;
      set => this.validateCondition = value;
    }
  }
}
