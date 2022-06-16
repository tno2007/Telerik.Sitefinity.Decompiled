// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SimpleView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Web.UI
{
  public abstract class SimpleView : CompositeControl
  {
    private GenericContainer container;
    private ITemplate layoutTemplate;

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.container = (GenericContainer) null;
      this.Controls.Clear();
      this.InitializeControls(this.Container);
      this.Controls.Add((Control) this.Container);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container of the instantiated template.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected abstract void InitializeControls(GenericContainer container);

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected virtual string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets a type from the resource assembly.
    /// Resource assembly is an assembly that contains embedded resources such as templates, images, CSS files and etc.
    /// By default this is Telerik.Sitefinity.Resources.dll.
    /// </summary>
    /// <value>The resources assembly info.</value>
    protected virtual Type ResourcesAssemblyInfo
    {
      get
      {
        Type type = this.GetType();
        return type.Assembly == typeof (ObjectFactory).Assembly ? Config.Get<ControlsConfig>().ResourcesAssemblyInfo : type;
      }
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public virtual string LayoutTemplatePath { get; set; }

    /// <summary>Gets or sets the layout template.</summary>
    [TemplateContainer(typeof (GenericContainer))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual ITemplate LayoutTemplate
    {
      get
      {
        if (this.layoutTemplate == null)
          this.layoutTemplate = ControlUtilities.GetTemplate(new TemplateInfo()
          {
            TemplatePath = this.LayoutTemplatePath,
            TemplateName = this.LayoutTemplateName,
            TemplateResourceInfo = this.ResourcesAssemblyInfo,
            ControlType = this.GetType(),
            Key = this.TemplateKey
          });
        return this.layoutTemplate;
      }
      set => this.layoutTemplate = value;
    }

    /// <summary>Gets or sets the template key.</summary>
    /// <value>The template key.</value>
    public virtual string TemplateKey { get; set; }

    /// <summary>
    /// Retrieves localized string by specifing global resource class ID and key of the resource.
    /// </summary>
    /// <param name="resourceClassId"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    internal virtual string GetLabel(string resourceClassId, string key) => !string.IsNullOrEmpty(resourceClassId) ? Res.Get(resourceClassId, key) : key;

    /// <summary>Gets or sets the container.</summary>
    /// <value>The container.</value>
    protected internal virtual GenericContainer Container
    {
      get
      {
        if (this.container == null)
          this.container = this.CreateContainer(this.LayoutTemplate);
        return this.container;
      }
    }

    /// <summary>Creates the container.</summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    protected internal virtual GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = new GenericContainer();
      template.InstantiateIn((Control) container);
      return container;
    }
  }
}
