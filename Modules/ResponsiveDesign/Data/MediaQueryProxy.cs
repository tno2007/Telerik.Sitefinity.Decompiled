// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Data.MediaQueryProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Data
{
  internal class MediaQueryProxy : IMediaQuery
  {
    public MediaQueryProxy(IMediaQuery from)
    {
      this.Id = from.Id;
      this.Name = from.Name;
      this.Behavior = from.Behavior;
      this.AdditionalCssFilePath = from.AdditionalCssFilePath;
      this.LayoutTransformationsJSON = from.LayoutTransformationsJSON;
      this.MiniSitePageId = from.MiniSitePageId;
      List<IMediaQueryRule> mediaQueryRuleList = new List<IMediaQueryRule>();
      foreach (IMediaQueryRule mediaQueryRule in from.MediaQueryRules)
        mediaQueryRuleList.Add((IMediaQueryRule) new MediaQueryRuleProxy(mediaQueryRule));
      this.MediaQueryRules = (IEnumerable<IMediaQueryRule>) mediaQueryRuleList;
      List<INavigationTransformation> navigationTransformationList = new List<INavigationTransformation>();
      foreach (INavigationTransformation navigationTransformation in from.NavigationTransformations)
        navigationTransformationList.Add((INavigationTransformation) new NavigationTransformationProxy(navigationTransformation, (IMediaQuery) this));
      this.NavigationTransformations = (IEnumerable<INavigationTransformation>) navigationTransformationList;
      this.IsActive = from.IsActive;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public ResponsiveDesignBehavior Behavior { get; private set; }

    public string AdditionalCssFilePath { get; private set; }

    public string LayoutTransformationsJSON { get; private set; }

    public Guid MiniSitePageId { get; private set; }

    public IEnumerable<IMediaQueryRule> MediaQueryRules { get; private set; }

    public IEnumerable<INavigationTransformation> NavigationTransformations { get; private set; }

    public bool IsActive { get; private set; }
  }
}
