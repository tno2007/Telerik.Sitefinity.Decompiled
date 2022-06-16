// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.IPageVariationPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages
{
  internal interface IPageVariationPlugin
  {
    string ModuleName { get; }

    string Key { get; }

    string GetPageVariationKey(PageSiteNode node);

    string GetDescription(PageSiteNode node);

    string GetReportsLink(PageSiteNode node);

    string GetReportsLinkTitle(PageSiteNode node);

    IGrantedActions GetGrantedActions();
  }
}
