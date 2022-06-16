// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI
{
  /// <summary>
  /// Designer control for the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock" /> control.
  /// </summary>
  public class ContentBlockDesigner : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.GenericContent.ContentBlockDesigner.ascx");
    private bool? addCultureToFilter;
    private ContentManager contentManager;
    private ContentBlock control;
    private EditorFilters? editorContentFilters;
    private bool isControlDefinitionProviderCorrect = true;
    private string sharedContent;
    private bool modifyAllowed = true;
    private bool isMissingSharedContent;
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    private const string designerScriptName = "Telerik.Sitefinity.Modules.GenericContent.Web.UI.Scripts.ContentBlockDesigner.js";
    private const string contentSelectorItemFilter = "Visible == true AND Status == Live";
    private const string contentItemsServiceUrl = "~/Sitefinity/Services/Content/ContentItemService.svc/";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ContentBlockDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overriden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    /// <value></value>
    protected override string ScriptDescriptorTypeName => this.GetType().FullName;

    /// <summary>
    /// Gets or sets a value indicating whether to add the culture of the property editor to the filter.
    /// </summary>
    /// <value><c>true</c> if the culture will be added to the filter; otherwise, <c>false</c>.</value>
    /// <remarks>
    /// If not set a value, the property will return the value of the <see cref="M:Telerik.Sitefinity.Model.IAppSettings.Multilingual" />.;
    /// </remarks>
    public bool AddCultureToFilter
    {
      get
      {
        if (!this.addCultureToFilter.HasValue)
          this.addCultureToFilter = new bool?(SystemManager.CurrentContext.AppSettings.Multilingual);
        return this.addCultureToFilter.Value;
      }
      set => this.addCultureToFilter = new bool?(value);
    }

    /// <summary>
    /// Gets or sets the message text - used to show any warnings and errors on the designers
    /// </summary>
    /// <value>The message.</value>
    public string TopMessageText { get; set; }

    /// <summary>Gets or sets the type of the top message.</summary>
    /// <value>The type of the top message.</value>
    public MessageType TopMessageType { get; set; }

    private ContentManager ContentManager
    {
      get
      {
        if (this.contentManager == null)
          this.contentManager = this.InitializeManager();
        return this.contentManager;
      }
    }

    private ContentBlock Control
    {
      get
      {
        if (this.control == null)
          this.control = this.PropertyEditor.Control as ContentBlock;
        return this.control;
      }
    }

    /// <summary>Gets the affected pages.</summary>
    private int AffectedPages => this.Control != null && this.Control.IsShared ? this.ContentManager.GetCountOfPagesThatUseContent(this.Control.SharedContentID) : 0;

    /// <summary>Gets the affected page templates.</summary>
    private int AffectedPageTemplates => this.Control != null && this.Control.IsShared ? this.ContentManager.GetCountOfPageTemplatesThatUseContent(this.Control.SharedContentID) : 0;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.HtmlField" /> control that is used
    /// for editing the HTML content of <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock" /> control.
    /// </summary>
    protected virtual HtmlField HtmlEditor => this.Container.GetControl<HtmlField>("htmlEditor", true);

    /// <summary>Gets the shared content selector control</summary>
    public ContentSelector SharedContentSelector => this.Container.GetControl<ContentSelector>("sharedContentSelector", true);

    /// <summary>Gets the command bar in edit view.</summary>
    /// <value>The command bar.</value>
    public CommandBar EditViewCommandBar => this.Container.GetControl<CommandBar>("editViewCommandBar", true);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Web.UI.PromptDialog" /> control used to share content.
    /// </summary>
    protected virtual PromptDialog ShareContentDialog => this.Container.GetControl<PromptDialog>("shareContentDialog", true);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Web.UI.PromptDialog" /> control used to confirm the user's choice for unsharing content.
    /// </summary>
    protected virtual PromptDialog UnshareContentDialog => this.Container.GetControl<PromptDialog>("unshareContentDialog", true);

    /// <summary>Gets the view pages link in preview view.</summary>
    /// <value>The view pages link.</value>
    public HyperLink ViewPagesLink => this.Container.GetControl<HyperLink>("viewPagesLink", true);

    /// <summary>
    /// Gets the command bar in preview and edit content view.
    /// </summary>
    /// <value>The command bar.</value>
    public CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true);

    /// <summary>Gets the content label in preview view.</summary>
    /// <value>The content label.</value>
    public Label ContentLabel => this.Container.GetControl<Label>("contentLabel", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.HtmlField" /> control that is used
    /// for editing the shared content of <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock" /> control.
    /// </summary>
    protected virtual HtmlField ContentEditor => this.Container.GetControl<HtmlField>("contentEditor", true);

    /// <summary>Gets the RadWindowManager</summary>
    protected virtual RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>Gets the client label manager.</summary>
    /// <value>The client label manager.</value>
    public ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the shared content label.</summary>
    /// <value>The shared content label.</value>
    public Label SharedContentLabel => this.Container.GetControl<Label>("sharedContentLabel", true);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Select share content" dialog
    /// </summary>
    public HtmlGenericControl SelectorTag => this.Container.GetControl<HtmlGenericControl>("selectorTag", true);

    /// <summary>Gets the providers selector control.</summary>
    /// <value>The providers selector control.</value>
    protected ProvidersSelector ProvidersSelector => this.Container.GetControl<ProvidersSelector>("providersSelector", false);

    /// <summary>Gets the message control.</summary>
    /// <value>The message control.</value>
    protected Message Message => this.Container.GetControl<Message>();

    /// <summary>Gets the shared content title label.</summary>
    protected virtual SitefinityLabel SharedContentTitleLabel => this.Container.GetControl<SitefinityLabel>("sharedContentTitleLabel", false);

    /// <summary>
    /// Gets a reference to the panel visible by users with View permissions allowed
    /// </summary>
    protected virtual HtmlGenericControl ViewPermissionsAllowedPanel => this.Container.GetControl<HtmlGenericControl>("viewPermissionsAllowedPanel", true);

    /// <summary>
    /// Gets a reference to the panel visible by users with View permissions denied
    /// </summary>
    protected virtual HtmlGenericControl ViewPermissionsDeniedPanel => this.Container.GetControl<HtmlGenericControl>("viewPermissionsDeniedPanel", true);

    /// <summary>
    /// Gets a reference to the literal visible by users with View permissions denied
    /// </summary>
    protected virtual Literal ViewPermissionsDeniedLiteral => this.Container.GetControl<Literal>("viewPermissionsDeniedLiteral", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.InitializeManager();
      if (!this.InitializeSharedContent())
        return;
      if (!this.isControlDefinitionProviderCorrect)
      {
        this.TopMessageText = Res.Get<Labels>("DefinedProviderNotAvailable");
        this.TopMessageType = MessageType.Negative;
      }
      this.SetHtmlFieldFilters();
      this.DesignerMode = ControlDesignerModes.Simple;
      this.AdvancedModeIsDefault = false;
      string str1 = (string) null;
      if (this.PropertyEditor != null)
      {
        str1 = this.PropertyEditor.PropertyValuesCulture;
        this.HtmlEditor.UICulture = str1;
        this.ContentEditor.UICulture = str1;
      }
      this.SharedContentSelector.ServiceUrl = "~/Sitefinity/Services/Content/ContentItemService.svc/";
      this.SharedContentSelector.ShowProvidersList = false;
      string str2 = "Visible == true AND Status == Live";
      this.SharedContentSelector.UICulture = str1;
      if (this.AddCultureToFilter && !string.IsNullOrEmpty(str1))
        str2 += string.Format(" AND Culture == {0}", (object) str1);
      this.SharedContentSelector.ItemsFilter = str2;
      if (this.ProvidersSelector == null)
        return;
      this.ProvidersSelector.Manager = (IManager) this.ContentManager;
      this.ProvidersSelector.RenderAsClickMenu = true;
      this.ProvidersSelector.SelectedProviderName = this.Control != null ? this.Control.ProviderName : (string) null;
    }

    /// <summary>Gets the editor strip formatting options.</summary>
    /// <param name="options">The options.</param>
    /// <returns></returns>
    /// <exception cref="T:System.NotImplementedException"></exception>
    private EditorStripFormattingOptions GetEditorStripFormattingOptions(
      string options)
    {
      List<string> list = ((IEnumerable<string>) options.Split(',')).ToList<string>();
      EditorStripFormattingOptions result = EditorStripFormattingOptions.None;
      Action<string> action = (Action<string>) (inputOption =>
      {
        foreach (EditorStripFormattingOptions formattingOptions in Enum.GetValues(typeof (EditorStripFormattingOptions)))
        {
          if (inputOption.Trim().Equals(formattingOptions.ToString(), StringComparison.InvariantCultureIgnoreCase))
            result |= formattingOptions;
        }
      });
      list.ForEach(action);
      return result;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (string.IsNullOrEmpty(this.TopMessageText))
        return;
      this.Message.Visible = true;
      this.Message.MessageText = this.TopMessageText;
      this.Message.Status = this.TopMessageType;
    }

    /// <summary>Gets the script descriptors.</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("htmlEditor", this.HtmlEditor.ClientID);
      controlDescriptor.AddComponentProperty("sharedContentSelector", this.SharedContentSelector.ClientID);
      controlDescriptor.AddComponentProperty("editViewCommandBar", this.EditViewCommandBar.ClientID);
      controlDescriptor.AddComponentProperty("shareContentDialog", this.ShareContentDialog.ClientID);
      controlDescriptor.AddComponentProperty("unshareContentDialog", this.UnshareContentDialog.ClientID);
      controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      controlDescriptor.AddComponentProperty("contentEditor", this.ContentEditor.ClientID);
      controlDescriptor.AddComponentProperty("windowManager", this.WindowManager.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      string str = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ContentItemService.svc/"));
      controlDescriptor.AddProperty("contentItemsServiceUrl", (object) str);
      controlDescriptor.AddProperty("uiCulture", (object) this.PropertyEditor.PropertyValuesCulture);
      Type type = typeof (ContentItem);
      controlDescriptor.AddProperty("_itemType", (object) type.FullName);
      controlDescriptor.AddProperty("_affectedPages", (object) this.AffectedPages);
      controlDescriptor.AddProperty("_affectedPageTemplates", (object) this.AffectedPageTemplates);
      object blankDataItem = this.CreateBlankDataItem();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(type, (IEnumerable<Type>) new Type[1]
        {
          type
        }).WriteObject((Stream) memoryStream, blankDataItem);
        controlDescriptor.AddProperty("_blankDataItem", (object) Encoding.Default.GetString(memoryStream.ToArray()));
      }
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        controlDescriptor.AddProperty("currentLanguage", (object) this.PropertyEditor.PropertyValuesCulture);
      controlDescriptor.AddElementProperty("viewPagesLink", this.ViewPagesLink.ClientID);
      controlDescriptor.AddElementProperty("contentLabel", this.ContentLabel.ClientID);
      controlDescriptor.AddElementProperty("sharedContentLabel", this.SharedContentLabel.ClientID);
      controlDescriptor.AddElementProperty("selectorTag", this.SelectorTag.ClientID);
      if (this.ProvidersSelector != null && this.ProvidersSelector.Visible)
        controlDescriptor.AddComponentProperty("providersSelector", this.ProvidersSelector.ClientID);
      controlDescriptor.AddProperty("isControlDefinitionProviderCorrect", (object) this.isControlDefinitionProviderCorrect);
      if (this.Message != null)
        controlDescriptor.AddComponentProperty("message", this.Message.ClientID);
      if (this.Control != null && this.Control.IsShared)
        controlDescriptor.AddProperty("_sharedHtml", (object) this.sharedContent);
      if (this.SharedContentTitleLabel != null)
        controlDescriptor.AddElementProperty("sharedContentTitleLabel", this.SharedContentTitleLabel.ClientID);
      controlDescriptor.AddProperty("_sharedContentInitialized", (object) this.ViewPermissionsAllowedPanel.Visible);
      controlDescriptor.AddProperty("_modifyAllowed", (object) this.modifyAllowed);
      controlDescriptor.AddProperty("_isMissingSharedContent", (object) this.isMissingSharedContent);
      return (IEnumerable<ScriptDescriptor>) source.ToArray();
    }

    /// <summary>Gets the script references.</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (ContentBlockDesigner).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.GenericContent.Web.UI.Scripts.ContentBlockDesigner.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources")
      }.ToArray();
    }

    private ContentManager InitializeManager()
    {
      if (this.contentManager != null)
        return this.contentManager;
      ContentManager contentManager = this.ResolveManagerWithProvider(this.Control != null ? this.Control.ProviderName : (string) null);
      if (contentManager == null)
      {
        contentManager = this.ResolveManagerWithProvider((string) null);
        this.isControlDefinitionProviderCorrect = false;
      }
      return contentManager;
    }

    private ContentManager ResolveManagerWithProvider(string providerName)
    {
      try
      {
        return ContentManager.GetManager(providerName);
      }
      catch (ConfigurationErrorsException ex)
      {
        return (ContentManager) null;
      }
    }

    private object CreateBlankDataItem()
    {
      bool suppressSecurityChecks = this.ContentManager.Provider.SuppressSecurityChecks;
      this.ContentManager.Provider.SuppressSecurityChecks = true;
      object blankDataItem = this.ContentManager.CreateItem(typeof (ContentItem), Guid.Empty);
      this.ContentManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      return blankDataItem;
    }

    protected virtual void SetHtmlFieldFilters()
    {
      this.editorContentFilters = new EditorFilters?(Config.Get<AppearanceConfig>().ContentBlockFilters);
      if (this.Control != null && this.Control.ContentEditorFilters.HasValue)
      {
        this.HtmlEditor.EditorContentFilters = this.Control.ContentEditorFilters;
        this.ContentEditor.EditorContentFilters = this.Control.ContentEditorFilters;
      }
      else
      {
        if (!this.editorContentFilters.HasValue)
          return;
        this.HtmlEditor.EditorContentFilters = this.editorContentFilters;
        this.ContentEditor.EditorContentFilters = this.editorContentFilters;
      }
    }

    private void SetButtonVisibility(CommandBar commandBar, string commandName, bool isGranted)
    {
      if (isGranted)
        return;
      ICommandButton commandButton = commandBar.Commands.OfType<ICommandButton>().Where<ICommandButton>((Func<ICommandButton, bool>) (b => b.CommandName == commandName)).FirstOrDefault<ICommandButton>();
      if (commandButton == null)
        return;
      ((ToolboxItemBase) commandButton).Visible = false;
    }

    private bool InitializeSharedContent()
    {
      bool isGranted = ((IEnumerable<string>) new string[5]
      {
        "ChangeOwner",
        "ChangePermissions",
        "Create",
        "Delete",
        "Modify"
      }).Any<string>((Func<string, bool>) (action => this.ContentManager.SecurityRoot.IsGranted("General", action)));
      if (this.Control != null && this.Control.IsShared)
      {
        using (new ContentBlockDesigner.ContentResolutionRegion(this.PropertyEditor.PropertyValuesCulture))
        {
          string str = string.Empty;
          ContentItem contentItem = (ContentItem) null;
          try
          {
            contentItem = this.ContentManager.GetContent(this.Control.SharedContentID);
          }
          catch (UnauthorizedAccessException ex)
          {
            str = Res.Get<ContentResources>().NoViewPermissionsMessage;
          }
          catch (ItemNotFoundException ex)
          {
            this.Control.SharedContentID = Guid.Empty;
            this.isMissingSharedContent = true;
          }
          if (!string.IsNullOrEmpty(str))
          {
            this.ViewPermissionsAllowedPanel.Visible = false;
            this.ViewPermissionsDeniedPanel.Visible = true;
            this.ViewPermissionsDeniedLiteral.Text = str;
            return false;
          }
          if (contentItem == null)
            return false;
          this.SharedContentTitleLabel.Text = (string) contentItem.Title;
          this.sharedContent = (string) contentItem.Content;
          this.modifyAllowed = contentItem.IsGranted("General", "Modify");
          this.SetButtonVisibility(this.CommandBar, "editContent", this.modifyAllowed);
          this.SetButtonVisibility(this.CommandBar, "unshareContent", this.modifyAllowed);
          this.SetButtonVisibility(this.CommandBar, "selectSharedContent", this.modifyAllowed);
        }
      }
      this.SetButtonVisibility(this.EditViewCommandBar, "shareContent", this.ContentManager.SecurityRoot.IsGranted("General", "Create"));
      this.SetButtonVisibility(this.EditViewCommandBar, "selectSharedContent", isGranted);
      return true;
    }

    private class ContentResolutionRegion : IDisposable
    {
      private CultureInfo prevUiCulture;
      private object prevContentFilters;
      private const string contentFiltersKey = "sfContentFilters";

      public ContentResolutionRegion(string cultureName)
        : this(CultureInfo.GetCultureInfo(cultureName))
      {
      }

      public ContentResolutionRegion(CultureInfo culture)
      {
        this.prevUiCulture = SystemManager.CurrentContext.Culture;
        SystemManager.CurrentContext.Culture = culture;
        if (SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
          this.prevContentFilters = SystemManager.HttpContextItems[(object) "sfContentFilters"];
        SystemManager.HttpContextItems[(object) "sfContentFilters"] = (object) new string[1]
        {
          "LinksParser"
        };
      }

      public void Dispose()
      {
        if (this.prevContentFilters != null)
          SystemManager.HttpContextItems[(object) "sfContentFilters"] = this.prevContentFilters;
        else
          SystemManager.HttpContextItems.Remove((object) "sfContentFilters");
        SystemManager.CurrentContext.Culture = this.prevUiCulture;
      }
    }
  }
}
