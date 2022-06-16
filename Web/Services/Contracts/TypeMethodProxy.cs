// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.TypeMethodProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class TypeMethodProxy : ITypeMethod
  {
    private string name;
    private string queryString;

    public TypeMethodProxy()
    {
    }

    public TypeMethodProxy(ITypeMethod copyFrom)
    {
      this.name = copyFrom.Name;
      this.queryString = copyFrom.QueryString;
    }

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    public string QueryString
    {
      get => this.queryString;
      set => this.queryString = value;
    }
  }
}
