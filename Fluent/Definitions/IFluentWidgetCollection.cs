// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.IFluentWidgetCollection`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Fluent.Definitions
{
  internal interface IFluentWidgetCollection<TImplement>
  {
    IFluentMultiFlatSelectorWiget<TImplement> AddMultiFlatSelector(
      string name);

    IFluentLiteralWidget<TImplement> AddLiteral(string name);

    IFluentLanguagesDropDownList<TImplement> AddLanguagesDropDownList(
      string name);

    IFluentDynamicCommandWidget<TImplement> AddDynamicCommandWidget(
      string name);

    IFluentContentItemInfo<TImplement> AddContentItemInfo(string name);

    IFluentLibraryInfoWidget<TImplement> AddLibraryInfoWidget(string name);

    IFluentCommandWidget<TImplement> AddCommandWidget(string name);

    IFluentStateWidget<TImplement> AddStateWidget(string name);

    IFluentModeStateWidget<TImplement> AddModeStateWidget(string name);

    IFluentCommandWidget<TImplement> AddStateCommandWidget(string name);

    IFluentSearchWidget<TImplement> AddSearchWidget(string name);

    IFluentProvidersListWidget<TImplement> AddProvidersListWidget(
      string name);

    IFluentDateFilteringWidget<TImplement> AddDateFilteringWidget(
      string name);

    IFluentActionMenuWidget<TImplement> AddActionMenuWidget(string name);
  }
}
