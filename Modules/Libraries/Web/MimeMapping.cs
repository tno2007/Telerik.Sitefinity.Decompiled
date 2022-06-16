// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.MimeMapping
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.Web
{
  /// <summary>Mapping file extensions to a mime type</summary>
  internal class MimeMapping
  {
    private MimeMapping()
    {
    }

    internal static string GetMimeMapping(string extension, string defaultContentType = "application/octet-stream")
    {
      MimeMappingElement mimeMappingElement = (MimeMappingElement) null;
      return Config.Get<LibrariesConfig>().MimeMappings.TryGetValue(extension, out mimeMappingElement) && !mimeMappingElement.MimeType.IsNullOrEmpty() ? mimeMappingElement.MimeType : defaultContentType;
    }

    internal static string GetExtension(string mimeType)
    {
      foreach (MimeMappingElement mimeMappingElement in (IEnumerable<MimeMappingElement>) Config.Get<LibrariesConfig>().MimeMappings.Values)
      {
        if (mimeMappingElement.MimeType == mimeType)
          return mimeMappingElement.FileExtension;
      }
      return (string) null;
    }
  }
}
