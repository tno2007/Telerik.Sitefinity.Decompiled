// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>Dialog for selecting libraries.</summary>
  public class LibrarySelectorDialog : AjaxDialogBase
  {
    private string itemsName;
    private string itemName;
    private string libraryName;
    private string parentType;
    private bool showOnlySystemLibraries;
    private string commandName;
    private string provider;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.LibrarySelectorDialog.ascx");
    private const string dialogScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.LibrarySelectorDialog.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorDialog" /> class.
    /// </summary>
    public LibrarySelectorDialog() => this.LayoutTemplatePath = LibrarySelectorDialog.layoutTemplatePath;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (LibrarySelectorDialog).FullName;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// A localized string representing the item in plural (for example Images).
    /// </summary>
    protected internal virtual string ItemsName
    {
      get
      {
        if (string.IsNullOrEmpty(this.itemsName) && !string.IsNullOrEmpty(this.Page.Request.QueryString["itemsName"]))
          this.itemsName = this.Page.Request.QueryString["itemsName"].ToLower();
        return this.itemsName;
      }
    }

    /// <summary>
    /// A localized string representing the item in singular (for example Image).
    /// </summary>
    protected internal virtual string ItemName
    {
      get
      {
        if (string.IsNullOrEmpty(this.itemName) && !string.IsNullOrEmpty(this.Page.Request.QueryString["itemName"]))
          this.itemName = this.Page.Request.QueryString["itemName"].ToLower();
        return this.itemName;
      }
    }

    /// <summary>
    /// A localized string representing the library type (for example Album)
    /// </summary>
    protected internal virtual string LibraryName
    {
      get
      {
        if (string.IsNullOrEmpty(this.libraryName) && !string.IsNullOrEmpty(this.Page.Request.QueryString["libraryTypeName"]))
          this.libraryName = this.Page.Request.QueryString["libraryTypeName"].ToLower();
        return this.libraryName;
      }
    }

    protected internal virtual string ParentType
    {
      get
      {
        if (string.IsNullOrEmpty(this.parentType) && !string.IsNullOrEmpty(this.Page.Request.QueryString["parentType"]))
          this.parentType = this.Page.Request.QueryString["parentType"];
        return this.parentType;
      }
    }

    protected internal virtual bool ShowOnlySystemLibraries
    {
      get
      {
        if (!string.IsNullOrEmpty(this.Page.Request.QueryString["showOnlySystemLibraries"]))
          this.showOnlySystemLibraries = Convert.ToBoolean(this.Page.Request.QueryString["showOnlySystemLibraries"]);
        return this.showOnlySystemLibraries;
      }
    }

    protected internal virtual string CommandName
    {
      get
      {
        if (string.IsNullOrWhiteSpace(this.commandName) && !string.IsNullOrWhiteSpace(this.Page.Request.QueryString["commandName"]))
          this.commandName = this.Page.Request.QueryString["commandName"];
        return this.commandName;
      }
    }

    /// <summary>Gets the provider name.</summary>
    /// <value>The provider name</value>
    protected internal virtual string Provider
    {
      get
      {
        if (this.provider.IsNullOrEmpty())
          this.provider = this.Page.Request.QueryString["provider"];
        return this.provider;
      }
    }

    /// <summary>A content selector for the retrieved items</summary>
    protected internal virtual FolderSelector FolderSelector => this.Container.GetControl<FolderSelector>("folderSelector", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.FolderSelector.ProviderName = this.Provider;
      this.FolderSelector.WebServiceUrl = this.GetWebServiceUrl();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the confirmation dialog for delete a setting.</summary>
    protected virtual PromptDialog MoveConfirmationDialog => this.Container.GetControl<PromptDialog>("moveConfirmationDialog", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) this.GetBaseScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("folderSelector", this.FolderSelector.ClientID);
      controlDescriptor.AddComponentProperty("moveConfirmationDialog", this.MoveConfirmationDialog.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(this.GetBaseScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.LibrarySelectorDialog.js", typeof (LibrarySelectorDialog).Assembly.FullName)
    };

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    internal virtual IEnumerable<ScriptReference> GetBaseScriptReferences() => base.GetScriptReferences();

    internal virtual string GetWebServiceUrl()
    {
      if (this.ParentType == typeof (Album).FullName)
        return VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/AlbumService.svc/folders/");
      if (this.ParentType == typeof (VideoLibrary).FullName)
        return VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/VideoLibraryService.svc/folders/");
      return this.ParentType == typeof (DocumentLibrary).FullName ? VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/") : "";
    }
  }
}
