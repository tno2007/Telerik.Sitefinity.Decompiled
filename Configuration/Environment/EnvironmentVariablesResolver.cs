// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Environment.EnvironmentVariablesResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Configuration.Environment
{
  internal class EnvironmentVariablesResolver : SecretDataResolver
  {
    public override SecretDataMode Mode => SecretDataMode.Link;

    public override bool IsReadOnly => true;

    public override string Resolve(string key) => EnvironmentVariables.Current.GetSetting(key);

    public override string GenerateKey(string value) => value;
  }
}
