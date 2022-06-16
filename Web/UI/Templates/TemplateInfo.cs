// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Templates.TemplateInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI.Templates
{
  /// <summary>
  /// Defines what template and resources should be loaded for a control.
  /// Usually template information is evaluated in the following order:
  ///   1. Virtual path to an external template.
  ///   2. Configuration mapping settings.
  ///   3. Embedded template (default) template.
  /// </summary>
  [DebuggerDisplay("TemplateInfo Path={TemplatePath} Name={TemplateName} Key={Key}")]
  public class TemplateInfo
  {
    private string templatePath;
    private string templateName;
    private Type resourceInfo;
    private Type controlType;
    private string key;
    private string configAdditionalKey;
    private string declaration;
    private bool addChildrenAsDirectDescendants;

    /// <summary>Specifies virtual path to an external template.</summary>
    public string TemplatePath
    {
      get => this.templatePath;
      set => this.templatePath = value;
    }

    /// <summary>Specifies the name of the embedded resource file.</summary>
    public string TemplateName
    {
      get => this.templateName;
      set => this.templateName = value;
    }

    /// <summary>
    /// Provides information about the assembly embedding the template.
    /// </summary>
    public Type TemplateResourceInfo
    {
      get
      {
        if (this.resourceInfo == (Type) null)
          this.resourceInfo = ControlsConfigManager.ResourcesAssemblyInfo;
        return this.resourceInfo;
      }
      set => this.resourceInfo = value;
    }

    /// <summary>Gets or sets the control type.</summary>
    public Type ControlType
    {
      get => this.controlType;
      set => this.controlType = value;
    }

    /// <summary>Gets or sets the key.</summary>
    public string Key
    {
      get => this.key;
      set => this.key = value;
    }

    /// <summary>
    /// Specifies a key for additional template.
    /// If there are additional templates mapped to the specified ConfigMapType,
    /// they will be searched for the provided key. If the key is not found,
    /// the default mapping will be used.
    /// </summary>
    public string ConfigAdditionalKey
    {
      get => this.configAdditionalKey;
      set => this.configAdditionalKey = value;
    }

    /// <summary>Gets or sets the HTML of the template.</summary>
    /// <value>The HTML.</value>
    public string Declaration
    {
      get => this.declaration;
      set => this.declaration = value;
    }

    /// <summary>
    /// If false, children will be appended via the template (e.g. Control.Controls.Add(template)
    /// If true, children will be appended in a foreach (e.g. foreach control in template add to Control.Controls)
    /// </summary>
    public bool AddChildrenAsDirectDescendants
    {
      get => this.addChildrenAsDirectDescendants;
      set => this.addChildrenAsDirectDescendants = value;
    }
  }
}
