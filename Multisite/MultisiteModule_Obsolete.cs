// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.MultisiteModule_Obsolete
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Multisite
{
  internal sealed class MultisiteModule_Obsolete : ModuleBase
  {
    internal const string ModuleName = "Multisite";

    public override Guid LandingPageId => Guid.Empty;

    public override Type[] Managers => (Type[]) null;

    public override void Install(SiteInitializer initializer)
    {
    }

    protected override ConfigSection GetModuleConfig() => (ConfigSection) null;
  }
}
