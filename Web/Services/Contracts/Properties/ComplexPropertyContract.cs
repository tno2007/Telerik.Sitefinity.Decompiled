// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.ComplexPropertyContract
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class ComplexPropertyContract : 
    PropertyMappingProxy,
    IComplexPropertyContract,
    IPropertyMapping
  {
    private string type;
    private bool readOnly;
    private bool selectedByDefault = true;

    public ComplexPropertyContract()
    {
    }

    public ComplexPropertyContract(IComplexPropertyContract source)
      : base((IPropertyMapping) source)
    {
      this.type = source.Type;
      this.readOnly = source.ReadOnly;
      this.selectedByDefault = source.SelectedByDefault;
    }

    public string Type
    {
      get => this.type;
      set => this.type = value;
    }

    public bool ReadOnly
    {
      get => this.readOnly;
      set => this.readOnly = value;
    }

    public bool SelectedByDefault
    {
      get => this.selectedByDefault;
      set => this.selectedByDefault = value;
    }
  }
}
