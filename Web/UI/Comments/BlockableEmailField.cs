// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.BlockableEmailField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Represents a field control used for viewing and blocking the email of the user posting the comment.
  /// </summary>
  public class BlockableEmailField : CompositeFieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Comments.BlockableEmailField.ascx");
    private const string webServiceUrl = "~/Sitefinity/Services/Content/CommentsService.svc";
    private const string clientManagerScript = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";
    private const string BlockableEmailFieldScript = "Telerik.Sitefinity.Web.UI.Scripts.BlockableEmailField.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? BlockableEmailField.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the label that displays the title of the status field.
    /// </summary>
    protected internal override WebControl TitleControl => this.Container.GetControl<WebControl>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the label that displays the description of the status field.
    /// </summary>
    protected internal override WebControl DescriptionControl => this.Container.GetControl<WebControl>("descriptionLabel", false);

    /// <summary>
    /// Gets the reference to the label that displays the example of the status field.
    /// </summary>
    protected internal override WebControl ExampleControl => this.Container.GetControl<WebControl>("exampleLabel", false);

    /// <summary>
    /// Gets the reference to the textbox that displays the email.
    /// </summary>
    protected internal virtual TextField EmailControl => this.Container.GetControl<TextField>("emailTextField", true);

    /// <summary>Gets the reference to the link that blocks the email.</summary>
    protected internal LinkButton BlockEmailLink => this.Container.GetControl<LinkButton>("blockEmailLink", false);

    /// <summary>
    /// Gets the reference to the link that unblocks the email.
    /// </summary>
    protected internal LinkButton UnblockEmailLink => this.Container.GetControl<LinkButton>("unblockEmailLink", false);

    /// <summary>
    /// Gets the reference to the hidden field which holds the url of the web service
    /// </summary>
    protected internal virtual HiddenField WebServiceUrl => this.Container.GetControl<HiddenField>("webServiceUrl", true);

    /// <summary>Gets the reference to the label manager</summary>
    public virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      ((ITextControl) this.TitleControl).SetTextOrHide(this.Title);
      ((ITextControl) this.DescriptionControl).SetTextOrHide(this.Description);
      ((ITextControl) this.ExampleControl).SetTextOrHide(this.Example);
      this.WebServiceUrl.Value = this.ResolveUrl("~/Sitefinity/Services/Content/CommentsService.svc");
      this.EmailControl.ValidatorDefinition.MessageCssClass = "sfError";
      this.EmailControl.ValidatorDefinition.RequiredViolationMessage = Res.Get<ContentResources>().EmailCannotBeEmpty;
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition) => base.Configure(definition);

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (BlockableEmailField).FullName, this.ClientID);
      controlDescriptor.AddProperty("_serviceUrl", (object) this.WebServiceUrl.Value);
      controlDescriptor.AddElementProperty("emailControl", this.EmailControl.ClientID);
      controlDescriptor.AddElementProperty("blockEmailLink", this.BlockEmailLink.ClientID);
      controlDescriptor.AddElementProperty("unblockEmailLink", this.UnblockEmailLink.ClientID);
      controlDescriptor.AddElementProperty("clientLabelManager", this.ClientLabelManager.ClientID);
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
      string fullName = typeof (BlockableEmailField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.BlockableEmailField.js", fullName)
      };
    }
  }
}
