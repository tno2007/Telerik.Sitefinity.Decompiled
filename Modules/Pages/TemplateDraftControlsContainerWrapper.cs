﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.TemplateDraftControlsContainerWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages
{
  internal class TemplateDraftControlsContainerWrapper : BaseControlsContainerWrapper
  {
    internal TemplateDraftControlsContainerWrapper(
      IControlsContainer container,
      PageDataProvider pageProvider,
      bool fetchAll = false)
      : base(container, pageProvider, fetchAll)
    {
    }

    internal override IQueryable<ControlData> GetControls() => (IQueryable<ControlData>) this.PageProvider.GetControls<TemplateDraftControl>().Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (x => x.Page.Id == this.Container.Id)).Include<TemplateDraftControl>((Expression<Func<TemplateDraftControl, object>>) (x => x.Properties));
  }
}
