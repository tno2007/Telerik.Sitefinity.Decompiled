// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.BlockableIpField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Represents a field control used for viewing and blocking the ip address from which the comment was posted.
  /// </summary>
  public class BlockableIpField : CompositeFieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Comments.BlockableIpField.ascx");
    private const string webServiceUrl = "~/Sitefinity/Services/Content/CommentsService.svc";
    private const string clientManagerScript = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";
    private const string BlockableIpFieldScript = "Telerik.Sitefinity.Web.UI.Scripts.BlockableIpField.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? BlockableIpField.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the label that displays the title of the field control.
    /// </summary>
    protected internal override WebControl TitleControl => this.Container.GetControl<WebControl>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the label that displays the description of the field control.
    /// </summary>
    protected internal override WebControl DescriptionControl => this.Container.GetControl<WebControl>("descriptionLabel", false);

    /// <summary>
    /// Gets the reference to the label that displays the example of the field control.
    /// </summary>
    protected internal override WebControl ExampleControl => this.Container.GetControl<WebControl>("exampleLabel", false);

    /// <summary>Gets the reference to the label that displays the ip.</summary>
    protected internal TextField IpControl => this.Container.GetControl<TextField>("ipTextField", true);

    /// <summary>Gets the reference to the link that blocks the ip.</summary>
    protected internal LinkButton BlockIpLink => this.Container.GetControl<LinkButton>("blockIpLink", true);

    /// <summary>Gets the reference to the link that unblocks the ip.</summary>
    protected internal LinkButton UnblockIpLink => this.Container.GetControl<LinkButton>("unblockIpLink", true);

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
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition) => base.Configure(definition);

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (BlockableIpField).FullName, this.ClientID);
      controlDescriptor.AddProperty("_serviceUrl", (object) this.WebServiceUrl.Value);
      controlDescriptor.AddElementProperty("ipTextField", this.IpControl.ClientID);
      controlDescriptor.AddElementProperty("blockIpLink", this.BlockIpLink.ClientID);
      controlDescriptor.AddElementProperty("unblockIpLink", this.UnblockIpLink.ClientID);
      controlDescriptor.AddElementProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = this.GetType().Assembly.FullName;
      ScriptReference scriptReference1 = new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", fullName);
      ScriptReference scriptReference2 = new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.BlockableIpField.js", fullName);
      scriptReferences.Add(scriptReference1);
      scriptReferences.Add(scriptReference2);
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
