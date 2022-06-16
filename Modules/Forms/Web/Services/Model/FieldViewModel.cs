// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.Services.Model.FieldViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Forms.Web.Services.Model
{
  /// <summary>
  /// Defines the properties for the form fields and is used for transferring the data through WCF.
  /// </summary>
  [KnownType(typeof (FieldViewModel))]
  [DataContract]
  public class FieldViewModel
  {
    private string key;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.Services.Model.FieldViewModel" /> class.
    /// </summary>
    public FieldViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.Services.Model.FieldViewModel" /> class.
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
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the key.</summary>
    /// <value>The key.</value>
    [DataMember]
    public string Key
    {
      get
      {
        this.key = this.Name ?? this.Title;
        return this.key;
      }
      set => this.key = value;
    }
  }
}
