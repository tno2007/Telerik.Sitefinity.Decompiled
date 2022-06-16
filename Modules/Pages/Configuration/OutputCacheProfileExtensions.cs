// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.OutputCacheProfileExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  internal static class OutputCacheProfileExtensions
  {
    internal static HttpCacheability GetCacheability(this ICacheProfile profile) => OutputCacheProfileExtensions.GetCacheability(profile.Location);

    internal static HttpCacheability GetCacheability(OutputCacheLocation location)
    {
      HttpCacheability cacheability;
      switch (location)
      {
        case OutputCacheLocation.Server:
          cacheability = HttpCacheability.Server;
          break;
        case OutputCacheLocation.Any:
          cacheability = HttpCacheability.Public;
          break;
        case OutputCacheLocation.Client:
          cacheability = HttpCacheability.Private;
          break;
        case OutputCacheLocation.ServerAndClient:
          cacheability = HttpCacheability.ServerAndPrivate;
          break;
        default:
          cacheability = HttpCacheability.NoCache;
          break;
      }
      return cacheability;
    }

    internal static ClientCacheControl ToClientCacheControl(
      this IOutputCacheProfile profile,
      bool clientCacheIsGloballyDisabled = false)
    {
      if (clientCacheIsGloballyDisabled)
        return ClientCacheControl.NoCache;
      int cacheability = (int) profile.GetCacheability();
      int duration = profile.Duration;
      int? nullable = profile.ClientMaxAge;
      if (nullable.HasValue)
      {
        nullable = profile.ClientMaxAge;
        duration = nullable.Value;
      }
      TimeSpan timeSpan1 = TimeSpan.FromSeconds((double) duration);
      nullable = profile.ProxyMaxAge;
      TimeSpan timeSpan2;
      if (!nullable.HasValue)
      {
        timeSpan2 = new TimeSpan();
      }
      else
      {
        nullable = profile.ProxyMaxAge;
        timeSpan2 = TimeSpan.FromSeconds((double) nullable.Value);
      }
      TimeSpan timeSpan3 = timeSpan2;
      TimeSpan maxAge = timeSpan1;
      TimeSpan proxyMaxAge = timeSpan3;
      return new ClientCacheControl((HttpCacheability) cacheability, maxAge, proxyMaxAge);
    }

    internal static int GetMaxAge(this IOutputCacheProfile profile)
    {
      if (!profile.IsClient())
        return 0;
      return !profile.ClientMaxAge.HasValue ? profile.Duration : profile.ClientMaxAge.Value;
    }

    internal static bool IsClient(this IOutputCacheProfile profile)
    {
      OutputCacheLocation location = profile.Location;
      switch (location)
      {
        case OutputCacheLocation.Any:
        case OutputCacheLocation.Client:
          return true;
        default:
          return location == OutputCacheLocation.ServerAndClient;
      }
    }

    internal static bool IsProxy(this IOutputCacheProfile profile) => profile.Location == OutputCacheLocation.Any;

    internal static bool TryGetNoStore(this IOutputCacheProfile profile)
    {
      bool flag;
      return profile.TryGetBoolParam("setNoStore", out flag) & flag;
    }

    internal static bool TryGetVaryByUserAgent(this IOutputCacheProfile profile, out bool value) => profile.TryGetBoolParam("varyByUserAgent", out value);

    internal static bool TryGetVaryByHost(this IOutputCacheProfile profile, out bool value) => profile.TryGetBoolParam("varyByHost", out value);

    internal static bool TryGetOmitVaryStar(this IOutputCacheProfile profile, out bool value) => profile.TryGetBoolParam("setOmitVaryStar", out value);

    internal static bool VaryByParams(this IOutputCacheProfile profile, out string[] value)
    {
      value = new string[0];
      string str;
      if (!profile.TryGetStringParam("varyByParams", out str))
        return false;
      value = str.Split(' ', ';', ',');
      return true;
    }

    internal static bool SetRevalidation(
      this IOutputCacheProfile profile,
      out HttpCacheRevalidation value)
    {
      return profile.TryGetEnumParam<HttpCacheRevalidation>("setRevalidation", out value);
    }

    internal static bool TryGetBoolParam(
      this IOutputCacheProfile profile,
      string name,
      out bool value)
    {
      string parameter = profile.Parameters[name];
      if (parameter != null && bool.TryParse(parameter, out value))
        return true;
      value = false;
      return false;
    }

    internal static bool TryGetIntParam(
      this IOutputCacheProfile profile,
      string name,
      out int value)
    {
      string parameter = profile.Parameters[name];
      if (parameter != null && int.TryParse(parameter, out value))
        return true;
      value = 0;
      return false;
    }

    internal static bool TryGetStringParam(
      this IOutputCacheProfile profile,
      string name,
      out string value)
    {
      value = profile.Parameters[name];
      if (value != null)
        return true;
      value = (string) null;
      return false;
    }

    internal static bool TryGetEnumParam<TEnum>(
      this IOutputCacheProfile profile,
      string name,
      out TEnum value)
      where TEnum : struct
    {
      string parameter = profile.Parameters[name];
      if (parameter != null && Enum.TryParse<TEnum>(parameter, out value))
        return true;
      value = default (TEnum);
      return false;
    }
  }
}
