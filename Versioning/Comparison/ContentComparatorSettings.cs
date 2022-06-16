// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Comparison.ContentComparatorSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Versioning.Comparison
{
  /// <summary>
  /// 
  /// </summary>
  public class ContentComparatorSettings
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Versioning.Comparison.ContentComparatorSettings" /> class.
    /// </summary>
    public ContentComparatorSettings() => this.Init();

    private void Init()
    {
      this.DateTimeDisplayFormat = "G";
      this.BoolDisplayFormat = "true|false";
      this.IntegerDisplayFormat = "G";
      this.DecimalDisplayFormat = "G";
      this.DoubleDisplayFormat = "G";
      this.DeletedCSSClassName = "diff_deleted";
      this.AddedCSSClassName = "diff_new";
    }

    /// <summary>Display format of the DateTime data type</summary>
    public string DateTimeDisplayFormat { get; set; }

    /// <summary>Gets or sets the bool display format.</summary>
    /// <value>The bool display format.</value>
    public string BoolDisplayFormat { get; set; }

    /// <summary>Gets or sets the integer display format.</summary>
    /// <value>The integer display format.</value>
    public string IntegerDisplayFormat { get; set; }

    /// <summary>Gets or sets the decimal display format.</summary>
    /// <value>The decimal display format.</value>
    public string DecimalDisplayFormat { get; set; }

    /// <summary>Gets or sets the double display format.</summary>
    /// <value>The double display format.</value>
    public string DoubleDisplayFormat { get; set; }

    /// <summary>Gets or sets the name of the deleted CSS class.</summary>
    /// <value>The name of the deleted CSS class.</value>
    public string DeletedCSSClassName { get; set; }

    /// <summary>Gets or sets the name of the added CSS class.</summary>
    /// <value>The name of the added CSS class.</value>
    public string AddedCSSClassName { get; set; }

    internal bool EncodeStrings { get; set; }

    internal string BeginTagFormat { get; set; }

    internal string EndTagFormat { get; set; }
  }
}
