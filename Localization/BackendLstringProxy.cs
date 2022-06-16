// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.BackendLstringProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;

namespace Telerik.Sitefinity.Localization
{
  internal class BackendLstringProxy : LstringProxy
  {
    public BackendLstringProxy(string monoVal)
      : base(monoVal)
    {
    }

    public override bool TryGetValue(out string value, CultureInfo culture = null)
    {
      int num = base.TryGetValue(out value, (CultureInfo) null) ? 1 : 0;
      value = Res.GetEvaledExpression(value, culture, true);
      return num != 0;
    }
  }
}
