// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.BulkEditFieldElement
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
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// Providers the configuration element for BulkEditFieldControl
  /// </summary>
  public class BulkEditFieldElement : 
    CompositeFieldElement,
    IBulkEditFieldDefinition,
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.BulkEditFieldElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public BulkEditFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.BulkEditFieldElement" /> class.
    /// </summary>
    internal BulkEditFieldElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new BulkEditFieldDefinition((ConfigElement) this);

    /// <summary>Gets or sets the name of the field control template.</summary>
    /// <value>The name of the template.</value>
    [ConfigurationProperty("templateName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BulkEditFieldDefinitionTemplateNameDescription", Title = "BulkEditFieldDefinitionTemplateNameCaption")]
    public string TemplateName
    {
      get => (string) this["templateName"];
      set => this["templateName"] = (object) value;
    }

    /// <summary>Gets or sets the name of the field control template.</summary>
    /// <value>The name of the template.</value>
    [ConfigurationProperty("templatePath")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BulkEditFieldDefinitionTemplatePathDescription", Title = "BulkEditFieldDefinitionTemplatePathCaption")]
    public string TemplatePath
    {
      get => (string) this["templatePath"];
      set => this["templatePath"] = (object) value;
    }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    [ConfigurationProperty("webServiceUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WebServiceUrlDescription", Title = "WebServiceUrlCaption")]
    public string WebServiceUrl
    {
      get => (string) this["webServiceUrl"];
      set => this["webServiceUrl"] = (object) value;
    }

    /// <summary>Gets or sets the type of the content view.</summary>
    /// <value>The type of the content.</value>
    [ConfigurationProperty("contentType")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewControlContentTypeDescription", Title = "ContentViewControlContentType")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type ContentType
    {
      get => (Type) this["contentType"];
      set => this["contentType"] = (object) value;
    }

    /// <summary>Gets or sets the type of the parent.</summary>
    /// <value>The type of the parent.</value>
    [ConfigurationProperty("parentType")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ParentTypeDescription", Title = "ParentTypeCaption")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type ParentType
    {
      get => (Type) this["parentType"];
      set => this["parentType"] = (object) value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (BulkEditFieldControl);

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct BulkEditFieldProps
    {
      public const string TemplateName = "templateName";
      public const string TemplatePath = "templatePath";
      public const string WebServiceUrl = "webServiceUrl";
      public const string ContentType = "contentType";
      public const string ParentType = "parentType";
    }
  }
}
