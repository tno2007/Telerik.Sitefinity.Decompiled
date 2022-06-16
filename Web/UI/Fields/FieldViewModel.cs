// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.FieldViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>Data transfer object for the forms field</summary>
  public class FieldViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.FieldViewModel" /> class.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="name">The name.</param>
    public FieldViewModel(string title, string name)
    {
      this.Title = title;
      this.Name = name;
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the name (the value saved in the configuration).
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>Gets the name (the value saved in the configuration).</summary>
    /// <value>The name.</value>
    public string Key => this.Name ?? this.Title;
  }
}
