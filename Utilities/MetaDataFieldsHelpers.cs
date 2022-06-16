// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.SocialMediaSeoTagHelpers
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Utilities
{
  internal class SocialMediaSeoTagHelpers
  {
    private static readonly ICollection<string> SocialMediaFieldNames = (ICollection<string>) new HashSet<string>()
    {
      "OpenGraphDescription",
      "OpenGraphTitle",
      "OpenGraphImage",
      "OpenGraphVideo"
    };
    private static readonly ICollection<string> SeoFieldNames = (ICollection<string>) new HashSet<string>()
    {
      "MetaTitle",
      "MetaDescription"
    };

    internal static bool IsSeoTagField(string fieldName) => SocialMediaSeoTagHelpers.SeoFieldNames.Contains(fieldName);

    internal static bool IsSocialMediaTagField(string fieldName) => SocialMediaSeoTagHelpers.SocialMediaFieldNames.Contains(fieldName);

    internal static bool IsSocialMediaSeoField(string fieldName) => SocialMediaSeoTagHelpers.IsSeoTagField(fieldName) || SocialMediaSeoTagHelpers.IsSocialMediaTagField(fieldName);

    internal class SocialMediaSeoFieldNames
    {
      public const string MetaTitle = "MetaTitle";
      public const string MetaDescription = "MetaDescription";
      public const string OpenGraphDescription = "OpenGraphDescription";
      public const string OpenGraphTitle = "OpenGraphTitle";
      public const string OpenGraphImage = "OpenGraphImage";
      public const string OpenGraphVideo = "OpenGraphVideo";
    }
  }
}
