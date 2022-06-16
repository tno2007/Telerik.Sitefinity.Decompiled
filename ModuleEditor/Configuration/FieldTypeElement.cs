// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Configuration.FieldTypeElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.ModuleEditor.Configuration
{
  /// <summary>Represents a configuration element for field type</summary>
  public class FieldTypeElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Configuration.FieldTypeElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public FieldTypeElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Configuration.FieldTypeElement" /> class.
    /// </summary>
    internal FieldTypeElement()
      : base(false)
    {
    }

    /// <summary>Gets or sets the name of the field.</summary>
    /// <value>The name of the field.</value>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the title of the field.</summary>
    /// <value>The title of the field.</value>
    [ConfigurationProperty("title", DefaultValue = "", IsRequired = true)]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Gets the controls.</summary>
    /// <value>The controls.</value>
    [ConfigurationProperty("controls")]
    public ConfigElementList<FieldControlElement> Controls => (ConfigElementList<FieldControlElement>) this["controls"];

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The resource class pageId.</value>
    [ConfigurationProperty("resourceClassId", DefaultValue = "", IsRequired = true)]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string name = "name";
      public const string title = "title";
      public const string controls = "controls";
      public const string resourceClassId = "resourceClassId";
    }
  }
}
