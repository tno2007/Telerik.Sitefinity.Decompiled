// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ParentAccessor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.SiteSync
{
  internal class ParentAccessor
  {
    public virtual IDataItem GetParent(object item)
    {
      string parentPropertyName = (string) null;
      return this.GetParent(item, out parentPropertyName);
    }

    public virtual IDataItem GetParent(object item, out string parentPropertyName)
    {
      IDataItem parent = (IDataItem) null;
      parentPropertyName = (string) null;
      switch (item)
      {
        case IHasParent _:
          parent = (IDataItem) ((IHasParent) item).Parent;
          parentPropertyName = "Parent";
          break;
        case IHasIDataItemParent _:
          parent = ((IHasIDataItemParent) item).ItemParent;
          parentPropertyName = "ItemParent";
          break;
        case IHierarchicalItem _:
          parent = (IDataItem) ((IHierarchicalItem) item).Parent;
          parentPropertyName = "Parent";
          break;
      }
      return parent;
    }

    public virtual string GetParentProperty(object item)
    {
      string parentPropertyName = (string) null;
      this.GetParent(item, out parentPropertyName);
      return parentPropertyName;
    }
  }
}
