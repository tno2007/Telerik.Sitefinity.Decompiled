// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Folders.Web.UI.MediaContentBreadcrumb
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Folders.Web.UI
{
  /// <summary>
  /// Represents the breadcrumb in Media Content public control designers.
  /// </summary>
  public class MediaContentBreadcrumb : SimpleScriptView
  {
    internal const string viewScript = "Telerik.Sitefinity.Folders.Web.UI.Scripts.MediaContentBreadcrumb.js";
    internal const string clientManagerScript = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";
    internal static readonly string layoutTemplatePath = "Telerik.Sitefinity.Resources.Templates.Backend.Folders.MediaContentBreadcrumb.ascx";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ControlUtilities.ToVppPath(MediaContentBreadcrumb.layoutTemplatePath) : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the base service URL.</summary>
    public string BaseServiceUrl { get; set; }

    /// <summary>Gets or sets the method service URL.</summary>
    public string MethodServiceUrl { get; set; }

    /// <summary>Gets or sets the name of the root folder.</summary>
    public string RootFolderName { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the reference to the label wrapper control.</summary>
    protected virtual HtmlContainerControl Wrapper => this.Container.GetControl<HtmlContainerControl>("wrapper", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddElementProperty("wrapper", this.Wrapper.ClientID);
      controlDescriptor.AddProperty("_excludeNeighbours", (object) bool.TrueString);
      controlDescriptor.AddProperty("_methodServiceUrl", (object) this.MethodServiceUrl);
      controlDescriptor.AddProperty("_baseServiceUrl", (object) this.Page.ResolveUrl(VirtualPathUtility.AppendTrailingSlash(this.BaseServiceUrl)));
      controlDescriptor.AddProperty("_rootFolderName", (object) this.RootFolderName);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = typeof (MediaContentBreadcrumb).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Folders.Web.UI.Scripts.MediaContentBreadcrumb.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;
  }
}
