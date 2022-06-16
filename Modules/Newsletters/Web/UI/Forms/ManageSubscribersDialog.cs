// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ManageSubscribersDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms
{
  /// <summary>
  /// A dialog for adding or removing existing subscribers from a mailing list.
  /// </summary>
  public class ManageSubscribersDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Forms.ManageSubscribersDialog.ascx");
    private const string serviceBaseUrl = "~/Sitefinity/Services/Newsletters/Subscriber.svc/";
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.ManageSubscribersDialog.js";
    internal const string clientManagerReference = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (ManageSubscribersDialog).FullName;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ManageSubscribersDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the reference to the control which displays the dialog title.
    /// </summary>
    protected virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>Gets the reference to the client label manager.</summary>
    protected virtual ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("labelManager", true);

    /// <summary>Gets the reference to the done link.</summary>
    protected virtual LinkButton DoneLink => this.Container.GetControl<LinkButton>("doneLink", true);

    /// <summary>Gets the reference to the subscribers flat selector.</summary>
    protected virtual FlatSelector SubscribersSelector => this.Container.GetControl<FlatSelector>("subscribersSelector", true);

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
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("serviceBaseUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/Subscriber.svc/"));
      controlDescriptor.AddElementProperty("titleLabel", this.TitleLabel.ClientID);
      controlDescriptor.AddElementProperty("doneLink", this.DoneLink.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.LabelManager.ClientID);
      controlDescriptor.AddComponentProperty("subscribersSelector", this.SubscribersSelector.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
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
      string fullName = typeof (ManageSubscribersDialog).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.ManageSubscribersDialog.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
