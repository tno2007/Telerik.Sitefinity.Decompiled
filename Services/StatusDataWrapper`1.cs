// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.StatusDataWrapper`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.Script.Serialization;

namespace Telerik.Sitefinity.Services
{
  internal abstract class StatusDataWrapper<TData>
  {
    [ScriptIgnore]
    private IStatusProvider provider;
    [ScriptIgnore]
    private TData data;

    protected StatusDataWrapper(IStatusProvider provider, TData data)
    {
      this.provider = provider;
      this.data = data;
    }

    [ScriptIgnore]
    public IStatusProvider Provider => this.provider;

    [ScriptIgnore]
    public TData Data => this.data;

    public int Priority => this.provider.Priority;

    public string Source => this.provider.Name;
  }
}
