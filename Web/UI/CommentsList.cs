// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.CommentsList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Xml;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI.Dialogs;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.Configuration.ContentView.Plugins;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Renders a list of comments</summary>
  [DefaultProperty("UiMode")]
  [Obsolete("Comments frontend UI is replaced by CommentsWidget.")]
  public class CommentsList : SimpleScriptView
  {
    private CommentsList.UiModes? uiMode;
    private string editCommentLink;
    private string noTextMessage;
    private bool? useGlobalSettings;
    private string groupDeleteClientId;
    private string groupHideClientId;
    private string groupHideAndSpamClientId;
    private string groupPublishClientId;
    private string searchClientId;
    private string newCommentTextClientId;
    private string addNewCommentClientId;
    private ICommentable commentableItem;
    /// <summary>
    /// 
    /// </summary>
    public static readonly string AdminTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.GenericContent.CommentsList.ascx");
    public static readonly string PublicTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.GenericContent.CommentsList.ascx");
    private const string ClientJsCodePath = "Telerik.Sitefinity.Web.Scripts.CommentsList.js";

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      if (this.HtmlEditor == null)
        return;
      using (Stream manifestResourceStream = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetManifestResourceStream("Telerik.Sitefinity.Resources.Themes.ToolsFile.xml"))
      {
        XmlDocument doc = new XmlDocument();
        doc.Load(manifestResourceStream);
        this.HtmlEditor.LoadToolsFile(doc);
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
      get
      {
        switch (this.UiMode)
        {
          case CommentsList.UiModes.Admin:
            return CommentsList.AdminTemplatePath;
          case CommentsList.UiModes.Simple:
            return CommentsList.PublicTemplatePath;
          case CommentsList.UiModes.Custom:
            return this.CustomLayoutTemplateName;
          default:
            throw new NotImplementedException();
        }
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the name of the custom layout template.</summary>
    /// <value>The name of the custom layout template.</value>
    /// <remarks>
    /// Custom layout template is a template that is being rendered if
    /// the <c>UiMode</c> property returns <c>CommentsList.UiModes.Custom</c>.
    /// </remarks>
    /// <seealso cref="P:Telerik.Sitefinity.Web.UI.CommentsList.LayoutTemplateName" />
    protected virtual string CustomLayoutTemplateName { get; set; }

    /// <summary>Defines the looks of the control</summary>
    public virtual CommentsList.UiModes UiMode
    {
      get
      {
        if (!this.uiMode.HasValue)
          this.uiMode = new CommentsList.UiModes?(CommentsList.UiModes.Admin);
        return this.uiMode.Value;
      }
      set => this.uiMode = new CommentsList.UiModes?(value);
    }

    /// <summary>Defines the type of content being commented.</summary>
    /// <remarks>
    /// <alert class="caution">This is the type of  content item that is commented,
    /// not the type of the comment itself.</alert>
    /// </remarks>
    public string ContentType { get; set; }

    /// <summary>Type of the comment itself</summary>
    public string CommentType { get; set; }

    /// <summary>Name of the content provider used by the web service</summary>
    public string ContentProviderName { get; set; }

    /// <summary>Name of the security provider use by the web service</summary>
    public string SecurityProviderName { get; set; }

    /// <summary>
    /// If set to a value != Guid.Empty, the client script control will display
    /// comments only for a item with the specified pageId.
    /// </summary>
    public Guid CommentedItemId { get; set; }

    /// <summary>
    /// Specifies the direction in which the comments will be sorted by date
    /// </summary>
    public System.Web.UI.WebControls.SortDirection SortDirection { get; set; }

    /// <summary>Determines the default filter for the client binder.</summary>
    public string DefaultFilter { get; set; }

    /// <summary>
    /// Determine whether to hide the group operations panel if present in the template or not
    /// </summary>
    public bool HideGroupCommands
    {
      get => this.GroupOperationsPanel == null || !this.GroupOperationsPanel.Visible;
      set
      {
        if (this.GroupOperationsPanel == null)
          return;
        this.GroupOperationsPanel.Visible = !value;
      }
    }

    /// <summary>
    /// Determine whether to hide the add comments panel if present in the tmplate or not
    /// </summary>
    public bool HideAddCommentsPanel
    {
      get => this.AddCommentsPanel == null || !this.AddCommentsPanel.Visible;
      set
      {
        if (this.AddCommentsPanel == null)
          return;
        this.AddCommentsPanel.Visible = !value;
      }
    }

    /// <summary>
    /// Defines the link to go to when the "edit" client command is fired
    /// </summary>
    public string EditCommentLink
    {
      get
      {
        if (string.IsNullOrEmpty(this.editCommentLink))
          this.editCommentLink = VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/" + typeof (EditCommentsDialog).Name);
        return this.editCommentLink;
      }
      set => this.editCommentLink = value;
    }

    /// <summary>
    /// Message to show when submitting a comment that has no text
    /// </summary>
    public string NoTextMessage
    {
      get
      {
        if (string.IsNullOrEmpty(this.noTextMessage))
          this.noTextMessage = Telerik.Sitefinity.Localization.Res.Get<Labels>().NoText;
        return this.noTextMessage;
      }
      set => this.noTextMessage = value;
    }

    /// <summary>Url parameter name for</summary>
    public string EditCommentParamName { get; set; }

    /// <summary>Enables and disables adding comment</summary>
    public bool EnableAddingComments
    {
      get => this.AddCommentsPanel != null && this.AddCommentsPanel.Visible;
      set
      {
        if (this.AddCommentsPanel == null)
          return;
        this.AddCommentsPanel.Visible = value;
      }
    }

    /// <summary>Enalbe or disable group operations</summary>
    public bool EnableGroupOperations
    {
      get => this.GroupOperationsPanel != null && this.GroupOperationsPanel.Visible;
      set
      {
        if (this.GroupOperationsPanel == null)
          return;
        this.GroupOperationsPanel.Visible = value;
      }
    }

    /// <summary>
    /// Determine whether CommentsList should use global settings or not
    /// </summary>
    /// <value>Default value is <c>false</c>.Will use default value until explicitly set.</value>
    /// <remarks>
    /// Affected properties:
    /// <list class="bullet">
    ///     <listItem>ModerateComments</listItem>
    /// </list>
    /// </remarks>
    public bool UseGlobalSettings
    {
      get => this.useGlobalSettings.HasValue && this.useGlobalSettings.Value;
      set => this.useGlobalSettings = new bool?(true);
    }

    /// <summary>
    /// Determines whether only accepted comments will appear on the public side or not
    /// </summary>
    public bool ModerateComments { get; set; }

    /// <summary>Gets the commented item.</summary>
    /// <value>The commented item.</value>
    public ICommentable CommentedItem
    {
      get
      {
        if (this.commentableItem == null)
        {
          Type itemType = TypeResolutionService.ResolveType(this.ContentType);
          this.commentableItem = DataExtensions.AppSettings.GetContentManager(itemType.FullName, this.ContentProviderName).GetCommentedItem(itemType, this.CommentedItemId);
        }
        return this.commentableItem;
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor("Telerik.Sitefinity.Web.UI.CommentsList", this.ClientID);
      bool moderateComments = this.ModerateComments;
      if (this.UseGlobalSettings)
        moderateComments = Config.Get<ContentPluginsConfig>().ModerateComments;
      string str = this.DefaultFilter;
      if (moderateComments)
        str = !string.IsNullOrEmpty(str) ? "(" + str + ") And (Comment.Status=Accepted)" : "Status=Accepted";
      string absolute = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Common/Comments.svc");
      behaviorDescriptor.AddProperty("serviceBaseUrl", (object) absolute);
      behaviorDescriptor.AddProperty("contentType", (object) this.ContentType);
      behaviorDescriptor.AddProperty("binderId", (object) this.ClientBinder.ClientID);
      behaviorDescriptor.AddProperty("providerName", (object) this.ContentProviderName);
      behaviorDescriptor.AddProperty("securityProvider", (object) this.SecurityProviderName);
      behaviorDescriptor.AddProperty("commentedItemId", (object) this.CommentedItemId);
      behaviorDescriptor.AddProperty("sortDirection", (object) this.SortDirection);
      behaviorDescriptor.AddProperty("defaultFilter", (object) str);
      behaviorDescriptor.AddProperty("editCommentLink", (object) this.EditCommentLink);
      behaviorDescriptor.AddProperty("commentType", (object) this.CommentType);
      behaviorDescriptor.AddProperty("contentBaseType", (object) typeof (Telerik.Sitefinity.GenericContent.Model.Content).FullName);
      behaviorDescriptor.AddProperty("_noTextMessage", (object) this.NoTextMessage);
      behaviorDescriptor.AddProperty("addNewCommentClientId", (object) this.AddNewCommentClientId);
      if (this.HtmlEditor != null)
        behaviorDescriptor.AddProperty("newCommentTextClientId", (object) this.HtmlEditor.ClientID);
      else
        behaviorDescriptor.AddProperty("newCommentTextClientId", (object) this.NewCommentTextClientId);
      behaviorDescriptor.AddProperty("searchClientId", (object) this.SearchClientId);
      behaviorDescriptor.AddProperty("groupPublishClientId", (object) this.GroupPublishClientId);
      behaviorDescriptor.AddProperty("groupHideAndSpamClientId", (object) this.GroupHideAndSpamClientId);
      behaviorDescriptor.AddProperty("groupDeleteClientId", (object) this.GroupDeleteClientId);
      behaviorDescriptor.AddProperty("groupHideClientId", (object) this.GroupHideClientId);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.CommentsList.js", typeof (CommentsList).Assembly.GetName().Name)
    };

    /// <summary>
    /// Specifies the client-side pageId of the control that fires the group delete event when clicked
    /// </summary>
    public string GroupDeleteClientId
    {
      get
      {
        if (string.IsNullOrEmpty(this.groupDeleteClientId))
          this.groupDeleteClientId = "group_delete";
        return this.groupDeleteClientId;
      }
    }

    /// <summary>
    /// Specifies the client-side pageId of the control that fires the group hide event when clicked
    /// </summary>
    public string GroupHideClientId
    {
      get
      {
        if (string.IsNullOrEmpty(this.groupHideClientId))
          this.groupHideClientId = "group_hide";
        return this.groupHideClientId;
      }
    }

    /// <summary>
    /// Specifies the client-side pageId of the control that fires the group hide and spam event when clicked
    /// </summary>
    public string GroupHideAndSpamClientId
    {
      get
      {
        if (string.IsNullOrEmpty(this.groupHideAndSpamClientId))
          this.groupHideAndSpamClientId = "group_hideAndSpam";
        return this.groupHideAndSpamClientId;
      }
    }

    /// <summary>
    /// Specifies the client-side pageId of the control that fires the group publish event when clicked
    /// </summary>
    public string GroupPublishClientId
    {
      get
      {
        if (string.IsNullOrEmpty(this.groupPublishClientId))
          this.groupPublishClientId = "group_publish";
        return this.groupPublishClientId;
      }
    }

    /// <summary>
    /// Specifies the client-side pageId of the control that fires the search event when clicked
    /// </summary>
    public string SearchClientId
    {
      get
      {
        if (string.IsNullOrEmpty(this.searchClientId))
          this.searchClientId = "searchComments";
        return this.searchClientId;
      }
    }

    /// <summary>
    /// Specifies the client-side pageId of the control from which the new comment's text is gotten
    /// </summary>
    /// <remarks>This property will be used if <c>HtmlEditor</c> is not defined in the template</remarks>
    public string NewCommentTextClientId
    {
      get
      {
        if (string.IsNullOrEmpty(this.newCommentTextClientId))
          this.newCommentTextClientId = "newCommentText";
        return this.newCommentTextClientId;
      }
    }

    /// <summary>
    /// Specifies the client-side pageId of the control that fires the search event when clicked
    /// </summary>
    public string AddNewCommentClientId
    {
      get
      {
        if (string.IsNullOrEmpty(this.addNewCommentClientId))
          this.addNewCommentClientId = "addNewComment";
        return this.addNewCommentClientId;
      }
    }

    /// <summary>
    /// Defines a reference to then control that encompases group-operations elements.
    /// </summary>
    /// <remarks>This control reference is optional.</remarks>
    protected virtual Control GroupOperationsPanel => this.Container.GetControl<Control>("groupOperationsPanel", false);

    /// <summary>
    /// Defines a reference to the client binder defined in the template.
    /// </summary>
    /// <remarks>This control reference is required.</remarks>
    protected virtual ClientBinder ClientBinder => this.Container.GetControl<ClientBinder>("clientBinder", true);

    /// <summary>
    /// Defines a reference to the control that encompases the html markup
    /// associated with adding a new comment.
    /// </summary>
    /// <remarks>This control reference is optional.</remarks>
    protected virtual Control AddCommentsPanel => this.Container.GetControl<Control>("addCommentsPanel", false);

    /// <summary>
    /// Reference to the control in the template that edits the comment text
    /// </summary>
    /// <remarks>This control reference is not required.</remarks>
    protected virtual RadEditor HtmlEditor => this.Container.GetControl<RadEditor>("htmlEditor", false);

    /// <summary>Defines the UI mode for Comments List</summary>
    public enum UiModes
    {
      /// <summary>
      /// Predefined looks and behaviour for Sitefinity Administration
      /// </summary>
      Admin,
      /// <summary>
      /// Predefined looks and behaviour for the public side of Sitefinity
      /// </summary>
      Simple,
      /// <summary>
      /// Use <c>CustomLayoutTemplateName</c> to set custom embedded resource name
      /// </summary>
      Custom,
    }
  }
}
