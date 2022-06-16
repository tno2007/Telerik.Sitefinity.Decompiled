// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.RelatingDataField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>This class represents relating data field control.</summary>
  [FieldDefinitionElement(typeof (RelatingDataFieldDefinitionElement))]
  public class RelatingDataField : FieldControl
  {
    internal const string ScriptReference = "Telerik.Sitefinity.Web.UI.Fields.Scripts.RelatingDataField.js";
    private const string IRelatingDataScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRelatingDataField.js";
    private const string ILocalizableFieldControlScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.RelatingDataField.ascx");

    /// <summary>Gets the reference to the field container control</summary>
    protected virtual HtmlControl RelatingDataFieldContainer => this.Container.GetControl<HtmlControl>("relatingDataFieldContainer", true);

    /// <summary>
    /// Gets the link that when clicked expands relating data control.
    /// </summary>
    protected HtmlAnchor RelatingDataExpander => this.Container.GetControl<HtmlAnchor>("relatingDataExpander", true);

    /// <summary>
    /// Gets the reference to the button that opens the dialog containing the relating items.
    /// </summary>
    protected virtual HtmlAnchor OpenDialogButton => this.Container.GetControl<HtmlAnchor>("openDialogButton", true);

    /// <summary>Gets the reference to the loading panel</summary>
    protected virtual HtmlControl LoadingPanel => this.Container.GetControl<HtmlControl>("loadingPanel", true);

    /// <summary>
    /// Represents the manager that controls the localization strings.
    /// </summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = source.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_siteBaseUrl", (object) (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') + "/"));
      controlDescriptor.AddProperty("_relatedDataService", (object) VirtualPathUtility.ToAbsolute("~/restapi/sitefinity/related-data"));
      controlDescriptor.AddProperty("_genericDataService", (object) VirtualPathUtility.ToAbsolute("~/restapi/sitefinity/generic-data"));
      controlDescriptor.AddProperty("_isMultilingual", (object) SystemManager.CurrentContext.AppSettings.Multilingual);
      controlDescriptor.AddElementProperty("loadingContainer", this.LoadingPanel.ClientID);
      controlDescriptor.AddElementProperty("container", this.RelatingDataFieldContainer.ClientID);
      controlDescriptor.AddElementProperty("expanderButton", this.RelatingDataExpander.ClientID);
      controlDescriptor.AddElementProperty("openDialogButton", this.OpenDialogButton.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = RelatingDataField.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
    {
      string fullName = this.GetType().Assembly.FullName;
      return (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
      {
        new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js", fullName),
        new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRelatingDataField.js", fullName),
        new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.RelatingDataField.js", fullName)
      };
    }
  }
}
