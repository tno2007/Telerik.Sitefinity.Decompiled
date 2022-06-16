// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.IFluentMasterGridView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  internal interface IFluentMasterGridView : IFluentContentMasterViewBase<ContentViewMasterElement>
  {
    IFluentMasterGridView Do(Action<ContentViewMasterElement> action);

    IFluentWidgetbarDefinition<IFluentMasterGridView> Toolbar(
      string name);

    IFluentWidgetbarDefinition<IFluentMasterGridView> Sidebar(
      string name);

    IFluentWidgetbarDefinition<IFluentMasterGridView> ContextBar(
      string name);

    IFluentGridModeDefinition Grid(string name);

    IFluentTreeModeDefinition Tree(string name);

    IFluentListModeDefinition List(string name);

    IFluentDecisionScreenDefinition AddDecisionScreen(string name);

    IFluentDialogDefinition<IFluentMasterGridView> AddDialog(
      string name);

    IFluentLinkDefinition<IFluentMasterGridView> AddLink(
      string name);

    IFluentPromptDialogDefinition<IFluentMasterGridView> AddPromptDialog(
      string name);
  }
}
