// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Test
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  internal class Test
  {
    private static void DoTest() => ((IFluentDefinitions) null).CreateContainer((ContentViewControlElement) null, "news backedn list", typeof (int)).AddMasterGridView("master view").Toolbar("toolbar").AddSection().Properties().Title("Wtf").ResourceClassId("sd").Visible(false).WrapperTagKey(HtmlTextWriterTag.A).Done().AddCommandWidget("ss").Done().Done().AddSection().AddLiteral("ss").Done().Done().Done().AddDialog("sd").OpenOnCommandName("create").Name("CreateNewsItem").AddQueryStringParameter("TypeName", "Telerik.Sitefinity.News.Model.NewsItem").AddQueryStringParameter("Title", "").AddQueryStringParameter("BackLabelText", "").AddQueryStringParameter("ItemsName", "").QueryString("?shw=true&hide=false").Done().AddLink("sad").Name("viewComments").CommandName("viewComments").NavigateUrl("~/Sitefinity/MyComments.aspx").Done().AddLink("ds").Name("viewSettings").CommandName("viewSettings").NavigateUrl("~/Sitefinity/Settings/Advanced.aspx").Done().Done().Done().SaveChanges();
  }
}
