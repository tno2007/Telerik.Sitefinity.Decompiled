// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.EmbeddedTemplateAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Defines an embeded template for a control.</summary>
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class EmbeddedTemplateAttribute : Attribute
  {
    private string resourceName;
    private string description;
    private bool isFrontEnd;
    private string path;
    private string lastModified;
    private string classId;

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.EmbeddedTemplateAttribute" />.
    /// </summary>
    /// <param name="resourceFileName">
    /// The name of the embeded resource file.
    /// </param>
    /// <param name="description">Description of the template.</param>
    /// <param name="defaultExternalPath">
    /// The default locaiton for extracting the embeded template.
    /// </param>
    /// <param name="isFrontEnd">
    /// Indicates if the template is used for frontend controls or backend.
    /// If the template is used in both, it should be treated as forntend.
    /// </param>
    /// <param name="lastModified">
    /// The date the template was last modified.
    /// </param>
    public EmbeddedTemplateAttribute(
      string resourceFileName,
      string description,
      string defaultExternalPath,
      bool isFrontEnd,
      string lastModified)
      : this(resourceFileName, description, defaultExternalPath, isFrontEnd, lastModified, (string) null)
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.EmbeddedTemplateAttribute" />.
    /// </summary>
    /// <param name="resourceFileName">
    /// The name of the embeded resource file.
    /// </param>
    /// <param name="description">Description of the template.</param>
    /// <param name="defaultExternalPath">
    /// The default locaiton for extracting the embeded template.
    /// </param>
    /// <param name="isFrontEnd">
    /// Indicates if the template is used for frontend controls or backend.
    /// If the template is used in both, it should be treated as forntend.
    /// </param>
    /// <param name="lastModified">
    /// The date the template was last modified.
    /// </param>
    /// <param name="classType">
    /// The resource class type for retrieving string values from global resources.
    /// The type must be assignable  form <see cref="T:Telerik.Sitefinity.Localization.Resource" /> class.
    /// If this parameter is provided, the description parameter will be considered as resouce key
    /// instead of description text.
    /// </param>
    public EmbeddedTemplateAttribute(
      string resourceFileName,
      string description,
      string defaultExternalPath,
      bool isFrontEnd,
      string lastModified,
      Type classType)
      : this(resourceFileName, description, defaultExternalPath, isFrontEnd, lastModified, classType.Name)
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.EmbeddedTemplateAttribute" />.
    /// </summary>
    /// <param name="resourceFileName">
    /// The name of the embeded resource file.
    /// </param>
    /// <param name="description">Description of the template.</param>
    /// <param name="defaultExternalPath">
    /// The default locaiton for extracting the embeded template.
    /// </param>
    /// <param name="isFrontEnd">
    /// Indicates if the template is used for frontend controls or backend.
    /// If the template is used in both, it should be treated as forntend.
    /// </param>
    /// <param name="lastModified">
    /// The date the template was last modified.
    /// </param>
    /// <param name="classId">
    /// The class ID for retrieving string values from global resources.
    /// If this parameter is provided, the description parameter will be considered as resouce key
    /// instead of description text.
    /// </param>
    public EmbeddedTemplateAttribute(
      string resourceFileName,
      string description,
      string defaultExternalPath,
      bool isFrontEnd,
      string lastModified,
      string classId)
    {
      this.resourceName = resourceFileName;
      this.description = description;
      this.isFrontEnd = isFrontEnd;
      this.path = defaultExternalPath;
      this.lastModified = lastModified;
      this.classId = classId;
    }

    /// <summary>Gets the name of the embeded resource file name.</summary>
    public string ResourceFileName => this.resourceName;

    /// <summary>
    /// Gets the description of the template.
    /// If ClassId property is set, the descripton will be retrieved form the global resources.
    /// </summary>
    public string Description => !string.IsNullOrEmpty(this.classId) ? Res.Get(this.classId, this.description) : this.description;

    /// <summary>
    /// Gets or sets class ID for retrieving string values from global resources.
    /// If this property is set, the Description will be considered as resouce key instead of
    /// description text.
    /// </summary>
    public string ClassId => this.classId;

    /// <summary>
    /// Gets or sets Bolean value indicating if the template is used for frontend controls or backend.
    /// If the template is used in both, it should be treated as forntend.
    /// </summary>
    public bool IsFrontEnd => this.isFrontEnd;

    /// <summary>
    /// Gets or sets the default locaiton for extracting the embeded template.
    /// </summary>
    public string DefaultExternalPath => this.path;

    /// <summary>Gets or sets the date the template was last modified.</summary>
    public string LastModified => this.lastModified;
  }
}
