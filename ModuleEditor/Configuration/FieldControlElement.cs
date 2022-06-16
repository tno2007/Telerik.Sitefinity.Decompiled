// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Configuration.FieldControlElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.ModuleEditor.Configuration
{
  /// <summary>Represents a configuration element for field type</summary>
  public class FieldControlElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Configuration.FieldControlElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public FieldControlElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Configuration.FieldControlElement" /> class.
    /// </summary>
    internal FieldControlElement()
      : base(false)
    {
    }

    /// <summary>Gets or sets the type of the field.</summary>
    /// <value>The type of the field.</value>
    [ConfigurationProperty("fieldType", IsKey = true, IsRequired = true)]
    public string FieldTypeOrPath
    {
      get => (string) this["fieldType"];
      set => this["fieldType"] = (object) value;
    }

    /// <summary>Gets or sets the title of the field.</summary>
    /// <value>The title of the field.</value>
    [ConfigurationProperty("title", DefaultValue = "", IsRequired = true)]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>Gets or sets the type of the field designer.</summary>
    /// <value>The type of the field designer.</value>
    [ConfigurationProperty("designerType", IsRequired = true)]
    public string DesignerType
    {
      get => (string) this["designerType"];
      set => this["designerType"] = (object) value;
    }

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

    /// <summary>
    /// Gets a value indicating whether FieldTypeOrPath returns a path to user control or type.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is user control; otherwise, <c>false</c>.
    /// </value>
    public bool IsUserControl => this.FieldTypeOrPath.StartsWith("~");

    internal IField GetFieldControl() => !this.IsUserControl ? (IField) Activator.CreateInstance(TypeResolutionService.ResolveType(this.FieldTypeOrPath)) : (IField) ControlUtilities.LoadControl(this.FieldTypeOrPath);

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string title = "title";
      public const string fieldType = "fieldType";
      public const string designerType = "designerType";
      public const string resourceClassId = "resourceClassId";
    }
  }
}
