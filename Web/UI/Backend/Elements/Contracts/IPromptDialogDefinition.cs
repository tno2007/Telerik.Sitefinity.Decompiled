// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IPromptDialogDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  public interface IPromptDialogDefinition : IDefinition
  {
    /// <summary>Gets or sets the name of the dialog.</summary>
    /// <value>The name of the dialog.</value>
    string DialogName { get; set; }

    /// <summary>
    /// Gets or sets the name of the command that will cause the dialog defined by this definition to be opened.
    /// </summary>
    string OpenOnCommandName { get; set; }

    bool AllowCloseButton { get; set; }

    List<ICommandToolboxItemDefinition> Commands { get; }

    string DefaultInputText { get; set; }

    bool Displayed { get; set; }

    int Height { get; set; }

    int InputRows { get; set; }

    string ItemTag { get; set; }

    string Message { get; set; }

    PromptMode Mode { get; set; }

    string OnClientCommand { get; set; }

    bool ShowOnLoad { get; set; }

    string TextFieldExample { get; set; }

    string TextFieldTitle { get; set; }

    string Title { get; set; }

    IValidatorDefinition ValidatorDefinition { get; }

    int Width { get; set; }

    string WrapperCssClass { get; set; }

    string WrapperTag { get; set; }

    string ResourceClassId { get; set; }
  }
}
