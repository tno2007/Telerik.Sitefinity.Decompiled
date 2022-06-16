// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Data.NavigationTransformationProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Data
{
  internal class NavigationTransformationProxy : INavigationTransformation
  {
    public NavigationTransformationProxy(INavigationTransformation source, IMediaQuery parent)
    {
      this.CssClasses = source.CssClasses;
      this.TransformationName = source.TransformationName;
      this.ParentMediaQueryId = source.ParentMediaQueryId;
      this.Parent = parent;
    }

    public string CssClasses { get; private set; }

    public string TransformationName { get; private set; }

    public Guid ParentMediaQueryId { get; private set; }

    public IMediaQuery Parent { get; private set; }
  }
}
