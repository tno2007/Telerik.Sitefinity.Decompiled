// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.ItemMeta.PropertyMeta
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.Services.Contracts.ItemMeta
{
  internal class PropertyMeta
  {
    public PropertyMeta()
    {
      this.AllowCreate = true;
      this.AllowAdd = true;
      this.AllowRemove = true;
      this.AllowView = true;
    }

    public string Name { get; set; }

    public bool ReadOnly { get; set; }

    public string Status { get; set; }

    public string StatusMessage { get; set; }

    public bool AllowCreate { get; set; }

    public bool AllowRemove { get; set; }

    public bool AllowAdd { get; set; }

    public bool AllowView { get; set; }

    public string PredefinedValue { get; set; }
  }
}
