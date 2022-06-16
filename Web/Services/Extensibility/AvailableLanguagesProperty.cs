// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.AvailableLanguagesProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// A property for retrieving in which languages ILocalizable item is available.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class AvailableLanguagesProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (IEnumerable<string>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> ret = new Dictionary<object, object>();
      foreach (ILocalizable localizable in items.Cast<ILocalizable>().ToList<ILocalizable>())
      {
        ILocalizable item = localizable;
        CommonMethods.ExecuteMlLogic<object>((Action<object>) (itemInner => ret.Add((object) item, (object) new List<string>()
        {
          SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name
        })), (Action<object>) (itemInner => ret.Add((object) item, (object) ((IEnumerable<string>) item.AvailableLanguages).Where<string>((Func<string, bool>) (e => !string.IsNullOrEmpty(e))).ToList<string>())), (Action<object>) (itemInner => ret.Add((object) item, (object) ((IEnumerable<string>) item.AvailableLanguages).Where<string>((Func<string, bool>) (e => !string.IsNullOrEmpty(e))).ToList<string>())));
      }
      return (IDictionary<object, object>) ret;
    }
  }
}
