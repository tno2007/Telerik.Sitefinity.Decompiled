// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Data.MediaQueryLinkProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Data
{
  internal class MediaQueryLinkProxy : IMediaQueryLink
  {
    internal MediaQueryLinkProxy(IMediaQueryLink from, bool skipMediaQueries = false)
    {
      this.ItemId = from.ItemId;
      this.ItemType = from.ItemType;
      if (!skipMediaQueries)
      {
        List<IMediaQuery> mediaQueryList = new List<IMediaQuery>();
        foreach (IMediaQuery mediaQuery in from.MediaQueries)
          mediaQueryList.Add((IMediaQuery) new MediaQueryProxy(mediaQuery));
        this.MediaQueries = (IEnumerable<IMediaQuery>) mediaQueryList;
      }
      this.LinkType = from.LinkType;
    }

    public Guid ItemId { get; set; }

    public string ItemType { get; set; }

    public IEnumerable<IMediaQuery> MediaQueries { get; set; }

    public MediaQueryLinkType LinkType { get; set; }
  }
}
