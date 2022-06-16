// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls
{
  /// <summary>Css selector dialog.</summary>
  public class CssSelectorDialog : AjaxDialogBase
  {
    internal const string DialogScript = "Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.CssSelectorDialog.js";
    private const string KendoScriptRef = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js";
    private static readonly string LayoutTemplateVppPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ResponsiveDesign.CssSelectorDialog.ascx");

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CssSelectorDialog.LayoutTemplateVppPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (CssSelectorDialog).FullName;

    /// <summary>Gets the file search pattern.</summary>
    /// <value>The file search pattern.</value>
    public virtual string FileSearchPattern => "*.css";

    /// <summary>
    /// Gets the ASP.NET application's virtual application root path on the server.
    /// </summary>
    /// <value>The virtual path of the current application.</value>
    public string ApplicationPath
    {
      get
      {
        string applicationPath = this.Context.Request.ApplicationPath;
        if (!applicationPath.EndsWith("/"))
          applicationPath += "/";
        return applicationPath;
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the reference to the control which holds file structure.
    /// </summary>
    protected virtual Literal FileStructureLiteral => this.Container.GetControl<Literal>("fileStructure", true);

    /// <summary>Gets the loading view.</summary>
    /// <value>The loading view.</value>
    protected Control LoadingView => this.Container.GetControl<Control>("loadingView", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ((ScriptComponentDescriptor) scriptDescriptors.Last<ScriptDescriptor>()).AddElementProperty("loadingView", this.LoadingView.ClientID);
      return scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (CssSelectorDialog).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.CssSelectorDialog.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.FullName)
      };
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.EnsureChildControls();
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The dialog container.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer) => this.RenderFileStructure();

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void RenderFileStructure()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("<ul id=\"css-selector-file-system-tree\">");
      DirectoryInfo directoryInfo = new DirectoryInfo(HostingEnvironment.MapPath("~/"));
      foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
        this.WriteDirectoryStructure(directory, sb);
      foreach (FileInfo file in directoryInfo.GetFiles(this.FileSearchPattern))
        this.WriteFileItem(sb, file);
      sb.Append("</ul>");
      this.FileStructureLiteral.Text = sb.ToString();
    }

    private void WriteDirectoryStructure(DirectoryInfo directory, StringBuilder sb)
    {
      sb.Append("<li class=\"sf_folder\">");
      sb.Append(directory.Name);
      sb.Append("<ul>");
      foreach (DirectoryInfo directory1 in directory.GetDirectories())
        this.WriteDirectoryStructure(directory1, sb);
      foreach (FileInfo file in directory.GetFiles(this.FileSearchPattern))
        this.WriteFileItem(sb, file);
      sb.Append("</ul>");
      sb.Append("</li>");
    }

    private void WriteFileItem(StringBuilder sb, FileInfo file)
    {
      string str = file.Extension;
      if (str.Length > 0)
        str = str.Substring(1);
      sb.Append("<li class=\"sf_file");
      if (!string.IsNullOrEmpty(str))
      {
        sb.Append(" ");
        sb.Append(str);
      }
      sb.Append("\"");
      sb.Append(" data-sf-path=\"");
      sb.Append(this.GetUrl(file.FullName));
      sb.Append("\">");
      sb.Append(file.Name);
      sb.Append("</li>");
    }

    private string GetUrl(string fullPath)
    {
      string str = HostingEnvironment.MapPath("~/");
      return "~/" + fullPath.Remove(0, str.Length).Replace("\\", "/");
    }
  }
}
