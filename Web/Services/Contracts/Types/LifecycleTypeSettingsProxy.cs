// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.LifecycleTypeSettingsProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class LifecycleTypeSettingsProxy : 
    TypeSettingsProxy,
    ILifecycleTypeSettings,
    ITypeSettings
  {
    private LifecycleStatus status;

    public LifecycleTypeSettingsProxy() => this.Status = LifecycleStatus.Live;

    public LifecycleTypeSettingsProxy(ILifecycleTypeSettings settings, Permissions? access)
      : base((ITypeSettings) settings, access)
    {
      this.Status = settings.Status;
    }

    public LifecycleStatus Status
    {
      get => this.status;
      set
      {
        this.status = value;
        this.ModifyId();
      }
    }

    private void ModifyId()
    {
      if (!(this.Properties.Where<IPropertyMapping>((Func<IPropertyMapping, bool>) (p => p.Name == "Id")).FirstOrDefault<IPropertyMapping>() is PersistentPropertyMappingProxy propertyMappingProxy))
        return;
      Type c = TypeResolutionService.ResolveType(this.ClrType, false);
      if (this.Status == LifecycleStatus.Live && !typeof (ILifecycleDataItemDraft).IsAssignableFrom(c) && !typeof (ILifecycleDataItemLive).IsAssignableFrom(c))
        propertyMappingProxy.PersistentName = LinqHelper.MemberName<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, object>>) (x => (object) x.OriginalContentId));
      else
        propertyMappingProxy.PersistentName = LinqHelper.MemberName<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, object>>) (x => (object) x.Id));
    }
  }
}
