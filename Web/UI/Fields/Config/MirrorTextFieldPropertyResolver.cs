// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.MirrorTextFieldPropertyResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  internal class MirrorTextFieldPropertyResolver : PropertyResolverBase
  {
    private static readonly string[] supportedProps = new string[4]
    {
      "regularExpressionFilter",
      "replaceWith",
      "toLower",
      "trim"
    };

    public override T ResolveProperty<T>(string propertyName) => ((IEnumerable<string>) MirrorTextFieldPropertyResolver.supportedProps).Contains<string>(propertyName) ? (T) Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().SiteUrlSettings.ClientUrlTransformations[propertyName] : default (T);
  }
}
