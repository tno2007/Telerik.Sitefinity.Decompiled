// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.ISiteSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.SiteSettings
{
  internal interface ISiteSettings
  {
    TContract GetSetting<TContract>(string policyType, string policyName) where TContract : ISettingsDataContract;

    TResult GetSetting<TContract, TResult>(string policyType, string policyName) where TContract : ISettingsDataContract, TResult;

    object GetSetting(Type contractType, string policyType, string policyName);
  }
}
