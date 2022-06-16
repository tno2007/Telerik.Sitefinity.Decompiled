// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.UrlMirrorTextField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI.Fields
{
  public class UrlMirrorTextField : MirrorTextField
  {
    private string extension;
    private string urlControlId;
    private const string mirrorFieldScript = "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.UrlMirrorTextField.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.UrlMirrorTextField" /> class.
    /// </summary>
    public UrlMirrorTextField() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.MirrorTextField.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the pageId of the mirrored control.</summary>
    /// <value>The pageId of the mirrored control.</value>
    public virtual string UrlControlId
    {
      get => this.urlControlId;
      set => this.urlControlId = value;
    }

    /// <summary>Gets the default page extension.</summary>
    /// <value>The page extension.</value>
    public string Extension
    {
      get => this.extension;
      set => this.extension = value;
    }

    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.Extension = Config.Get<PagesConfig>().DefaultExtension;
    }

    /// <summary>
    /// Configures the field control by the specified definition.
    /// </summary>
    /// <param name="definition">The specified definition.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      this.UrlControlId = ((UrlMirrorTextFieldDefinition) definition).UrlControlId;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().ElementAt<ScriptDescriptor>(0) as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_urlControlId", (object) this.UrlControlId);
      controlDescriptor.AddProperty("_extension", (object) this.Extension);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference()
      {
        Assembly = typeof (UrlMirrorTextField).Assembly.FullName,
        Name = "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.UrlMirrorTextField.js"
      }
    }.ToArray();
  }
}
