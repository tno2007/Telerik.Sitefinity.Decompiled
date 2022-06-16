// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.LanguagesOrderedListField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// This control provides the user with a way to manipulate a list of languages.
  /// The user can add, remove, reorder and set default languages.
  /// </summary>
  public class LanguagesOrderedListField : FieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.LanguagesOrderedListField.ascx");
    private const string scriptFileName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.LanguagesOrderedListField.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LanguagesOrderedListField.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the names of the sites will be included.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the names of the sites are included; otherwise, <c>false</c>.
    /// </value>
    public bool IncludeSitesNames { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the header will be shown.
    /// </summary>
    /// <value>
    ///   <c>true</c> if header is shown; otherwise, <c>false</c>.
    /// </value>
    public bool ShowHeaderRow { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether confirmation dialog will be shown when a language is removing.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if confirmation dialog is shown; otherwise, <c>false</c>.
    /// </value>
    public bool ShowConfirmationOnLanguageRemove { get; set; }

    public bool UseGlobalLocalization { get; set; }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.Container.GetControl<Label>("titleLabel", false);

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) this.Container.GetControl<Label>("descriptionLabel", false);

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) this.Container.GetControl<Label>("exampleLabel", false);

    /// <summary>Gets the control that opens the selector.</summary>
    /// <value>The open selector.</value>
    protected virtual Control OpenSelector => this.Container.GetControl<Control>("openSelector", false);

    /// <summary>Gets the RadWindowManager.</summary>
    /// <value>The RadWindowManager.</value>
    protected virtual RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>Gets the binder.</summary>
    /// <value>The binder.</value>
    protected virtual GenericCollectionBinder Binder => this.Container.GetControl<GenericCollectionBinder>("binder", true);

    /// <summary>Gets the items list.</summary>
    /// <value>The items list.</value>
    protected virtual HtmlGenericControl ItemsList => this.Container.GetControl<HtmlGenericControl>("itemsList", true);

    /// <summary>Gets the DefaultColumnLi.</summary>
    /// <value>The items list.</value>
    protected virtual HtmlGenericControl DefaultColumnLi => this.HeaderRow.FindControl(nameof (DefaultColumnLi)) as HtmlGenericControl;

    /// <summary>Gets the client label manager.</summary>
    /// <value>The client label manager.</value>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the control that displays the header.</summary>
    /// <value>The header row.</value>
    protected virtual HtmlGenericControl HeaderRow => this.Container.GetControl<HtmlGenericControl>("headerRow", false);

    /// <summary>
    /// Gets the label control that displays the "used by" header text.
    /// </summary>
    /// <value>The "used by" header control.</value>
    protected virtual Literal UsedByLabel => this.HeaderRow.FindControl("usedByLabel") as Literal;

    /// <summary>Gets the sites using culture label.</summary>
    /// <value>The sites using culture label.</value>
    protected virtual Label SitesUsingCultureLabel => this.Container.GetControl<Label>("sitesUsingCultureLabel", false);

    /// <summary>
    /// Gets the panel control that displays the confirmation dialog.
    /// </summary>
    /// <value>The confirmation dialog panel.</value>
    protected virtual Panel ConfirmationDialogPanel => this.Container.GetControl<Panel>("confirmationDialogPanel", false);

    /// <summary>Gets the remove language confirmation messages panel.</summary>
    /// <value>The remove language confirmation messages panel.</value>
    protected virtual Panel RemoveLanguageConfirmationMessagesPanel => this.Container.GetControl<Panel>("removeLanguageConfirmationMessagesPanel", false);

    /// <summary>Gets the remove language confirmation buttons panel.</summary>
    /// <value>The remove language confirmation buttons panel.</value>
    protected virtual Panel RemoveLanguageConfirmationButtonsPanel => this.Container.GetControl<Panel>("removeLanguageConfirmationButtonsPanel", false);

    /// <summary>Gets the cannot remove lang messages panel.</summary>
    /// <value>The cannot remove lang messages panel.</value>
    protected virtual Panel CannotRemoveLangMessagesPanel => this.Container.GetControl<Panel>("cannotRemoveLangMessagesPanel", false);

    /// <summary>Gets the confirm remove link control.</summary>
    /// <value>The confirm remove link.</value>
    protected virtual HyperLink ConfirmRemoveLink => this.Container.GetControl<HyperLink>("confirmRemoveLink", false);

    /// <summary>Gets the cancel remove link control.</summary>
    /// <value>The cancel remove link.</value>
    protected virtual HyperLink CancelRemoveLink => this.Container.GetControl<HyperLink>("cancelRemoveLink", false);

    /// <summary>Gets the cancel okay link.</summary>
    /// <value>The cancel okay link.</value>
    protected virtual HyperLink CancelOkayLink => this.Container.GetControl<HyperLink>("cancelOkayLink", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.IncludeSitesNames)
      {
        for (int index = 0; index < this.Binder.Containers.Count; ++index)
        {
          BinderContainer container1 = this.Binder.Containers[0];
          if (container1.ID != "multisiteBinderContainer")
          {
            container1.Visible = false;
            this.Binder.Containers.Remove(container1);
            --index;
          }
        }
        if (this.DefaultColumnLi != null)
          this.DefaultColumnLi.Visible = false;
      }
      else
      {
        BinderContainer binderContainer = this.Binder.Containers.FirstOrDefault<BinderContainer>((Func<BinderContainer, bool>) (c => c.ID == "multisiteBinderContainer"));
        if (binderContainer != null)
        {
          binderContainer.Visible = false;
          this.Binder.Containers.Remove(binderContainer);
        }
        if (this.UsedByLabel != null)
          this.UsedByLabel.Visible = false;
      }
      if (this.HeaderRow != null)
        this.HeaderRow.Visible = this.ShowHeaderRow;
      if (!this.ShowConfirmationOnLanguageRemove || this.ConfirmationDialogPanel == null)
        return;
      this.ConfirmationDialogPanel.Visible = true;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        return;
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.JQueryUI);
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
      controlDescriptor.AddElementProperty("openSelector", this.OpenSelector.ClientID);
      controlDescriptor.AddComponentProperty("windowManager", this.WindowManager.ClientID);
      string url = "~/Sitefinity/Dialog/LanguageSelectorDialog";
      if (this.UseGlobalLocalization)
        url += "?useGlobal=true";
      controlDescriptor.AddProperty("dialogUrl", (object) RouteHelper.ResolveUrl(url, UrlResolveOptions.Rooted));
      controlDescriptor.AddComponentProperty("binder", this.Binder.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("itemsList", this.ItemsList.ClientID);
      controlDescriptor.AddElementProperty("headerRow", this.HeaderRow.ClientID);
      if (this.IncludeSitesNames)
      {
        controlDescriptor.AddProperty("moreText", (object) Res.Get<Labels>().MoreText);
        controlDescriptor.AddProperty("lessText", (object) Res.Get<Labels>().LessText);
        controlDescriptor.AddProperty("_confirmDialogMoreText", (object) string.Format(Res.Get<Labels>().ShowMore, (object) string.Empty));
        controlDescriptor.AddProperty("_confirmDialogLessText", (object) string.Format(Res.Get<Labels>().ShowLess, (object) string.Empty));
      }
      if (this.ShowConfirmationOnLanguageRemove && this.ConfirmationDialogPanel != null)
      {
        controlDescriptor.AddElementProperty("confirmationDialogPanel", this.ConfirmationDialogPanel.ClientID);
        controlDescriptor.AddElementProperty("confirmRemoveLink", this.ConfirmRemoveLink.ClientID);
        controlDescriptor.AddElementProperty("cancelRemoveLink", this.CancelRemoveLink.ClientID);
        controlDescriptor.AddElementProperty("cancelOkayLink", this.CancelOkayLink.ClientID);
        controlDescriptor.AddElementProperty("removeLanguageConfirmationMessagesPanel", this.RemoveLanguageConfirmationMessagesPanel.ClientID);
        controlDescriptor.AddElementProperty("removeLanguageConfirmationButtonsPanel", this.RemoveLanguageConfirmationButtonsPanel.ClientID);
        controlDescriptor.AddElementProperty("cannotRemoveLangMessagesPanel", this.CannotRemoveLangMessagesPanel.ClientID);
        controlDescriptor.AddElementProperty("sitesUsingCultureLabel", this.SitesUsingCultureLabel.ClientID);
        controlDescriptor.AddProperty("showConfirmationDialog", (object) true);
      }
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (LanguagesOrderedListField).Assembly.FullName;
      if (this.IncludeSitesNames)
        scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.ExpandableLabel.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.LanguagesOrderedListField.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
