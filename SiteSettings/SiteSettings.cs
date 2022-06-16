// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.SiteSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Reflection;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.SiteSettings
{
  internal class SiteSettings : ISiteSettings
  {
    public TContract GetSetting<TContract>(string policyType, string policyName) where TContract : ISettingsDataContract => this.GetSetting<TContract, TContract>(policyType, policyName);

    public TResult GetSetting<TContract, TResult>(string policyType, string policyName) where TContract : ISettingsDataContract, TResult
    {
      TResult setting = (TResult) this.GetSetting(typeof (TContract), policyType, policyName);
      if ((object) setting != null)
        return setting;
      if (Attribute.GetCustomAttribute((MemberInfo) typeof (TResult), typeof (ConfigSettingsAttribute)) is ConfigSettingsAttribute customAttribute)
        return (TResult) Config.GetByPath<ConfigElement>(customAttribute.ConfigPath);
      TContract instance = Activator.CreateInstance<TContract>();
      instance.LoadDefaults(false);
      return (TResult) instance;
    }

    public object GetSetting(Type contractType, string policyType, string policyName) => SiteSettingsManager.GetSetting(contractType, policyType, policyName);
  }
}
