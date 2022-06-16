// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ContentViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Contracts;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// An abstract class that has all information to construct the actual view.
  /// </summary>
  [DefaultProperty("ViewName")]
  public abstract class ContentViewDefinition : DefinitionBase, IContentViewDefinition, IDefinition
  {
    private IContentViewControlDefinition control;
    private string controlDefinitionName;
    private string description;
    private FieldDisplayMode displayMode;
    private List<IContentViewPlugInDefinition> plugIns;
    private string resourceClassId;
    private string templateName;
    private string templatePath;
    private string templateKey;
    private string title;
    private string parentTitleFormat;
    private Type viewType;
    private string viewName;
    private string viewVirtualPath;
    private bool? useWorkflow;
    private string controlId;
    private Dictionary<string, string> externalClientScripts;
    private Dictionary<string, string> clientMappedCommnadNames;
    private Dictionary<string, string> localization;
    private ICommentsSettingsDefinition commentsSettingsDefinition;
    private List<IPromptDialogDefinition> promptDialogs;
    private IList<ILabelDefinition> labels;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewDefinition" /> class.
    /// </summary>
    public ContentViewDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ContentViewDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets the collection of dialog definitions that are used on the view.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IPromptDialogDefinition> PromptDialogs
    {
      get => this.ResolveProperty<List<IPromptDialogDefinition>>(nameof (PromptDialogs), this.promptDialogs);
      internal set => this.promptDialogs = value;
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ContentViewDefinition GetDefinition() => this;

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName
    {
      get => this.ResolveProperty<string>(nameof (ControlDefinitionName), this.controlDefinitionName);
      set => this.controlDefinitionName = value;
    }

    /// <summary>
    /// Gets or sets the control definition (parent) that holds this view.
    /// </summary>
    /// <value></value>
    [Browsable(false)]
    public IContentViewControlDefinition Control
    {
      get => this.control;
      set => this.control = value;
    }

    /// <summary>
    /// Use this as a key to load a resource, in case ResourceClassId is set;
    /// othewrise use it directly as description.
    /// </summary>
    /// <value>The description of the view.</value>
    public string Description
    {
      get => this.ResolveProperty<string>(nameof (Description), this.description);
      set => this.description = value;
    }

    /// <summary>
    /// Determines the display mode in which the FieldControls of the view ought to be rendered. See enumeration for possible values.
    /// </summary>
    /// <value>The display mode of the FieldControls.</value>
    public FieldDisplayMode DisplayMode
    {
      get => this.ResolveProperty<FieldDisplayMode>(nameof (DisplayMode), this.displayMode);
      set => this.displayMode = value;
    }

    /// <summary>
    /// Defines a list of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewPlugInDefinition" /> objects.
    /// </summary>
    /// <value>A list of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewPlugInDefinition" /> objects</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public IEnumerable<IContentViewPlugInDefinition> PlugIns => this.ResolveProperty<IEnumerable<IContentViewPlugInDefinition>>(nameof (PlugIns), (IEnumerable<IContentViewPlugInDefinition>) this.plugIns);

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The resource class pageId.</value>
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }

    /// <summary>Gets or sets the name of the view template.</summary>
    /// <value>The name of the view template.</value>
    public string TemplateName
    {
      get => this.ResolveProperty<string>(nameof (TemplateName), this.templateName);
      set => this.templateName = value;
    }

    /// <summary>Gets or sets the path of the view template.</summary>
    /// <value>The path of the view template.</value>
    public string TemplatePath
    {
      get => this.ResolveProperty<string>(nameof (TemplatePath), this.templatePath);
      set => this.templatePath = value;
    }

    /// <summary>Gets or sets the template key.</summary>
    /// <value>The template key.</value>
    public string TemplateKey
    {
      get => this.ResolveProperty<string>(nameof (TemplateKey), this.templateKey);
      set => this.templateKey = value;
    }

    /// <summary>Gets or sets the title of the view.</summary>
    /// <value>The title of the view.</value>
    public string Title
    {
      get => this.ResolveProperty<string>(nameof (Title), this.title);
      set => this.title = value;
    }

    /// <summary>
    /// Gets or sets the format for the Title generated from the parent content item name.
    /// </summary>
    /// <value>The parent title format.</value>
    /// <remarks>
    /// This format will be used if the Content type has a parent.
    /// Set it to null if you want the view title to be configured from the definition's Title property.
    /// </remarks>
    public string ParentTitleFormat
    {
      get => this.ResolveProperty<string>(nameof (ParentTitleFormat), this.parentTitleFormat);
      set => this.parentTitleFormat = value;
    }

    /// <summary>
    /// Gets or sets the value that indicates if this view is a master view. If true view is
    /// a master view; otherwise it is a detail view.
    /// </summary>
    /// <value></value>
    public bool IsMasterView => typeof (IContentViewMasterDefinition).IsAssignableFrom(this.GetType());

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    [PropertyPersistence(IsKey = true)]
    public string ViewName
    {
      get => this.ResolveProperty<string>(nameof (ViewName), this.viewName);
      set => this.viewName = value;
    }

    /// <summary>Gets or sets the type of the view.</summary>
    /// <value>The type of the view.</value>
    public Type ViewType
    {
      get => this.ResolveProperty<Type>(nameof (ViewType), this.viewType);
      set => this.viewType = value;
    }

    /// <summary>
    /// Gets or sets the virtual path of the view if the view is implemented
    /// as a user control.
    /// </summary>
    /// <value></value>
    public string ViewVirtualPath
    {
      get => this.ResolveProperty<string>(nameof (ViewVirtualPath), this.viewVirtualPath);
      set => this.viewVirtualPath = value;
    }

    /// <summary>
    /// Gets or sets value determining weather view ought to use workflow.
    /// </summary>
    /// <value>True if view ought to use workflow; otherwise false.</value>
    public bool? UseWorkflow
    {
      get => this.ResolveProperty<bool?>(nameof (UseWorkflow), this.useWorkflow);
      set => this.useWorkflow = value;
    }

    /// <summary>
    /// Gets or sets the value for the <see cref="T:System.Web.UI.Control" /> ID property of the control that will be constructed based on this definition.
    /// </summary>
    /// <value>The control id.</value>
    public string ControlId
    {
      get => this.ResolveProperty<string>(nameof (ControlId), this.controlId);
      set => this.controlId = value;
    }

    /// <summary>
    /// Gets or sets a dictionary of external scripts to use with the MasterView and DetailFormView. The key of each
    /// element is the virtual path to the external script, while the value is the name of a method in
    /// that external script that will handle the ViewLoaded event.
    /// </summary>
    /// <value>The dictionary of external client scripts.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Dictionary<string, string> ExternalClientScripts
    {
      get
      {
        if (this.externalClientScripts == null)
          this.externalClientScripts = ((ContentViewDefinitionElement) this.ConfigDefinition).ExternalClientScripts;
        return this.externalClientScripts;
      }
      set => this.externalClientScripts = value;
    }

    /// <summary>
    /// Gets or sets a dictionary of localization properties.
    /// Key identifies the localization string, and value is the current culture's translation
    /// </summary>
    /// <value>The dictionary of localization strings</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Dictionary<string, string> Localization
    {
      get => this.ResolveProperty<Dictionary<string, string>>(nameof (Localization), this.localization);
      set => this.localization = value;
    }

    /// <summary>
    /// Gets or sets a dictionary defining custom client command replacing the standard ones, if applicable.
    /// Key: the name of the standard command (e.g. "view")
    /// Value: the custom command name (e.g. "viewOriginalImage")
    /// </summary>
    /// <value>The client mapped command names.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Dictionary<string, string> ClientMappedCommnadNames
    {
      get => this.ResolveProperty<Dictionary<string, string>>(nameof (ClientMappedCommnadNames), this.clientMappedCommnadNames);
      set => this.clientMappedCommnadNames = value;
    }

    /// <summary>Gets or sets the comments definition.</summary>
    /// <value>The comments definition.</value>
    public ICommentsSettingsDefinition CommentsSettingsDefinition
    {
      get
      {
        if (this.commentsSettingsDefinition == null)
        {
          ICommentsSettingsDefinition settingsDefinition = this.ResolveProperty<ICommentsSettingsDefinition>(nameof (CommentsSettingsDefinition), this.commentsSettingsDefinition);
          this.commentsSettingsDefinition = settingsDefinition == null ? (ICommentsSettingsDefinition) new Telerik.Sitefinity.Modules.GenericContent.CommentsSettingsDefinition() : (ICommentsSettingsDefinition) settingsDefinition.GetDefinition();
        }
        return this.commentsSettingsDefinition;
      }
      set => this.commentsSettingsDefinition = value;
    }

    /// <summary>
    /// Read-only collection of resource strings that are going to be available on the client via an instance of ClientLabelManager
    /// </summary>
    public IList<ILabelDefinition> Labels
    {
      get
      {
        if (this.labels == null)
        {
          ConfigElementList<LabelDefinitionElement> source = this.ResolveProperty<ConfigElementList<LabelDefinitionElement>>("LabelsConfig", (ConfigElementList<LabelDefinitionElement>) null);
          if (source != null)
            this.labels = (IList<ILabelDefinition>) source.Cast<ILabelDefinition>().ToList<ILabelDefinition>();
        }
        return this.labels;
      }
      set => this.labels = value;
    }
  }
}
