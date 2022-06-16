// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEntryDetailsField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  [RequiresDataItem]
  public class FormEntryDetailsField : FieldControl, IFormFieldControl, IValidatable
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Forms.FormEntryDetailsField.ascx");
    private const string scriptFileName = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormEntryDetailsField.js";
    private const string reqDataContextScriptFileName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FormEntryDetailsField.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    [NonMultilingual]
    public IMetaField MetaField
    {
      get
      {
        if (this.metaField == null)
          this.metaField = (IMetaField) this.LoadDefaultMetaField();
        return this.metaField;
      }
      set => this.metaField = value;
    }

    /// <summary>
    /// Gets the reference to the control that displays the Ip Address.
    /// </summary>
    /// <value></value>
    protected internal Label IpAddressLabel => this.Container.GetControl<Label>("labelIpAddressValue", true);

    /// <summary>
    /// Gets the reference to the control that displays the date the entry was submitted.
    /// </summary>
    /// <value></value>
    protected internal Label SubmittedOnLabel => this.Container.GetControl<Label>("labelsubmittedOnValue", true);

    /// <summary>
    /// Gets the reference to the control that displays the Username.
    /// </summary>
    /// <value></value>
    protected internal Label UsernameLabel => this.Container.GetControl<Label>("labelUsernameValue", true);

    protected internal override WebControl TitleControl => (WebControl) null;

    protected internal override WebControl DescriptionControl => (WebControl) null;

    protected internal override WebControl ExampleControl => (WebControl) null;

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition) => this.ConfigureBaseDefinition(definition);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConstructControl();

    protected internal virtual void ConstructControl()
    {
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptorList = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_baseBackendUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/"));
      controlDescriptor.AddElementProperty("ipAddressLabelId", this.IpAddressLabel.ClientID);
      controlDescriptor.AddElementProperty("submittedOnLabelId", this.SubmittedOnLabel.ClientID);
      controlDescriptor.AddElementProperty("usernameLabelId", this.UsernameLabel.ClientID);
      scriptDescriptorList.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptorList.ToArray();
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (TextField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormEntryDetailsField.js", fullName)
      };
    }

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    public IMetaField metaField { get; set; }
  }
}
