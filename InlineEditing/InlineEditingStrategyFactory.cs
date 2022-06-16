﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.InlineEditingStrategyFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.InlineEditing.Strategies;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.InlineEditing
{
  /// <inheritdoc />
  public class InlineEditingStrategyFactory : IInlineEditingStrategyFactory
  {
    /// <inheritdoc />
    public virtual IInlineEditingStrategy GetStrategy(Type itemType) => !typeof (ControlData).IsAssignableFrom(itemType) ? (!typeof (IApprovalWorkflowItem).IsAssignableFrom(itemType) ? ObjectFactory.Resolve<IInlineEditingStrategy>(typeof (GenericItemStrategy).Name) : ObjectFactory.Resolve<IInlineEditingStrategy>(typeof (WorkflowItemStrategy).Name)) : ObjectFactory.Resolve<IInlineEditingStrategy>(typeof (PageControlStrategy).Name);
  }
}
