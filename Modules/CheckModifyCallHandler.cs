// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.CheckModifyPermissionsCallHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// Checks for modify and edit content permissions for pages.
  /// </summary>
  public class CheckModifyPermissionsCallHandler : ISitefinityCallHanlder
  {
    /// <summary>Executes the permissions check.</summary>
    /// <param name="args">The event arguments.</param>
    public void Invoke(EventHandlerArgs args)
    {
      PageNode pageNode = (PageNode) null;
      if (args.Item is PageNode)
      {
        pageNode = (PageNode) args.Item;
        if (pageNode.Parent == null)
          return;
      }
      else if (args.Item is PageData)
        pageNode = ((PageData) args.Item).NavigationNode;
      if (pageNode == null || ((IDataItem) pageNode).Provider is DataProviderBase provider && provider.SuppressSecurityChecks)
        return;
      if (!(pageNode.IsGranted("Pages", "Modify") | pageNode.IsGranted("Pages", "EditContent")))
        throw new SecurityDemandFailException(string.Format(Res.Get<PageResources>("PageNoModifyPermission"), (object) pageNode.Title));
    }
  }
}
