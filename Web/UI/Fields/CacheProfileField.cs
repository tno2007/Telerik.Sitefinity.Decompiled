// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.CacheProfileField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Represents a cache profile field control. Used for editing cache profiles.
  /// </summary>
  public class CacheProfileField : CompositeFieldControl
  {
    private List<object> profileDetails;
    private const string ProfileNameKey = "name";
    private const string NoExplicitCachingKey = "noExplicitCaching";
    private const string EnableCachingKey = "enabled";
    private const string EnableCachingStringKey = "enabledString";
    private const string LocationKey = "location";
    private const string DurationKey = "serverMaxAge";
    private const string ClientDurationKey = "browserMaxAge";
    private const string SlidingExpirationKey = "sliding";
    private const string HttpHeaderKey = "httpHeader";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.CacheProfileField.ascx");
    internal const string fieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.CacheProfileField.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.CacheProfileField" /> class.
    /// </summary>
    public CacheProfileField() => this.LayoutTemplatePath = CacheProfileField.layoutTemplatePath;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is for output cache or for client cache.
    /// </summary>
    public virtual bool IsOutputCache { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the profile choice field definition.</summary>
    /// <value>The profile choice field definition.</value>
    public virtual IChoiceFieldDefinition ProfileChoiceFieldDefinition { get; set; }

    /// <summary>
    /// Gets or sets the cache settings location in the administration.
    /// </summary>
    /// <example>
    /// "Administration &gt; Settings &gt; Advanced settings &gt; Caching"
    /// </example>
    /// <value>The cache settings location.</value>
    public virtual string CacheSettingsLocation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is for output cache or for client cache.
    /// </summary>
    protected virtual bool DisplayClientCacheSettings => false;

    /// <summary>
    /// Gets a reference to the choice field control that lists the available cache profiles.
    /// </summary>
    protected internal virtual ChoiceField ProfileSelect => this.Container.GetControl<ChoiceField>("profileSelect", true);

    protected internal override WebControl TitleControl => (WebControl) this.Container.GetControl<Label>("titleLabel", false);

    protected internal override WebControl DescriptionControl => (WebControl) null;

    protected internal override WebControl ExampleControl => (WebControl) null;

    /// <summary>
    /// Gets a reference to the button that toggle the area with cache profile details.
    /// </summary>
    protected internal virtual Control DetailsButton => this.Container.GetControl<Control>("detailsBtn", true);

    /// <summary>
    /// Gets a reference to the control that contains the cache profile details.
    /// </summary>
    protected internal virtual Control DetailsContainer => this.Container.GetControl<Control>("detailsCnt", true);

    /// <summary>
    /// Gets a reference to the control that displays the cache settings location.
    /// </summary>
    protected internal virtual ITextControl SettingsLocationControl => this.Container.GetControl<ITextControl>("settingsLocation", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      SystemConfig systemConfig = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>();
      if (this.IsOutputCache)
      {
        ConfigElementDictionary<string, OutputCacheProfileElement> profiles = systemConfig.CacheSettings.Profiles;
        OutputCacheProfileElement profileConfig1 = profiles[systemConfig.CacheSettings.DefaultProfile];
        this.profileDetails = new List<object>();
        this.profileDetails.Add((object) this.GetProfileDetails(profileConfig1, true));
        foreach (OutputCacheProfileElement profileConfig2 in (IEnumerable<OutputCacheProfileElement>) profiles.Values)
        {
          this.ProfileChoiceFieldDefinition.Choices.Add((IChoiceDefinition) new ChoiceElement()
          {
            Text = this.GetProfileCaption(profileConfig2),
            Value = profileConfig2.Name
          });
          this.profileDetails.Add((object) this.GetProfileDetails(profileConfig2));
        }
      }
      else
      {
        ConfigElementDictionary<string, OutputCacheProfileElement> mediaCacheProfiles = systemConfig.CacheSettings.MediaCacheProfiles;
        OutputCacheProfileElement profileConfig3 = mediaCacheProfiles[systemConfig.CacheSettings.DefaultImageProfile];
        this.profileDetails = new List<object>();
        this.profileDetails.Add((object) this.GetProfileDetails(profileConfig3, true));
        foreach (OutputCacheProfileElement profileConfig4 in (IEnumerable<OutputCacheProfileElement>) mediaCacheProfiles.Values)
        {
          this.ProfileChoiceFieldDefinition.Choices.Add((IChoiceDefinition) new ChoiceElement()
          {
            Text = this.GetProfileCaption(profileConfig4),
            Value = profileConfig4.Name
          });
          this.profileDetails.Add((object) this.GetProfileDetails(profileConfig4));
        }
      }
      this.ProfileSelect.Configure((IFieldDefinition) this.ProfileChoiceFieldDefinition);
      ((ITextControl) this.TitleControl).SetTextOrHide(this.Title);
      string settingsLocation = this.CacheSettingsLocation;
      if (string.IsNullOrEmpty(settingsLocation))
        settingsLocation = Res.Get<Labels>().CacheSettingsLocation;
      this.SettingsLocationControl.Text = settingsLocation;
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is ICacheProfileFieldDefinition profileFieldDefinition))
        return;
      this.IsOutputCache = profileFieldDefinition.IsOutputCache;
      this.ProfileChoiceFieldDefinition = (IChoiceFieldDefinition) profileFieldDefinition.ProfileChoiceFieldDefinition.GetDefinition();
      this.CacheSettingsLocation = profileFieldDefinition.CacheSettingsLocation;
    }

    public override string JavaScriptComponentName => typeof (CacheProfileField).FullName;

    /// <summary>Gets the caption of the client cache profile.</summary>
    /// <param name="profileConfig">The configuration element of the profile.</param>
    /// <returns></returns>
    protected virtual string GetProfileCaption(OutputCacheProfileElement profileConfig) => profileConfig.Name;

    /// <summary>
    /// Gets a collection with the details of the output cache profile.
    /// </summary>
    /// <param name="profileConfig">The configuration element of the profile.</param>
    /// <returns></returns>
    protected virtual Dictionary<string, object> GetProfileDetails(
      OutputCacheProfileElement profileConfig,
      bool isDefault = false)
    {
      string name = isDefault ? (string) null : profileConfig.Name;
      string location = Res.Get<ConfigDescriptions>().Get(Enum.GetName(typeof (Telerik.Sitefinity.Web.OutputCacheLocation), (object) profileConfig.Location));
      string controlHeaderValue = profileConfig.ToClientCacheControl().ToHttpCacheControlHeaderValue();
      string httpHeader = controlHeaderValue == null ? (string) null : "Cache-Control" + ": " + controlHeaderValue;
      return this.GetProfileDetails(name, new bool?(profileConfig.Enabled), profileConfig.Duration, new bool?(profileConfig.SlidingExpiration), httpHeader, location, profileConfig.GetMaxAge());
    }

    protected virtual Dictionary<string, object> GetProfileDetails(
      string name,
      bool? enabled,
      int duration,
      bool? isSlidingExpiration = null,
      string httpHeader = null,
      string location = null,
      int clientDuration = 0)
    {
      Labels labels = Res.Get<Labels>();
      name = name ?? labels.SameAsForWholeSite;
      string str1 = !enabled.HasValue ? labels.NoExplicitClientCachingMessage : (string) null;
      bool? nullable = enabled;
      bool flag1 = true;
      bool flag2 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue;
      string str2 = flag2 ? labels.Yes : labels.No;
      string readableString1 = TimeSpan.FromSeconds((double) duration).ToReadableString();
      string readableString2 = TimeSpan.FromSeconds((double) clientDuration).ToReadableString();
      string str3;
      if (isSlidingExpiration.HasValue)
      {
        nullable = isSlidingExpiration;
        bool flag3 = true;
        str3 = nullable.GetValueOrDefault() == flag3 & nullable.HasValue ? labels.Yes : labels.No;
      }
      else
        str3 = (string) null;
      string str4 = str3;
      return new Dictionary<string, object>()
      {
        {
          nameof (name),
          (object) name
        },
        {
          "noExplicitCaching",
          (object) str1
        },
        {
          nameof (enabled),
          (object) flag2
        },
        {
          "enabledString",
          (object) str2
        },
        {
          "serverMaxAge",
          (object) readableString1
        },
        {
          "browserMaxAge",
          (object) readableString2
        },
        {
          "sliding",
          (object) str4
        },
        {
          nameof (httpHeader),
          (object) httpHeader
        },
        {
          nameof (location),
          (object) location
        }
      };
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        controlDescriptor.AddComponentProperty("profileSelect", this.ProfileSelect.ClientID);
        controlDescriptor.AddElementProperty("detailsButton", this.DetailsButton.ClientID);
        controlDescriptor.AddElementProperty("detailsContainer", this.DetailsContainer.ClientID);
        controlDescriptor.AddProperty("profileDetails", (object) this.profileDetails);
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
      string fullName = typeof (CacheProfileField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.CacheProfileField.js", fullName)
      };
    }

    /// <inheritdoc />
    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.MicrosoftAjaxTemplates;
  }
}
