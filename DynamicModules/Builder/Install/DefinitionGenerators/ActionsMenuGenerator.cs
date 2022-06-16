// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators.ActionsMenuGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators
{
  internal static class ActionsMenuGenerator
  {
    /// <summary>Creates the action menu command.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="name">The name.</param>
    /// <param name="wrapperTagKey">The wrapper tag key.</param>
    /// <param name="commandName">Name of the command.</param>
    /// <param name="text">The text.</param>
    /// <param name="cssClass">The CSS class.</param>
    /// <returns></returns>
    public static CommandWidgetElement CreateActionMenuCommand(
      ConfigElement parent,
      string name,
      HtmlTextWriterTag wrapperTagKey,
      string commandName,
      string text,
      string cssClass)
    {
      CommandWidgetElement actionMenuCommand = ActionsMenuGenerator.CreateActionMenuCommand(parent, name, wrapperTagKey, commandName, text);
      actionMenuCommand.CssClass = cssClass;
      return actionMenuCommand;
    }

    /// <summary>Creates the action menu widget element.</summary>
    /// <param name="parent">The parent.</param>
    /// <param name="name">The name.</param>
    /// <param name="wrapperTagName">Name of the wrapper tag.</param>
    /// <param name="commandName">Name of the command.</param>
    /// <param name="text">The text.</param>
    /// <returns></returns>
    public static CommandWidgetElement CreateActionMenuCommand(
      ConfigElement parent,
      string name,
      HtmlTextWriterTag wrapperTagKey,
      string commandName,
      string text)
    {
      CommandWidgetElement actionMenuCommand = parent != null ? new CommandWidgetElement(parent) : throw new ArgumentNullException(nameof (parent));
      actionMenuCommand.Name = name;
      actionMenuCommand.WrapperTagKey = wrapperTagKey;
      actionMenuCommand.CommandName = commandName;
      actionMenuCommand.Text = text;
      actionMenuCommand.WidgetType = typeof (CommandWidget);
      return actionMenuCommand;
    }
  }
}
