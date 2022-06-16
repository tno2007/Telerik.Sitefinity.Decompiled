// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentViewMasterDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Frontend
{
  public class DynamicContentViewMasterDefinition : ContentViewMasterDefinition
  {
    public DynamicContentViewMasterDefinition()
    {
      this.ViewName = "DynamicContentMasterView";
      this.AllowPaging = new bool?(true);
      this.ItemsPerPage = new int?(20);
      this.FilterExpression = "Visible = true AND Status = Live";
      this.SortExpression = "PublicationDate DESC";
    }
  }
}
