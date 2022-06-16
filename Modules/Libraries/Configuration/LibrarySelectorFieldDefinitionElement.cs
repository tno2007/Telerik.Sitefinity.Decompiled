// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.LibrarySelectorFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Web.UI;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Config;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// The configuration element for library selector fields.
  /// </summary>
  public class LibrarySelectorFieldDefinitionElement : 
    FieldControlDefinitionElement,
    ILibrarySelectorFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public LibrarySelectorFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new LibrarySelectorFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the text field.
    /// </summary>
    [ConfigurationProperty("expandableDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ExpandableControlElementDescription", Title = "ExpandableControlElementCaption")]
    public new ExpandableControlElement ExpandableDefinitionConfig
    {
      get => (ExpandableControlElement) this["expandableDefinition"];
      set => this["expandableDefinition"] = (object) value;
    }

    /// <summary>Gets or sets the type of the content view.</summary>
    /// <value>The type of the content.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewControlContentTypeDescription", Title = "ContentViewControlContentType")]
    [ConfigurationProperty("contentType")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type ContentType
    {
      get => (Type) this["contentType"];
      set => this["contentType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether to get system libraries or general libraries.
    /// </summary>
    /// <value>True if only system libraries are to be shown false if general libraries are to be shown </value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowOnlySystemLibrariesDescription", Title = "ShowOnlySystemLibrariesCaption")]
    [ConfigurationProperty("showOnlySystemLibraries")]
    public bool ShowOnlySystemLibraries
    {
      get => (bool) this["showOnlySystemLibraries"];
      set => this["showOnlySystemLibraries"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the sort expression for the list of items
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("sortExpression")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SortExpressionDescription", Title = "SortExpressionCaption")]
    public string SortExpression
    {
      get => (string) this["sortExpression"];
      set => this["sortExpression"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the text field.
    /// </summary>
    /// <value></value>
    public IExpandableControlDefinition ExpandableDefinition => (IExpandableControlDefinition) this.ExpandableDefinitionConfig;

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (LibrarySelectorField);

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      public const string ExpandableDefinition = "expandableDefinition";
    }
  }
}
