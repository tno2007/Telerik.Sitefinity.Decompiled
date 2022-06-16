// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Contracts;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Config
{
  /// <summary>
  /// Configuration element for the particular view. All more specific view configurations must inherit this class.
  /// </summary>
  [DefaultProperty("ViewName")]
  public abstract class ContentViewDefinitionElement : 
    DefinitionConfigElement,
    IContentViewDefinition,
    IDefinition
  {
    private string controlDefinitionName;
    private IList<IContentViewPlugInDefinition> plugIns;
    private Dictionary<string, string> externalClientScripts = new Dictionary<string, string>();
    private Dictionary<string, string> localization;
    private Dictionary<string, string> clientMappedCommnadNames = new Dictionary<string, string>();
    private List<IPromptDialogDefinition> promptDialogs;

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ContentViewDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a list of prompt dialogs. Initialize from PromptDialogsConfig
    /// </summary>
    /// <value>The prompt dialogs added to the instance of the control</value>
    public virtual List<IPromptDialogDefinition> PromptDialogs
    {
      get
      {
        if (this.promptDialogs == null)
          this.promptDialogs = this.PromptDialogsConfig.Elements.Select<PromptDialogElement, IPromptDialogDefinition>((Func<PromptDialogElement, IPromptDialogDefinition>) (p => (IPromptDialogDefinition) p.ToDefinition())).ToList<IPromptDialogDefinition>();
        return this.promptDialogs;
      }
      set => this.promptDialogs = value;
    }

    /// <summary>
    /// Gets the collection of dialog config elements that are used on the view.
    /// </summary>
    [ConfigurationProperty("PromptDialogs")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DetailViewPromptDialogsDescription", Title = "DetailViewPromptDialogsCaption")]
    public ConfigElementList<PromptDialogElement> PromptDialogsConfig => (ConfigElementList<PromptDialogElement>) this["PromptDialogs"];

    public override DefinitionBase GetDefinition() => throw new NotImplementedException();

    /// <summary>
    /// Defines a dictionary of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewPlugInElement" /> elements.
    /// </summary>
    /// <value>a dictionary of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewPlugInElement" /> elements</value>
    [ConfigurationProperty("plugInsConfig")]
    [ConfigurationCollection(typeof (ContentViewPlugInElement), AddItemName = "plugInElement")]
    public ConfigElementDictionary<string, ContentViewPlugInElement> PlugInsConfig => (ConfigElementDictionary<string, ContentViewPlugInElement>) this["plugInsConfig"];

    /// <summary>
    /// Defines a list of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewPlugInDefinition" /> objects.
    /// </summary>
    /// <value>A list of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewPlugInDefinition" /> objects</value>
    public IEnumerable<IContentViewPlugInDefinition> PlugIns => this.PlugInsConfig.Cast<IContentViewPlugInDefinition>();

    /// <summary>
    /// Gets or sets a dictionary of external scripts to use with the MasterView and DetailFormView. The key of each
    /// element is the virtual path to the external script, while the value is the name of a method in
    /// that external script that will handle the ViewLoaded event.
    /// </summary>
    [Obsolete("Use 'Scripts' property instead.")]
    [Browsable(false)]
    public Dictionary<string, string> ExternalClientScripts
    {
      get => this.Scripts.Elements.ToDictionary<ClientScriptElement, string, string>((Func<ClientScriptElement, string>) (s => s.ScriptLocation), (Func<ClientScriptElement, string>) (s => s.LoadMethodName));
      set
      {
        ConfigElementDictionary<string, ClientScriptElement> scripts = this.Scripts;
        foreach (KeyValuePair<string, string> keyValuePair in value)
        {
          ClientScriptElement clientScriptElement;
          if (scripts.TryGetValue(keyValuePair.Key, out clientScriptElement))
            clientScriptElement.LoadMethodName = keyValuePair.Value;
          else
            scripts.Add(new ClientScriptElement((ConfigElement) scripts)
            {
              ScriptLocation = keyValuePair.Key,
              LoadMethodName = keyValuePair.Value
            });
        }
      }
    }

    [ConfigurationProperty("scripts")]
    [ConfigurationCollection(typeof (ClientScriptElement), AddItemName = "script")]
    public ConfigElementDictionary<string, ClientScriptElement> Scripts => (ConfigElementDictionary<string, ClientScriptElement>) this["scripts"];

    /// <summary>Configuration property for localization</summary>
    [ConfigurationProperty("localization")]
    public ConfigElementList<LocalizationMessageElement> LocalizationConfig
    {
      get => (ConfigElementList<LocalizationMessageElement>) this["localization"];
      set => this["localization"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a dictionary of localization properties.
    /// Key identifies the localization string, and value is the current culture's translation
    /// </summary>
    /// <value>The dictionary of localization strings</value>
    public Dictionary<string, string> Localization
    {
      get
      {
        if (this.localization == null)
          this.localization = this.LocalizationConfig.Elements.ToDictionary<LocalizationMessageElement, string, string>((Func<LocalizationMessageElement, string>) (l => l.MessageKey), (Func<LocalizationMessageElement, string>) (l => l.Translation));
        return this.localization;
      }
      set => this.localization = value;
    }

    /// <summary>
    /// Gets or sets the control definition (parent) that holds this view.
    /// </summary>
    /// <value></value>
    [Browsable(false)]
    public string ControlDefinitionName
    {
      get
      {
        if (string.IsNullOrEmpty(this.controlDefinitionName))
        {
          if (!(this.Parent is ContentViewControlElement parent))
            parent = this.Parent.Parent as ContentViewControlElement;
          this.controlDefinitionName = parent.ControlDefinitionName;
        }
        return this.controlDefinitionName;
      }
      set => this.controlDefinitionName = value;
    }

    /// <summary>Gets or sets the description of the view.</summary>
    /// <value>The description of the view.</value>
    [ConfigurationProperty("description", DefaultValue = "")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>
    /// Determines the display mode in which the FieldControls of the view ought to be rendered. See enumeration for possible values.
    /// </summary>
    /// <value>The display mode of the view.</value>
    [ConfigurationProperty("displayMode", DefaultValue = FieldDisplayMode.Read, IsRequired = true)]
    public FieldDisplayMode DisplayMode
    {
      get => (FieldDisplayMode) this["displayMode"];
      set => this["displayMode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The resource class pageId.</value>
    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>Gets or sets the name of the view template.</summary>
    /// <value>The name of the view template.</value>
    [ConfigurationProperty("templateName", DefaultValue = "")]
    public string TemplateName
    {
      get => (string) this["templateName"];
      set => this["templateName"] = (object) value;
    }

    /// <summary>Gets or sets the path of the view template.</summary>
    /// <value>The path of the view template.</value>
    [ConfigurationProperty("templatePath", DefaultValue = "")]
    public string TemplatePath
    {
      get => (string) this["templatePath"];
      set => this["templatePath"] = (object) value;
    }

    /// <summary>
    /// Gets or sets value determining weather view ought to use workflow.
    /// </summary>
    /// <value>True if view ought to use workflow; otherwise false.</value>
    [ConfigurationProperty("useWorkflow", IsRequired = false)]
    public bool? UseWorkflow
    {
      get => (bool?) this["useWorkflow"];
      set => this["useWorkflow"] = (object) value;
    }

    /// <summary>Gets or sets the template key.</summary>
    /// <value>The template key.</value>
    [ConfigurationProperty("templateKey", DefaultValue = "")]
    public string TemplateKey
    {
      get => (string) this["templateKey"];
      set => this["templateKey"] = (object) value;
    }

    /// <summary>Gets or sets the title of the view.</summary>
    /// <value>The title of the view.</value>
    [ConfigurationProperty("title", DefaultValue = "", IsRequired = true)]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the format for the Title generated from the parent content item name.
    /// </summary>
    /// <value>The parent title format.</value>
    /// <remarks>
    /// This format will be used if the Content type has a parent.
    /// Set it to null if you want the view title to be configured from the definition's Title property.
    /// </remarks>
    [ConfigurationProperty("parentTitleFormat", DefaultValue = "", IsRequired = false)]
    public string ParentTitleFormat
    {
      get => (string) this["parentTitleFormat"];
      set => this["parentTitleFormat"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value that indicates if this view is a master view. If true view is
    /// a master view; otherwise it is a detail view.
    /// </summary>
    [ConfigurationProperty("isMasterView")]
    public bool IsMasterView => typeof (IContentViewMasterDefinition).IsAssignableFrom(this.GetType());

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    [ConfigurationProperty("viewName", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string ViewName
    {
      get => (string) this["viewName"];
      set => this["viewName"] = (object) value;
    }

    /// <summary>Gets or sets the type of the view.</summary>
    /// <value>The type of the view.</value>
    [ConfigurationProperty("viewType")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type ViewType
    {
      get => (Type) this["viewType"];
      set => this["viewType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the virtual path of the view if the view is implemented
    /// as a user control.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("viewVirtualPath")]
    public string ViewVirtualPath
    {
      get => (string) this["viewVirtualPath"];
      set => this["viewVirtualPath"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value for the <see cref="T:System.Web.UI.Control" /> ID property of the control that will be constructed based on this definition.
    /// </summary>
    /// <value>The control id.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ControlIdDescription", Title = "ControlIdCaption")]
    [ConfigurationProperty("controlId")]
    public string ControlId
    {
      get => (string) this["controlId"];
      set => this["controlId"] = (object) value;
    }

    public Dictionary<string, string> ClientMappedCommnadNames
    {
      get => this.clientMappedCommnadNames;
      set => this.clientMappedCommnadNames = value;
    }

    /// <summary>Gets or sets the comments definition config.</summary>
    /// <value>The comments config.</value>
    [ConfigurationProperty("commentsSettingsDefinition")]
    public CommentsDefinitionConfig CommentsDefinitionConfig
    {
      get => this["commentsSettingsDefinition"] as CommentsDefinitionConfig;
      set => this["commentsSettingsDefinition"] = (object) value;
    }

    /// <summary>Gets or sets the comments definition.</summary>
    /// <value>The comments definition.</value>
    public ICommentsSettingsDefinition CommentsSettingsDefinition => (ICommentsSettingsDefinition) this.CommentsDefinitionConfig;

    /// <summary>
    /// Defines a collection of resource strings (labels) which will be availalbe on the client via ClientLabelManager
    /// </summary>
    [ConfigurationProperty("labels")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LabelsConfigDescirption", Title = "LabelsConfigCaption")]
    public ConfigElementList<LabelDefinitionElement> LabelsConfig
    {
      get => (ConfigElementList<LabelDefinitionElement>) this["labels"];
      set => this["labels"] = (object) value;
    }

    /// <summary>
    /// Read-only collection of resource strings that are going to be available on the client via an instance of ClientLabelManager
    /// </summary>
    IList<ILabelDefinition> IContentViewDefinition.Labels => (IList<ILabelDefinition>) this.LabelsConfig.Cast<ILabelDefinition>().ToList<ILabelDefinition>();

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ViewProps
    {
      public const string PlugIns = "plugInsConfig";
      public const string Description = "description";
      public const string DisplayMode = "displayMode";
      public const string ResourceClassId = "resourceClassId";
      public const string TemplateName = "templateName";
      public const string TemplatePath = "templatePath";
      public const string UseWorkflow = "useWorkflow";
      public const string TemplateKey = "templateKey";
      public const string Title = "title";
      public const string ParentTitleFormat = "parentTitleFormat";
      public const string IsMasterView = "isMasterView";
      public const string ViewName = "viewName";
      public const string ViewType = "viewType";
      public const string ViewVirtualPath = "viewVirtualPath";
      public const string ExternalClientScripts = "externalClientScripts";
      public const string CommentsDefinition = "commentsSettingsDefinition";
      public const string Localization = "localization";
      public const string ClientMappedCommnadNames = "clientMappedCommnadNames";
      public const string Labels = "labels";
      public const string ControlId = "controlId";
      public const string PromptDialogs = "PromptDialogs";
    }
  }
}
