// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssFileEmbedDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.PublicControls.Enums;
using Telerik.Web.UI;
using Telerik.Web.UI.FileExplorer;

namespace Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views
{
  /// <summary>
  /// </summary>
  public class CssFileEmbedDesignerView : ContentViewDesignerView
  {
    /// <summary>Name of the template to use</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.EmbedControls.CssFileEmbedDesignerView.ascx");
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    internal const string designerViewJs = "Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.CssFileEmbedDesignerView.js";

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => nameof (CssFileEmbedDesignerView);

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<PublicControlsResources>().LinkToCssFile;

    /// <summary>Gets the name of the embedded layout template.</summary>
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CssFileEmbedDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Represents the text field that holds the Url for the style sheet
    /// </summary>
    protected internal virtual TextField UrlTextField => this.Container.GetControl<TextField>("urlTextField", true);

    /// <summary>
    /// Represents the choices between all media types and specific ones
    /// </summary>
    protected internal virtual ChoiceField MediaChoiceField => this.Container.GetControl<ChoiceField>("mediaChoiceField", true);

    /// <summary>Represents the media type choices</summary>
    protected internal virtual ChoiceField MediaTypesChoiceField => this.Container.GetControl<ChoiceField>("mediaTypesChoiceField", true);

    /// <summary>The button that invokes the select file functionality</summary>
    protected internal virtual LinkButton SelectFileButton => this.Container.GetControl<LinkButton>("selectFileButton", true);

    /// <summary>The button that invokes the select file functionality</summary>
    protected internal virtual LinkButton DoneSelectButton => this.Container.GetControl<LinkButton>("doneSelectButton", true);

    /// <summary>The button that canceles the file selection</summary>
    protected internal virtual LinkButton CancelSelectButton => this.Container.GetControl<LinkButton>("cancelSelectButton", true);

    /// <summary>Represents the file explorer</summary>
    protected internal virtual RadFileExplorer FileExplorer => this.Container.GetControl<RadFileExplorer>("fileExplorer", true);

    /// <summary>Represents the panel that contians the file explorer</summary>
    protected internal virtual Panel FileExplorerPanel => this.Container.GetControl<Panel>("fileExplorerPanel", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      foreach (string name in Enum.GetNames(typeof (MediaType)))
      {
        if (!(name == MediaType.all.ToString()))
          this.MediaTypesChoiceField.Choices.Add(new ChoiceItem()
          {
            Text = name,
            Value = ((int) Enum.Parse(typeof (MediaType), name)).ToString()
          });
      }
      this.MediaTypesChoiceField.Style.Add("display", "none");
      this.SetUpFileExplorer();
      this.FileExplorerPanel.Style.Add("display", "none");
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      this.FileExplorer.TreeView.ShowLineImages = false;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("_domain", (object) this.GetDomainUrl());
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      foreach (int num in Enum.GetValues(typeof (MediaType)))
        dictionary.Add(Enum.GetName(typeof (MediaType), (object) num), num);
      controlDescriptor.AddProperty("_mediaTypesMap", (object) dictionary);
      controlDescriptor.AddElementProperty("selectFileButton", this.SelectFileButton.ClientID);
      controlDescriptor.AddElementProperty("doneSelectButton", this.DoneSelectButton.ClientID);
      controlDescriptor.AddElementProperty("fileExplorerPanel", this.FileExplorerPanel.ClientID);
      controlDescriptor.AddElementProperty("cancelSelectButton", this.CancelSelectButton.ClientID);
      controlDescriptor.AddComponentProperty("fileExplorer", this.FileExplorer.ClientID);
      controlDescriptor.AddComponentProperty("urlTextField", this.UrlTextField.ClientID);
      controlDescriptor.AddComponentProperty("mediaChoiceField", this.MediaChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("mediaTypesChoiceField", this.MediaTypesChoiceField.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferenceList = new List<ScriptReference>();
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.CssFileEmbedDesignerView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferenceList.ToArray();
    }

    /// <summary>Sets up file explorer.</summary>
    protected internal virtual void SetUpFileExplorer()
    {
      string applicationPath = this.Context.Request.ApplicationPath;
      this.FileExplorer.ExplorerMode = FileExplorerMode.FileTree;
      this.FileExplorer.Configuration.ViewPaths = new string[1]
      {
        applicationPath
      };
      this.FileExplorer.InitialPath = applicationPath;
      this.FileExplorer.VisibleControls = FileExplorerControls.TreeView;
      this.FileExplorer.EnableOpenFile = false;
    }

    /// <summary>Gets the domain URL.</summary>
    /// <returns></returns>
    protected internal virtual string GetDomainUrl()
    {
      string domainUrl = UrlPath.GetDomainUrl();
      if (string.IsNullOrEmpty(domainUrl))
      {
        Uri url = SystemManager.CurrentHttpContext.Request.Url;
        domainUrl = url.AbsoluteUri.Replace(url.PathAndQuery, string.Empty);
      }
      return domainUrl;
    }
  }
}
