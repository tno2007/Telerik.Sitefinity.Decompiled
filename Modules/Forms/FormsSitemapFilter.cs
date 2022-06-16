// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormsSitemapFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Forms
{
  internal class FormsSitemapFilter : ISitemapNodeFilter
  {
    /// <summary>
    ///  The dictionary of security actions that allow node access.
    /// </summary>
    internal static readonly Dictionary<string, string[]> BackendNodeFilterSecuritySets = new Dictionary<string, string[]>()
    {
      {
        "Forms",
        new string[5]
        {
          "Create",
          "Delete",
          "Modify",
          "ChangeOwner",
          "ChangePermissions"
        }
      },
      {
        "SitemapGeneration",
        new string[1]{ "ViewBackendLink" }
      }
    };

    public bool IsNodeAccessPrevented(PageSiteNode pageNode)
    {
      if (!pageNode.IsBackend || !(pageNode.Id == FormsModule.HomePageId) && !(pageNode.Id == FormsModule.EntriesPageID))
        return false;
      if (!LicenseState.CheckIsModuleLicensedInCurrentDomain("A64410F7-2F1E-4068-81D0-E28D864DE323"))
        return true;
      if (!this.IsFilterEnabled("Forms"))
        return false;
      List<ISecuredObject> source = new List<ISecuredObject>();
      foreach (DataProviderBase contextProvider in FormsManager.GetManager().GetContextProviders())
        source.Add(contextProvider.SecurityRoot);
      bool flag = true;
      foreach (KeyValuePair<string, string[]> filterSecuritySet in FormsSitemapFilter.BackendNodeFilterSecuritySets)
      {
        KeyValuePair<string, string[]> securitySet = filterSecuritySet;
        foreach (string str in securitySet.Value)
        {
          string actionName = str;
          if (source.Any<ISecuredObject>((Func<ISecuredObject, bool>) (r => r.IsGranted(securitySet.Key, actionName))))
          {
            flag = false;
            break;
          }
        }
      }
      return flag;
    }
  }
}
