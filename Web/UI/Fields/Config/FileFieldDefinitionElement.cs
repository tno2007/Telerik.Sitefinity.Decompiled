// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.FileFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for file fields.</summary>
  public class FileFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IFileFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public FileFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new FileFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the name of the single item being managed by the file field control.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("itemName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FileFieldItemNameDescription", Title = "FileFieldItemNameCaption")]
    public string ItemName
    {
      get => (string) this["itemName"];
      set => this["itemName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the single item in plural being managed by the file field control.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("itemNamePlural")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FileFieldItemNamePluralDescription", Title = "FileFieldItemNamePluralCaption")]
    public string ItemNamePlural
    {
      get => (string) this["itemNamePlural"];
      set => this["itemNamePlural"] = (object) value;
    }

    /// <summary>Gets or sets the type of the library content.</summary>
    /// <value>The type of the library content.</value>
    [ConfigurationProperty("libraryContentType")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LibraryContentTypeDescription", Title = "LibraryContentTypeCaption")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type LibraryContentType
    {
      get => (Type) this["libraryContentType"];
      set => this["libraryContentType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the uploader allows multiple files to be selected.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("isMultiselect", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsMultiselectDescription", Title = "IsMultiselectCaption")]
    public bool IsMultiselect
    {
      get => (bool) this["isMultiselect"];
      set => this["isMultiselect"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the maximum allowed number of files selected in the uploader.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("maxFileCount", DefaultValue = 1)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MaxFileCountDescription", Title = "MaxFileCountCaption")]
    public int MaxFileCount
    {
      get => (int) this["maxFileCount"];
      set => this["maxFileCount"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      public const string ItemName = "itemName";
      public const string ItemNamePlural = "itemNamePlural";
      public const string LibraryContentType = "libraryContentType";
      public const string IsMultiselect = "isMultiselect";
      public const string MaxFileCount = "maxFileCount";
    }
  }
}
