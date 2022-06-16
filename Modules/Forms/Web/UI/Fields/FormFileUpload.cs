// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFileUpload
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.PublicControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  [ControlDesigner(typeof (FormFileUploadDesigner))]
  [PropertyEditorTitle(typeof (FormsResources), "FileUpload")]
  [DatabaseMapping(UserFriendlyDataType.FileUpload)]
  [RequiresDataItem]
  internal class FormFileUpload : 
    FieldControl,
    IMultiDisplayModesSupport,
    IFormFieldControl,
    IValidatable,
    IRequireLibrary
  {
    private IMetaField metaField;
    internal const string fieldScript = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormFileUpload.js";
    internal const string reqDataItemScriptFileName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Forms.FormFileUpload.ascx");
    private const string fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";
    private static readonly ConcurrentProperty<Dictionary<AllowedFileTypes, string[]>> allowedExtensionsDictionary = new ConcurrentProperty<Dictionary<AllowedFileTypes, string[]>>(new Func<Dictionary<AllowedFileTypes, string[]>>(FormFileUpload.BuildAllowedExtensionsDictionary));
    private FormDescription form;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormInstructionalText" /> class.
    /// </summary>
    public FormFileUpload()
    {
      this.LoadDefaultMetaField();
      this.LayoutTemplatePath = FormFileUpload.layoutTemplatePath;
      this.DisplayMode = FieldDisplayMode.Write;
      this.Title = Res.Get<FormsResources>().Untitled;
    }

    /// <inheritdoc />
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <inheritdoc />
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <inheritdoc />
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the label control that represents the title of the field control.
    /// </summary>
    /// <remarks>This control works in any mode.</remarks>
    protected internal virtual Label TitleLabel => this.DisplayMode == FieldDisplayMode.Read ? this.Container.GetControl<Label>("titleLabel_read", true) : this.Container.GetControl<Label>("titleLabel_write", true);

    /// <summary>
    /// Gets the reference to the label control that represents the description of the field control.
    /// </summary>
    /// <remarks>This control works in any mode.</remarks>
    protected internal virtual Label DescriptionLabel => this.DisplayMode == FieldDisplayMode.Read ? this.Container.GetControl<Label>("descriptionLabel_read", true) : this.Container.GetControl<Label>("descriptionLabel_write", true);

    /// <summary>
    /// Gets the reference to the label control that displays the example for this
    /// field control.
    /// </summary>
    /// <remarks>This control is mandatory only in the write mode.</remarks>
    protected internal virtual Label ExampleLabel => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<Label>("exampleLabel_write", true) : this.Container.GetControl<Label>("exampleLabel_read", false);

    /// <summary>
    /// Gets the reference to the link button that is used to expand the control.
    /// </summary>
    /// <value>The expand link.</value>
    protected internal virtual LinkButton ExpandLink => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<LinkButton>("expandButton_write", true) : this.Container.GetControl<LinkButton>("expandButton_read", false);

    /// <summary>Gets a reference to the rad upload</summary>
    internal RadUpload RadUploadControl { get; set; }

    /// <summary>Gets the reference to the selector container control.</summary>
    protected virtual EditorContentManagerDialog Selector => this.Container.GetControl<EditorContentManagerDialog>("selector", true);

    /// <summary>
    /// Gets the reference to the button that opens the dialog for choosing assets.
    /// </summary>
    protected virtual LinkButton SelectFileButton => this.Container.GetControl<LinkButton>("selectFileButton", true);

    /// <summary>
    /// Gets the reference to the control which displays text of the button that opens the dialog for choosing assets.
    /// </summary>
    protected virtual Literal SelectFileButtonText => this.Container.GetControl<Literal>("selectFileButtonText", true);

    /// <summary>
    /// Gets the control which displays the list of selected files
    /// </summary>
    protected virtual HtmlGenericControl SelectedFilesList => this.Container.GetControl<HtmlGenericControl>("selectedFiles_" + this.DisplayMode.ToString().ToLower(), true);

    /// <summary>
    /// Gets the placeholder control which displays the list of selected files in the backend.
    /// </summary>
    protected virtual Panel BackendUploadPlaceHolder => this.Container.GetControl<Panel>("backendUploadPlaceHolder", true);

    /// <summary>Gets the selected files hidden.</summary>
    /// <value>The selected files hidden.</value>
    protected virtual HtmlInputHidden SelectedFilesHidden => this.Container.GetControl<HtmlInputHidden>("selectedFiles", true);

    /// <inheritdoc />
    [TypeConverter(typeof (ExpandableObjectConverter))]
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

    /// <inheritdoc />
    public override object Value
    {
      get
      {
        object obj = (object) null;
        switch (this.DisplayMode)
        {
          case FieldDisplayMode.Read:
            obj = (object) this.UploadedFiles;
            break;
          case FieldDisplayMode.Write:
            obj = this.RadUploadControl != null ? (object) this.RadUploadControl.UploadedFiles : (object) (UploadedFileCollection) null;
            break;
        }
        return obj;
      }
      set => this.UploadedFiles = value is ContentLink[] ? this.GetMediaContentFromContentLinks((ContentLink[]) value).ToArray<MediaContent>() : value as MediaContent[];
    }

    public MediaContent[] UploadedFiles { get; set; }

    /// <summary>Gets or sets the min file size in megabytes (MB).</summary>
    public virtual int MinFileSizeInMb { get; set; }

    /// <summary>Gets or sets the max file size in megabytes (MB).</summary>
    public virtual int MaxFileSizeInMb { get; set; }

    /// <summary>Gets or sets the allowed file types.</summary>
    [TypeConverter(typeof (StringFlagsEnumTypeConverter))]
    public virtual AllowedFileTypes AllowedFileTypes { get; set; }

    /// <summary>Gets or sets the other file types.</summary>
    [TypeConverter(typeof (Telerik.Sitefinity.Utilities.TypeConverters.StringArrayConverter))]
    public virtual Array OtherFileTypes { get; set; }

    /// <summary>Gets or sets the range violation message.</summary>
    /// <value>The range violation message.</value>
    public virtual string RangeViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow multiple file attachments.
    /// </summary>
    public virtual bool AllowMultipleAttachments { get; set; }

    /// <summary>Gets or sets the form Id.</summary>
    /// <value>The form id.</value>
    [NonMultilingual]
    public Guid FormId { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The form id.</value>
    [NonMultilingual]
    public string FormsProviderName { get; set; }

    /// <summary>Gets the current form from specified FormId.</summary>
    /// <value>The form.</value>
    private FormDescription Form
    {
      get
      {
        if (this.form == null && this.FormId != Guid.Empty)
          this.form = FormsManager.GetManager(this.FormsProviderName).GetForm(this.FormId);
        return this.form;
      }
    }

    /// <summary>Gets or sets the read only modes.</summary>
    /// <value>The read only modes.</value>
    [TypeConverter(typeof (Telerik.Sitefinity.Utilities.TypeConverters.StringArrayConverter))]
    public string[] ReadOnlyModes { get; set; }

    /// <summary>Gets or sets the hidden modes.</summary>
    /// <value>The hidden modes.</value>
    [TypeConverter(typeof (Telerik.Sitefinity.Utilities.TypeConverters.StringArrayConverter))]
    public string[] HiddenModes { get; set; }

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.IsDesignMode() && !this.IsPreviewMode())
        return;
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        Panel control = this.Container.GetControl<Panel>("uploadPlaceHolder", true);
        if (this.IsBackend() && !this.IsPreviewMode())
        {
          this.BackendUploadPlaceHolder.Visible = true;
          Library formLibrary = this.Form.GetFormLibrary();
          string name = ((DataProviderBase) formLibrary.Provider).Name;
          this.Selector.UploadViewProviderName = name;
          this.Selector.SelectViewProviderName = name;
          this.SelectFileButtonText.Text = Res.Get<Labels>().Select + " " + Res.Get<Labels>().File;
          this.Selector.TargetLibraryId = formLibrary.Id;
          this.Selector.UseOnlyUploadMode = true;
          this.Selector.AllowCreateLibrary = false;
        }
        else
        {
          this.RadUploadControl = new RadUpload();
          this.RadUploadControl.Skin = "Default";
          this.RadUploadControl.Localization.Add = Res.Get<FormsResources>().AddAnotherFile;
          this.RadUploadControl.ControlObjectsVisibility = !this.AllowMultipleAttachments ? ControlObjectsVisibility.None : ControlObjectsVisibility.RemoveButtons | ControlObjectsVisibility.AddButton;
          this.RadUploadControl.AllowedFileExtensions = this.GetAllowedExtensions();
          control.Controls.Add((Control) this.RadUploadControl);
          this.RadUploadControl.ValidatingFile += new ValidateFileEventHandler(this.RadUploadControl_ValidatingFile);
        }
      }
      this.AddCssClass("sfFormFile");
      this.TitleLabel.Text = HttpUtility.HtmlEncode(this.Title);
      this.DescriptionLabel.Text = HttpUtility.HtmlEncode(this.Description);
      if (this.ExampleLabel == null)
        return;
      this.ExampleLabel.Text = HttpUtility.HtmlEncode(this.Example);
    }

    protected virtual void RadUploadControl_ValidatingFile(object sender, ValidateFileEventArgs e)
    {
      SitefinityLabel errorMessageControl = this.ErrorMessageControl as SitefinityLabel;
      if (this.MinFileSizeInMb > 0 || this.MaxFileSizeInMb > 0)
      {
        int num1 = this.MinFileSizeInMb * 1024 * 1024;
        int num2 = this.MaxFileSizeInMb * 1024 * 1024;
        if (num1 > 0 && e.UploadedFile.ContentLength < (long) num1 || num2 > 0 && e.UploadedFile.ContentLength > (long) num2)
        {
          if (errorMessageControl != null)
            errorMessageControl.Text = this.RangeViolationMessage;
          e.IsValid = false;
        }
      }
      string str = e.UploadedFile.GetExtension() ?? "";
      if (!str.IsNullOrEmpty())
        str = str.TrimStart('.').ToLower();
      if (this.RadUploadControl.AllowedFileExtensions == null || ((IEnumerable<string>) this.RadUploadControl.AllowedFileExtensions).Count<string>() <= 0 || ((IEnumerable<string>) this.RadUploadControl.AllowedFileExtensions).Contains<string>(str))
        return;
      if (errorMessageControl != null)
        errorMessageControl.Text = Res.Get<LibrariesResources>().CantUploadFiles.Arrange((object) ("." + str), (object) string.Join(", ", ((IEnumerable<string>) this.RadUploadControl.AllowedFileExtensions).Select<string, string>((Func<string, string>) (s => "." + s))));
      e.IsValid = false;
    }

    /// <summary>
    /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void RenderContents(HtmlTextWriter writer)
    {
      if (!this.IsDesignMode() || this.IsPreviewMode())
        base.RenderContents(writer);
      else
        writer.Write(Res.Get<PublicControlsResources>().ControlCannotBeRenderedInDesignMode.Arrange((object) Res.Get<FormsResources>().FileUpload));
    }

    /// <inheritdoc />
    public override bool IsValid() => !this.Visible || this.RadUploadControl.InvalidFiles.Count == 0;

    private string[] GetAllowedExtensions()
    {
      if (this.AllowedFileTypes == AllowedFileTypes.All)
        return (string[]) null;
      Dictionary<AllowedFileTypes, string[]> dictionary = FormFileUpload.allowedExtensionsDictionary.Value;
      Array values = Enum.GetValues(typeof (AllowedFileTypes));
      IEnumerable<string> strings = (IEnumerable<string>) new string[0];
      foreach (AllowedFileTypes allowedFileTypes in values)
      {
        if (this.AllowedFileTypes.HasFlag((Enum) allowedFileTypes) && dictionary.ContainsKey(allowedFileTypes))
          strings = strings.Union<string>((IEnumerable<string>) dictionary[allowedFileTypes], (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      }
      if (this.OtherFileTypes != null && this.AllowedFileTypes.HasFlag((Enum) AllowedFileTypes.Other))
      {
        string[] array = ((IEnumerable<string>) this.OtherFileTypes).Select<string, string>((Func<string, string>) (s => s.ToLowerInvariant())).ToArray<string>();
        strings = strings.Union<string>(array.Cast<string>(), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      }
      return strings.ToArray<string>();
    }

    private static Dictionary<AllowedFileTypes, string[]> BuildAllowedExtensionsDictionary() => new Dictionary<AllowedFileTypes, string[]>()
    {
      [AllowedFileTypes.Images] = new string[5]
      {
        "jpg",
        "jpeg",
        "png",
        "gif",
        "bmp"
      },
      [AllowedFileTypes.Documents] = new string[9]
      {
        "pdf",
        "doc",
        "docx",
        "ppt",
        "pptx",
        "pps",
        "ppsx",
        "xls",
        "xlsx"
      },
      [AllowedFileTypes.Audio] = new string[4]
      {
        "mp3",
        "ogg",
        "wav",
        "wma"
      },
      [AllowedFileTypes.Video] = new string[6]
      {
        "avi",
        "mpg",
        "mpeg",
        "mov",
        "mp4",
        "wmv"
      }
    };

    /// <summary>Gets the media content from content links.</summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    private IEnumerable<MediaContent> GetMediaContentFromContentLinks(
      ContentLink[] values)
    {
      ContentLink[] contentLinkArray = values;
      for (int index = 0; index < contentLinkArray.Length; ++index)
      {
        if (contentLinkArray[index].GetLinkedItem() is MediaContent linkedItem)
          yield return linkedItem;
      }
      contentLinkArray = (ContentLink[]) null;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      if (!this.IsDesignMode() || this.IsPreviewMode())
      {
        if (this.IsBackend() && !this.IsPreviewMode())
        {
          controlDescriptor.AddProperty("documentServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/DocumentService.svc/"));
          controlDescriptor.AddElementProperty("selectedFilesList", this.SelectedFilesList.ClientID);
          if (this.DisplayMode == FieldDisplayMode.Write)
          {
            controlDescriptor.AddProperty("cantUploadFilesFormat", (object) Res.Get<LibrariesResources>().CantUploadFiles);
            controlDescriptor.AddProperty("rangeViolationMessage", (object) this.RangeViolationMessage);
            controlDescriptor.AddProperty("errorLabelId", (object) this.ErrorMessageControl.ClientID);
            controlDescriptor.AddProperty("maxFileSize", (object) (this.MaxFileSizeInMb * 1024 * 1024));
            controlDescriptor.AddProperty("minFileSize", (object) (this.MinFileSizeInMb * 1024 * 1024));
            controlDescriptor.AddProperty("allowedExtensions", (object) this.GetAllowedExtensions());
            controlDescriptor.AddProperty("allowMultipleAttachments", (object) this.AllowMultipleAttachments);
            controlDescriptor.AddProperty("documentTypeFullName", (object) typeof (Document).FullName);
            controlDescriptor.AddProperty("entriesTypeName", (object) this.Form.EntriesTypeName);
            controlDescriptor.AddProperty("providerName", (object) ((DataProviderBase) this.Form.Provider).Name);
            controlDescriptor.AddProperty("contentLinksApplicationName", (object) ContentLinksManager.GetManager().Provider.ApplicationName);
            controlDescriptor.AddElementProperty("selectFileButton", this.SelectFileButton.ClientID);
            controlDescriptor.AddComponentProperty("selector", this.Selector.ClientID);
            controlDescriptor.AddElementProperty("selectedFilesHiddenElement", this.SelectedFilesHidden.ClientID);
          }
        }
        else if (this.DisplayMode == FieldDisplayMode.Write && this.RadUploadControl != null)
        {
          controlDescriptor.AddProperty("cantUploadFilesFormat", (object) Res.Get<LibrariesResources>().CantUploadFiles);
          controlDescriptor.AddProperty("rangeViolationMessage", (object) this.RangeViolationMessage);
          controlDescriptor.AddProperty("errorLabelId", (object) this.ErrorMessageControl.ClientID);
          controlDescriptor.AddProperty("maxFileSize", (object) (this.MaxFileSizeInMb * 1024 * 1024));
          controlDescriptor.AddProperty("minFileSize", (object) (this.MinFileSizeInMb * 1024 * 1024));
          controlDescriptor.AddProperty("allowedExtensions", (object) this.GetAllowedExtensions());
          controlDescriptor.AddComponentProperty("radUpload", this.RadUploadControl.ClientID);
          controlDescriptor.AddProperty("value", (object) null);
        }
        else if (this.DisplayMode == FieldDisplayMode.Read)
          controlDescriptor.AddProperty("value", (object) null);
      }
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (FormFileUpload).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormFileUpload.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", fullName)
      };
    }
  }
}
