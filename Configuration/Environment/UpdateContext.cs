// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Environment.UpdateContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Configuration.Environment
{
  internal class UpdateContext
  {
    private Variables change;
    private EnvironmentVariables parent;

    public UpdateContext(EnvironmentVariables parent) => this.parent = parent;

    public UpdateContext ConnectionString(string key, string value)
    {
      this.Change.AddConnectionString(key, value);
      return this;
    }

    public UpdateContext Setting(string key, string value)
    {
      this.Change.AddSetting(key, value);
      return this;
    }

    public UpdateContext ValidationKey(string value) => this.Setting(nameof (ValidationKey), value);

    public UpdateContext DecryptionKey(string value) => this.Setting(nameof (DecryptionKey), value);

    public EnvironmentVariables Save()
    {
      if (this.change != null)
      {
        this.parent.Provider.UpdateVariables(this.change);
        this.change = (Variables) null;
      }
      return this.parent;
    }

    protected Variables Change
    {
      get
      {
        if (this.change == null)
          this.change = new Variables();
        return this.change;
      }
    }
  }
}
