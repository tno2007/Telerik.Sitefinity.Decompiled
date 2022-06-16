// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// The view for specifying data mapping between sitefinity and external connectors' fields.
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Web.UI.SimpleView" />
  public class ConnectorDataMappingSelector : SimpleScriptView
  {
    private const string ControlScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ConnectorDataMappingSelector.js";
    private static readonly string LayoutTemplate = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ConnectorDataMappingSelector.ascx");
    private static readonly string WebServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Forms/FormsService.svc");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingSelector" /> class.
    /// </summary>
    public ConnectorDataMappingSelector() => this.LayoutTemplatePath = ConnectorDataMappingSelector.LayoutTemplate;

    /// <summary>
    /// Gets the type (full name) of the script descriptor to be used. By default
    /// it returns current type.
    /// </summary>
    protected virtual string ScriptDescriptorType => this.GetType().FullName;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
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
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorType, this.ClientID);
      string str1 = this.Page.ResolveUrl(string.Format("{0}/fields/", (object) "~/Sitefinity/Services/Forms/FormsService.svc"));
      string str2 = this.Page.ResolveUrl(string.Format("{0}/autocomplete", (object) "~/Sitefinity/Services/Forms/FormsService.svc"));
      controlDescriptor.AddProperty("webServiceUrl", (object) str1);
      controlDescriptor.AddProperty("autoCompleteWebServiceUrl", (object) str2);
      controlDescriptor.AddProperty("extenders", (object) this.GetExtendersJson());
      controlDescriptor.AddProperty("_duplicateMappedFieldsFormat", (object) Res.Get<FormsResources>().ConnectorFieldNameShouldBeMappedOnlyOnceFormat);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference()
      {
        Name = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ConnectorDataMappingSelector.js",
        Assembly = this.GetType().Assembly.FullName
      }
    };

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.
    /// </returns>
    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.KendoAll;

    private string GetExtendersJson() => new JavaScriptSerializer().Serialize((object) ObjectFactory.Container.ResolveAll<ConnectorDataMappingExtender>().OrderByDescending<ConnectorDataMappingExtender, int>((Func<ConnectorDataMappingExtender, int>) (extender => extender.Ordinal)));
  }
}
