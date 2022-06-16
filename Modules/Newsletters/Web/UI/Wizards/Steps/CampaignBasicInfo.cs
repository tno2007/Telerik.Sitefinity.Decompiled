// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignBasicInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps
{
  /// <summary>
  /// Control which lets user manage the basic info of the campaign.
  /// </summary>
  public class CampaignBasicInfo : SitefinityWizardStepControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.Steps.CampaignBasicInfo.ascx");
    internal new const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.Scripts.CampaignBasicInfo.js";
    private const string listWebServiceUrl = "~/Sitefinity/Services/Newsletters/MailingList.svc";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CampaignBasicInfo.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the text field containing the campaign name.
    /// </summary>
    protected virtual TextField Name => this.Container.GetControl<TextField>("name", true);

    /// <summary>
    /// Gets the reference to the text field containing the from name.
    /// </summary>
    protected virtual TextField FromName => this.Container.GetControl<TextField>("fromName", true);

    /// <summary>
    /// Gets the reference to the text field containing the reply to email.
    /// </summary>
    protected virtual TextField ReplyToEmail => this.Container.GetControl<TextField>("replyToEmail", true);

    /// <summary>
    /// Gets the reference to the text field containing the message subject.
    /// </summary>
    protected virtual TextField MessageSubject => this.Container.GetControl<TextField>("messageSubject", true);

    protected virtual CheckBox UseGoogleTrackingField => this.Container.GetControl<CheckBox>("useGoogleTrackingField", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
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
      ScriptControlDescriptor controlDescriptor = scriptDescriptors.Count<ScriptDescriptor>() != 0 ? (ScriptControlDescriptor) scriptDescriptors.Last<ScriptDescriptor>() : new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("nameField", this.Name.ClientID);
      controlDescriptor.AddComponentProperty("fromNameField", this.FromName.ClientID);
      controlDescriptor.AddComponentProperty("replyToEmailField", this.ReplyToEmail.ClientID);
      controlDescriptor.AddComponentProperty("messageSubjectField", this.MessageSubject.ClientID);
      controlDescriptor.AddElementProperty("useGoogleTrackingField", this.UseGoogleTrackingField.ClientID);
      controlDescriptor.AddProperty("listWebServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Newsletters/MailingList.svc"));
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.Scripts.CampaignBasicInfo.js", typeof (CampaignBasicInfo).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
