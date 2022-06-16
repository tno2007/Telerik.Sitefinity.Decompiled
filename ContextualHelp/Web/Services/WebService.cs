// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContextualHelp.Web.Services.WebService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json.Linq;
using ServiceStack;
using System;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.ContextualHelp.Web.Services.DTO.Request;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services.ServiceStack.Filters;

namespace Telerik.Sitefinity.ContextualHelp.Web.Services
{
  /// <summary>
  /// This class is a <see cref="T:ServiceStack.Service" /> containing operations related to the Sitefinity contextual help.
  /// </summary>
  /// <seealso cref="T:ServiceStack.Service" />
  [RequestBackendAuthenticationFilter(false)]
  [DisableClientCacheResponseFilter]
  [AddHeader(ContentType = "application/json")]
  internal class WebService : Service
  {
    public const string ContextualHelpPreferenceKey = "contextual-help";
    public const string IsContextualHelpEnabledKey = "onboardingTips";

    public object Get(GetTooltips data)
    {
      UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(ClaimsManager.GetCurrentUserId());
      JObject jobject = new JObject();
      if (!string.IsNullOrEmpty(cachedUserProfile.Preferences))
        jobject = JObject.Parse(cachedUserProfile.Preferences);
      JToken jtoken;
      jobject.TryGetValue("contextual-help", out jtoken);
      if (jtoken == null)
        jtoken = JToken.FromObject((object) string.Empty);
      bool result;
      bool.TryParse(HttpUtility.HtmlEncode(cachedUserProfile.GetPreference<string>("onboardingTips", "true")), out result);
      return (object) new
      {
        ids = jtoken.ToString(),
        isEnabled = result
      };
    }

    /// <summary>Update the value for the user-specific tooltip data.</summary>
    /// <param name="data">The data.</param>
    public void Post(MarkTooltips data)
    {
      string str = string.Join(",", data.Ids.Where<string>((Func<string, bool>) (id => !string.IsNullOrWhiteSpace(id))));
      UserProfilesHelper.GetUserProfileManager(typeof (SitefinityProfile)).SetPreference<string>(ClaimsManager.GetCurrentUserId(), "contextual-help", (object) str);
    }
  }
}
