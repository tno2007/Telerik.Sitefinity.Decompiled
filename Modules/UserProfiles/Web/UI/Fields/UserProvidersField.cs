// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields.UserProvidersField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields
{
  [RequiresDataItem]
  public class UserProvidersField : FieldControl
  {
    private const string reqDataItemScriptFileName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.UserProfiles.UserProvidersField.ascx");
    private const string UserProviderFieldScriptName = "Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Scripts.UserProvidersField.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceField" /> class.
    /// </summary>
    public UserProvidersField() => this.LayoutTemplatePath = UserProvidersField.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

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
    /// Converts a control ID used in conditional templates accoding to this.DisplayMode
    /// </summary>
    /// <param name="originalName">Original ID of the control</param>
    /// <returns>Unique control ID</returns>
    protected virtual string GetConditionalControlName(string originalName)
    {
      string lower = this.DisplayMode.ToString().ToLower();
      return originalName + "_" + lower;
    }

    /// <summary>
    /// Shortcut for this.Container.GetControl(this.GetConditionalControlName(originalName), required)
    /// </summary>
    /// <typeparam name="T">Type of the control to load</typeparam>
    /// <param name="originalName">Original ID of the control</param>
    /// <param name="required">Throw exception if control is not found and this parameter is true</param>
    /// <returns>Loaded control</returns>
    protected T GetConditionalControl<T>(string originalName, bool required) => this.Container.GetControl<T>(this.GetConditionalControlName(originalName), required);

    /// <summary>
    /// Gets the reference to the label control that represents the title of the field control.
    /// </summary>
    /// <remarks>This control is mandatory only in write mode.</remarks>
    protected internal virtual Label TitleLabel => this.GetConditionalControl<Label>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the label control that represents the description of the field control.
    /// </summary>
    /// <remarks>This control is mandatory only in write mode.</remarks>
    protected internal virtual Label DescriptionLabel => this.GetConditionalControl<Label>("descriptionLabel", true);

    /// <summary>
    /// Gets the reference to the label control that displays the example for this
    /// field control.
    /// </summary>
    /// <remarks>This control is mandatory only in the write mode.</remarks>
    protected internal virtual Label ExampleLabel => this.GetConditionalControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the use template radio.</summary>
    /// <value>The use template radio.</value>
    protected internal virtual RadioButton UseAllUserProvidersRadio => this.GetConditionalControl<RadioButton>("useAllUserProvidersRadio", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the dont use template radio.</summary>
    /// <value>The dont use template radio.</value>
    protected internal virtual RadioButton UseSpecificUserProvidersRadio => this.GetConditionalControl<RadioButton>("useSepcificUserProvidersRadio", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the dont use template radio.</summary>
    /// <value>The dont use template radio.</value>
    protected internal virtual ChoiceField ProvidersSelector => this.GetConditionalControl<ChoiceField>("providersSelector", this.DisplayMode == FieldDisplayMode.Write);

    protected override void InitializeControls(GenericContainer container)
    {
      this.TitleLabel.SetTextOrHide(this.Title);
      this.DescriptionLabel.SetTextOrHide(this.Description);
      this.ExampleLabel.SetTextOrHide(this.Example);
      ChoiceField providersSelector = this.ProvidersSelector;
      foreach (DataProviderBase contextProvider in UserManager.GetManager().GetContextProviders())
      {
        ChoiceItem choiceItem = new ChoiceItem()
        {
          Text = contextProvider.Name,
          Value = contextProvider.Name
        };
        providersSelector.Choices.Add(choiceItem);
      }
      if (providersSelector.Choices.Count > 1)
        return;
      this.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
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
        controlDescriptor.AddElementProperty("useAllUserProvidersRadio", this.UseAllUserProvidersRadio.ClientID);
        controlDescriptor.AddElementProperty("useSpecificUserProvidersRadio", this.UseSpecificUserProvidersRadio.ClientID);
        controlDescriptor.AddComponentProperty("providersSelector", this.ProvidersSelector.ClientID);
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
      string fullName = typeof (UserProvidersField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Scripts.UserProvidersField.js", fullName)
      };
    }
  }
}
