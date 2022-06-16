// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.CheckLinkedNodeCallHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages
{
  public class CheckLinkedNodeCallHandler : ISitefinityCallHanlder
  {
    public void Invoke(EventHandlerArgs args)
    {
      if (!(args.Item is PageNode))
        return;
      string propertyName = args.PropertyName;
      if (!(propertyName == "LinkedNodeId") && !(propertyName == "LinkedNodeProvider"))
        return;
      PageNode pageNode = (PageNode) args.Item;
      string str1 = !(((IDataItem) pageNode).Provider is DataProviderBase provider) ? (string) null : provider.Name;
      Guid id = pageNode.Id;
      Guid guid = pageNode.LinkedNodeId;
      string str2 = pageNode.LinkedNodeProvider;
      if (propertyName == "LinkedNodeId")
      {
        Guid? nullable = (Guid?) args.Value;
        guid = !nullable.HasValue ? Guid.Empty : nullable.Value;
      }
      else
        str2 = (string) args.Value;
      if (string.IsNullOrEmpty(str2))
        str2 = ManagerBase<PageDataProvider>.GetDefaultProviderName();
      if (string.IsNullOrEmpty(str1))
        str1 = ManagerBase<PageDataProvider>.GetDefaultProviderName();
      if (guid == id && str2 == str1)
        throw new ArgumentException("You cannot link a page node to itself");
    }
  }
}
