// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.MarketingProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  internal class MarketingProperty : CalculatedProperty
  {
    public override Type ReturnType => typeof (MarketingPropertyValue);

    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (PageNode pageNode in items.Cast<PageNode>())
      {
        PageViewModel viewModel = new PageViewModel(pageNode);
        values.Add((object) pageNode, (object) PageHelper.MapPageViewModelToMarketingProperty(viewModel));
      }
      return (IDictionary<object, object>) values;
    }
  }
}
