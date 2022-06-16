// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.IFluentPromptDialogDefinition`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  internal interface IFluentPromptDialogDefinition<TParent>
  {
    IFluentPromptDialogDefinition<TParent> DialogName(string name);

    IFluentPromptDialogDefinition<TParent> OpenOnCommandName(
      string name);

    IFluentPromptDialogDefinition<TParent> AllowCloseButton(
      bool autoClose);

    IFluentPromptDialogDefinition<TParent> DefaultInputText(string text);

    IFluentPromptDialogDefinition<TParent> Displayed(bool displayed);

    IFluentPromptDialogDefinition<TParent> Height(int height);

    IFluentPromptDialogDefinition<TParent> InputRows(int rows);

    IFluentPromptDialogDefinition<TParent> ItemTag(string tag);

    IFluentPromptDialogDefinition<TParent> Message(string message);

    IFluentPromptDialogDefinition<TParent> Mode(PromptMode mode);

    IFluentPromptDialogDefinition<TParent> OnClientCommand(
      string command);

    IFluentPromptDialogDefinition<TParent> ShowOnLoad(bool show);

    IFluentPromptDialogDefinition<TParent> TextFieldExample(
      string example);

    IFluentPromptDialogDefinition<TParent> TextFieldTitle(string title);

    IFluentPromptDialogDefinition<TParent> Title(string title);

    IFluentPromptDialogDefinition<TParent> Width(int width);

    IFluentPromptDialogDefinition<TParent> WrapperCssClass(
      string cssClass);

    IFluentPromptDialogDefinition<TParent> WrapperTag(string tagName);

    IFluentPromptDialogDefinition<TParent> ResourceClassId(
      string classId);

    TParent Done();
  }
}
