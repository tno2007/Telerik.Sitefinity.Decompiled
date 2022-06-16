// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.DynamicMailingListSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.DynamicLists;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms
{
  /// <summary>
  /// The dialog that allows configuring dynamic mailing lists.
  /// </summary>
  public class DynamicMailingListSettings : AjaxDialogBase
  {
    private NewslettersManager newslettersManager;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Forms.DynamicMailingListSettings.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.DynamicMailingListSettings.js";
    private const string clientManagerScript = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";
    private const string dynamicListServiceBaseUrl = "~/Sitefinity/Services/Newsletters/DynamicList.svc";

    /// <summary>
    /// Gets or sets the name of the newsletters provider to be used by this control.
    /// </summary>
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the instance of the <see cref="P:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.DynamicMailingListSettings.NewslettersManager" />.
    /// </summary>
    public NewslettersManager NewslettersManager
    {
      get
      {
        if (this.newslettersManager == null)
          this.newslettersManager = NewslettersManager.GetManager(this.ProviderName);
        return this.newslettersManager;
      }
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = DynamicMailingListSettings.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (DynamicMailingListSettings).FullName;

    /// <summary>
    /// Gets the reference to the drop down list with the available dynamic list providers.
    /// </summary>
    protected virtual ChoiceField DynamicListProviderChoiceField => this.Container.GetControl<ChoiceField>("dynamicListProviderChoiceField", true);

    /// <summary>
    /// Gets the reference to the drop down list with the available dynamic lists.
    /// </summary>
    protected virtual ChoiceField DynamicListsChoiceField => this.Container.GetControl<ChoiceField>("dynamicListsChoiceField", true);

    /// <summary>
    /// Gets the reference to generic html control holding the mappings user interface.
    /// </summary>
    protected virtual HtmlGenericControl MappingsContainer => this.Container.GetControl<HtmlGenericControl>("mappingsContainer", true);

    /// <summary>Gets the reference to the first name mapped field.</summary>
    protected virtual ChoiceField FirstNameMappedField => this.Container.GetControl<ChoiceField>("firstNameMappedField", true);

    /// <summary>Gets the reference to the last name mapped field.</summary>
    protected virtual ChoiceField LastNameMappedField => this.Container.GetControl<ChoiceField>("lastNameMappedField", true);

    /// <summary>Gets the reference to the email mapped field.</summary>
    protected virtual ChoiceField EmailMappedField => this.Container.GetControl<ChoiceField>("emailMappedField", true);

    /// <summary>
    /// Gets the reference to the <see cref="P:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.DynamicMailingListSettings.CommandBar" /> control of the form.
    /// </summary>
    protected virtual CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true);

    /// <summary>Gets the reference to the connection name text field.</summary>
    protected virtual TextField ConnectionNameField => this.Container.GetControl<TextField>("connectionName", true);

    /// <summary>Gets the reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the reference to client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManagerControl => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.BindDynamicListProviders();

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
      controlDescriptor.AddComponentProperty("dynamicListProviderChoiceField", this.DynamicListProviderChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("dynamicListsChoiceField", this.DynamicListsChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("firstNameMappedField", this.FirstNameMappedField.ClientID);
      controlDescriptor.AddComponentProperty("lastNameMappedField", this.LastNameMappedField.ClientID);
      controlDescriptor.AddComponentProperty("emailMappedField", this.EmailMappedField.ClientID);
      controlDescriptor.AddProperty("_mappingsContainerId", (object) this.MappingsContainer.ClientID);
      controlDescriptor.AddProperty("_dynamicListServiceBaseUrl", (object) VirtualPathUtility.AppendTrailingSlash(this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/DynamicList.svc")));
      controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      controlDescriptor.AddComponentProperty("connectionNameField", this.ConnectionNameField.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManagerControl.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.DynamicMailingListSettings.js", typeof (DynamicMailingListSettings).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", typeof (DynamicMailingListSettings).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected virtual void BindDynamicListProviders()
    {
      this.DynamicListProviderChoiceField.Choices.Clear();
      this.DynamicListProviderChoiceField.Choices.Add(new ChoiceItem()
      {
        Text = Res.Get<NewslettersResources>().SelectConnectionSource,
        Value = string.Empty
      });
      foreach (DynamicListProviderSettings providerSettings in (IEnumerable<DynamicListProviderSettings>) Config.Get<NewslettersConfig>().DynamicListProviders.Values)
      {
        if (this.NewslettersManager.GetDynamicListProvider(providerSettings.Name).GetDynamicLists().Count<DynamicListInfo>() != 0)
          this.DynamicListProviderChoiceField.Choices.Add(new ChoiceItem()
          {
            Text = providerSettings.Title,
            Value = providerSettings.Name
          });
      }
    }
  }
}
