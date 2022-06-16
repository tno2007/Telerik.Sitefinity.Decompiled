// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.MediaItemPropertiesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.Json;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.FieldControls;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Displays list of field controls that allows you to edit the properties of a media item when you upload it.
  /// </summary>
  public class MediaItemPropertiesView : SimpleScriptView
  {
    private string uiCulture;
    private FieldControlsBinder fieldsBinder;
    private const string script = "Telerik.Sitefinity.Web.UI.Scripts.MediaItemPropertiesView.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.MediaItemPropertiesView.ascx");
    protected List<SectionControl> sectionControls;
    protected List<Control> fieldControls;

    /// <summary>
    /// Gets or sets the definitions that determine the behaviour of the control.
    /// </summary>
    /// <value>The definitions.</value>
    public IDetailFormViewDefinition Definitions { get; set; }

    /// <summary>Gets or sets the media item service URL.</summary>
    /// <value>The media item service URL.</value>
    public string MediaItemServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the full type name of the content (Image, Video, Document).
    /// </summary>
    /// <value>The type of the content.</value>
    public string ContentType { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the full name of the library type (Album, VideoLibrary, ImageLibrary).
    /// </summary>
    /// <value>The name of the library type.</value>
    public string LibraryTypeName { get; set; }

    /// <summary>Gets the field controls.</summary>
    /// <value>The field controls.</value>
    protected List<Control> FieldControls
    {
      get
      {
        if (this.fieldControls == null)
        {
          this.fieldControls = new List<Control>();
          foreach (SectionControl sectionControl in this.sectionControls)
            this.fieldControls.AddRange((IEnumerable<Control>) sectionControl.FieldControls);
        }
        return this.fieldControls;
      }
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = MediaItemPropertiesView.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the UI culture.</summary>
    /// <value>The UI culture.</value>
    public string UICulture
    {
      get
      {
        if (this.uiCulture == null)
          this.uiCulture = SystemManager.CurrentHttpContext.Request.QueryString["uiCulture"];
        return this.uiCulture;
      }
      set => this.uiCulture = value;
    }

    /// <summary>Gets or sets the field controls binder.</summary>
    /// <value>The fields binder.</value>
    public FieldControlsBinder FieldsBinder
    {
      get
      {
        if (this.fieldsBinder == null)
          this.fieldsBinder = this.Container.GetControl<FieldControlsBinder>("fieldsBinder", true);
        return this.fieldsBinder;
      }
    }

    /// <summary>
    /// Gets the reference to the repeater which binds the sections of the form
    /// </summary>
    protected virtual Repeater Sections => this.Container.GetControl<Repeater>("sections", true);

    /// <summary>Gets or sets the target library id.</summary>
    /// <value>The target library id.</value>
    public Guid TargetLibraryId { get; set; }

    /// <summary>
    /// Handles the ItemCreated event of the Sections control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    protected virtual void Sections_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.FindControl("section") is SectionControl control) || !(e.Item.DataItem is IContentViewSectionDefinition dataItem))
        return;
      control.SectionDefinition = dataItem;
      string name = string.IsNullOrEmpty(dataItem.ControlId) ? dataItem.Name : dataItem.ControlId;
      if (!string.IsNullOrEmpty(name))
        ControlUtilities.SetControlIdFromName(name, (Control) control);
      this.sectionControls.Add(control);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.InitializeBinder();
      this.sectionControls = new List<SectionControl>();
      List<DetailFormViewElement> views = CustomFieldsContext.GetViews(this.ContentType);
      if (this.ContentType == typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName)
        this.Definitions = (IDetailFormViewDefinition) views.Find((Predicate<DetailFormViewElement>) (x => x.ViewName == "SingleImageUploadDetailsView"));
      else if (this.ContentType == typeof (Video).FullName)
        this.Definitions = (IDetailFormViewDefinition) views.Find((Predicate<DetailFormViewElement>) (x => x.ViewName == "SingleVideoUploadDetailsView"));
      else if (this.ContentType == typeof (Document).FullName)
        this.Definitions = (IDetailFormViewDefinition) views.Find((Predicate<DetailFormViewElement>) (x => x.ViewName == "SingleDocumentUploadDetailsView"));
      if (this.Definitions == null)
        return;
      this.Definitions.Sections.OfType<ContentViewSectionElement>().SelectMany<ContentViewSectionElement, FieldDefinitionElement>((Func<ContentViewSectionElement, IEnumerable<FieldDefinitionElement>>) (s => s.Fields.Elements)).OfType<IAssetsFieldDefinition>().ToList<IAssetsFieldDefinition>().ForEach((Action<IAssetsFieldDefinition>) (f => f.UseOnlySelectMode = new bool?(true)));
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that
    /// corresponds to this Web server control. This property is used primarily by control
    /// developers.
    /// </summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration
    /// values.</returns>
    /// <value></value>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      this.Sections.DataSource = (object) this.Definitions.Sections;
      this.Sections.ItemCreated += new RepeaterItemEventHandler(this.Sections_ItemCreated);
      this.Sections.DataBind();
      base.OnPreRender(e);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      MediaItemPropertiesView itemPropertiesView = this;
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(itemPropertiesView.GetType().FullName, itemPropertiesView.ClientID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      controlDescriptor.AddComponentProperty("binder", itemPropertiesView.FieldsBinder.ClientID);
      controlDescriptor.AddProperty("_blankDataItem", (object) itemPropertiesView.CreateBlankEntry());
      controlDescriptor.AddProperty("fieldControlIds", (object) scriptSerializer.Serialize((object) itemPropertiesView.GetFieldControlsClientIds()));
      controlDescriptor.AddProperty("sectionsIds", (object) scriptSerializer.Serialize((object) itemPropertiesView.sectionControls.Select<SectionControl, string>((Func<SectionControl, string>) (c => c.ClientID))));
      controlDescriptor.AddProperty("requireDataItemFieldControlIds", (object) scriptSerializer.Serialize((object) itemPropertiesView.GetRequireDataItemFieldControlClientIds()));
      controlDescriptor.AddProperty("providerName", (object) itemPropertiesView.ProviderName);
      controlDescriptor.AddProperty("itemType", (object) itemPropertiesView.ContentType);
      controlDescriptor.AddProperty("_targetLibraryId", (object) itemPropertiesView.TargetLibraryId.ToString());
      if (!string.IsNullOrEmpty(itemPropertiesView.UICulture))
        controlDescriptor.AddProperty("uiCulture", (object) itemPropertiesView.UICulture);
      yield return (ScriptDescriptor) controlDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      yield return new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.MediaItemPropertiesView.js", typeof (MediaItemPropertiesView).Assembly.FullName);
    }

    private List<string> GetFieldControlsClientIds()
    {
      List<string> controlsClientIds = new List<string>();
      foreach (Control fieldControl in this.FieldControls)
        controlsClientIds.Add(fieldControl.ClientID);
      return controlsClientIds;
    }

    private List<string> GetRequireDataItemFieldControlClientIds()
    {
      List<string> controlClientIds = new List<string>();
      foreach (Control fieldControl in this.FieldControls)
      {
        if (fieldControl.GetType().GetCustomAttributes(typeof (RequiresDataItemAttribute), true) != null)
          controlClientIds.Add(fieldControl.ClientID);
      }
      return controlClientIds;
    }

    private string CreateBlankEntry()
    {
      string transactionName = Guid.NewGuid().ToString();
      LibrariesManager manager = LibrariesManager.GetManager(this.ProviderName, transactionName);
      bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
      try
      {
        manager.Provider.SuppressSecurityChecks = true;
        Type itemType = TypeResolutionService.ResolveType(this.ContentType);
        MediaContent mediaContent = (MediaContent) manager.CreateItem(itemType);
        IEnumerable<Library> source = (IEnumerable<Library>) null;
        if (this.ContentType == typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName)
          source = (IEnumerable<Library>) manager.GetAlbums().AsEnumerable<Album>();
        else if (this.ContentType == typeof (Video).FullName)
          source = (IEnumerable<Library>) manager.GetVideoLibraries().AsEnumerable<VideoLibrary>();
        else if (this.ContentType == typeof (Document).FullName)
          source = (IEnumerable<Library>) manager.GetDocumentLibraries().AsEnumerable<DocumentLibrary>();
        mediaContent.Parent = source.FirstOrDefault<Library>((Func<Library, bool>) (a => a.Title == (Lstring) "Default Library")) ?? source.FirstOrDefault<Library>();
        return mediaContent.ToJson(mediaContent.GetType());
      }
      finally
      {
        manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        TransactionManager.DisposeTransaction(transactionName);
      }
    }

    /// <summary>Initializes the binder.</summary>
    private void InitializeBinder()
    {
      this.FieldsBinder.ServiceUrl = VirtualPathUtility.ToAbsolute(this.MediaItemServiceUrl) + "/?itemType=" + this.ContentType;
      if (string.IsNullOrEmpty(this.UICulture))
        return;
      this.FieldsBinder.UICulture = this.UICulture;
    }
  }
}
