// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.CacheSettingsFieldControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Represents a cache settings field control. Used for editing cache settings.
  /// </summary>
  public class CacheSettingsFieldControl : CompositeFieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.CacheSettingsFieldControl.ascx");
    internal const string cacheSettingsFieldControlScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.CacheSettingsFieldControl.js";
    internal const string blockUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery.blockUI.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.CacheSettingsFieldControl" /> class.
    /// </summary>
    public CacheSettingsFieldControl() => this.LayoutTemplatePath = CacheSettingsFieldControl.layoutTemplatePath;

    /// <summary>
    /// Gets or sets the name of the cache settings data field.
    /// </summary>
    /// <value>The name of the cache settings data field.</value>
    public string CacheSettingsDataFieldName { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    public virtual ITextFieldDefinition CacheDurationTextFieldDefinition { get; set; }

    public virtual IChoiceFieldDefinition SlidingExpirationChoiceFieldDefinition { get; set; }

    public virtual IChoiceFieldDefinition EnableCachingFieldDefinition { get; set; }

    public virtual IChoiceFieldDefinition UseDefaultCachingFieldDefinition { get; set; }

    public virtual bool IsOutputCache { get; set; }

    protected internal override WebControl DescriptionControl => (WebControl) this.Container.GetControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);

    protected internal override WebControl ExampleControl => (WebControl) this.Container.GetControl<Label>("exampleLabel", false);

    /// <summary>Gets the reference to the title choice field.</summary>
    protected internal override WebControl TitleControl => (WebControl) this.Container.GetControl<ChoiceField>("cacheStatusChoiceField", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the reference to the content panel.</summary>
    protected internal Panel ContentPanel => this.Container.GetControl<Panel>("contentPanel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the inner content panel - that will be enabled/disabled.
    /// </summary>
    protected internal Panel InnerContentPanel => this.Container.GetControl<Panel>("innerContentPanel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the reference to the "UseDefault" choice field.</summary>
    protected internal ChoiceField UseDefaultChoiceField => this.Container.GetControl<ChoiceField>("useDefault", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the reference to the cache duration TextField.</summary>
    protected internal TextField CacheDurationTextField => this.Container.GetControl<TextField>("cacheDuration", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the sliding expiration ChoiceField.
    /// </summary>
    protected internal ChoiceField SlidingExpirationChoiceField => this.Container.GetControl<ChoiceField>("slidingExpirationChoiceField", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.CacheDurationTextField.Configure((IFieldDefinition) this.CacheDurationTextFieldDefinition);
      this.SlidingExpirationChoiceField.Configure((IFieldDefinition) this.SlidingExpirationChoiceFieldDefinition);
      ((FieldControl) this.TitleControl).Configure((IFieldDefinition) this.EnableCachingFieldDefinition);
      this.UseDefaultChoiceField.Configure((IFieldDefinition) this.UseDefaultCachingFieldDefinition);
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      this.ConfigureFields((ICacheSettingsFieldDefinition) definition);
    }

    public override string JavaScriptComponentName => typeof (CacheSettingsFieldControl).FullName;

    /// <summary>Configures the fields with the definition.</summary>
    /// <param name="cacheSettingsFieldDefinition">The cache settings field definition.</param>
    public virtual void ConfigureFields(
      ICacheSettingsFieldDefinition cacheSettingsFieldDefinition)
    {
      this.CacheDurationTextFieldDefinition = cacheSettingsFieldDefinition.CacheDurationTextFieldDefinition;
      this.SlidingExpirationChoiceFieldDefinition = cacheSettingsFieldDefinition.SlidingExpirationChoiceFieldDefinition;
      this.EnableCachingFieldDefinition = cacheSettingsFieldDefinition.EnableCachingFieldDefinition;
      this.UseDefaultCachingFieldDefinition = cacheSettingsFieldDefinition.UseDefaultSettingsForCachingFieldDefinition;
      this.IsOutputCache = cacheSettingsFieldDefinition.IsOutputCache;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      if (this.DisplayMode != FieldDisplayMode.Write)
        throw new NotImplementedException("Read mode for this control is not implemented.");
      controlDescriptor.AddComponentProperty("cacheStatusChoiceField", this.TitleControl.ClientID);
      controlDescriptor.AddComponentProperty("useDefaultChoiceField", this.UseDefaultChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("cacheDurationTextField", this.CacheDurationTextField.ClientID);
      controlDescriptor.AddComponentProperty("slidingExpirationChoiceField", this.SlidingExpirationChoiceField.ClientID);
      controlDescriptor.AddProperty("_contentPanelId", (object) this.ContentPanel.ClientID);
      controlDescriptor.AddProperty("_innerContentPanelId", (object) this.InnerContentPanel.ClientID);
      SystemConfig systemConfig = Config.Get<SystemConfig>();
      bool flag = false;
      int duration;
      if (this.IsOutputCache)
      {
        OutputCacheProfileElement profile = systemConfig.CacheSettings.Profiles[systemConfig.CacheSettings.DefaultProfile];
        duration = profile.Duration;
        flag = profile.SlidingExpiration;
      }
      else
        duration = systemConfig.CacheSettings.MediaCacheProfiles[systemConfig.CacheSettings.DefaultImageProfile].Duration;
      controlDescriptor.AddProperty("_defaultCacheDuration", (object) duration);
      controlDescriptor.AddProperty("_defaultCacheSlidingExpiration", (object) flag);
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
      string fullName = typeof (CacheSettingsFieldControl).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.CacheSettingsFieldControl.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery.blockUI.js", "Telerik.Sitefinity.Resources")
      };
    }

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();
  }
}
