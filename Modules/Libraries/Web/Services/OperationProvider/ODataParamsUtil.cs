// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider.ODataParamsUtil
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider
{
  internal static class ODataParamsUtil
  {
    internal static T GetQueryParam<T>(IDictionary<string, string> queryParams, string paramName)
    {
      string input = (string) null;
      T queryParam = default (T);
      if (queryParams.TryGetValue(paramName, out input))
        queryParam = ODataParamsUtil.TryConvert<T>(input);
      return queryParam;
    }

    private static T TryConvert<T>(string input)
    {
      try
      {
        TypeConverter converter = TypeDescriptor.GetConverter(typeof (T));
        return converter != null ? (T) converter.ConvertFromString(input) : default (T);
      }
      catch (FormatException ex)
      {
        return default (T);
      }
      catch (NotSupportedException ex)
      {
        return default (T);
      }
    }
  }
}
