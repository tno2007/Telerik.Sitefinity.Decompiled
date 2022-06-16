// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Converters.ComboBoxToArgConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.VisualBasic.Activities;
using System;
using System.Activities;
using System.Activities.Expressions;
using System.Activities.Presentation.Model;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Telerik.Sitefinity.Workflow.Converters
{
  public class ComboBoxToArgConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      ModelItem modelItem = value as ModelItem;
      if (value != null && modelItem.GetCurrentValue() is InArgument<string> currentValue)
      {
        Activity<string> expression = currentValue.Expression;
        VisualBasicValue<string> visualBasicValue = expression as VisualBasicValue<string>;
        if (expression is Literal<string> literal)
          return (object) literal.Value.Trim('"');
        if (visualBasicValue != null)
          return (object) visualBasicValue.ExpressionText.Trim('"');
      }
      return (object) null;
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      try
      {
        return (object) new InArgument<string>((Activity<string>) new VisualBasicValue<string>("\"" + value.ToString() + "\""));
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("ConvertBack Exception " + ex.Message);
        return (object) "exception";
      }
    }
  }
}
