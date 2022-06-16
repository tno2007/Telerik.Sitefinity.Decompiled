// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.Designers.NullableExtension
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Windows.Markup;

namespace Telerik.Sitefinity.Workflow.Activities.Designers
{
  /// <summary>
  /// Allows to specify nullable types in XAML for xaml expression types
  /// <example>
  /// ExpressionType="{local:NullableExtension sys:Int32}"
  /// </example>
  /// </summary>
  public class NullableExtension : TypeExtension
  {
    public NullableExtension()
    {
    }

    public NullableExtension(string type)
      : base(type)
    {
    }

    public NullableExtension(Type type)
      : base(type)
    {
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => (object) typeof (Nullable<>).MakeGenericType((Type) base.ProvideValue(serviceProvider));
  }
}
