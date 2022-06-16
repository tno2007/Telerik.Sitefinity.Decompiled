// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MobilePreviewView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web
{
  /// <summary>This view represents the mobile preview.</summary>
  public class MobilePreviewView : KendoView
  {
    private static string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ResponsiveDesign.MobilePreviewView.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.MobilePreviewView.js";

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = MobilePreviewView.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the url of the preview page.</summary>
    public string PreviewPageUrl { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the
    /// event data.
    /// </param>
    protected override void OnPreRender(EventArgs e) => base.OnPreRender(e);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript)
    /// client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" />
    /// objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (MobilePreviewView).FullName, this.ClientID);
      string str = new JavaScriptSerializer().Serialize((object) Config.Get<ResponsiveDesignConfig>().PreviewDevices.Values.Select(pd => new
      {
        Name = pd.Name,
        Title = pd.Title,
        CssClass = pd.CssClass,
        DeviceWidth = pd.DeviceWidth,
        DeviceHeight = pd.DeviceHeight,
        ViewportWidth = pd.ViewportWidth,
        ViewportHeight = pd.ViewportHeight,
        OffsetX = pd.OffsetX,
        OffsetY = pd.OffsetY,
        OffsetXLandscape = pd.OffsetXLandscape,
        OffsetYLandscape = pd.OffsetYLandscape,
        DeviceCategory = pd.DeviceCategory
      }));
      controlDescriptor.AddProperty("_devicesSettings", (object) str);
      controlDescriptor.AddProperty("_previewPageUrl", (object) this.PreviewPageUrl);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects
    /// that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" />
    /// objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.MobilePreviewView.js", typeof (MobilePreviewView).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
