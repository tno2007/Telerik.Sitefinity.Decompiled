// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyLoaders.NotTranslatableLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.PropertyLoaders
{
  internal class NotTranslatableLoader : PropertyLoader
  {
    public NotTranslatableLoader(ObjectData source)
      : base(source)
    {
    }

    internal override IEnumerable<ControlProperty> GetProperties(
      CultureInfo language = null,
      bool fallbackToAnyLanguage = false)
    {
      return this.Source.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == null));
    }

    internal override IEnumerable<ControlProperty> ClearProperties(
      object component = null,
      CultureInfo language = null)
    {
      List<ControlProperty> controlPropertyList = new List<ControlProperty>((IEnumerable<ControlProperty>) this.Source.Properties);
      this.Source.Properties.Clear();
      return (IEnumerable<ControlProperty>) controlPropertyList;
    }
  }
}
