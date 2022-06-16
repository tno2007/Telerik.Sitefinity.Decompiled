// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Extenders;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Extenders.Definitions;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>A field control for selecting libraries.</summary>
  public class LibrarySelectorField : FieldControl, IExpandableControl
  {
    private bool? expanded = new bool?(true);
    private bool showOnlySystemLibraries;
    private string sortExpression;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.LibrarySelectorField.ascx");
    private const string script = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.LibrarySelectorField.js";
    private const string iparentSelectorField = "Telerik.Sitefinity.Web.Scripts.IParentSelectorField.js";
    private ExpandableControlDefinition expandableControlDefinition;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField" /> class.
    /// </summary>
    public LibrarySelectorField() => this.LayoutTemplatePath = LibrarySelectorField.layoutTemplatePath;

    /// <summary>
    /// Gets web service url based on <see cref="P:Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField.ContentType" />
    /// </summary>
    protected string WebServiceUrl
    {
      get
      {
        if (this.ContentType == typeof (Document) || this.ContentType == typeof (DocumentLibrary))
          return "~/Sitefinity/Services/Content/DocumentLibraryService.svc/";
        if (this.ContentType == typeof (Video) || this.ContentType == typeof (VideoLibrary))
          return "~/Sitefinity/Services/Content/VideoLibraryService.svc/";
        if (this.ContentType == typeof (Telerik.Sitefinity.Libraries.Model.Image) || this.ContentType == typeof (Album))
          return "~/Sitefinity/Services/Content/AlbumService.svc/";
        throw new NotImplementedException();
      }
    }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    public Type ContentType { get; set; }

    /// <summary>
    /// Gets or sets whether the selctor should show User files or general libraries
    /// </summary>
    public bool ShowOnlySystemLibraries
    {
      get => this.showOnlySystemLibraries;
      set => this.showOnlySystemLibraries = value;
    }

    /// <summary>Gets or sets the sort expression for libraries.</summary>
    public string SortExpression
    {
      get => this.sortExpression;
      set => this.sortExpression = value;
    }

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The value.</value>
    public override object Value
    {
      get => (object) null;
      set
      {
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the expandable control definition.</summary>
    /// <value>The expandable control definition.</value>
    public ExpandableControlDefinition ExpandableControlDefinition
    {
      get
      {
        if (this.expandableControlDefinition == null)
          this.expandableControlDefinition = new ExpandableControlDefinition()
          {
            Expanded = this.Expanded,
            ExpandText = this.ExpandText
          };
        return this.expandableControlDefinition;
      }
      set
      {
        this.expandableControlDefinition = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the text that will be displayed on the control that can expand the hidden part.
    /// </summary>
    /// <value></value>
    public string ExpandText { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the control is expanded by default. If control is to
    /// be expanded by default true; otherwise false.
    /// </summary>
    /// <value></value>
    public bool? Expanded
    {
      get => this.expanded;
      set => this.expanded = value;
    }

    /// <summary>
    /// Gets or sets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    /// <value></value>
    public WebControl ExpandControl => (WebControl) this.ExpandLink;

    /// <summary>
    /// Gets or sets the reference to the control that is hidden when Expanded is false and displayed
    /// upon clicking of the ExpandControl.
    /// </summary>
    /// <value></value>
    public WebControl ExpandTarget => this.Container.GetControl<WebControl>("expandableTarget", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the label control that represents the title of the field control.
    /// </summary>
    /// <remarks>This control is mandatory only in write mode.</remarks>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the label control that represents the description of the field control.
    /// </summary>
    /// <remarks>This control is mandatory only in write mode.</remarks>
    protected internal virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", true);

    /// <summary>
    /// Gets the reference to the label control that displays the example for this
    /// field control.
    /// </summary>
    /// <remarks>This control is mandatory only in the write mode.</remarks>
    protected internal virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", true);

    /// <summary>
    /// Gets the reference to the link button that is used to expand the control.
    /// </summary>
    /// <value>The expand link.</value>
    protected internal virtual LinkButton ExpandLink => this.Container.GetControl<LinkButton>("expandButton", true);

    /// <summary>
    /// Gets the text box control which should be displayed at Write Mode
    /// </summary>
    /// <value>The text box control.</value>
    protected internal virtual DropDownList ListControl => this.Container.GetControl<DropDownList>("librariesDropDown", true);

    /// <summary>
    /// Gets the label control which should be displayed at Read Mode
    /// </summary>
    /// <value>The text label control.</value>
    protected internal virtual Label LabelControl => this.Container.GetControl<Label>("textLabel", true);

    /// <summary>Gets the hidden control.</summary>
    /// <value>The hidden control.</value>
    protected internal virtual HiddenField HiddenControl => this.Container.GetControl<HiddenField>("idHidden", true);

    /// <summary>Gets the binder.</summary>
    /// <value>The binder.</value>
    protected internal virtual ClientBinder Binder => this.Container.GetControl<ClientBinder>("librariesBinder", true);

    /// <summary>Gets or sets the client side templates container.</summary>
    /// <value>The client side templates container.</value>
    protected internal virtual Panel ClientSideTemplatesContainer { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConstructControl();

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptorList = new List<ScriptDescriptor>();
      if (this.DisplayMode == FieldDisplayMode.Write)
        scriptDescriptorList.Add((ScriptDescriptor) this.GetExpandableExtenderDescriptor(this.ClientID));
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_libraryType", (object) this.ContentType.FullName);
      controlDescriptor.AddProperty("_serviceBaseUrl", (object) VirtualPathUtility.ToAbsolute(this.WebServiceUrl));
      controlDescriptor.AddElementProperty("labelElement", this.LabelControl.ClientID);
      controlDescriptor.AddElementProperty("selectElement", this.ListControl.ClientID);
      controlDescriptor.AddComponentProperty("binder", this.Binder.ClientID);
      if (this.ContentType == typeof (Album))
        controlDescriptor.AddProperty("_selectedOptionText", (object) Res.Get<ImagesResources>().SelectAnAlbum);
      else
        controlDescriptor.AddProperty("_selectedOptionText", (object) Res.Get<LibrariesResources>().SelectLibrary);
      scriptDescriptorList.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptorList.ToArray();
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (LibrarySelectorField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        this.GetExpandableExtenderScript(),
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.IParentSelectorField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.LibrarySelectorField.js", fullName)
      };
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      if (!(definition is ILibrarySelectorFieldDefinition selectorFieldDefinition))
        return;
      this.ExpandableControlDefinition = new ExpandableControlDefinition()
      {
        ControlDefinitionName = selectorFieldDefinition.ControlDefinitionName,
        ViewName = selectorFieldDefinition.ViewName,
        SectionName = selectorFieldDefinition.SectionName,
        FieldName = selectorFieldDefinition.FieldName
      };
      this.ContentType = selectorFieldDefinition.ContentType;
      this.ShowOnlySystemLibraries = selectorFieldDefinition.ShowOnlySystemLibraries;
      this.SortExpression = selectorFieldDefinition.SortExpression;
    }

    /// <summary>
    /// The method that is used to set the properties of the contained controls.
    /// </summary>
    protected internal virtual void ConstructControl()
    {
      this.TitleLabel.Text = this.Title;
      switch (this.DisplayMode)
      {
        case FieldDisplayMode.Read:
          this.LabelControl.TabIndex = this.TabIndex;
          break;
        case FieldDisplayMode.Write:
          this.ExampleLabel.Text = this.Example;
          this.TitleLabel.AssociatedControlID = this.ListControl.ID;
          this.DescriptionLabel.Text = this.Description;
          this.Binder.ServiceUrl = VirtualPathUtility.ToAbsolute(this.WebServiceUrl);
          this.ConfigureExpandableControl((IExpandableControlDefinition) this.ExpandableControlDefinition);
          if (string.IsNullOrEmpty(this.SortExpression))
            break;
          this.Binder.DefaultSortExpression = this.SortExpression;
          break;
      }
    }

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();
  }
}
