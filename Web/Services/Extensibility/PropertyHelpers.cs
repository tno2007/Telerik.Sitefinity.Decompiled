// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.PropertyHelpers
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  internal class PropertyHelpers
  {
    internal static PageSiteNode GetSiteMapNode(object item)
    {
      PageSiteNode siteMapNode = (PageSiteNode) null;
      switch (item)
      {
        case PageSiteNode pageSiteNode:
          siteMapNode = pageSiteNode;
          break;
        case PageNode page:
          siteMapNode = page.GetSiteMapNode();
          break;
      }
      return siteMapNode;
    }

    internal static bool IsFormPageBreak(IManager manager, ControlData control)
    {
      if (manager is FormsManager formsManager)
      {
        if (typeof (IFormPageBreak).IsAssignableFrom(ControlUtilities.BehaviorResolver.GetBehaviorObject(formsManager.LoadControl((ObjectData) control, (CultureInfo) null)).GetType()))
          return true;
      }
      return false;
    }

    internal static bool IsFormNavigation(IManager manager, ControlData control)
    {
      if (manager is FormsManager formsManager)
      {
        if (typeof (IFormNavigation).IsAssignableFrom(ControlUtilities.BehaviorResolver.GetBehaviorObject(formsManager.LoadControl((ObjectData) control, (CultureInfo) null)).GetType()))
          return true;
      }
      return false;
    }
  }
}
