// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ImportSubscribers
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms
{
  /// <summary>
  /// Form for importing subscribers from an external source into an existing list.
  /// </summary>
  public class ImportSubscribers : AjaxDialogBase
  {
    private Guid[] listIds;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Forms.ImportSubscribers.ascx");

    /// <summary>
    /// Gets the id od the list to which the subscribers ought to be imported.
    /// </summary>
    public Guid[] ListIds
    {
      get
      {
        if (this.listIds == null)
        {
          string[] strArray = this.ListIdsHidden.Value.Split(new string[1]
          {
            ","
          }, StringSplitOptions.RemoveEmptyEntries);
          List<Guid> guidList = new List<Guid>();
          foreach (string g in strArray)
            guidList.Add(new Guid(g));
          this.listIds = guidList.ToArray();
        }
        return this.listIds;
      }
      set => this.listIds = value;
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ImportSubscribers.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the reference to the radio button which marks the source of subscribers
    /// to be a comma separated list of values.
    /// </summary>
    protected virtual RadioButton CommaSeparatedListRadio => this.Container.GetControl<RadioButton>("commaSeparatedList", true);

    /// <summary>
    /// Gets the reference to the radio button which marks the source of subscribers
    /// to be tab separated list of values.
    /// </summary>
    protected virtual RadioButton TabSepearatedListRadio => this.Container.GetControl<RadioButton>("tabSepearatedList", true);

    /// <summary>Gets the reference to the import subscribers button.</summary>
    protected virtual LinkButton ImportSubscribersButton => this.Container.GetControl<LinkButton>("importSubscribersButton", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Web.UI.RadUpload" /> component for uploading the
    /// import files.
    /// </summary>
    protected virtual RadUpload SubscribersFileUpload => this.Container.GetControl<RadUpload>("subscribersFileUpload", true);

    /// <summary>
    /// Gets the reference to the hidden field containing the list id.
    /// </summary>
    protected virtual HiddenField ListIdsHidden => this.Container.GetControl<HiddenField>("listIdsHidden", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Web.UI.RadComboBox" /> that determines in which
    /// column is email info located.
    /// </summary>
    protected virtual RadComboBox EmailFieldMapping => this.Container.GetControl<RadComboBox>("emailFieldMapping", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Web.UI.RadComboBox" /> that determines in which
    /// column is first name info located.
    /// </summary>
    protected virtual RadComboBox FirstNameFieldMapping => this.Container.GetControl<RadComboBox>("firstNameFieldMapping", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Web.UI.RadComboBox" /> that determines in which
    /// column is last name info located.
    /// </summary>
    protected virtual RadComboBox LastNameFieldMapping => this.Container.GetControl<RadComboBox>("lastNameFieldMapping", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> which indicates whether existing
    /// subscribers should be overriden.
    /// </summary>
    protected virtual RadioButton OverwriteExistingSubscribers => this.Container.GetControl<RadioButton>("overwriteExistingSubscribers", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> which indicates that the first row
    /// of imported file should be skipped (when it represents headers).
    /// </summary>
    protected virtual CheckBox SkipFirstRow => this.Container.GetControl<CheckBox>("skipFirstRow", true);

    /// <summary>
    /// Gets the reference to the hidden field which indicates whether the dialog should be closed.
    /// </summary>
    protected virtual HiddenField ShouldCloseHidden => this.Container.GetControl<HiddenField>("shouldCloseHidden", true);

    /// <summary>Gets the reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SubscribersFileUpload.Localization.Select = Res.Get<NewslettersResources>().Browse;
      this.ImportSubscribersButton.Click += new EventHandler(this.ImportSubscribersButton_Click);
    }

    /// <summary>
    /// Handles the Click event of the ImportSubscribersButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void ImportSubscribersButton_Click(object sender, EventArgs e)
    {
      if (this.SubscribersFileUpload.UploadedFiles.Count > 0)
      {
        UploadedFile uploadedFile = this.SubscribersFileUpload.UploadedFiles[0];
        SubscriberImporter subscriberImporter = new SubscriberImporter();
        bool flag;
        if (this.CommaSeparatedListRadio.Checked)
        {
          flag = subscriberImporter.ImportFromCommaSeparatedList(uploadedFile.InputStream, this.ListIds, int.Parse(this.FirstNameFieldMapping.SelectedValue), int.Parse(this.LastNameFieldMapping.SelectedValue), int.Parse(this.EmailFieldMapping.SelectedValue), this.OverwriteExistingSubscribers.Checked, this.SkipFirstRow.Checked);
        }
        else
        {
          if (!this.TabSepearatedListRadio.Checked)
            throw new NotSupportedException();
          flag = subscriberImporter.ImportFromTabSeparatedList(uploadedFile.InputStream, this.ListIds, int.Parse(this.FirstNameFieldMapping.SelectedValue), int.Parse(this.LastNameFieldMapping.SelectedValue), int.Parse(this.EmailFieldMapping.SelectedValue), this.OverwriteExistingSubscribers.Checked, this.SkipFirstRow.Checked);
        }
        if (flag)
          this.ShouldCloseHidden.Value = "true";
        else
          this.MessageControl.ShowNegativeMessage(Res.Get<NewslettersResources>().FileCannotBeImported);
      }
      else if (this.SubscribersFileUpload.InvalidFiles.Count != 0)
        this.MessageControl.ShowNegativeMessage(Res.Get<NewslettersResources>().UnsupportedFileFormat);
      else
        this.MessageControl.ShowNegativeMessage(Res.Get<NewslettersResources>().NoFileSelectedForImport);
    }
  }
}
