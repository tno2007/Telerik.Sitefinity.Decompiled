// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.BaseControlsContainerWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Claims;

namespace Telerik.Sitefinity.Modules.Pages
{
  internal abstract class BaseControlsContainerWrapper : IControlsContainer, IRendererCommonData
  {
    private IControlsContainer container;
    private PageDataProvider provider;
    private IEnumerable<ControlData> controls;
    private bool fetchAll;

    internal BaseControlsContainerWrapper(
      IControlsContainer container,
      PageDataProvider pageProvider,
      bool fetchAll = false)
    {
      this.container = container;
      this.provider = pageProvider;
      this.fetchAll = fetchAll;
    }

    [SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable")]
    public virtual IEnumerable<ControlData> Controls
    {
      get
      {
        if (this.controls == null)
        {
          IQueryable<ControlData> queryable = this.GetControls();
          if (!ClaimsManager.IsUnrestricted() || this.fetchAll)
            queryable = queryable.Include<ControlData>((Expression<Func<ControlData, object>>) (x => x.Permissions));
          this.controls = (IEnumerable<ControlData>) queryable.ToList<ControlData>();
        }
        return this.controls;
      }
    }

    public int LastControlId
    {
      get => this.container.LastControlId;
      set => this.container.LastControlId = value;
    }

    public int Version
    {
      get => this.container.Version;
      set => this.container.Version = value;
    }

    public Guid Id => this.container.Id;

    public string Renderer
    {
      get => (this.container as IRendererCommonData).Renderer;
      set => (this.container as IRendererCommonData).Renderer = value;
    }

    public string TemplateName
    {
      get => (this.container as IRendererCommonData).TemplateName;
      set => (this.container as IRendererCommonData).TemplateName = value;
    }

    internal PageDataProvider PageProvider
    {
      get
      {
        if (this.provider == null)
          this.provider = PageManager.GetManager().Provider;
        return this.provider;
      }
    }

    internal IControlsContainer Container => this.container;

    internal abstract IQueryable<ControlData> GetControls();
  }
}
