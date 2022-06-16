// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGallerySettingsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Represents the Settings view for the Image Gallery control.
  /// </summary>
  public class ImageGallerySettingsDesignerView : MediaContentSettingsDesignerView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.ImageGallerySettingsDesignerView.ascx");
    private const string settingsViewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ImageGallerySettingsDesignerView.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ImageGallerySettingsDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => "imageGallerySettingsDesignerView";

    /// <summary>
    /// Gets an array containing the name of all master views.
    /// </summary>
    /// <value></value>
    public override string[] MasterViewNameMap => new string[4]
    {
      "ImagesFrontendThumbnailsListBasic",
      "ImagesFrontendThumbnailsListLightBox",
      "ImagesFrontendThumbnailsListStrip",
      "ImagesFrontendThumbnailsListSimple"
    };

    /// <summary>
    /// Gets a dictionary containing the ClientID of all showPrevAndNextLinks controls.
    /// </summary>
    protected virtual IDictionary<string, string> ShowPrevAndNextLinksClientIDs => (IDictionary<string, string>) this.Container.GetControls<ChoiceField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (c => c.Key.StartsWith("showPrevAndNextLinks"))).ToDictionary<KeyValuePair<string, Control>, string, string>((Func<KeyValuePair<string, Control>, string>) (i => i.Key.Substring(i.Key.Length - 1)), (Func<KeyValuePair<string, Control>, string>) (e => e.Value.ClientID));

    /// <summary>
    /// Gets a dictionary containing the ClientID of all selectPrevAndNextLinksType controls.
    /// </summary>
    protected virtual IDictionary<string, string> SelectPrevAndNextLinksTypeClientIDs => (IDictionary<string, string>) this.Container.GetControls<ChoiceField>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (c => c.Key.StartsWith("selectPrevAndNextLinksType"))).ToDictionary<KeyValuePair<string, Control>, string, string>((Func<KeyValuePair<string, Control>, string>) (i => i.Key.Substring(i.Key.Length - 1)), (Func<KeyValuePair<string, Control>, string>) (e => e.Value.ClientID));

    /// <summary>
    /// Gets the link that opens up the ThumbnailList template for editing
    /// </summary>
    protected virtual HyperLink EditThumbnailListTemplateLink => this.Container.GetControl<HyperLink>("editThumbnailListTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the Detail template for editing
    /// </summary>
    protected virtual HyperLink EditDetailTemplateLink => this.Container.GetControl<HyperLink>("editDetailTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the ThumbnailListLightbox template for editing
    /// </summary>
    protected virtual HyperLink EditThumbnailLightboxTemplateLink => this.Container.GetControl<HyperLink>("editThumbnailLightboxTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the ThumbnailStrip template for editing
    /// </summary>
    protected virtual HyperLink EditThumbnailStripTemplateLink => this.Container.GetControl<HyperLink>("editThumbnailStripTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the Detail template for editing
    /// </summary>
    protected virtual HyperLink EditDetailTemplateLink2 => this.Container.GetControl<HyperLink>("editDetailTemplateLink2", true);

    /// <summary>
    /// Gets the link that opens up the SimpleList template for editing
    /// </summary>
    protected virtual HyperLink EditSimpleListTemplateLink => this.Container.GetControl<HyperLink>("editSimpleListTemplateLink", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_showPrevAndNextLinksClientIDs", (object) this.ShowPrevAndNextLinksClientIDs);
      controlDescriptor.AddProperty("_selectPrevAndNextLinksTypeClientIDs", (object) this.SelectPrevAndNextLinksTypeClientIDs);
      controlDescriptor.AddElementProperty("editThumbnailListTemplateLink", this.EditThumbnailListTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editDetailTemplateLink", this.EditDetailTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editThumbnailLightboxTemplateLink", this.EditThumbnailLightboxTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editThumbnailStripTemplateLink", this.EditThumbnailStripTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editDetailTemplateLink2", this.EditDetailTemplateLink2.ClientID);
      controlDescriptor.AddElementProperty("editSimpleListTemplateLink", this.EditSimpleListTemplateLink.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          this.EditThumbnailListTemplateLink.ClientID,
          "DB0D628C-5471-4197-A94F-000000000001"
        },
        {
          this.EditDetailTemplateLink.ClientID,
          "DB0D628C-5471-4197-A94F-000000000005"
        },
        {
          this.EditThumbnailLightboxTemplateLink.ClientID,
          "DB0D628C-5471-4197-A94F-000000000002"
        },
        {
          this.EditThumbnailStripTemplateLink.ClientID,
          "DB0D628C-5471-4197-A94F-000000000003"
        },
        {
          this.EditDetailTemplateLink2.ClientID,
          "DB0D628C-5471-4197-A94F-000000000005"
        },
        {
          this.EditSimpleListTemplateLink.ClientID,
          "DB0D628C-5471-4197-A94F-000000000004"
        }
      };
      controlDescriptor.AddProperty("_templateLinkIdMap", (object) dictionary);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ImageGallerySettingsDesignerView.js", typeof (ImageGallerySettingsDesignerView).Assembly.FullName)
    };
  }
}
