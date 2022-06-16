// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.Services.CulturesMapProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Multisite.Web.Services
{
  internal class CulturesMapProperty : CalculatedProperty
  {
    public override Type ReturnType => typeof (CultureModel[]);

    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (Site key in items)
      {
        ISite siteById = SystemManager.CurrentContext.MultisiteContext.GetSiteById(key.Id);
        List<CultureModel> cultureModelList = new List<CultureModel>();
        foreach (CultureInfo publicContentCulture in siteById.PublicContentCultures)
          cultureModelList.Add(new CultureModel()
          {
            Name = publicContentCulture.Name,
            DisplayName = publicContentCulture.DisplayName
          });
        values.Add((object) key, (object) cultureModelList.ToArray());
      }
      return (IDictionary<object, object>) values;
    }
  }
}
