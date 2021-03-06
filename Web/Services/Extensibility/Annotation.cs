// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.Annotation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A DTO that holds the annotation value.</summary>
  public class Annotation
  {
    private object value;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.Extensibility.Annotation" /> class.
    /// </summary>
    /// <param name="val">The value.</param>
    public Annotation(object val) => this.value = val;

    /// <summary>Gets the value.</summary>
    public object Value => this.value;
  }
}
