// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentDetailsWindow
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;
using Telerik.Web.UI.FileExplorer;

namespace Telerik.Sitefinity.TrackingConsent.UI
{
  /// <summary>
  /// A control managing a client window for creating and updating page tracking consents.
  /// </summary>
  public class TrackingConsentDetailsWindow : SimpleScriptView
  {
    private const string Path = "App_Data/Sitefinity/TrackingConsent/";
    private static readonly string LayoutTemplateVirtualPath = "~/Res/" + "Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentDetailsWindow.ascx";
    internal const string JavaScriptReference = "Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentDetailsWindow.js";

    /// <summary>Gets the HTML control containing the window body.</summary>
    /// <value>The window body.</value>
    protected HtmlGenericControl WindowBody => this.Container.GetControl<HtmlGenericControl>("windowBody", true);

    /// <summary>Gets the done button.</summary>
    /// <value>The done button.</value>
    protected HtmlControl DoneButton => this.Container.GetControl<HtmlControl>("doneButton", true);

    /// <summary>Gets the cancel button.</summary>
    /// <value>The cancel button.</value>
    protected HtmlControl CancelButton => this.Container.GetControl<HtmlControl>("cancelButton", true);

    /// <summary>Gets change template button.</summary>
    /// <value>The change template button.</value>
    protected HtmlControl ChangeButton => this.Container.GetControl<HtmlControl>("changeButton", true);

    /// <summary>Gets the file explorer</summary>
    protected RadFileExplorer FileExplorer => this.Container.GetControl<RadFileExplorer>("fileExplorer", true);

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TrackingConsentDetailsWindow.LayoutTemplateVirtualPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (TrackingConsentDetailsWindow).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("windowBody", this.WindowBody.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("changeButton", this.ChangeButton.ClientID);
      controlDescriptor.AddComponentProperty("fileExplorer", this.FileExplorer.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentDetailsWindow.js", typeof (TrackingConsentDetailsWindow).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      string applicationPath = this.Context.Request.ApplicationPath;
      if (!applicationPath.EndsWith("/"))
        applicationPath += "/";
      string str = applicationPath + "App_Data/Sitefinity/TrackingConsent/";
      this.FileExplorer.ExplorerMode = FileExplorerMode.FileTree;
      this.FileExplorer.Configuration.ViewPaths = new string[1]
      {
        str
      };
      this.FileExplorer.InitialPath = str;
      this.FileExplorer.VisibleControls = FileExplorerControls.TreeView;
      this.FileExplorer.EnableOpenFile = false;
      this.FileExplorer.Configuration.SearchPatterns = new string[1]
      {
        "*.html"
      };
    }

    /// <inheritdoc />
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      this.FileExplorer.TreeView.ShowLineImages = false;
    }

    /// <inheritdoc />
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.KendoWeb;
  }
}
