// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration.ICommentsMasterViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration
{
  internal interface ICommentsMasterViewDefinition : IViewDefinition, IDefinition
  {
    IWidgetBarDefinition Toolbar { get; }

    IWidgetBarDefinition Sidebar { get; }

    /// <summary>
    /// Gets the collection of dialog definitions that are used on the view.
    /// </summary>
    List<IDialogDefinition> Dialogs { get; }

    /// <summary>
    /// Gets the collection of decision screen definitions that are used on the view.
    /// </summary>
    List<IDecisionScreenDefinition> DecisionScreens { get; }
  }
}
