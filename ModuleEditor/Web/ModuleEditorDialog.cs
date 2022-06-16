// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.ModuleEditorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.ModuleEditor.Web.UI;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.ModuleEditor.Web
{
  /// <summary>
  /// The dialog that is used to edit the data structure and the backend user interface of the modules.
  /// </summary>
  public class ModuleEditorDialog : AjaxDialogBase
  {
    private bool autoBind;
    private string contentType;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleEditor.ModuleEditorDialog.ascx");
    internal const string moduleEditorDialogScript = "Telerik.Sitefinity.ModuleEditor.Web.Scripts.ModuleEditorDialog.js";
    private const string serviceBaseUrl = "~/Sitefinity/Services/MetaData/ModuleEditor.svc";
    private bool refreshParentOnCancel;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Web.ModuleEditorDialog" /> class.
    /// </summary>
    public ModuleEditorDialog() => this.LayoutTemplatePath = ModuleEditorDialog.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (ModuleEditorDialog).FullName;

    /// <summary>
    /// Gets or sets whether addition of content link data type fields is allowed.
    /// </summary>
    /// <value>Whether addition of content link data type fields is allowed.</value>
    public bool AllowContentLinks { get; set; }

    /// <summary>Gets the topmost back button.</summary>
    /// <value>The topmost back button.</value>
    protected virtual HtmlAnchor TopBackButton => this.Container.GetControl<HtmlAnchor>("backButton", true);

    /// <summary>Gets the FieldsGrid listing custom fields</summary>
    protected virtual FieldsGrid FieldsGridCustom => this.Container.GetControl<FieldsGrid>("customFieldsGrid", true);

    /// <summary>Gets the fields grid listing default fields</summary>
    protected virtual FieldsGrid FieldsGridDefault => this.Container.GetControl<FieldsGrid>("defaultFieldsGrid", true);

    /// <summary>Gets the command bar.</summary>
    public CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true);

    /// <summary>Gets the bottom command bar.</summary>
    /// <value>The bottom command bar.</value>
    public CommandBar BottomCommandBar => this.Container.GetControl<CommandBar>("bottomCommandBar", true);

    /// <summary>Gets the message control.</summary>
    protected Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    protected Control LoadingView => this.Container.GetControl<Control>("loadingView", true);

    /// <summary>
    /// Gets the link that when clicked expands default fields grid.
    /// </summary>
    protected HtmlAnchor DefaultFieldsExpander => this.Container.GetControl<HtmlAnchor>("defaultFieldsExpander", true);

    protected Control DefaultFieldsWrapper => this.Container.GetControl<Control>("defaultFieldsWrapper", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      bool result;
      if (bool.TryParse(this.Page.Request.QueryString.Get("autoBind"), out result))
        this.autoBind = result;
      if (this.autoBind)
      {
        string buttonText = this.Page.Request.QueryString.Get("backLabelText");
        string buttonLink = "#";
        string redirectUri = this.Page.Request.QueryString.Get("backLabelLink");
        if (ObjectFactory.Resolve<IRedirectUriValidator>().IsValid(redirectUri))
          buttonLink = redirectUri;
        this.ConfigureButtonLink(this.TopBackButton, buttonText, buttonLink);
        this.ConfigureBackButton(this.CommandBar, buttonLink);
        this.ConfigureBackButton(this.BottomCommandBar, buttonLink);
      }
      this.contentType = this.Page.Request.QueryString.Get("TypeName");
      string str1 = this.Page.Request.QueryString.Get("AllowContentLinks");
      this.AllowContentLinks = !string.IsNullOrEmpty(str1) && str1.Equals("true", StringComparison.InvariantCultureIgnoreCase);
      this.FieldsGridCustom.AllowContentLinks = this.AllowContentLinks;
      string str2 = this.Page.Request.QueryString.Get("RefreshParentOnCancel");
      this.refreshParentOnCancel = !string.IsNullOrEmpty(str2) && str2.Equals("true", StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      ModuleEditorContext graph = new ModuleEditorContext();
      graph.ContentType = this.contentType;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(typeof (ModuleEditorContext), (IEnumerable<Type>) new Type[1]
        {
          graph.GetType()
        }).WriteObject((Stream) memoryStream, (object) graph);
        controlDescriptor.AddProperty("_emptyContext", (object) Encoding.Default.GetString(memoryStream.ToArray()));
      }
      controlDescriptor.AddProperty("autoBind", (object) this.autoBind);
      if (this.autoBind)
        controlDescriptor.AddProperty("contentType", (object) this.contentType);
      controlDescriptor.AddProperty("serviceBaseUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/MetaData/ModuleEditor.svc"));
      controlDescriptor.AddProperty("refreshParentOnCancel", (object) this.refreshParentOnCancel);
      controlDescriptor.AddComponentProperty("customFieldsGrid", this.FieldsGridCustom.ClientID);
      controlDescriptor.AddComponentProperty("defaultFieldsGrid", this.FieldsGridDefault.ClientID);
      controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      controlDescriptor.AddComponentProperty("bottomCommandBar", this.BottomCommandBar.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("backButton", this.TopBackButton.ClientID);
      controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
      controlDescriptor.AddElementProperty("defaultFieldsExpander", this.DefaultFieldsExpander.ClientID);
      controlDescriptor.AddElementProperty("defaultFieldsWrapper", this.DefaultFieldsWrapper.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (ModuleEditorDialog).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Validation.Scripts.Validator.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.ModuleEditor.Web.Scripts.ModuleEditorDialog.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void ConfigureBackButton(CommandBar commandBar, string buttonLink) => this.ConfigureButtonLink(commandBar.Controls.OfType<GenericContainer>().SelectMany<GenericContainer, HtmlAnchor>((Func<GenericContainer, IEnumerable<HtmlAnchor>>) (s => s.GetControls<HtmlAnchor>().Values.OfType<HtmlAnchor>())).Single<HtmlAnchor>((Func<HtmlAnchor, bool>) (c => c.Attributes["class"] == "sfCancel")), (string) null, buttonLink);

    private void ConfigureButtonLink(HtmlAnchor button, string buttonText, string buttonLink)
    {
      if (!string.IsNullOrEmpty(buttonText))
        button.InnerText = buttonText;
      if (string.IsNullOrEmpty(buttonLink))
        return;
      button.Attributes.Remove("href");
      button.Attributes.Remove("onclick");
      button.HRef = buttonLink;
    }
  }
}
