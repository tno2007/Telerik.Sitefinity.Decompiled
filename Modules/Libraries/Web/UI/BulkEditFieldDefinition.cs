// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.BulkEditFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// A class that provides all information that is needed to construct a BulkEditField control
  /// </summary>
  public class BulkEditFieldDefinition : 
    CompositeFieldDefinition,
    IBulkEditFieldDefinition,
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string templateName;
    private string templatePath;
    private string webServiceUrl;
    private Type contentType;
    private Type parentType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.BulkEditFieldDefinition" /> class.
    /// </summary>
    public BulkEditFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.BulkEditFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public BulkEditFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the name of the field control template.</summary>
    /// <value>The name of the template.</value>
    public string TemplateName
    {
      get => this.ResolveProperty<string>(nameof (TemplateName), this.templateName);
      set => this.templateName = value;
    }

    /// <summary>Gets or sets the path of the field control template.</summary>
    /// <value>The path of the template.</value>
    public string TemplatePath
    {
      get => this.ResolveProperty<string>(nameof (TemplatePath), this.templatePath);
      set => this.templatePath = value;
    }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public string WebServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (WebServiceUrl), this.webServiceUrl);
      set => this.webServiceUrl = value;
    }

    /// <summary>Gets or sets the type of the item.</summary>
    /// <value>The type of the item.</value>
    public Type ContentType
    {
      get => this.ResolveProperty<Type>(nameof (ContentType), this.contentType);
      set => this.contentType = value;
    }

    /// <summary>Gets or sets the type of the parent.</summary>
    /// <value>The type of the parent.</value>
    public Type ParentType
    {
      get => this.ResolveProperty<Type>(nameof (ParentType), this.parentType);
      set => this.parentType = value;
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (BulkEditFieldControl);
  }
}
