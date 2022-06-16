// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ContentStatisticsField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Analytics;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Control that provides functionality for displaying information for the status of the content workflow
  /// </summary>
  [RequiresDataItem]
  public class ContentStatisticsField : FieldControl, ICommandField
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ContentStatisticsField.ascx");
    internal const string fieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentStatisticsField.js";
    internal const string requiresDataItemContextInterfaceScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js";
    internal const string commandFieldInterfaceScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ICommandField.js";

    /// <summary>
    /// Initializes new instances of <c>ContentWorkflowStatusInfoField</c>
    /// </summary>
    public ContentStatisticsField() => this.LayoutTemplatePath = ContentStatisticsField.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the type (full name) of the script descriptor to be used.
    /// </summary>
    protected override string ScriptDescriptorType => typeof (ContentStatisticsField).FullName;

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that shows the linking phrase/word between the status and the date labels.
    /// </summary>
    public HiddenField ServiceBaseUrl => this.Container.GetControl<HiddenField>("serviceBaseUrl", false);

    public HyperLink StatsLink => this.Container.GetControl<HyperLink>("lbtnStatistics", false);

    public Panel WrapperPanel => this.Container.GetControl<Panel>("pnlStatistics", true);

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      if (!(definition is IContentStatisticsFieldDefinition))
        return;
      this.ConfigureBaseDefinition(definition);
      this.ConfigureControl((IContentStatisticsFieldDefinition) definition);
    }

    /// <summary>Configures the fields with the definition.</summary>
    /// <param name="statusFieldDefinition">The status field definition.</param>
    internal virtual void ConfigureControl(
      IContentStatisticsFieldDefinition statusFieldDefinition)
    {
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      try
      {
        if (ObjectFactory.Resolve<IAnalyticsApiAccessManager>().IsConfigured)
          this.ServiceBaseUrl.Value = string.Format("{0}{1}{2}", SystemManager.CurrentHttpContext.Request.ApplicationPath == "/" ? (object) string.Empty : (object) SystemManager.CurrentHttpContext.Request.ApplicationPath, (object) "/Sitefinity/marketing/Analytics", (object) "#/Content/Top_content/sf:allcontentpages:");
        else
          this.WrapperPanel.Visible = false;
      }
      catch
      {
        this.WrapperPanel.Visible = false;
      }
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
      if (this.ServiceBaseUrl != null)
        controlDescriptor.AddElementProperty("serviceBaseUrl", this.ServiceBaseUrl.ClientID);
      if (this.StatsLink != null)
        controlDescriptor.AddElementProperty("statsLink", this.StatsLink.ClientID);
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
      string fullName = typeof (ContentStatisticsField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ICommandField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentStatisticsField.js", fullName)
      };
    }

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);
  }
}
