// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.ReorderDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>Dialog for reordering the images in an album</summary>
  public class ReorderDialog : AjaxDialogBase
  {
    private string itemsName;
    private string itemName;
    private string itemNameWithArticle;
    private string libraryName;
    private Type itemType;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.ReorderDialog.ascx");
    internal const string dialogScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ReorderDialog.js";
    internal const string contentWebServiceUrl = "~/Sitefinity/Services/Content/ContentService.svc";

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (ReorderDialog).FullName;

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
    /// A localized string representing the item in singular with an article in front of it (for example an image).
    /// </summary>
    protected internal virtual string ItemNameWithArticle
    {
      get
      {
        if (string.IsNullOrEmpty(this.itemNameWithArticle) && !string.IsNullOrEmpty(this.Page.Request.QueryString["itemNameWithArticle"]))
          this.itemNameWithArticle = this.Page.Request.QueryString["itemNameWithArticle"];
        return this.itemNameWithArticle;
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

    /// <summary>Gets the type of the content.</summary>
    protected internal virtual Type ItemType
    {
      get
      {
        if (this.itemType == (Type) null && !string.IsNullOrEmpty(this.Page.Request.QueryString["itemType"]))
          this.itemType = TypeResolutionService.ResolveType(this.Page.Request.QueryString["itemType"]);
        return this.itemType;
      }
    }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ReorderDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the image controls client binder.</summary>
    protected internal virtual RadListViewBinder ImageBinder => this.Container.GetControl<RadListViewBinder>("imageBinder", true);

    /// <summary>
    /// Reference to the RadListView control that binds to the image data.
    /// </summary>
    protected internal virtual RadListView ImageListView => this.Container.GetControl<RadListView>("imageListView", true);

    /// <summary>
    /// Gets the reference to the control that represents the command bar.
    /// </summary>
    /// <value>The command bar.</value>
    protected internal virtual CommandBar TopCommandBar => this.Container.GetControl<CommandBar>("topCommandBar", true);

    /// <summary>
    /// Gets the reference to the control that represents the command bar.
    /// </summary>
    /// <value>The command bar.</value>
    protected internal virtual CommandBar BottomCommandBar => this.Container.GetControl<CommandBar>("bottomCommandBar", true);

    /// <summary>
    /// Gets the reference to the control that represents the back button.
    /// </summary>
    /// <value>The back button.</value>
    protected internal virtual LinkButton BackButton => this.Container.GetControl<LinkButton>("backButton", true);

    /// <summary>
    /// Gets the reference to the control that represents the dialog title.
    /// </summary>
    /// <value>The dialog title.</value>
    protected internal virtual HtmlGenericControl DialogTitle => this.Container.GetControl<HtmlGenericControl>("dialogTitle", true);

    /// <summary>
    /// Gets the reference to dragToChangeOrderLiteral control.
    /// </summary>
    protected internal virtual ITextControl DragToChangeOrderLiteral => this.Container.GetControl<ITextControl>("dragToChangeOrderLiteral", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      BinderContainer binderContainer = this.ImageBinder.Containers.Where<BinderContainer>((Func<BinderContainer, bool>) (c => c.ID == "BinderContainer1")).FirstOrDefault<BinderContainer>();
      if (binderContainer != null)
      {
        if (this.ItemType == typeof (Video))
          binderContainer.Markup = "<span><img sys:src='{{SnapshotUrl}}' sys:alt='{{Title}}' /></span><span class='imgSelect'/>";
        else if (this.ItemType == typeof (Telerik.Sitefinity.Libraries.Model.Image))
          binderContainer.Markup = "<span><img sys:src='{{ThumbnailUrl}}' sys:alt='{{Title}}' /></span><span class='imgSelect'/>";
        else if (this.ItemType == typeof (Document))
          binderContainer.Markup = "<span sys:class=\"{{ 'sfext sf' + Extension.substring(1).toLowerCase()}}\" sys:title=\"{{Title}}\">{{(Title.length > 22) ? Title.substring(0,22) + '...' : Title}}</span>";
      }
      this.ImageBinder.HandleItemReordering = true;
      this.ImageBinder.AutoUpdateOrdinals = false;
      this.ImageBinder.ServiceUrl = this.GetWebServiceUrl();
      this.ImageBinder.ServiceChildItemsBaseUrl = this.GetParentServiceUrl();
      this.BackButton.Text = string.Format(Res.Get<Labels>().BackToAllItemsParameter, (object) this.ItemsName);
      this.DragToChangeOrderLiteral.Text = string.Format(Res.Get<LibrariesResources>().DragToChangeOrder, (object) this.ItemNameWithArticle);
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
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) this.GetBaseScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("_webServiceUrl", (object) this.GetWebServiceUrl());
      controlDescriptor.AddProperty("_itemType", (object) this.ItemType.FullName);
      controlDescriptor.AddProperty("_dialogTitlePattern", (object) string.Format(Res.Get<LibrariesResources>().ReorderItemsInLibrary, (object) this.ItemsName, (object) HttpUtility.HtmlEncode(this.LibraryName)));
      controlDescriptor.AddElementProperty("dialogTitle", this.DialogTitle.ClientID);
      controlDescriptor.AddComponentProperty("imageBinder", this.ImageBinder.ClientID);
      controlDescriptor.AddComponentProperty("imageListView", this.ImageListView.ClientID);
      controlDescriptor.AddComponentProperty("topCommandBar", this.TopCommandBar.ClientID);
      controlDescriptor.AddComponentProperty("bottomCommandBar", this.BottomCommandBar.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ReorderDialog.js", typeof (ReorderDialog).Assembly.FullName)
    };

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    internal virtual IEnumerable<ScriptReference> GetBaseScriptReferences() => base.GetScriptReferences();

    internal virtual string GetWebServiceUrl()
    {
      if (this.ItemType == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        return VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ImageService.svc/");
      if (this.ItemType == typeof (Video))
        return VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/VideoService.svc/");
      return this.ItemType == typeof (Document) ? VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/DocumentService.svc/") : VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ContentService.svc");
    }

    internal virtual string GetParentServiceUrl()
    {
      if (this.ItemType == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        return VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ImageService.svc/parent/");
      if (this.ItemType == typeof (Video))
        return VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/VideoService.svc/parent/");
      return this.ItemType == typeof (Document) ? VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/DocumentService.svc/parent/") : VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ContentService.svc/parent/");
    }
  }
}
