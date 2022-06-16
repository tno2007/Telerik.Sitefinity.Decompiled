// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.UrlUtilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>Yet another helper class</summary>
  internal static class UrlUtilities
  {
    internal static string StripQueryString(string url, out string queryString)
    {
      int num = url.IndexOf("?");
      if (num > -1)
      {
        url = url.Substring(0, num);
        queryString = url.Substring(num);
      }
      else
        queryString = (string) null;
      return url;
    }

    internal static string StripQueryString(string url)
    {
      int length = url.IndexOf("?");
      if (length > -1)
        url = url.Substring(0, length);
      return url;
    }

    internal static bool IsInlineEditingUrl(string url) => UrlUtilities.IsActionUrl(url, "InEdit") && ControlExtensions.BrowseAndEditIsEnabled();

    internal static bool IsActionUrl(string url, string actionKey)
    {
      bool flag = false;
      int count = SystemManager.CurrentContext.AppSettings.Multilingual ? 3 : 2;
      string[] source = UrlUtilities.UrlPathSegments(url);
      if (source.Length >= count)
      {
        string[] array = ((IEnumerable<string>) source).Skip<string>(source.Length - count).Take<string>(count).ToArray<string>();
        if (array != null && array.Length > 1)
        {
          int num = -1;
          if ("Action".Equals(array[array.Length - 2], StringComparison.OrdinalIgnoreCase))
            num = array.Length - 2;
          else if (array.Length > 2 && "Action".Equals(array[array.Length - 3], StringComparison.OrdinalIgnoreCase))
            num = array.Length - 3;
          if (num > -1)
            flag = array[num + 1].Equals(actionKey, StringComparison.OrdinalIgnoreCase);
        }
      }
      return flag;
    }

    internal static string[] UrlPathSegments(string url)
    {
      url = ((IEnumerable<string>) url.Split('?')).First<string>();
      if (Uri.IsWellFormedUriString(url, UriKind.Relative))
        return url.Split(new char[1]{ '/' }, StringSplitOptions.RemoveEmptyEntries);
      return Uri.IsWellFormedUriString(url, UriKind.Absolute) ? new Uri(url).GetLeftPart(UriPartial.Path).Split(new char[1]
      {
        '/'
      }, StringSplitOptions.RemoveEmptyEntries) : throw new ArgumentException(string.Format("The URL: '{0}' is not valid.", (object) url));
    }
  }
}
