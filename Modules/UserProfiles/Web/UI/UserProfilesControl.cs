// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfilesControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles.Configuration;
using Telerik.Sitefinity.Modules.UserProfiles.Web.Services;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.FieldControls;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.UserProfiles.Web.UI
{
  /// <summary>
  /// Represents the control that displays the default views for all user profile types.
  /// </summary>
  [RequiresDataItem]
  public class UserProfilesControl : CompositeFieldControl
  {
    private Dictionary<string, SectionControl> sectionControls = new Dictionary<string, SectionControl>();
    private List<Control> fieldControls = new List<Control>();
    private const string reqDataItemScriptFileName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.UserProfiles.UserProfilesControl.ascx");
    private const string scriptName = "Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Scripts.UserProfilesControl.js";
    private string viewName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfilesControl" /> class.
    /// </summary>
    public UserProfilesControl() => this.LayoutTemplatePath = UserProfilesControl.layoutTemplatePath;

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the kind of view to display. This property is only used if the ViewName property is null.
    /// </summary>
    /// <value>The kind of view to display.</value>
    public ProfileTypeViewKind ViewKind { get; set; }

    /// <summary>
    /// Gets or sets the name of the view to display. If null, the ViewKind property is used to obtain the name.
    /// </summary>
    /// <value>The name of the view.</value>
    public string ViewName
    {
      get => string.IsNullOrEmpty(this.viewName) ? UserProfilesHelper.GetContentViewName(this.ViewKind) : this.viewName;
      set => this.viewName = value;
    }

    /// <summary>
    /// If this is set, only profile types that can use the specified membership provider will be displayed.
    /// </summary>
    /// <value>The name of the membership provider.</value>
    public string MembershipProviderName { get; set; }

    /// <summary>
    /// If not null, this property defines a list of profile types to display.
    /// </summary>
    /// <value>The displayed profile types.</value>
    public List<string> DisplayedProfileTypes { get; set; }

    /// <summary>
    /// Gets or sets the CSS class that is being added to each of the field controls that are rendered.
    /// </summary>
    /// <value>A CSS class.</value>
    public string FieldControlsCssClass { get; set; }

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
    protected internal virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the field controls client binder.</summary>
    protected internal virtual FieldControlsBinder FieldControlsBinder => this.Container.GetControl<FieldControlsBinder>("fieldsBinder", true);

    internal override IEnumerable<Control> GetContainerControls()
    {
      this.EnsureFieldControls();
      return (IEnumerable<Control>) this.fieldControls;
    }

    /// <summary>Gets the name of the java script component.</summary>
    /// <value>The name of the java script component.</value>
    public override string JavaScriptComponentName => typeof (UserProfilesControl).FullName;

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The dialog container.</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      this.TitleLabel.SetTextOrHide(this.Title);
      this.DescriptionLabel.SetTextOrHide(this.Description);
      this.ExampleLabel.SetTextOrHide(this.Example);
      this.FieldControlsBinder.BindOnLoad = false;
      this.FieldControlsBinder.ServiceUrl = "not used, but required";
      ContentViewConfig contentViewConfig = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>();
      Type baseType = typeof (UserProfile);
      IEnumerable<ContentViewControlElement> viewControlElements = contentViewConfig.ContentViewControls.Values.Where<ContentViewControlElement>((Func<ContentViewControlElement, bool>) (c => baseType.IsAssignableFrom(c.ContentType) && c.ContentType != baseType));
      string viewName = this.ViewName;
      int num = 0;
      foreach (ContentViewControlElement viewControlElement in viewControlElements)
      {
        UserProfileTypeViewModel userProfileType = UserProfilesHelper.GetUserProfileType(viewControlElement.ContentType, (string) null);
        if (userProfileType != null && (this.DisplayedProfileTypes == null || this.DisplayedProfileTypes.Contains(userProfileType.DynamicTypeName)) && (this.MembershipProviderName == null || UserProfilesHelper.IsProfileTypeAvailable(userProfileType.DynamicTypeName, this.MembershipProviderName)))
        {
          foreach (IContentViewDefinition view in (Collection<IContentViewDefinition>) viewControlElement.Views)
          {
            if (view is IDetailFormViewDefinition formViewDefinition && formViewDefinition.ViewName == viewName && formViewDefinition.Sections.Any<IContentViewSectionDefinition>())
            {
              SectionControl child = new SectionControl();
              child.SectionDefinition = formViewDefinition.Sections.First<IContentViewSectionDefinition>();
              child.ID = "section_" + num.ToString();
              if (!string.IsNullOrEmpty(this.FieldControlsCssClass))
                child.FieldControlsCssClass = this.FieldControlsCssClass;
              if (string.IsNullOrEmpty(child.SectionDefinition.Title) && userProfileType.DynamicTypeName != typeof (SitefinityProfile).FullName)
                child.Title = userProfileType.Title;
              this.sectionControls.Add(userProfileType.DynamicTypeName, child);
              this.Controls.Add((Control) child);
              ++num;
              break;
            }
          }
        }
      }
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void EnsureFieldControls()
    {
      foreach (KeyValuePair<string, SectionControl> sectionControl in this.sectionControls)
      {
        foreach (Control fieldControl in sectionControl.Value.FieldControls)
        {
          if (fieldControl.ID == "isPublicProfileField" && !SystemManager.IsModuleAccessible("Forums"))
            fieldControl.Visible = false;
          else if (fieldControl.ID == "isPublicProfileField" && SystemManager.IsModuleAccessible("Forums"))
          {
            ChoiceField choiceField = fieldControl as ChoiceField;
            choiceField.CssClass = "sfIsPublicProfile  sfprofileField";
            this.fieldControls.Add((Control) choiceField);
          }
          else
            this.fieldControls.Add(fieldControl);
        }
      }
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      this.EnsureFieldControls();
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      List<string> list = this.fieldControls.Select<Control, string>((Func<Control, string>) (c => c.ClientID)).ToList<string>();
      controlDescriptor.AddProperty("fieldControlIds", (object) list);
      IEnumerable<string> strings = this.fieldControls.Where<Control>((Func<Control, bool>) (c => (uint) c.GetType().GetCustomAttributes(typeof (RequiresDataItemAttribute), true).Length > 0U)).Select<Control, string>((Func<Control, string>) (c => c.ClientID));
      controlDescriptor.AddProperty("requireDataItemControlIds", (object) strings);
      controlDescriptor.AddComponentProperty("binder", this.FieldControlsBinder.ClientID);
      Dictionary<string, List<string>> dictionary1 = new Dictionary<string, List<string>>();
      foreach (ProfileTypeSettings profileTypesSetting in (ConfigElementCollection) Telerik.Sitefinity.Configuration.Config.Get<UserProfilesConfig>().ProfileTypesSettings)
      {
        bool? membershipProviders = profileTypesSetting.UseAllMembershipProviders;
        if (membershipProviders.HasValue)
        {
          membershipProviders = profileTypesSetting.UseAllMembershipProviders;
          if (!membershipProviders.Value)
          {
            List<string> stringList = new List<string>();
            foreach (MembershipProviderElement membershipProvider in profileTypesSetting.MembershipProviders)
              stringList.Add(membershipProvider.ProviderName);
            dictionary1.Add(profileTypesSetting.Name, stringList);
          }
        }
      }
      controlDescriptor.AddProperty("typeProviders", (object) dictionary1);
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      foreach (KeyValuePair<string, SectionControl> sectionControl in this.sectionControls)
        dictionary2.Add(sectionControl.Key, sectionControl.Value.ClientID);
      controlDescriptor.AddProperty("sectionControls", (object) dictionary2);
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
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", assembly));
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = assembly,
        Name = "Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Scripts.UserProfilesControl.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
