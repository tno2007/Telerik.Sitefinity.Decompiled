// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibrariesMasterGridView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;

namespace Telerik.Sitefinity.Modules.Libraries
{
  public class LibrariesMasterGridView : MasterGridView
  {
    private Guid runningTaskId;
    private bool isParentEditable;
    private bool isParentDeletable;
    private string parentUrlName;
    private string libraryType;
    internal const string masterGridViewScript = "Telerik.Sitefinity.Modules.Libraries.Scripts.LibrariesMasterGridView.js";

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overridden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    protected override string ScriptDescriptorTypeName => typeof (LibrariesMasterGridView).FullName;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <param name="definition"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      base.InitializeControls(container, definition);
      if (this.Folder == null)
      {
        if (this.ParentItem != null)
        {
          this.isParentDeletable = this.ParentItem is ISecuredObject parentItem && parentItem.IsSecurityActionTypeGranted(SecurityActionTypes.Delete);
          this.isParentEditable = parentItem != null && parentItem.IsSecurityActionTypeGranted(SecurityActionTypes.Manage);
          this.parentUrlName = ((ContentManagerBase<LibrariesDataProvider>) this.Manager).GetItemUrl((ILocatable) (this.ParentItem as Library));
        }
        else
          this.parentUrlName = "";
      }
      else
      {
        this.isParentEditable = true;
        this.isParentDeletable = true;
        this.parentUrlName = this.Folder.UrlName.ToString();
      }
      if (this.ParentItem != null)
      {
        this.runningTaskId = (this.ParentItem as Library).RunningTask;
        this.libraryType = this.ParentItem.GetType().FullName;
      }
      else
        this.libraryType = string.Empty;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) scriptDescriptors.Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("isParentDeletable", (object) this.isParentDeletable);
      controlDescriptor.AddProperty("isParentEditable", (object) this.isParentEditable);
      controlDescriptor.AddProperty("parentUrlName", (object) this.parentUrlName);
      controlDescriptor.AddProperty("libraryType", (object) this.libraryType);
      controlDescriptor.AddProperty("runningTaskId", (object) this.runningTaskId);
      if (this.ParentItem is Telerik.Sitefinity.Libraries.Model.Album)
      {
        controlDescriptor.AddProperty("libraryServiceUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Services/Content/AlbumService.svc/"));
        return scriptDescriptors;
      }
      if (this.ParentItem is Telerik.Sitefinity.Libraries.Model.VideoLibrary)
      {
        controlDescriptor.AddProperty("libraryServiceUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Services/Content/VideoLibraryService.svc/"));
        return scriptDescriptors;
      }
      if (!(this.ParentItem is Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
        return scriptDescriptors;
      controlDescriptor.AddProperty("libraryServiceUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Services/Content/DocumentLibraryService.svc/"));
      return scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Scripts.LibrariesMasterGridView.js", typeof (LibrariesMasterGridView).Assembly.FullName)
    };
  }
}
