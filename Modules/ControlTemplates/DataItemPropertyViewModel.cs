// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.DataItemPropertyViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.ControlTemplates
{
  /// <summary>View model for properties of the data item.</summary>
  [DataContract]
  public class DataItemPropertyViewModel
  {
    private string name;
    private string displayName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ControlTemplates.DataItemPropertyViewModel" /> class.
    /// </summary>
    public DataItemPropertyViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ControlTemplates.DataItemPropertyViewModel" /> class.
    /// </summary>
    /// <param name="property">The property.</param>
    public DataItemPropertyViewModel(PropertyDescriptor property) => this.Property = property;

    /// <summary>Gets or sets the property.</summary>
    /// <value>The property.</value>
    public PropertyDescriptor Property { get; set; }

    /// <summary>Gets or sets the name of the property.</summary>
    /// <value>The name of the property.</value>
    [DataMember]
    public string Name
    {
      get => this.Property != null ? this.Property.Name : this.name;
      set => this.name = value;
    }

    /// <summary>Gets or sets the display name of the property.</summary>
    /// <value>The display name of the property.</value>
    [DataMember]
    public string DisplayName
    {
      get
      {
        string empty = string.Empty;
        string displayName = this.Property == null ? this.displayName : this.Property.Name;
        if (displayName.Length > 23)
          displayName = displayName.Substring(0, 23) + "...";
        return displayName;
      }
      set => this.displayName = value;
    }

    /// <summary>
    /// Contains the markup that represents the property in a control template. Used by the template editor for non-trivial properties
    /// </summary>
    [DataMember]
    public string ControlTag { get; set; }
  }
}
