// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.Services.Data.DataItemContext`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Publishing.Web.Services.Data
{
  [DataContract(Name = "DataItemContext", Namespace = "Telerik.Sitefinity.Publishing.Web.Services.Data")]
  public class DataItemContext<T>
  {
    private T item;
    private string itemType;

    [DataMember]
    public T Item
    {
      get => this.item;
      set
      {
        this.item = value;
        this.itemType = value.GetType().FullName;
      }
    }

    [DataMember]
    public string ItemType
    {
      get => this.itemType;
      set => this.itemType = value;
    }
  }
}
