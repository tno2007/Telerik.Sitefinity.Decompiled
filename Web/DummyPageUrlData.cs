// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.DummyPageUrlData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web
{
  internal class DummyPageUrlData
  {
    private string url;
    private bool redirectToDefault;

    public DummyPageUrlData(PageUrlData pageUrlData)
    {
      this.url = pageUrlData.Url;
      this.redirectToDefault = pageUrlData.RedirectToDefault;
    }

    public string Url => this.url;

    public bool RedirectToDefault => this.redirectToDefault;
  }
}
