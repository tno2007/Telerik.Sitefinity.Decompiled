// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.Designers.PlaceHolderNameToStringConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace Telerik.Sitefinity.Workflow.Activities.Designers
{
  public class PlaceHolderNameToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (object) value.ToString();

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return (object) (PlaceholderName) Enum.Parse(typeof (PlaceholderName), value.ToString(), true);
    }
  }
}
