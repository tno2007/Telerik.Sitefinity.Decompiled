// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.IFluentDialogDefinition`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  internal interface IFluentDialogDefinition<TParent>
  {
    IFluentDialogDefinition<TParent> ControlDefinitionName(string name);

    IFluentDialogDefinition<TParent> ViewName(string name);

    IFluentDialogDefinition<TParent> Name(string name);

    IFluentDialogDefinition<TParent> OpenOnCommandName(string name);

    IFluentDialogDefinition<TParent> NavigateUrl(string url);

    IFluentDialogDefinition<TParent> Height(Unit height);

    IFluentDialogDefinition<TParent> Width(Unit width);

    IFluentDialogDefinition<TParent> InitialBehaviors(WindowBehaviors initial);

    IFluentDialogDefinition<TParent> Behaviors(WindowBehaviors byDefault);

    IFluentDialogDefinition<TParent> IsFullScreen(bool fullscreen);

    IFluentDialogDefinition<TParent> VisibleStatusBar(bool visible);

    IFluentDialogDefinition<TParent> VisibleTitleBar(bool visible);

    IFluentDialogDefinition<TParent> Skin(string name);

    IFluentDialogDefinition<TParent> IsModal(bool modal);

    IFluentDialogDefinition<TParent> DestroyOnClose(bool destroy);

    IFluentDialogDefinition<TParent> ReloadOnShow(bool reload);

    IFluentDialogDefinition<TParent> CssClass(string cssClass);

    IFluentDialogDefinition<TParent> AddQueryStringParameter(
      string pramName,
      string value);

    IFluentDialogDefinition<TParent> QueryString(string wholeQueryString);

    TParent Done();
  }
}
