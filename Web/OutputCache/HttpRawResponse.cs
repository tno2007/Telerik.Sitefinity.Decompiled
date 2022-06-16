// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.HttpRawResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Specialized;

namespace Telerik.Sitefinity.Web.OutputCache
{
  [Serializable]
  internal sealed class HttpRawResponse
  {
    public ArrayList Buffers { get; set; }

    public NameValueCollection Headers { get; set; }

    public int StatusCode { get; set; }

    public string StatusDescription { get; set; }

    public string SubstitutionInfo { get; set; }
  }
}
