// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Contracts;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails
{
  public class ThumbnailProfileField : FieldControl
  {
    public static readonly string layouthTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.ThumbnailProfileField.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailProfileField.js";
    private const string iReuiresProviderScript = "Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js";
    private bool bindOnLoad = true;
    private string libraryType;
    private string thumbnailSettingsServiceUrl;

    public ThumbnailProfileField() => this.LayoutTemplatePath = ThumbnailProfileField.layouthTemplatePath;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">
    /// An instance of <see cref="T:Telerik.Sitefinity.Web.UI.GenericContainer" /> class in which the template was instantiated.
    /// </param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.TitleLabel.SetTextOrHide(this.Title);
      this.DescriptionLabel.SetTextOrHide(this.Description);
      this.ExampleLabel.SetTextOrHide(this.Example);
    }

    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IThumbnailProfileFieldDefinition profileFieldDefinition))
        return;
      this.LibraryType = profileFieldDefinition.LibraryType;
      this.ThumbnailSettingsServiceUrl = profileFieldDefinition.ThumbnailSettingsServiceUrl;
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
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        controlDescriptor.AddElementProperty("openSelector", this.OpenSelector.ClientID);
        controlDescriptor.AddComponentProperty("thumbnailProfileSelectorDialog", this.ThumbnailProfileSelectorDialog.ClientID);
      }
      controlDescriptor.AddComponentProperty("clientLabelManager", this.LabelManager.ClientID);
      controlDescriptor.AddElementProperty("generateThumbnail", this.GenerateThumbnail.ClientID);
      controlDescriptor.AddElementProperty("doNotGenerateThumbnail", this.DoNotGenerateThumbnail.ClientID);
      controlDescriptor.AddProperty("thumbnailSettingsServiceUrl", (object) RouteHelper.ResolveUrl(this.ThumbnailSettingsServiceUrl, UrlResolveOptions.Rooted));
      controlDescriptor.AddProperty("libType", (object) this.LibraryType);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.TelerikSitefinity;

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script
    /// resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of
    /// <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js", typeof (ThumbnailProfileField).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailProfileField.js", typeof (ThumbnailProfileField).Assembly.FullName)
    };

    /// <summary>
    /// Gets or sets the option to automatically bind the selector when the control loads
    /// </summary>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => ThumbnailProfileField.layouthTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the title label</summary>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>Gets the description label</summary>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>Gets the example label</summary>
    protected internal override WebControl ExampleControl => (WebControl) this.DescriptionLabel;

    /// <summary>Gets the title label</summary>
    protected SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("titleLabel", true);

    /// <summary>Gets the description label</summary>
    protected SitefinityLabel DescriptionLabel => this.Container.GetControl<SitefinityLabel>("descriptionLabel", true);

    /// <summary>Gets the example label</summary>
    protected SitefinityLabel ExampleLabel => this.Container.GetControl<SitefinityLabel>("exampleLabel", true);

    /// <summary>Gets the button that opens the selector.</summary>
    /// <value>The button.</value>
    protected virtual LinkButton OpenSelector => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<LinkButton>("openSelector", true) : (LinkButton) null;

    /// <summary>Gets the thumbnail profile selector dialog</summary>
    protected ThumbnailProfileSelectorDialog ThumbnailProfileSelectorDialog => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<ThumbnailProfileSelectorDialog>("thumbnailProfileSelectorDialog", true) : (ThumbnailProfileSelectorDialog) null;

    /// <summary>Gets the "generate thumbnail" control</summary>
    protected WebControl GenerateThumbnail
    {
      get
      {
        if (this.DisplayMode == FieldDisplayMode.Read)
          return (WebControl) this.Container.GetControl<Label>("generateThumbnail_read", true);
        return this.DisplayMode == FieldDisplayMode.Write ? (WebControl) this.Container.GetControl<RadioButton>("generateThumbnail_write", true) : (WebControl) null;
      }
    }

    /// <summary>Gets the "do not generate thumbnail" control</summary>
    protected WebControl DoNotGenerateThumbnail
    {
      get
      {
        if (this.DisplayMode == FieldDisplayMode.Read)
          return (WebControl) this.Container.GetControl<Label>("doNotGenerateThumbnail_read", true);
        return this.DisplayMode == FieldDisplayMode.Write ? (WebControl) this.Container.GetControl<RadioButton>("doNotGenerateThumbnail_write", true) : (WebControl) null;
      }
    }

    /// <summary>Gets the label manager.</summary>
    protected ClientLabelManager LabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    internal string LibraryType
    {
      get => this.libraryType;
      set => this.libraryType = value;
    }

    internal string ThumbnailSettingsServiceUrl
    {
      get => this.thumbnailSettingsServiceUrl;
      set => this.thumbnailSettingsServiceUrl = value;
    }
  }
}
